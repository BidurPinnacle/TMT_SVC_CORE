using Azure.Core;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TMT_Code_Migration1.DataLogics.Utility;
using TMT_Code_Migration1.Models.Authentication;
using TMT_Code_Migration1.Views.Interfaces;

namespace TMT_Code_Migration1.DataLogics
{
    public class AuthenticationDL : IAuthentication
    {
        private readonly CommonDl _commonDl;
        private readonly IConfiguration _configuration;
        public AuthenticationDL(CommonDl commonDl , IConfiguration configuration)
        {
            _commonDl = commonDl;
            _configuration = configuration;
        }
        public LoginResponse LoginUsers(UserLoginRequest loginRequest)
        {
            LoginResponse usersResponse = new LoginResponse();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("sp_getloggedin_user_info", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        SqlParameter[] param = new SqlParameter[2];
                        param[0] = new SqlParameter("@username", loginRequest.username.ToString());
                        myCMD.Parameters.Add(param[0]);

                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            if (myReader.HasRows)
                            {
                                do
                                {
                                    while (myReader.Read())
                                    {

                                        string passwardfromdb = myReader["PASSWORD"].ToString();
                                        usersResponse.firstName = myReader["firstName"].ToString();
                                        usersResponse.lastName = myReader["lastName"].ToString();
                                        usersResponse.RoleName = myReader["RoleName"].ToString();
                                        usersResponse.UserId = myReader["UserId"].ToString();
                                        usersResponse.username = myReader["username"].ToString();
                                        usersResponse.CompanyID = myReader["CompanyID"].ToString();
                                        usersResponse.CName = myReader["CName"].ToString();
                                        string keyBase64 = _configuration.GetSection("KeyValues")["keyBase64"];
                                        string vectorBase64 = _configuration.GetSection("KeyValues")["vectorBase64"];
                                        var passwardfromdb_decrypt = new ManagedAes(passwardfromdb, keyBase64, vectorBase64);

                                        var passwardfromrequest_decrypt = new ManagedAes(loginRequest.password, keyBase64, vectorBase64);

                                        if (passwardfromdb_decrypt.GetPlainText.ToString().ToUpper() == passwardfromrequest_decrypt.GetPlainText.ToString().ToUpper())
                                        {
                                            break;
                                        }
                                        else
                                            usersResponse = null;
                                    }
                                }
                                while (myReader.NextResult());
                            }
                            myReader.Close();
                            conn.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in LoginUsers DL. ", ex);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return usersResponse;
        }
        public Object GetToken()
        {
            try
            {

                //string key = "my_dominionva_key_2022";
                string key = _configuration.GetSection("JWTKey")["key"];
                /*
                 System.ArgumentOutOfRangeException: 
                'IDX10720: Unable to create KeyedHashAlgorithm for algorithm 'HS256', 
                the key size must be greater than: '256' bits, key has '176' bits. 
                See https://aka.ms/IdentityModel/UnsafeRelaxHmacKeySizeValidation (Parameter 'keyBytes')'

                 */


                var issuer = "http://dominionqa.com";     

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var permClaims = new List<Claim>();
                permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
  
                var token = new JwtSecurityToken(issuer, //Issure    
                                issuer,  //Audience    
                                permClaims,
                                expires: DateTime.Now.AddDays(1),
                                signingCredentials: credentials);
                var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);
                return jwt_token;

            }
            catch (Exception ex)
            {

                throw new Exception("Error in GetToken Controller Method. ", ex);
            }
        }
        public UserRegistrationResponse RegisterUser(UserRegistrationRequest userRegistrationRequest)
        {
            UserRegistrationResponse user = new UserRegistrationResponse();
            var passwardfromdb_decrypt = new ManagedAes("2021");
            userRegistrationRequest.encPwd = passwardfromdb_decrypt.GetEncryptedText;

            string query = string.Empty;
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("spUsers", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userRegistrationRequest.userid);
                    cmd.Parameters.AddWithValue("@UserRoleID", userRegistrationRequest.roleid);
                    cmd.Parameters.AddWithValue("@firstName", userRegistrationRequest.firstname);
                    cmd.Parameters.AddWithValue("@lastName", userRegistrationRequest.lastname);
                    cmd.Parameters.AddWithValue("@CompanyID", userRegistrationRequest.companyid);
                    cmd.Parameters.AddWithValue("@Activestatus", userRegistrationRequest.userstatus);
                    cmd.Parameters.AddWithValue("@StatementType", userRegistrationRequest.StatementType);
                    cmd.Parameters.AddWithValue("@encPwd", userRegistrationRequest.encPwd.ToString());
                    int k = cmd.ExecuteNonQuery();
                    if (k != 0)
                    {
                        user.IsInserted = true;

                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in RegisterUser. ", ex);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
                
            }
            return user;
        }
    }
}
