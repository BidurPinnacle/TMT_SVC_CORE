using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using TMT_Code_Migration1.DataLogics.Utility;
using TMT_Code_Migration1.Models.Masters.Work_Type;
using TMT_Code_Migration1.Views.Interfaces.Masters;

namespace TMT_Code_Migration1.DataLogics.Masters
{
    public class WorkTypeDL: IWorkType
    {
        private readonly CommonDl _commonDl;
        private readonly IConfiguration _config;
        public WorkTypeDL(CommonDl commonDl, [FromServices] IConfiguration config)
        {
            _commonDl = commonDl;
            _config = config;
        }
        public List<WorkTypeData> GetWorkTypeData(int taskId)
        {
            List<WorkTypeData> workTypeDatas = new List<WorkTypeData>();

            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_GET_WORKTYPE_DATA_FOR_MASTER", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        myCMD.Parameters.Add(new SqlParameter("@taskId", SqlDbType.Int)).Value = taskId;
                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                WorkTypeData obj = new WorkTypeData();
                                obj.WorkTypeValue = myReader["worktypevalue"] != DBNull.Value ? Convert.ToInt32(myReader["worktypevalue"]) : 0;
                                obj.WorkypeName = myReader["worktypename"] != DBNull.Value ? myReader["worktypename"].ToString() : "";
                                obj.WorkTypeDescription = myReader["WORKTYPEDESCRIPTION"] != DBNull.Value ? myReader["WORKTYPEDESCRIPTION"].ToString() : "";
                                obj.TaskId = myReader["TASKID"] != DBNull.Value ? Convert.ToInt32(myReader["TASKID"]) : 0;
                                obj.TaskText = myReader["TASKTEXT"] != DBNull.Value ? myReader["TASKTEXT"].ToString() : "";
                                obj.ActiveStatus = myReader["ACTIVE_STATUS"] != DBNull.Value ? myReader["ACTIVE_STATUS"].ToString() : "";
                                obj.TargetPerDay = myReader["TargetPerDay"] != DBNull.Value ? Convert.ToInt32(myReader["TargetPerDay"]) : 0;
                                workTypeDatas.Add(obj);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in SP_GET_WORKTYPE_DATA_FOR_MASTER. ", ex);
                }
            }
            return workTypeDatas;
        }
        public List<WorkTypeData> GetWorkTypeDataById(int workTypeValue)
        {
            List<WorkTypeData> workTypeDatas = new List<WorkTypeData>();

            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_GET_WORKTYPE_DATA_BY_ID_FOR_MASTER", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        SqlParameter[] param = new SqlParameter[1];
                        param[0] = new SqlParameter("@WORK_TYPE_VALUE", workTypeValue);
                        myCMD.Parameters.Add(param[0]);
                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                WorkTypeData obj = new WorkTypeData();
                                obj.WorkTypeValue = myReader["worktypevalue"] != DBNull.Value ? Convert.ToInt32(myReader["worktypevalue"]) : 0;
                                obj.WorkypeName = myReader["worktypename"] != DBNull.Value ? myReader["worktypename"].ToString() : "";
                                obj.WorkTypeDescription = myReader["WORKTYPEDESCRIPTION"] != DBNull.Value ? myReader["WORKTYPEDESCRIPTION"].ToString() : "";
                                obj.TaskId = myReader["TASKID"] != DBNull.Value ? Convert.ToInt32(myReader["TASKID"]) : 0;
                                obj.TaskText = myReader["TASKTEXT"] != DBNull.Value ? myReader["TASKTEXT"].ToString() : "";
                                obj.ActiveStatus = myReader["ACTIVE_STATUS"] != DBNull.Value ? myReader["ACTIVE_STATUS"].ToString() : "";
                                obj.TargetPerDay = myReader["TargetPerDay"] != DBNull.Value ? Convert.ToInt32(myReader["TargetPerDay"]) : 0;
                                workTypeDatas.Add(obj);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in SP_GET_WORKTYPE_DATA_BY_ID_FOR_MASTER. ", ex);
                }
            }
            return workTypeDatas;
        }
        public InsertUpdateWorkTypeData insertUpdateWork(InsUpdateWorkRequest rqst)
        {
            InsertUpdateWorkTypeData res = new InsertUpdateWorkTypeData();

            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_INSERT_UPDATE_WORK_TYPE_FOR_MASTER", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        myCMD.Parameters.AddWithValue("@TASKID", rqst.taskId.HasValue ? rqst.taskId : (object)DBNull.Value);
                        myCMD.Parameters.AddWithValue("@WORKNAME", rqst.workName != null ? rqst.workName : (object)DBNull.Value);
                        myCMD.Parameters.AddWithValue("@WORKTYPEDESCRIPTION", rqst.workDescription != null ? rqst.workDescription : (object)DBNull.Value);
                        myCMD.Parameters.AddWithValue("@WORKVALUE", rqst.workValue.HasValue ? rqst.workValue : (object)DBNull.Value);
                        myCMD.Parameters.AddWithValue("@OPERATION_TYPE", rqst.operationType);
                        myCMD.Parameters.AddWithValue("@ACTIVESTATUS", rqst.ActiveStatus != null ? rqst.ActiveStatus : (object)DBNull.Value);
                        myCMD.Parameters.AddWithValue("@targetVal", rqst.Target.HasValue ? rqst.Target : (object)DBNull.Value);
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
                    throw new Exception("Error in SP_INSERT_UPDATE_WORK_TYPE_FOR_MASTER. ", ex);
                }
            }
            return res;
        }
    }
}
