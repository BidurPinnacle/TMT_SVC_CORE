using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using TMT_Code_Migration1.DataLogics.Utility;
using TMT_Code_Migration1.Models.Masters.Task_List;
using TMT_Code_Migration1.Views.Interfaces.Masters;

namespace TMT_Code_Migration1.DataLogics.Masters
{
    public class TaskListDL: ITaskList
    {
        private readonly CommonDl _commonDl;
        private readonly IConfiguration _config;
        public TaskListDL(CommonDl commonDl, [FromServices] IConfiguration config)
        {
            _commonDl = commonDl;
            _config = config;
        }
        public List<TaskListData> GetTaskListData()
        {
            List<TaskListData> taskListDatas = new List<TaskListData>();

            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_GET_TASK_LIST_DATA_FOR_MASTER", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                TaskListData obj = new TaskListData();
                                obj.TASKID = myReader["TASKID"] != DBNull.Value ? Convert.ToInt32(myReader["TASKID"]) : 0;
                                obj.TASKTEXT = myReader["TASKTEXT"] != DBNull.Value ? myReader["TASKTEXT"].ToString() : "";
                                obj.labelAs = myReader["labelAs"] != DBNull.Value ? myReader["labelAs"].ToString() : "";
                                obj.ACTIVE = myReader["ACTIVE"] != DBNull.Value ? myReader["ACTIVE"].ToString() : "";
                                obj.priority = myReader["proiority"] != DBNull.Value ? Convert.ToInt32(myReader["proiority"]) : 0;
                                obj.assignmentStatus = myReader["assignmentStatus"] != DBNull.Value ? myReader["assignmentStatus"].ToString() : "";
                                taskListDatas.Add(obj);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in SP_GET_TASK_LIST_DATA_FOR_MASTER. ", ex);
                }
            }
            return taskListDatas;
        }
        public List<TaskListData> GetTaskDataById(int tid)
        {
            List<TaskListData> taskListDatas = new List<TaskListData>();

            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_GET_TASK_LIST_DATA_BY_ID_FOR_MASTER", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        SqlParameter[] param = new SqlParameter[1];
                        param[0] = new SqlParameter("@TASKID", tid);
                        myCMD.Parameters.Add(param[0]);
                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                TaskListData obj = new TaskListData();
                                obj.TASKID = myReader["TASKID"] != DBNull.Value ? Convert.ToInt32(myReader["TASKID"]) : 0;
                                obj.TASKTEXT = myReader["TASKTEXT"] != DBNull.Value ? myReader["TASKTEXT"].ToString() : "";
                                obj.labelAs = myReader["labelAs"] != DBNull.Value ? myReader["labelAs"].ToString() : "";
                                obj.ACTIVE = myReader["ACTIVE"] != DBNull.Value ? myReader["ACTIVE"].ToString() : "";
                                obj.priority = myReader["proiority"] != DBNull.Value ? Convert.ToInt32(myReader["proiority"]) : 0;
                                obj.assignmentStatus = myReader["assignmentStatus"] != DBNull.Value ? myReader["assignmentStatus"].ToString() : "";
                                taskListDatas.Add(obj);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in SP_GET_TASK_LIST_DATA_FOR_MASTER. ", ex);
                }
            }
            return taskListDatas;
        }
        public InsertUpdateTaskListData insertUpdateTask(InsUpdateTaskRequest rqst)
        {
            InsertUpdateTaskListData res = new InsertUpdateTaskListData();

            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_INSERT_UPDATE_TASK_FOR_MASTER", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        myCMD.Parameters.AddWithValue("@TASKID", rqst.taskId.HasValue ? rqst.taskId : (object)DBNull.Value);
                        myCMD.Parameters.AddWithValue("@TASKTEXT", rqst.taskText != null ? rqst.taskText : (object)DBNull.Value);
                        myCMD.Parameters.AddWithValue("@STATUS", rqst.active != null ? rqst.active : (object)DBNull.Value);
                        myCMD.Parameters.AddWithValue("@PRIORITY", rqst.priority.HasValue ? rqst.priority : (object)DBNull.Value);
                        myCMD.Parameters.AddWithValue("@OPERATION_TYPE", rqst.operationType);
                        myCMD.Parameters.AddWithValue("@LABELS", rqst.labels != null ? rqst.labels : (object)DBNull.Value);
                        myCMD.Parameters.AddWithValue("@ASSIGNMENT", rqst.assignmentStatus != null ? rqst.assignmentStatus : (object)DBNull.Value);
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
                    throw new Exception("Error in SP_INSERT_UPDATE_TASK_FOR_MASTER. ", ex);
                }
            }
            return res;
        }
        public List<TasksListItems> GetTaskDataForWorkType()
        {
            List<TasksListItems> taskListDatas = new List<TasksListItems>();

            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_GET_ALL_TASKS_IN_WORKTYPE_FOR_MASTER", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                TasksListItems obj = new TasksListItems();
                                obj.taskId = myReader["TASKID"] != DBNull.Value ? Convert.ToInt32(myReader["TASKID"]) : 0;
                                obj.taskText = myReader["TASKTEXT"] != DBNull.Value ? myReader["TASKTEXT"].ToString() : "";
                                taskListDatas.Add(obj);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in SP_GET_ALL_TASKS_IN_WORKTYPE_FOR_MASTER. ", ex);
                }
            }
            return taskListDatas;
        }

        public List<AssignmentPageData> GetAssignmentPageData()
        {
            List<AssignmentPageData> assignmentPageDatas = new List<AssignmentPageData>();

            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_GET_ASSIGNMENT_PAGE_NAME", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                AssignmentPageData obj = new AssignmentPageData();
                                obj.AssignmentVal = myReader["assignmentVal"] != DBNull.Value ? myReader["assignmentVal"].ToString() : "";
                                obj.AssignmentText = myReader["assignmentText"] != DBNull.Value ? myReader["assignmentText"].ToString() : "";
                                assignmentPageDatas.Add(obj);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in SP_GET_ASSIGNMENT_PAGE_NAME. ", ex);
                }
            }
            return assignmentPageDatas;
        }
    }
}
