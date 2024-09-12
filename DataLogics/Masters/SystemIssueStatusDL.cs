using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using TMT_Code_Migration1.DataLogics.Utility;
using TMT_Code_Migration1.Models.Masters.System_Issue;
using TMT_Code_Migration1.Views.Interfaces.Masters;

namespace TMT_Code_Migration1.DataLogics.Masters
{
    public class SystemIssueStatusDL: ISystemIssueStatus
    {
        private readonly CommonDl _commonDl;
        private readonly IConfiguration _config;
        public SystemIssueStatusDL(CommonDl commonDl, [FromServices] IConfiguration config)
        {
            _commonDl = commonDl;
            _config = config;
        }
        public List<SystemIssueStatusData> GetSystemIssueStatusData()
        {
            List<SystemIssueStatusData> systemIssueStatusDatas = new List<SystemIssueStatusData>();

            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_GET_SYSTEM_ISSUE_STATUS_DATA_FOR_MASTER", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                SystemIssueStatusData obj = new SystemIssueStatusData();
                                obj.statusId = myReader["SYID"] != DBNull.Value ? Convert.ToInt32(myReader["SYID"]) : 0;
                                obj.statusName = myReader["SNAME"] != DBNull.Value ? myReader["SNAME"].ToString() : "";
                                obj.statusActive = myReader["SACTIVE"] != DBNull.Value ? myReader["SACTIVE"].ToString() : "";
                                systemIssueStatusDatas.Add(obj);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in SP_GET_SYSTEM_ISSUE_STATUS_DATA_FOR_MASTER. ", ex);
                }
            }
            return systemIssueStatusDatas;
        }
        public List<SystemIssueStatusData> GetSystemIssueStatusDataById(int statusId)
        {
            List<SystemIssueStatusData> systemIssueStatusDatas = new List<SystemIssueStatusData>();

            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_GET_SYSTEM_ISSUE_STATUS_DATA_BY_ID_FOR_MASTER", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        SqlParameter[] param = new SqlParameter[1];
                        param[0] = new SqlParameter("@SYID", statusId);
                        myCMD.Parameters.Add(param[0]);
                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                SystemIssueStatusData obj = new SystemIssueStatusData();
                                obj.statusId = myReader["SYID"] != DBNull.Value ? Convert.ToInt32(myReader["SYID"]) : 0;
                                obj.statusName = myReader["SNAME"] != DBNull.Value ? myReader["SNAME"].ToString() : "";
                                obj.statusActive = myReader["SACTIVE"] != DBNull.Value ? myReader["SACTIVE"].ToString() : "";
                                systemIssueStatusDatas.Add(obj);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in SP_GET_SYSTEM_ISSUE_STATUS_DATA_BY_ID_FOR_MASTER. ", ex);
                }
            }
            return systemIssueStatusDatas;
        }
        public InsertUpdateSystemIssueStatusData insertUpdateSystemIssueStatus(InsUpdateSystemIssueStatusRequest rqst)
        {
            InsertUpdateSystemIssueStatusData res = new InsertUpdateSystemIssueStatusData();

            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_INSERT_UPDATE_SYSTEM_ISSUE_STATUS_FOR_MASTER", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        myCMD.Parameters.AddWithValue("@SYID", rqst.statusId.HasValue ? rqst.statusId : (object)DBNull.Value);
                        myCMD.Parameters.AddWithValue("@SNAME", rqst.statusName != null ? rqst.statusName : (object)DBNull.Value);
                        myCMD.Parameters.AddWithValue("@SACTIVE", rqst.statusActive != null ? rqst.statusActive : (object)DBNull.Value);
                        myCMD.Parameters.AddWithValue("@OPERATION_TYPE", rqst.operationType);
                        myCMD.Parameters.Add("@VOUTPUT", SqlDbType.Char, 100);
                        myCMD.Parameters["@VOUTPUT"].Direction = ParameterDirection.Output;
                        int k = myCMD.ExecuteNonQuery();
                        string str = (string)myCMD.Parameters["@VOUTPUT"].Value;
                        res.returnText = str;
                        res.returnCode = k;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in SP_INSERT_UPDATE_SYSTEM_ISSUE_STATUS_FOR_MASTER. ", ex);
                }
            }
            return res;
        }
    }
}
