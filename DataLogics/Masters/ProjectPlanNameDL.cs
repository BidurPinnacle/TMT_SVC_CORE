using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using TMT_Code_Migration1.DataLogics.Utility;
using TMT_Code_Migration1.Models.Masters.Project_Plan_Name;
using TMT_Code_Migration1.Views.Interfaces.Masters;

namespace TMT_Code_Migration1.DataLogics.Masters
{
    public class ProjectPlanNameDL: IProjectPlanName
    {
        private readonly CommonDl _commonDl;
        private readonly IConfiguration _config;
        public ProjectPlanNameDL(CommonDl commonDl, [FromServices] IConfiguration config)
        {
            _commonDl = commonDl;
            _config = config;
        }
        public List<ProjectPlanData> GetProjectPlanData()
        {
            List<ProjectPlanData> projectPlanDatas = new List<ProjectPlanData>();

            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_GET_PROJECT_PLAN_DATA_FOR_MASTER", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                ProjectPlanData obj = new ProjectPlanData();
                                obj.projectId = myReader["id"] != DBNull.Value ? Convert.ToInt32(myReader["id"]) : 0;
                                obj.projectPlanName = myReader["plan_name"] != DBNull.Value ? myReader["plan_name"].ToString() : "";
                                obj.projectStatus = myReader["PLAN_Status"] != DBNull.Value ? myReader["PLAN_Status"].ToString() : "";
                                projectPlanDatas.Add(obj);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in SP_GET_PROJECT_PLAN_DATA_FOR_MASTER. ", ex);
                }
            }
            return projectPlanDatas;
        }
        public List<ProjectPlanData> GetProjectPlanDataById(int projectId)
        {
            List<ProjectPlanData> projectPlanDatas = new List<ProjectPlanData>();

            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_GET_PROJECT_PLAN_DATA_BY_ID_FOR_MASTER", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        SqlParameter[] param = new SqlParameter[1];
                        param[0] = new SqlParameter("@PROJECT_ID", projectId);
                        myCMD.Parameters.Add(param[0]);
                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                ProjectPlanData obj = new ProjectPlanData();
                                obj.projectId = myReader["id"] != DBNull.Value ? Convert.ToInt32(myReader["id"]) : 0;
                                obj.projectPlanName = myReader["plan_name"] != DBNull.Value ? myReader["plan_name"].ToString() : "";
                                obj.projectStatus = myReader["PLAN_STATUS"] != DBNull.Value ? myReader["PLAN_STATUS"].ToString() : "";
                                projectPlanDatas.Add(obj);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in SP_GET_PROJECT_PLAN_DATA_BY_ID_FOR_MASTER. ", ex);
                }
            }
            return projectPlanDatas;
        }
        public InsertUpdateProjectPlanData insertUpdateProject(InsUpdateProjectRequest rqst)
        {
            InsertUpdateProjectPlanData res = new InsertUpdateProjectPlanData();

            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_INSERT_UPDATE_PROJECT_PLAN_FOR_MASTER", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        myCMD.Parameters.AddWithValue("@PROJECT_ID", rqst.projectId.HasValue ? rqst.projectId : (object)DBNull.Value);
                        myCMD.Parameters.AddWithValue("@PLAN_NAME", rqst.planName != null ? rqst.planName : (object)DBNull.Value);
                        myCMD.Parameters.AddWithValue("@STATUS", rqst.status != null ? rqst.status : (object)DBNull.Value);
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
                    throw new Exception("Error in SP_INSERT_UPDATE_PROJECT_PLAN_FOR_MASTER. ", ex);
                }
            }
            return res;
        }
    }
}
