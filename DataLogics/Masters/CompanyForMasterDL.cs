using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using TMT_Code_Migration1.DataLogics.Utility;
using TMT_Code_Migration1.Models.Masters.CompanyForMaster;
using TMT_Code_Migration1.Views.Interfaces.Masters;

namespace TMT_Code_Migration1.DataLogics.Masters
{
    public class CompanyForMasterDL : ICompanyForMaster
    {
        private readonly CommonDl _commonDl;
        private readonly IConfiguration _configuration;
        public CompanyForMasterDL(CommonDl commonDl, [FromServices] IConfiguration configuration)
        {
            _commonDl = commonDl;
            _configuration = configuration;
        }
        public List<CompanyForMasterData> GetCompanyForMasterData()
        {
            List<CompanyForMasterData> companyForMasterDatas = new List<CompanyForMasterData>();

            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_GET_COMPANY_DATA_FOR_MASTER", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                CompanyForMasterData obj = new CompanyForMasterData();
                                obj.companyId = myReader["CId"] != DBNull.Value ? Convert.ToInt32(myReader["CId"]) : 0;
                                obj.companyName = myReader["CName"] != DBNull.Value ? myReader["CName"].ToString() : "";
                                obj.status = myReader["CSTATUS"] != DBNull.Value ? myReader["CSTATUS"].ToString() : "";
                                obj.companyDesc = myReader["CDescription"] != DBNull.Value ? myReader["CDescription"].ToString() : "";
                                companyForMasterDatas.Add(obj);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in SP_GET_COMPANY_DATA_FOR_MASTER. ", ex);
                }
            }
            return companyForMasterDatas;
        }
        public List<CompanyForMasterData> GetCompanyForMasterDataById(int companyId)
        {
            List<CompanyForMasterData> companyForMasterDatas = new List<CompanyForMasterData>();

            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_GET_COMPANY_DATA_BY_ID_FOR_MASTER", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        SqlParameter[] param = new SqlParameter[1];
                        param[0] = new SqlParameter("@WORK_TYPE_VALUE", companyId);
                        myCMD.Parameters.Add(param[0]);
                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                CompanyForMasterData obj = new CompanyForMasterData();
                                obj.companyId = myReader["CId"] != DBNull.Value ? Convert.ToInt32(myReader["CId"]) : 0;
                                obj.companyName = myReader["CName"] != DBNull.Value ? myReader["CName"].ToString() : "";
                                obj.status = myReader["CSTATUS"] != DBNull.Value ? myReader["CSTATUS"].ToString() : "";
                                obj.companyDesc = myReader["CDescription"] != DBNull.Value ? myReader["CDescription"].ToString() : "";
                                companyForMasterDatas.Add(obj);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in SP_GET_COMPANY_DATA_BY_ID_FOR_MASTER. ", ex);
                }
            }
            return companyForMasterDatas;
        }
        public InsertUpdateCompanyForMasterData insertUpdateCompanyForMaster(InsUpdateCompanyForMasterRequest rqst)
        {
            InsertUpdateCompanyForMasterData res = new InsertUpdateCompanyForMasterData();

            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_INSERT_UPDATE_COMPANY_FOR_MASTER", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        myCMD.Parameters.AddWithValue("@TASKID", rqst.companyId.HasValue ? rqst.companyId : DBNull.Value);
                        myCMD.Parameters.AddWithValue("@WORKNAME", rqst.companyName != null ? rqst.companyName : DBNull.Value);
                        myCMD.Parameters.AddWithValue("@WORKTYPEDESCRIPTION", rqst.status != null ? rqst.status : DBNull.Value);
                        myCMD.Parameters.AddWithValue("@WORKVALUE", rqst.companyDesc != null ? rqst.companyDesc : DBNull.Value);
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
