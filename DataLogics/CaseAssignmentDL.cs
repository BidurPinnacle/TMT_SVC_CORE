using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using TMT_Code_Migration1.DataLogics.Utility;
using TMT_Code_Migration1.Models.caseAssignment;
using TMT_Code_Migration1.Views.Interfaces;

namespace TMT_Code_Migration1.DataLogics
{
    public class CaseAssignmentDL : ICaseAssignment
    {
        private readonly CommonDl _commonDl;
        private readonly IConfiguration _configuration;
        private readonly AuthenticationDL _authenticationDL;
        public CaseAssignmentDL(CommonDl commonDl, [FromServices] IConfiguration configuration)
        {
            _commonDl = commonDl;
            _configuration = configuration;
            _authenticationDL = new AuthenticationDL(commonDl, configuration);
        }
        public List<TaskTeamUserResponse> GetAllTaskTeamUsers()
        {
            List<TaskTeamUserResponse> taskTeamUsers = new List<TaskTeamUserResponse>();

            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_GET_ALL_TASK_TEAM_USERS", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                TaskTeamUserResponse obj = new TaskTeamUserResponse
                                {
                                    UserId = myReader["userId"].ToString(),
                                    FullName = myReader["fullname"].ToString()
                                };

                                taskTeamUsers.Add(obj);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in GetAllTaskTeamUsers. ", ex);
                }
            }

            return taskTeamUsers;
        }

        public List<ExcelUploadedDataResponse> GetExcelUploadedData(ExcelUploadedDataRequest request)
        {
            List<ExcelUploadedDataResponse> excelData = new List<ExcelUploadedDataResponse>();

            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("sp_get_excel_uploaded_data", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;

                        // Add parameters from the request object
                        myCMD.Parameters.Add(new SqlParameter("@tasktype", request.TaskType ?? (object)DBNull.Value));
                        myCMD.Parameters.Add(new SqlParameter("@workTypeId", string.IsNullOrEmpty(request.WorkTypeId) ? (object)DBNull.Value : request.WorkTypeId));
                        myCMD.Parameters.Add(new SqlParameter("@cardStatusId", request.CardStatusId ?? (object)DBNull.Value));
                        myCMD.Parameters.Add(new SqlParameter("@userName", string.IsNullOrEmpty(request.UserName) ? (object)DBNull.Value : request.UserName));
                        myCMD.Parameters.Add(new SqlParameter("@installNum", string.IsNullOrEmpty(request.InstallNum) ? (object)DBNull.Value : request.InstallNum));
                        myCMD.Parameters.Add(new SqlParameter("@bpemNum", string.IsNullOrEmpty(request.BpemNum) ? (object)DBNull.Value : request.BpemNum));

                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                ExcelUploadedDataResponse obj = new ExcelUploadedDataResponse
                                {
                                    TaskText = myReader["tasktext"].ToString(),
                                    WorkTypeName = myReader["worktypename"].ToString(),
                                    Bpem = myReader["bpem"].ToString(),
                                    Installation = myReader["installation"].ToString(),
                                    CardDescription = myReader["card_description"].ToString(),
                                    RecordCount = Convert.ToInt32(myReader["record_count"]),
                                    DateAdded = Convert.ToDateTime(myReader["date_added"]),
                                    Uploader = myReader["Uploader"].ToString(),
                                    AssignedTo = myReader["Assigned To"].ToString()
                                };

                                excelData.Add(obj);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in GetExcelUploadedData. ", ex);
                }
            }

            return excelData;
        }

        public string ReassignUser(ReassignUserRequest request)
        {
            string result = string.Empty;

            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("sp_reassign_user", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        string jsonData = JsonConvert.SerializeObject(request.JsonData);
                        myCMD.Parameters.Add(new SqlParameter("@newUser", request.NewUser));
                        myCMD.Parameters.Add(new SqlParameter("@reassignUser", request.ReassignUser));
                        myCMD.Parameters.Add(new SqlParameter("@jsonData", jsonData));
                        SqlParameter outputParam = new SqlParameter("@result", SqlDbType.NVarChar, -1) { Direction = ParameterDirection.Output };
                        myCMD.Parameters.Add(outputParam);
                        myCMD.ExecuteNonQuery();
                        result = outputParam.Value.ToString();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in ReassignUser. ", ex);
                }
            }

            return result;
        }
    }
}
