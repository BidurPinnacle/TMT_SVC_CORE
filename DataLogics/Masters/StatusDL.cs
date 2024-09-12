using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using TMT_Code_Migration1.DataLogics.Utility;
using TMT_Code_Migration1.Models.Masters.Status;
using TMT_Code_Migration1.Views.Interfaces.Masters;

namespace TMT_Code_Migration1.DataLogics.Masters
{
    public class StatusDL : IStatus
    {
        private readonly CommonDl _commonDl;
        private readonly IConfiguration _config;
        public StatusDL(CommonDl commonDl, [FromServices] IConfiguration config)
        {
            _commonDl = commonDl;
            _config = config;
        }
        public List<StatusData> GetStatusData()
        {
            List<StatusData> statusDatas = new List<StatusData>();

            using (SqlConnection conn =  _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_GET_STATUS_DATA_FOR_MASTER", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                StatusData obj = new StatusData();
                                obj.statusId = myReader["card_status"] != DBNull.Value ? Convert.ToInt32(myReader["card_status"]) : 0;
                                obj.statusText = myReader["card_Description"] != DBNull.Value ? myReader["card_Description"].ToString() : "NA";
                                obj.statusActive = myReader["ACTIVE"] != DBNull.Value ? myReader["ACTIVE"].ToString() : "NA";
                                obj.statusRank = myReader["CARD_RANK"] != DBNull.Value ? Convert.ToInt32(myReader["CARD_RANK"]) : 0;
                                statusDatas.Add(obj);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in SP_GET_STATUS_DATA_FOR_MASTER. ", ex);
                }
            }
            return statusDatas;
        }
        public List<StatusData> GetStatusDataById(int statusId)
        {
            List<StatusData> statusDatas = new List<StatusData>();

            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_GET_STATUS_DATA_BY_ID_FOR_MASTER", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        SqlParameter[] param = new SqlParameter[1];
                        param[0] = new SqlParameter("@STATUS_ID", statusId);
                        myCMD.Parameters.Add(param[0]);
                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                StatusData obj = new StatusData();
                                obj.statusId = myReader["card_status"] != DBNull.Value ? Convert.ToInt32(myReader["card_status"]) : 0;
                                obj.statusText = myReader["card_Description"] != DBNull.Value ? myReader["card_Description"].ToString() : "NA";
                                obj.statusActive = myReader["ACTIVE"] != DBNull.Value ? myReader["ACTIVE"].ToString() : "NA";
                                obj.statusRank = myReader["CARD_RANK"] != DBNull.Value ? Convert.ToInt32(myReader["CARD_RANK"]) : 0;
                                statusDatas.Add(obj);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in SP_GET_STATUS_DATA_BY_ID_FOR_MASTER. ", ex);
                }
            }
            return statusDatas;
        }
        public InsertUpdateStatusData insertUpdateStatus(InsUpdateStatusRequest rqst)
        {
            InsertUpdateStatusData res = new InsertUpdateStatusData();

            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_INSERT_UPDATE_STATUS_FOR_MASTER", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        myCMD.Parameters.AddWithValue("@STATUSID", rqst.statusId.HasValue ? rqst.statusId : (object)DBNull.Value);
                        myCMD.Parameters.AddWithValue("@STATUSTEXT", rqst.statusText != null ? rqst.statusText : (object)DBNull.Value);
                        myCMD.Parameters.AddWithValue("@STATUSACTIVE", rqst.statusActive != null ? rqst.statusActive : (object)DBNull.Value);
                        myCMD.Parameters.AddWithValue("@STATUSRANK", rqst.statusRank.HasValue ? rqst.statusRank : (object)DBNull.Value);
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
                    throw new Exception("Error in SP_INSERT_UPDATE_STATUS_FOR_MASTER. ", ex);
                }
            }
            return res;
        }
    }
}
