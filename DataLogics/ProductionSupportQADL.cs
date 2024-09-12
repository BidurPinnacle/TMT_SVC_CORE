using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using TMT_Code_Migration1.DataLogics.Utility;
using TMT_Code_Migration1.Models.Production_Support_QA;
using TMT_Code_Migration1.Views.Interfaces;

namespace TMT_Code_Migration1.DataLogics
{
    public class ProductionSupportQADL: IProductionSupportQA
    {
        private readonly CommonDl _commonDl;
        private readonly IConfiguration _config;
        public ProductionSupportQADL(CommonDl commonDl, [FromServices] IConfiguration config)
        {
            _commonDl = commonDl;
            _config = config;
        }
        public JsonResult GET_GETCARDBYUSERQA_SEARCH(QA_Search_request request, string Dynamic_sp)
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

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@Logged_in_user", request.Logged_in_user));
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@SearchUser", request.SearchUser));
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@TaskTypeID", request.TaskTypeID));
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@WorkTypeID", request.WorkTypeID));
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@FROMDATE", request.Fromdate));
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@TODATE", request.Todate));
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@CardsID", request.cardsid));
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@searchType", request.searchType));
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@qaedUser", request.qaedUser));
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@Installation", request.V_Installation));
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@qaStatus", request.QASts));
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
                        conn.Dispose();
                    }
                }
            }
        }
        public JsonResult GET_GETCARDBYUSERQA_SEARCHById(int request, string Dynamic_sp)
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
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@CardsID", request));

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
                        conn.Dispose();
                    }
                }
            }
        }
        public List<StatusForQAResponse> GetCardStatusDL()
        {
            List<StatusForQAResponse> statuses = new List<StatusForQAResponse>();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("[SP_GET_STATUS_FOR_SUPPORT_QA]", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                StatusForQAResponse obj = new StatusForQAResponse();
                                obj.CARD_STATUS = myReader.GetInt32(0);
                                obj.CARD_DESCRIPTION = myReader.GetString(1);
                                statuses.Add(obj);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in Cards. ", ex);
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed)
                    {
                        conn.Dispose();
                    }
                }
            }
            return statuses;
        }
        public List<QAEDUsers> GetQAEDByUserDL(string loginUser)
        {
            List<QAEDUsers> qaedUsers = new List<QAEDUsers>();

            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_GET_QAED_BY_USER", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        myCMD.Parameters.AddWithValue("@userId", loginUser);  // Pass the loginUser as parameter

                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                QAEDUsers obj = new QAEDUsers();
                                obj.userId = myReader.IsDBNull(myReader.GetOrdinal("UserId")) ? string.Empty : myReader.GetString(myReader.GetOrdinal("UserId"));
                                obj.userName = myReader.IsDBNull(myReader.GetOrdinal("UserName")) ? string.Empty : myReader.GetString(myReader.GetOrdinal("UserName"));
                                qaedUsers.Add(obj);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in QAED Users.", ex);
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed)
                    {
                        conn.Dispose();
                    }
                }
            }

            return qaedUsers;
        }

        public updateResponse UpdateCardForQA(List<updateCardRequestForQA> requests)
        {
            updateResponse firstResponse = null;

            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    foreach (var request in requests)
                    {
                        SqlCommand cmd = new SqlCommand("SP_UPDATE_CARD_DETAILS_FOR_QA", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CARDID", request.cardId);
                        cmd.Parameters.AddWithValue("@QASTATUS", request.statusId);
                        cmd.Parameters.AddWithValue("@QAMARK", request.RemarkValue);
                        cmd.Parameters.AddWithValue("@USER_ID", request.userId);

                        cmd.Parameters.Add("@VOUTPUT", SqlDbType.Char, 100);
                        cmd.Parameters["@VOUTPUT"].Direction = ParameterDirection.Output;

                        int k = cmd.ExecuteNonQuery();
                        string str = (string)cmd.Parameters["@VOUTPUT"].Value;

                        updateResponse currentResponse = new updateResponse
                        {
                            Message = str,
                            Code = k
                        };

                        if (firstResponse == null)
                        {
                            firstResponse = currentResponse;
                        }
                        else if (firstResponse.Message != currentResponse.Message || firstResponse.Code != currentResponse.Code)
                        {
                            return currentResponse;  // Return the different response immediately
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in Cards. ", ex);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }

            return firstResponse;  // Return the first response if all are the same
        }

        public List<QAStatus> GetDistinctQAStatus()
        {
            List<QAStatus> qaStatuses = new List<QAStatus>();

            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_GET_DISTINCT_QA_STATUS", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                QAStatus obj = new QAStatus();

                                obj.CardDescription = myReader.IsDBNull(myReader.GetOrdinal("card_description"))
                                    ? string.Empty
                                    : myReader.GetString(myReader.GetOrdinal("card_description"));

                                obj.QAStatuses = myReader.IsDBNull(myReader.GetOrdinal("qastatus"))
                                    ? 0
                                    : myReader.GetInt32(myReader.GetOrdinal("qastatus"));

                                qaStatuses.Add(obj);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in GetDistinctQAStatus.", ex);
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed)
                    {
                        conn.Dispose();
                    }
                }
            }

            return qaStatuses;
        }

    }
}
