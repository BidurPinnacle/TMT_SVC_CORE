using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using TMT_Code_Migration1.DataLogics.Utility;
using TMT_Code_Migration1.Models.New_Dashboard;
using TMT_Code_Migration1.Views.Interfaces;

namespace TMT_Code_Migration1.DataLogics
{
    public class NewDashBoardDL : INewDashBoard
    {
        private readonly CommonDl _commonDl;
        private readonly IConfiguration _config;
        public NewDashBoardDL(CommonDl connonDl , [FromServices] IConfiguration config)
        {
            _commonDl = connonDl;
            _config = config;
        }

        public List<Assignement_Response> GetAssignements(Assignement_Request request)
        {
            List<Assignement_Response> responses = new List<Assignement_Response>();

            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {

                    using (SqlCommand myCMD = new SqlCommand("SP_GET_ASSIGNMENT", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        myCMD.Parameters.AddWithValue("@CID", request.CompanyID);

                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                Assignement_Response obj = new Assignement_Response();
                                obj.AssignmentID = myReader.GetInt32(0);
                                obj.AssignmentName = myReader.GetString(1);
                                responses.Add(obj);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in SP_GET_ASSIGNMENT. ", ex);
                }
            }

            return responses;
        }
        public List<Projects_Response> GetProjects(Projects_Request request)
        {
            List<Projects_Response> responses = new List<Projects_Response>();
            string SP_GET_PROJECTS = _config.GetSection("storedProcedures")["SP_GET_PROJECTS"];
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {

                    using (SqlCommand myCMD = new SqlCommand(SP_GET_PROJECTS, conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        myCMD.Parameters.AddWithValue("@AssignmentID", request.AssignmentID);
                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                Projects_Response obj = new Projects_Response();
                                obj.ProjectID = myReader.GetInt32(0);
                                obj.ProjectName = myReader.GetString(1);
                                responses.Add(obj);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in SP_GET_PROJECTS. ", ex);
                }
            }

            return responses;
        }
        public List<WeekCount_Response> GetWeek()
        {
            List<WeekCount_Response> responses = new List<WeekCount_Response>();
            string SP_GET_WEEKCOUNT = _config.GetSection("storedProcedures")["SP_GET_WEEKCOUNT"];
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {

                    using (SqlCommand myCMD = new SqlCommand(SP_GET_WEEKCOUNT, conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                WeekCount_Response obj = new WeekCount_Response();
                                obj.ID = myReader.GetInt32(0);
                                obj.WeekCount = myReader.GetString(1);
                                responses.Add(obj);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in SP_GET_WEEKCOUNT. ", ex);
                }
            }

            return responses;
        }
        public List<Status_Response> GetStatus(Status_Request request)
        {
            List<Status_Response> responses = new List<Status_Response>();
            string SP_Get_Assignment_status = _config.GetSection("storedProcedures")["SP_Get_Assignment_status"];
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {

                    using (SqlCommand myCMD = new SqlCommand(SP_Get_Assignment_status, conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        myCMD.Parameters.AddWithValue("@ASSIGNMENTID", request.AssignmentID);
                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                Status_Response obj = new Status_Response();
                                obj.StatusID = myReader.GetInt32(0);
                                obj.StatusText = myReader.GetString(1);
                                responses.Add(obj);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in SP_Get_Assignment_status. ", ex);
                }
            }

            return responses;
        }


        public async Task<JsonResult> GetNewDashboard_Data(NewDashboard_request request)
        {
            using (SqlConnection conn = _commonDl.Connect())
            {
                var dataset = new DataSet();
                var adapter = new SqlDataAdapter();
                try
                {
                    adapter.SelectCommand = new SqlCommand("SP_GETPPRODUCTION_DATA", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@AssignmentID", request.AssignmentID));
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@projectID", request.projectID));
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@Date_from", request.Date_from));
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@Date_To", request.Date_To));
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@Status", request.Status));
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@WeekListindex", request.WeekListindex));
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@ClientID", request.client));

                    await Task.Run(() => adapter.Fill(dataset)); // Use Task.Run to run the Fill method asynchronously

                    var responseObject = new
                    {
                        Data = dataset,
                    };
                    return new JsonResult(responseObject);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in GetNewDashboard_Data. ", ex);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }

        public JsonResult GetNewDashboard_Data_Status(NewDashboard_Status_Request request)

        {
            string SP_GET_COUNT_AS_STATUS = _config.GetSection("storedProcedures")["SP_GET_COUNT_AS_STATUS"];
            using (SqlConnection conn = _commonDl.Connect())
            {
                var dataset = new DataSet();
                var adapter = new SqlDataAdapter();
                try
                {
                    adapter.SelectCommand = new SqlCommand(SP_GET_COUNT_AS_STATUS, conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@AssignmentID", request.AssignmentID));
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@projectID", request.projectID));
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@Date_from", request.Date_from));
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@Date_To", request.Date_To));
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@Status", request.Status));
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@WeekListindex", request.WeekListindex));


                    adapter.Fill(dataset);
                    var result = new
                    {
                        Data = dataset
                    };
                    return new JsonResult(result);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in SP_GET_COUNT_AS_STATUS. ", ex);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }


            }

        }
        public JsonResult GetNewDashboard_Data_User(NewDashboard_User_Request request)
        {
            string SP_GET_COUNT_AS_USER = _config.GetSection("storedProcedures")["SP_GET_COUNT_AS_USER"];
            using (SqlConnection conn = _commonDl.Connect())
            {
                var dataset = new DataSet();
                var adapter = new SqlDataAdapter();
                try
                {
                    adapter.SelectCommand = new SqlCommand(SP_GET_COUNT_AS_USER, conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@AssignmentID", request.AssignmentID));
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@projectID", request.projectID));
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@Date_from", request.Date_from));
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@Date_To", request.Date_To));
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@Status", request.Status));
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@WeekListindex", request.WeekListindex));
                    adapter.Fill(dataset);
                    var res = new
                    {
                        Data = dataset,
                    };
                    return new JsonResult(res);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in SP_GET_COUNT_AS_USER.", ex);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }


        }
        public JsonResult GET_Dashboard_all_tbl_data(DS_REQUEST request, string Dynamic_sp)
        {

            using (SqlConnection conn = _commonDl.Connect())
            {
                var dataset = new DataSet();
                var adapter = new SqlDataAdapter();

                try
                {
                    adapter.SelectCommand = new SqlCommand(Dynamic_sp, conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    if (request != null)
                    {
                        adapter.SelectCommand.Parameters.Add(request.AssignmentID != null ? new SqlParameter("@AssignmentID", request.AssignmentID) : new SqlParameter("@AssignmentID", DBNull.Value));
                        adapter.SelectCommand.Parameters.Add(request.projectID != null ? new SqlParameter("@projectID", request.projectID) : new SqlParameter("@projectID", DBNull.Value));
                        adapter.SelectCommand.Parameters.Add(request.Date_from != null ? new SqlParameter("@Date_from", request.Date_from) : new SqlParameter("@Date_from", DBNull.Value));
                        adapter.SelectCommand.Parameters.Add(request.Date_To != null ? new SqlParameter("@Date_To", request.Date_To) : new SqlParameter("@Date_To", DBNull.Value));
                        adapter.SelectCommand.Parameters.Add(request.Status != null ? new SqlParameter("@Status", request.Status) : new SqlParameter("@Status", DBNull.Value));
                        adapter.SelectCommand.Parameters.Add(request.WeekListindex != null ? new SqlParameter("@WeekListindex", request.WeekListindex) : new SqlParameter("@WeekListindex", DBNull.Value));
                        adapter.SelectCommand.Parameters.Add(request.ClientID != null ? new SqlParameter("@ClientID", request.ClientID) : new SqlParameter("@ClientID", DBNull.Value));
                    }
                    adapter.Fill(dataset);
                    var res = new
                    {
                        Data = dataset,
                    };
                    return new JsonResult(res);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in  " + Dynamic_sp, ex);
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed)
                    {
                        conn.Dispose(); // Asynchronously close and dispose the connection
                    }
                }
            }
        }


        public JsonResult GET_Dashboard_GRAPH(DS_REQUEST request, string Dynamic_sp)
        {
            using (SqlConnection conn = _commonDl.Connect())
            {
                var dataset = new DataSet();
                var adapter = new SqlDataAdapter();

                try
                {
                    adapter.SelectCommand = new SqlCommand(Dynamic_sp, conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@AssignmentID", request.AssignmentID));
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@projectID", request.projectID));
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@Date_from", request.Date_from));
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@Date_To", request.Date_To));
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@Status", request.Status));
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@WeekListindex", request.WeekListindex));
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@ClientID", request.ClientID));



                    adapter.Fill(dataset);
                    var res = new
                    {
                        Data = dataset,
                    };
                    return new JsonResult(res);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in  " + Dynamic_sp, ex);
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed)
                    {
                        conn.Dispose(); // Asynchronously close and dispose the connection
                    }
                }
            }
        }

        public AccessForComment GetAccessCodeForComment(string userId)
        {
            AccessForComment accessCode = new AccessForComment();

            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {

                    using (SqlCommand myCMD = new SqlCommand("SP_ACCESS_FOR_EDIT_DASHBOARD_COMMENT", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        myCMD.Parameters.AddWithValue("@userid", !string.IsNullOrEmpty(userId) ? userId : (object)DBNull.Value);

                        myCMD.Parameters.Add("@voutput", SqlDbType.Int);
                        myCMD.Parameters["@voutput"].Direction = ParameterDirection.Output;

                        myCMD.ExecuteNonQuery();
                        accessCode.AccessCode = (int)myCMD.Parameters["@voutput"].Value;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in SP_ACCESS_FOR_EDIT_DASHBOARD_COMMENT.", ex);
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed)
                    {
                        conn.Dispose(); // Asynchronously close and dispose the connection
                    }
                }
            }
            return accessCode;
        }
        public DashboardCommentResponse GetDashboardComment(DashboardCommentRequest request)
        {
            DashboardCommentResponse comment = null;

            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("SP_GET_DASHBOARD_COMMENT", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ENTRYDATE", request.EntryDate);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                comment = new DashboardCommentResponse
                                {
                                    AutoId = Convert.ToInt32(reader["AUTO_ID"]),
                                    CommentText = reader["COMMENT_TEXT"].ToString()
                                };
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception
                    Console.WriteLine("Error: " + ex.Message);
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed)
                    {
                        conn.Dispose(); // Asynchronously close and dispose the connection
                    }
                }
            }

            return comment;
        }
        public DashboardCommentInsUpdateResponse InsertUpdateDashboardComment(DashboardCommentInsUpdateRequest request)
        {
            DashboardCommentInsUpdateResponse response = new DashboardCommentInsUpdateResponse();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("SP_INSERT_UPDATE_DASHBOARD_COMMENT", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ENTRYDATE", request.EntryDate);
                        cmd.Parameters.AddWithValue("@COMMENT_TEXT", request.CommentText);
                        cmd.Parameters.AddWithValue("@USERID", request.UserId);
                        cmd.Parameters.AddWithValue("@COMPANY_ID", request.CompanyId);

                        SqlParameter outputParam = new SqlParameter("@VOUTPUT", SqlDbType.VarChar, 50);
                        outputParam.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(outputParam);

                        cmd.ExecuteNonQuery();
                        response.OutputMessage = outputParam.Value.ToString();
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception
                    Console.WriteLine("Error: " + ex.Message);
                    response.OutputMessage = "Error: " + ex.Message;
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed)
                    {
                        conn.Dispose(); // Asynchronously close and dispose the connection
                    }
                }
            }

            return response;
        }

        public List<DashboardCommentList> GetAllDashboardComments()
        {
            List<DashboardCommentList> comments = new List<DashboardCommentList>();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("SP_GET_ALL_DASHBOARD_COMMENTS", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DashboardCommentList comment = new DashboardCommentList
                                {
                                    EntryDate = reader.IsDBNull(0) ? DateTime.MinValue : reader.GetDateTime(0),
                                    CommentText = reader.IsDBNull(1) ? string.Empty : reader.GetString(1)
                                };
                                comments.Add(comment);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception
                    Console.WriteLine("Error: " + ex.Message);
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed)
                    {
                        conn.Dispose(); // Asynchronously close and dispose the connection
                    }
                }
            }

            return comments;
        }
        public string DeleteDashboardComment(DashboardCommentRequest request)
        {
            string outputMessage = string.Empty;
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("SP_DELETE_DASHBOARD_COMMENT", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ENTRYDATE", SqlDbType.DateTime)).Value = request.EntryDate;
                        SqlParameter outputParam = new SqlParameter("@VOUTPUT", SqlDbType.VarChar, 100)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(outputParam);
                        cmd.ExecuteNonQuery();
                        outputMessage = outputParam.Value.ToString();
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception
                    Console.WriteLine("Error: " + ex.Message);
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed)
                    {
                        conn.Dispose(); // Asynchronously close and dispose the connection
                    }
                }
            }

            return outputMessage;
        }
    }
}
