using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using TMT_Code_Migration1.DataLogics.Utility;
using TMT_Code_Migration1.Models.Masters.System_Issue;
using TMT_Code_Migration1.Views.Interfaces.Masters;

namespace TMT_Code_Migration1.DataLogics.Masters
{
    public class SystemIssueListDL: ISystemIssueList
    {
        private readonly CommonDl _commonDl;
        private readonly IConfiguration _config;
        public SystemIssueListDL(CommonDl commonDl, [FromServices] IConfiguration config)
        {
            _commonDl = commonDl;
            _config = config;
        }
        public List<SystemIssueListData> GetIssueListDatas()
        {
            List<SystemIssueListData> systemIssueListDatas = new List<SystemIssueListData>();

            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_GET_SYSTEM_ISSUE_LIST_DATA_FOR_MASTER", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                SystemIssueListData obj = new SystemIssueListData();
                                obj.issueId = myReader["ISSUE_ID"] != DBNull.Value ? Convert.ToInt32(myReader["ISSUE_ID"]) : 0;
                                obj.issueText = myReader["ISSUE_NAME"] != DBNull.Value ? myReader["ISSUE_NAME"].ToString() : "NA";
                                obj.issueDescription = myReader["ISSUE_DESC"] != DBNull.Value ? myReader["ISSUE_DESC"].ToString() : "NA";
                                obj.issueStatus = myReader["ISSUE_STATUS"] != DBNull.Value ? myReader["ISSUE_STATUS"].ToString() : "NA";
                                systemIssueListDatas.Add(obj);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in SP_GET_SYSTEM_ISSUE_LIST_DATA_FOR_MASTER. ", ex);
                }
            }
            return systemIssueListDatas;
        }
        public List<SystemIssueListData> GetIssueListDatasById(int sid)
        {
            List<SystemIssueListData> systemIssueListDatas = new List<SystemIssueListData>();

            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_GET_SYSTEM_ISSUE_LIST_DATA_BY_ID_FOR_MASTER", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        SqlParameter[] param = new SqlParameter[1];
                        param[0] = new SqlParameter("@ISSUE_ID", sid);
                        myCMD.Parameters.Add(param[0]);
                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                SystemIssueListData obj = new SystemIssueListData();
                                obj.issueId = myReader["ISSUE_ID"] != DBNull.Value ? Convert.ToInt32(myReader["ISSUE_ID"]) : 0;
                                obj.issueText = myReader["ISSUE_NAME"] != DBNull.Value ? myReader["ISSUE_NAME"].ToString() : "NA";
                                obj.issueDescription = myReader["ISSUE_DESC"] != DBNull.Value ? myReader["ISSUE_DESC"].ToString() : "NA";
                                obj.issueStatus = myReader["ISSUE_STATUS"] != DBNull.Value ? myReader["ISSUE_STATUS"].ToString() : "NA";
                                systemIssueListDatas.Add(obj);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in SP_GET_SYSTEM_ISSUE_LIST_DATA_BY_ID_FOR_MASTER. ", ex);
                }
            }
            return systemIssueListDatas;
        }
        public InsertUpdateSystemIssueListData insertUpdateSystemIssue(InsUpdateSystemIssueRequest rqst)
        {
            InsertUpdateSystemIssueListData res = new InsertUpdateSystemIssueListData();

            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_INSERT_UPDATE_SYSTEM_ISSUE_LIST_FOR_MASTER", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        myCMD.Parameters.AddWithValue("@ISSUEID", rqst.issueId.HasValue ? rqst.issueId : (object)DBNull.Value);
                        myCMD.Parameters.AddWithValue("@ISSUETEXT", rqst.issueText != null ? rqst.issueText : (object)DBNull.Value);
                        myCMD.Parameters.AddWithValue("@ISSUEDESCRIPTION", rqst.issueDescription != null ? rqst.issueDescription : (object)DBNull.Value);
                        myCMD.Parameters.AddWithValue("@ISSUESTATUS", rqst.issueStatus != null ? rqst.issueStatus : (object)DBNull.Value);
                        myCMD.Parameters.AddWithValue("@OPERATION_TYPE", rqst.operationType);
                        myCMD.Parameters.Add("@VOUTPUT", SqlDbType.Char, 100);
                        myCMD.Parameters["@VOUTPUT"].Direction = ParameterDirection.Output;
                        int k = myCMD.ExecuteNonQuery();
                        string outputMessage = (string)myCMD.Parameters["@VOUTPUT"].Value;
                        res.returnText = outputMessage;
                        res.returnCode = k;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in SP_INSERT_UPDATE_SYSTEM_ISSUE_LIST_FOR_MASTER. ", ex);
                }
            }
            return res;
        }
    }
}
