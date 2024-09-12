using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using TMT_Code_Migration1.DataLogics.Utility;
using TMT_Code_Migration1.Models.Masters.Root_Cause;
using TMT_Code_Migration1.Views.Interfaces.Masters;

namespace TMT_Code_Migration1.DataLogics.Masters
{
    public class RootCauseDL: IRootCause
    {
        private readonly CommonDl _commonDl;
        private readonly IConfiguration _config;
        public RootCauseDL(CommonDl commonDl, [FromServices] IConfiguration config)
        {
            _commonDl = commonDl;
            _config = config;
        }
        public List<RootCauseForMasterData> GetRootCauseForMasterData()
        {
            List<RootCauseForMasterData> rootCauseForMasterDatas = new List<RootCauseForMasterData>();

            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_GET_ROOT_CAUSE_DATA_FOR_MASTER", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                RootCauseForMasterData obj = new RootCauseForMasterData();
                                obj.causeId = myReader["ID"] != DBNull.Value ? Convert.ToInt32(myReader["ID"]) : 0;
                                obj.RootCauseText = myReader["ROOT_CAUSE_TEXT"] != DBNull.Value ? myReader["ROOT_CAUSE_TEXT"].ToString() : "";
                                obj.status = myReader["CAUSE_STATUS"] != DBNull.Value ? myReader["CAUSE_STATUS"].ToString() : "";
                                obj.createdBy = myReader["CREATED_BY"] != DBNull.Value ? myReader["CREATED_BY"].ToString() : "";
                                obj.causeDescription = myReader["CAUSE_DESCRIPTION"] != DBNull.Value ? myReader["CAUSE_DESCRIPTION"].ToString() : "";
                                obj.companyName = myReader["COMPANY_NAME"] != DBNull.Value ? myReader["COMPANY_NAME"].ToString() : "";
                                rootCauseForMasterDatas.Add(obj);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in SP_GET_ROOT_CAUSE_DATA_FOR_MASTER. ", ex);
                }
            }
            return rootCauseForMasterDatas;
        }
        public List<RootCauseForMasterDataById> GetRootCauseForMasterDataById(int causeId)
        {
            List<RootCauseForMasterDataById> rootCauseForMasterDatas = new List<RootCauseForMasterDataById>();

            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_GET_ROOT_CAUSE_DATA_BY_ID_FOR_MASTER", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        SqlParameter[] param = new SqlParameter[1];
                        param[0] = new SqlParameter("@CAUSE_ID", causeId);
                        myCMD.Parameters.Add(param[0]);
                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                RootCauseForMasterDataById obj = new RootCauseForMasterDataById();
                                obj.causeId = myReader["ID"] != DBNull.Value ? Convert.ToInt32(myReader["ID"]) : 0;
                                obj.RootCauseText = myReader["ROOT_CAUSE_TEXT"] != DBNull.Value ? myReader["ROOT_CAUSE_TEXT"].ToString() : "";
                                obj.status = myReader["CAUSE_STATUS"] != DBNull.Value ? myReader["CAUSE_STATUS"].ToString() : "";
                                obj.createdBy = myReader["CREATED_BY"] != DBNull.Value ? myReader["CREATED_BY"].ToString() : "";
                                obj.causeDescription = myReader["CAUSE_DESCRIPTION"] != DBNull.Value ? myReader["CAUSE_DESCRIPTION"].ToString() : "";
                                obj.companyId = myReader["COMPANY_ID"] != DBNull.Value ? Convert.ToInt32(myReader["COMPANY_ID"]) : 0;
                                rootCauseForMasterDatas.Add(obj);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in SP_GET_ROOT_CAUSE_DATA_BY_ID_FOR_MASTER. ", ex);
                }
            }
            return rootCauseForMasterDatas;
        }
        public InsertUpdateRootCauseForMasterData insertUpdateRootCauseForMaster(InsUpdateRootCauseForMasterRequest rqst)
        {
            InsertUpdateRootCauseForMasterData res = new InsertUpdateRootCauseForMasterData();

            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_INSERT_UPDATE_ROOT_CAUSE_FOR_MASTER", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        myCMD.Parameters.AddWithValue("@CAUSEID", rqst.causeId.HasValue ? rqst.causeId : (object)DBNull.Value);
                        myCMD.Parameters.AddWithValue("@ROOTCAUSETEXT", rqst.RootCauseText != null ? rqst.RootCauseText : (object)DBNull.Value);
                        myCMD.Parameters.AddWithValue("@STATUS", rqst.status != null ? rqst.status : (object)DBNull.Value);
                        myCMD.Parameters.AddWithValue("@CREATEDBY", rqst.createdBy != null ? rqst.createdBy : (object)DBNull.Value);
                        myCMD.Parameters.AddWithValue("CAUSE_DESC", rqst.causeDesc != null ? rqst.causeDesc : (object)DBNull.Value);
                        myCMD.Parameters.AddWithValue("@COMPANY_ID", rqst.companyid != null ? rqst.companyid : (object)DBNull.Value);
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
                    throw new Exception("Error in SP_INSERT_UPDATE_COMPANY_FOR_MASTER. ", ex);
                }
            }
            return res;
        }
    }
}
