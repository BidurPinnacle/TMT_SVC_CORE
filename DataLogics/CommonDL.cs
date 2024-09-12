using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Data;
using TMT_Code_Migration1.DataLogics.Utility;
using Microsoft.AspNetCore.Mvc;
using TMT_Code_Migration1.Models.Common;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace TMT_Code_Migration1.DataLogics
{
    public class CommonDL
    {
        private readonly CommonDl _commonDl;
        private readonly IConfiguration _config;
        public CommonDL(CommonDl connonDl, [FromServices] IConfiguration config)
        {
            _commonDl = connonDl;
            _config = config;
        }
        public List<WorkType> GetWorkTypeDL(WorkTypeRequest typeRequest)
        {
            List<WorkType> works = new List<WorkType>();

            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_GetWorkType", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        myCMD.Parameters.AddWithValue("@TaskID", typeRequest.taskID);
                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                WorkType obj = new WorkType();
                                obj.worktypevalue = myReader.GetInt32(0);
                                obj.worktypename = myReader.GetString(1);
                                obj.labelAs = myReader.GetString(2);
                                works.Add(obj);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in SP_GetWorkType. ", ex);
                }
            }
            return works;
        }
        public List<TaskType> GetTaskTypeDLForExecutionTest()
        {
            List<TaskType> Tasks = new List<TaskType>();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("[SP_GETTASKLIST_FOR_EXECUTION_TEST]", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                TaskType obj = new TaskType();
                                obj.TASKID = myReader.GetInt32(0);
                                obj.TASKTEXT = myReader.GetString(1);
                                Tasks.Add(obj);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in Cards. ", ex);
                }
            }
            return Tasks;
        }
        public List<TaskType> GetTaskTypeDLForScriptingTest()
        {
            List<TaskType> Tasks = new List<TaskType>();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("[SP_GETTASKLIST_FOR_SCRIPTING_TEST]", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                TaskType obj = new TaskType();
                                obj.TASKID = myReader.GetInt32(0);
                                obj.TASKTEXT = myReader.GetString(1);
                                Tasks.Add(obj);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in Cards. ", ex);
                }
            }
            return Tasks;
        }
        public List<RootCause> GetRootCauseDL(rootCauseRequest rqst)
        {
            List<RootCause> rootCauses = new List<RootCause>();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_TASK_CAUSE_MAPPING", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        myCMD.Parameters.AddWithValue("@TASK_ID", rqst.taskId);
                        myCMD.Parameters.AddWithValue("@WORKTYPE_ID", rqst.workTypeId);
                        myCMD.Parameters.AddWithValue("@STATUS_ID", rqst.statusId);
                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                RootCause obj = new RootCause();
                                obj.causeId = myReader.GetInt32(0);
                                obj.rootCause = myReader.GetString(1);
                                rootCauses.Add(obj);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in Cards. ", ex);
                }
            }
            return rootCauses;
        }
        public List<TaskType> GetTaskTypeDL()
        {
            List<TaskType> Tasks = new List<TaskType>();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("[SP_GETTASKLIST]", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                TaskType obj = new TaskType();
                                obj.TASKID = myReader.GetInt32(0);
                                obj.TASKTEXT = myReader.GetString(1);
                                Tasks.Add(obj);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in Cards. ", ex);
                }
            }
            return Tasks;
        }
        public List<CaseStatusType> GetCaseStatusTypeDL(int assignid)
        {
            List<CaseStatusType> Tasks = new List<CaseStatusType>();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_GET_CASE_STATUS", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        myCMD.Parameters.AddWithValue("@ASSIGNID", assignid);
                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                CaseStatusType obj = new CaseStatusType();
                                obj.STATUSID = myReader.GetInt32(0);
                                obj.STATUSNAME = myReader.GetString(1);
                                Tasks.Add(obj);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in Cards. ", ex);
                }
            }
            return Tasks;
        }
        public List<BusinessType> GetBusinessUnitDL()
        {
            List<BusinessType> Tasks = new List<BusinessType>();
            string SP_GET_BUSINESS_UNIT = _config.GetSection("storedProcedures")["SP_GET_BUSINESS_UNIT"];
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand(SP_GET_BUSINESS_UNIT, conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                BusinessType obj = new BusinessType();
                                obj.BID = myReader.GetInt32(0);
                                obj.BUSINESS_UNIT = myReader.GetString(1);
                                Tasks.Add(obj);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in Cards. ", ex);
                }
            }
            return Tasks;
        }
        public List<BatcheListResponse> GetBatchDL(BatchListRequest Batchstatus)
        {
            List<BatcheListResponse> works = new List<BatcheListResponse>();

            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_BATCH_LIST_FILTER", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        myCMD.Parameters.AddWithValue("@batchstatus", Batchstatus.BatchStatus);
                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                BatcheListResponse obj = new BatcheListResponse();
                                obj.BatchID = myReader.GetInt32(0);
                                obj.BatchName = myReader.GetString(1);
                                works.Add(obj);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in GetBatchDL. ", ex);
                }
            }
            return works;
        }
        public List<workYears> GetWorkYearsDL()
        {
            List<workYears> years = new List<workYears>();

            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("[SP_GETWORKYEARS]", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                workYears obj = new workYears();
                                obj.workyear = Convert.ToInt32(myReader["WorkYear"].ToString() == null ? 0 : myReader["WorkYear"]);
                                obj.yearstatus = Convert.ToInt32(myReader["YearStatus"].ToString() == null ? 0 : myReader["YearStatus"]);
                                years.Add(obj);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in year SP Cards. ", ex);
                }
            }
            return years;
        }
        public List<Role> GetRoleListDL()
        {
            List<Role> roles = new List<Role>();

            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("sp_GetRolerank", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                Role obj = new Role();
                                obj.Roleid = myReader.GetInt32(0);
                                obj.RoleName = myReader.GetString(1);
                                roles.Add(obj);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in Roles Dl. ", ex);
                }
            }
            return roles;
        }
        public List<Company> GetCompanyDL()
        {
            List<Company> companies = new List<Company>();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("sp_GetCompany", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            do
                            {
                                while (myReader.Read())
                                {
                                    Company obj = new Company();
                                    obj.CId = myReader.GetInt32(0);
                                    obj.CName = myReader.GetString(1);
                                    obj.CDescription = myReader.GetString(2);
                                    companies.Add(obj);
                                }
                            }
                            while (myReader.NextResult());
                        }
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception("Error in Company. ", ex);
                }
            }
            return companies;
        }
        public List<DashboardResponse> GetDashboard(DashboardRequest request)
        {
            List<DashboardResponse> cards = new List<DashboardResponse>();
            string query = string.Empty;
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SpReports", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ROLE", request.role);
                    cmd.Parameters.AddWithValue("@ProfileID", request.ProfileID);


                    using (SqlDataReader myReader = cmd.ExecuteReader())
                    {
                        while (myReader.Read())
                        {
                            DashboardResponse obj = new DashboardResponse();
                            obj.CountedCard = myReader["CountedCard"].ToString();
                            if (myReader["Editor"].ToString() != "" && myReader["Editor"] != null && string.IsNullOrEmpty(myReader["Editor"].ToString()))
                                obj.Editor = myReader["Editor"].ToString();

                            if (myReader["QCby"] != null)
                                obj.QCby = myReader["QCby"].ToString();
                            obj.status = myReader["card_status"].ToString();
                            obj.Description = myReader["card_Description"].ToString();
                            cards.Add(obj);
                        }
                    }
                    if (conn.State == ConnectionState.Open)
                        conn.Close();

                }
                catch (Exception ex)
                {
                    throw new Exception("Error in GetDashboard. ", ex);
                }
            }
            return cards;

        }
        public List<Issue> GetIssue()
        {
            List<Issue> issues = new List<Issue>();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_GETISSUE", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            do
                            {
                                while (myReader.Read())
                                {
                                    Issue obj = new Issue();
                                    obj.ISSUENAME = myReader["ISSUENAME"].ToString();
                                    obj.ISSUEID = (int)myReader["ISSUEID"];
                                    issues.Add(obj);
                                }
                            }
                            while (myReader.NextResult());
                        }
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception("Error in issuedl. ", ex);
                }
            }
            return issues;
        }
        public List<CardStatus> GetStatus(CardStatus_request request)
        {
            List<CardStatus> statuses = new List<CardStatus>();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {


                    using (SqlCommand myCMD = new SqlCommand("SP_GETSTATUS", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        myCMD.Parameters.AddWithValue("@value", request.CardStatus_value);

                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            do
                            {
                                while (myReader.Read())
                                {
                                    CardStatus obj = new CardStatus();
                                    obj.Card_status = myReader.GetString(0);
                                    obj.card_Description = myReader.GetString(1);
                                    statuses.Add(obj);
                                }
                            }
                            while (myReader.NextResult());
                        }
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception("Error in statusDL. ", ex);
                }
            }
            return statuses;
        }
        public List<InvoiceListResponse> GetInvoicelist()
        {
            List<InvoiceListResponse> invoiceLists = new List<InvoiceListResponse>();

            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_INVOICE_LIST", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                InvoiceListResponse obj = new InvoiceListResponse();
                                obj.INVOICE_NUMBER = myReader["INVOICE_NUMBER"].ToString();
                                obj.INVOICE_ID = (int)myReader["INVOICEID"];
                                invoiceLists.Add(obj);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in GetInvoicelist. ", ex);
                }
            }
            return invoiceLists;
        }
        public List<ErrorListResponse> GetErrorlist()
        {
            List<ErrorListResponse> ErrorDataList = new List<ErrorListResponse>();

            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_GET_ERROR_ALL", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                ErrorListResponse obj = new ErrorListResponse();
                                obj.ErrorID = (int)myReader["error_id"];
                                obj.ErrorName = myReader["error_name"].ToString();
                                ErrorDataList.Add(obj);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in GetErrorlist. ", ex);
                }
            }
            return ErrorDataList;
        }
        public List<Notification_Response> CRU_Notification(Notification_Request _Request)
        {
            List<Notification_Response> Notifications = new List<Notification_Response>();

            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_CRU_NOTIFICATION", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        myCMD.Parameters.AddWithValue("@USERID", _Request.UserID);
                        myCMD.Parameters.AddWithValue("@NID", _Request.Nid);
                        myCMD.Parameters.AddWithValue("@STATEMENTTYPE", _Request.Statement);



                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                Notification_Response obj = new Notification_Response();
                                obj.Premise = myReader["Premise"].ToString();
                                obj.SERV_ORDER_NO = myReader["SERV_ORDER_NO"].ToString();
                                obj.card_address = myReader["card_address"].ToString();
                                obj.ETYPENAME = myReader["ETYPENAME"].ToString();
                                obj.ESUBTYPENAME = myReader["ESUBTYPENAME"].ToString();
                                obj.NOTIFICATION_TEXT = myReader["NOTIFICATION_TEXT"].ToString();
                                obj.NOTIFICATION_SEEN = myReader["NOTIFICATION_SEEN"].ToString();
                                obj.NOTICE_SEND_BY = myReader["NOTICE_SEND_BY"].ToString();
                                obj.CREATED_DATE = myReader["CREATED_DATE"].ToString();
                                obj.Nid = (int)myReader["NID"];

                                Notifications.Add(obj);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in CRU_Notification. ", ex);
                }
            }
            return Notifications;
        }
        public Notification_count_Response Notification_Count(Notification_count_Request request)
        {
            Notification_count_Response notification_Count_ = new Notification_count_Response();

            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_NOTIFICATION_COUNT", conn))
                    {

                        myCMD.CommandType = CommandType.StoredProcedure;
                        myCMD.Parameters.AddWithValue("@USERID", request.userid);

                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                notification_Count_.Notification_count = (int)myReader["NOTIFICATION_COUNT"];
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in Notification_Count. ", ex);
                }
            }
            return notification_Count_;
        }
        
    }
}
