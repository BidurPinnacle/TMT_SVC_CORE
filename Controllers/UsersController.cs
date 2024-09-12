using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Description;
using System.Data.SqlClient;
using System.Net;
using TMT_Code_Migration1.DataLogics;
using TMT_Code_Migration1.DataLogics.Utility;
using TMT_Code_Migration1.Models.Users;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using TMT_Code_Migration1.Models.Authentication;

namespace TMT_Code_Migration1.Controllers
{
    [Route("TMTService")]
    public class UsersController : Controller
    {
        private readonly CommonDl _commonDl;
        private readonly UsersDL _usersDL ;
        private readonly IConfiguration _configuration;

        public UsersController(CommonDl commonDl, [FromServices] IConfiguration configuration)
        {
            _commonDl = commonDl;
            _usersDL = new UsersDL(commonDl, configuration);
            _configuration = configuration;
        }

        [Route("GetUsers")]
        [ResponseType(typeof(HttpResponseMessage))]
        [HttpGet]
        public IActionResult GetUsers()
        {
            try
            {
                List<GetUser> getUsers = _usersDL.GetUsers();
                return Ok(getUsers);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GetUsers Controller Method. ", ex);
            }
        }


        [Route("ChangePassword")]
        [HttpPost]
        public IActionResult ChangePassword(UserspasswordRequest usersRequest)
        {
            try
            {
                UserspasswordResponse usersResponse = _usersDL.changepassword(usersRequest);

                return new ObjectResult(usersResponse)
                {
                    StatusCode = (int)HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new ObjectResult($"Error in ChangePassword Controller Method: {ex.Message}")
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }


        [Route("ChangeRole")]
        [HttpPost]
        public IActionResult ChangeRole(Usersrolerequest usersRequest)
        {
            try
            {
                Usersroleresponse usersResponse = _usersDL.changerole(usersRequest);
                return new ObjectResult(usersResponse)
                {
                    StatusCode = (int)HttpStatusCode.OK
                };

            }
            catch (Exception ex)
            {
                return new ObjectResult($"Error in ChangeRole Controller Method: {ex.Message}")
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }

        [Route("GetUsersForSearch")]
        [HttpPost]
        [Authorize]
        public IActionResult GetUsersForSearch(UserRequestForSearch userRqst)
        {
            try
            {
                List<GetUserForSearch>  getUsers = _usersDL.GetUsersForSearch(userRqst);
                return Ok(getUsers);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GetUsersForSearch Controller Method. ", ex);
            }

        }


        [Route("GetUsersForQASearch")]
        [HttpPost]
        [Authorize]
        public IActionResult GetUsersForQASearch(UserRequestForSearch userRqst)
        {
            try
            {
                List<GetUserForSearch> getUsers = _usersDL.GetUsersForQASearch(userRqst);
                return Ok(getUsers);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GetUsersForQASearch Controller Method. ", ex);
            }

        }


        [Route("GetUser")]
        [HttpPost]
        [Authorize]
        public String GetUser()
        {
            try
            {

                if (User.Identity.IsAuthenticated)
                {
                    var identity = User.Identity as ClaimsIdentity;
                    if (identity != null)
                    {
                        IEnumerable<Claim> claims = identity.Claims;
                    }
                    return "Valid";
                }
                else
                {
                    return "Invalid";
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GetUser Controller Method. ", ex);
            }
        }


        [Route("DecryptEncryptedText")]
        [HttpPost]
        public IActionResult DecryptEncryptedText(UserLoginRequest usersRequest)
        {
            LoginResponse usersResponse = new LoginResponse();
            string decryptedText = string.Empty;
            try
            {


                string keyBase64 = _configuration.GetSection("KeyValues")["keyBase64"];
                string vectorBase64 = _configuration.GetSection("KeyValues")["vectorBase64"];
                var managedAesdecrypt = new ManagedAes(usersRequest.password, keyBase64, vectorBase64);
                decryptedText = managedAesdecrypt.GetPlainText.ToString();
                usersResponse.Token = decryptedText;
                return new ObjectResult(usersResponse)
                {
                    StatusCode = (int)HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new ObjectResult($"Error in DecryptEncryptedText Controller Method: {ex.Message}")
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }

        [Route("EncryptePlainText")]
        [HttpPost]
        public string EncryptePlainText([FromBody] string PlainText)
        {
            string EncryptedText = string.Empty;
            try
            {
                string keyBase64 = _configuration.GetSection("KeyValues")["keyBase64"];
                string vectorBase64 = _configuration.GetSection("KeyValues")["vectorBase64"];
                var managedAes = new ManagedAes(PlainText);
                EncryptedText = managedAes.GetEncryptedText.ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("Error in DecryptEncryptedText. ", ex);
            }
            return EncryptedText;
        }


        [Route("RoleMenus")]
        [ResponseType(typeof(HttpResponseMessage))]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetRoleMenus(AccessRequest request)
        {
            var response = await _usersDL.GetRoleMenuAccessDL(request);
            return Ok(response);
        }
    }
}
