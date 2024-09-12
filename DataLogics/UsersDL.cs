using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using TMT_Code_Migration1.DataLogics.Utility;
using TMT_Code_Migration1.Models.Authentication;
using TMT_Code_Migration1.Models.Users;
using TMT_Code_Migration1.Views.Interfaces;

namespace TMT_Code_Migration1.DataLogics
{
    public class UsersDL: IUsers
    {
        private readonly CommonDl _commonDl;
        private readonly IConfiguration _configuration;
        private readonly AuthenticationDL _authenticationDL;
        public UsersDL(CommonDl commonDl,[FromServices] IConfiguration configuration)
        {
            _commonDl = commonDl;
            _configuration = configuration;
            _authenticationDL = new AuthenticationDL(commonDl ,configuration);
        }
        public List<GetUser> GetUsers()
        {
            List<GetUser> getUsers = new List<GetUser>();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("sp_GetAllUserRoles", conn))
                    {
                        SqlDataReader myReader = myCMD.ExecuteReader();
                        do
                        {
                            while (myReader.Read())
                            {
                                GetUser obj = new GetUser();
                                obj.UserId = myReader["UserId"].ToString();
                                obj.UserType = myReader["RoleName"].ToString();
                                obj.UserName = myReader["UserName"].ToString();
                                obj.CompanyID = (int)myReader["CompanyID"];
                                obj.Company = myReader["CName"].ToString();
                                obj.firstname = myReader["firstname"].ToString();
                                obj.lastName = myReader["lastName"].ToString();
                                obj.roleid = (int)myReader["roleid"];
                                obj.status = (int)myReader["Activestatus"];
                                getUsers.Add(obj);
                            }
                        }
                        while (myReader.NextResult());
                        myReader.Close();
                        conn.Close();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in GetCardsAllDL. ", ex);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return getUsers;
        }
        public List<GetUserForSearch> GetUsersForSearch(UserRequestForSearch userRqst)
        {
            List<GetUserForSearch> getUsers = new List<GetUserForSearch>();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_GET_ALL_USER_FOR_SEARCH", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        myCMD.Parameters.AddWithValue("@CREATEDBY", userRqst.UserId);
                        SqlDataReader myReader = myCMD.ExecuteReader();
                        do
                        {
                            while (myReader.Read())
                            {
                                GetUserForSearch obj = new GetUserForSearch();
                                obj.USERID = myReader["USERID"].ToString();
                                obj.FULLNAME = myReader["FULLNAME"].ToString();

                                getUsers.Add(obj);
                            }
                        }
                        while (myReader.NextResult());
                        myReader.Close();
                        conn.Close();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in GetCardsAllDL. ", ex);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return getUsers;
        }

        public List<GetUserForSearch> GetUsersForQASearch(UserRequestForSearch userRqst)
        {
            List<GetUserForSearch> getUsers = new List<GetUserForSearch>();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_GET_ALL_USER_FOR_QA_SEARCH", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        myCMD.Parameters.AddWithValue("@CREATEDBY", userRqst.UserId);
                        SqlDataReader myReader = myCMD.ExecuteReader();
                        do
                        {
                            while (myReader.Read())
                            {
                                GetUserForSearch obj = new GetUserForSearch();
                                obj.USERID = myReader["USERID"].ToString();
                                obj.FULLNAME = myReader["FULLNAME"].ToString();

                                getUsers.Add(obj);
                            }
                        }
                        while (myReader.NextResult());
                        myReader.Close();
                        conn.Close();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in GetCardsAllDL. ", ex);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return getUsers;
        }
        public UserspasswordResponse changepassword(UserspasswordRequest userspasswordRequest)
        {
            UserspasswordResponse userspasswordResponse = new UserspasswordResponse();
            UserLoginRequest userRequest = new UserLoginRequest
            {
                username = userspasswordRequest.UserId,
                password = userspasswordRequest.OldPassword
            };
            LoginResponse loggedInUser = _authenticationDL.LoginUsers(userRequest);

            if (loggedInUser != null)
            {
                using (SqlConnection conn = _commonDl.Connect())
                {
                    try
                    {
                        using (SqlCommand myCMD = new SqlCommand("sp_Password_updt", conn))
                        {
                            myCMD.CommandType = CommandType.StoredProcedure;
                            myCMD.Parameters.AddWithValue("@password", userspasswordRequest.Password);
                            myCMD.Parameters.AddWithValue("@userid", userspasswordRequest.UserId);
                            myCMD.Connection = conn;
                            int k = myCMD.ExecuteNonQuery();
                            if (k != 0)
                            {
                                userspasswordResponse.Message = "The password has been updated successfully.";
                                userspasswordResponse.isChanged = true;
                            }
                            else
                            {
                                userspasswordResponse.Message = " Try again after some time ";
                                userspasswordResponse.isChanged = false;
                            }
                            conn.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error in changepassword DL. ", ex);
                    }
                    finally
                    {
                        conn.Close();
                        conn.Dispose();
                    }
                }
            }
            else
            {
                userspasswordResponse.Message = " Old Password is incorrect ";
                userspasswordResponse.isChanged = false;
            }

            return userspasswordResponse;
        }
        public Usersroleresponse changerole(Usersrolerequest usersrolerequest)
        {
            Usersroleresponse usersroleresponse = new Usersroleresponse();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("sp_Role_updt", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        myCMD.Parameters.AddWithValue("@UserRoleID", usersrolerequest.roleid);
                        myCMD.Parameters.AddWithValue("@UserId", usersrolerequest.UserId);
                        myCMD.Connection = conn;
                        int k = myCMD.ExecuteNonQuery();
                        if (k != 0)
                        {
                            usersroleresponse.Message = "Record updated Succesfully ";
                            usersroleresponse.isChanged = true;
                        }
                        else
                        {
                            usersroleresponse.Message = " Try again after some time ";
                            usersroleresponse.isChanged = false;
                        }
                        conn.Close();
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception("Error in changerole. ", ex);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return usersroleresponse;
        }
        public List<AccessResponse> GetRoleMenuAccessDL1(AccessRequest request)
        {
            List<AccessResponse> response = new List<AccessResponse>();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_GET_ROLE_MENU", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        myCMD.Parameters.AddWithValue("@USERID", request.userid);
                        myCMD.Parameters.AddWithValue("@COMPANYID", request.companyid);
                        using (SqlDataReader reader = myCMD.ExecuteReader())
                        {
                            response = DataReaderMapToList<AccessResponse>(reader);
                        }
                        conn.Close();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in GetRoleMenuAccessDL. ", ex);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return response;
        }
        public async Task<JsonResult> GetRoleMenuAccessDL(AccessRequest request)
        {
            using (SqlConnection conn = _commonDl.Connect())
            {
                var result = await GetAccessResponseListAsync(request, conn);
                var responseObject = new
                {
                    Data = result,
                };
                return new JsonResult(responseObject);
            }
        }

        private async Task<List<AccessResponse>> GetAccessResponseListAsync(AccessRequest request, SqlConnection conn)
        {
            try
            {
                var result = new List<AccessResponse>();

                using (SqlCommand myCMD = new SqlCommand("SP_GET_ROLE_MENU", conn))
                {
                    myCMD.CommandType = CommandType.StoredProcedure;
                    myCMD.Parameters.AddWithValue("@USERID", request.userid);
                    myCMD.Parameters.AddWithValue("@COMPANYID", request.companyid);

                    using (SqlDataReader myReader = await myCMD.ExecuteReaderAsync())
                    {
                        while (await myReader.ReadAsync())
                        {
                            AccessResponse obj = new AccessResponse
                            {
                                role_id = (int)myReader["role_id"],
                                menu_id = (int)myReader["menu_id"],
                                menu_text = myReader["menu_text"].ToString(),
                                menu_path = myReader["menu_path"].ToString(),
                                icontext = myReader["icontext"].ToString(),
                                Submenus = new List<Submenu>()
                            };

                            if (!myReader.IsDBNull(myReader.GetOrdinal("SUBMENU_ID")))
                            {
                                obj.Submenus.Add(new Submenu
                                {
                                    SUBMENU_ID = (int)myReader["SUBMENU_ID"],
                                    SUBMENU_TEXT = myReader["SUBMENU_TEXT"].ToString(),
                                    SUBMENU_PATH = myReader["SUBMENU_PATH"].ToString(),
                                    SUBICONTEXT = myReader["SUBICONTEXT"].ToString()
                                });
                            }

                            result.Add(obj);
                        }
                    }
                }

                var groupedResult = result.GroupBy(r => new
                {
                    r.role_id,
                    r.menu_id,
                    r.menu_text,
                    r.menu_path,
                    r.icontext
                })
                .Select(g => new AccessResponse
                {
                    role_id = g.Key.role_id,
                    menu_id = g.Key.menu_id,
                    menu_text = g.Key.menu_text,
                    menu_path = g.Key.menu_path,
                    icontext = g.Key.icontext,
                    Submenus = g.SelectMany(x => x.Submenus).ToList()
                })
                .ToList();

                return groupedResult;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GetNewDashboard_Data. ", ex);
            }
        }
        public static List<T> DataReaderMapToList<T>(IDataReader dr)
        {
            List<T> list = new List<T>();
            T obj = default(T);
            while (dr.Read())
            {
                obj = Activator.CreateInstance<T>();
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    if (!object.Equals(dr[prop.Name], DBNull.Value))
                    {
                        prop.SetValue(obj, dr[prop.Name], null);
                    }
                }
                list.Add(obj);
            }
            return list;
        }
    }
}
