using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Net;
using System.Web.Http.Description;
using TMT_Code_Migration1.DataLogics;
using TMT_Code_Migration1.DataLogics.Utility;
using TMT_Code_Migration1.Models.Authentication;

namespace TMT_Code_Migration1.Controllers
{
    [Route("TMTService")]
    public class AuthenticationController : Controller
    {
        private readonly CommonDl _commonDl;
        private readonly AuthenticationDL _authenticationDL;
        public AuthenticationController(CommonDl commonDl , [FromServices] IConfiguration config)
        {
            _commonDl = commonDl;
            _authenticationDL = new AuthenticationDL(commonDl, config);
        }

        //All Routes starts from here.
        [Route("LoginUser")]
        [HttpPost]
        public IActionResult Login([FromBody] UserLoginRequest rqst)
        {
            string decryptedText = string.Empty;
            try
            {
                LoginResponse loginResponse = _authenticationDL.LoginUsers(rqst);
                if (loginResponse.firstName != null)
                    loginResponse.Token = _authenticationDL.GetToken();
                return Ok(loginResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
            
        }

        [Route("registeruser")]
        [HttpPost]
        [Authorize]
        public IActionResult registeruser([FromBody] UserRegistrationRequest userRegistrationRequest)
        {
            try
            {
                UserRegistrationResponse userresponse = _authenticationDL.RegisterUser(userRegistrationRequest);
                return Ok(userresponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
    }
}
