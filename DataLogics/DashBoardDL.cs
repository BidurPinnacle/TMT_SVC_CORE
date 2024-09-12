using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using TMT_Code_Migration1.DataLogics.Utility;
using TMT_Code_Migration1.Models.Common;

namespace TMT_Code_Migration1.DataLogics
{
    public class DashBoardDL
    {
        private readonly CommonDl _commonDl;
        private readonly IConfiguration _config;
        private readonly CardsDL _cardsDl;
        public DashBoardDL(CommonDl connonDl, [FromServices] IConfiguration config, CardsDL cardsDl)
        {
            _commonDl = connonDl;
            _config = config;
            _cardsDl = cardsDl;
        }
        public DashboardResponse_All Dashboard_Data(DashboardRequest request)
        {
            DashboardResponse_All dashboardResponse_ = new DashboardResponse_All();
            string query = string.Empty;
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_DASHBOAD_DATA", conn);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ROLE", request.role);
                    cmd.Parameters.AddWithValue("@ProfileID", request.ProfileID);
                    cmd.Parameters.AddWithValue("@FROMDATE", request.FROMDATE);
                    cmd.Parameters.AddWithValue("@TODATE", request.TODATE);

                    cmd.CommandType = CommandType.StoredProcedure;
                    DataSet dsdata = new DataSet();
                    SqlDataAdapter sdadata = new SqlDataAdapter(cmd);
                    sdadata.Fill(dsdata);
                    foreach (DataTable table in dsdata.Tables)
                    {
                        foreach (DataRow myReader in table.Rows)
                        {

                            ////        using (SqlDataReader myReader = cmd.ExecuteReader())
                            //{
                            //    while (myReader.Read())
                            //    {

                            dashboardResponse_.total_card_process = (int)myReader["total_card_process"];
                            dashboardResponse_.total_qa_completed = (int)myReader["total_qa_completed"];
                            dashboardResponse_.total_card_draw_complete = (int)myReader["total_card_draw_complete"];
                            dashboardResponse_.total_card_process_lastweek = (int)myReader["total_card_process_lastweek"];
                            dashboardResponse_.total_qa_completed_lastweek = (int)myReader["total_qa_completed_lastweek"];
                            dashboardResponse_.total_card_draw_complete_lastweek = (int)myReader["total_card_draw_complete_lastweek"];
                            dashboardResponse_.total_issue_card = (int)myReader["total_issue_card"];
                            dashboardResponse_.total_issue_card_lastweek = (int)myReader["total_issue_card_lastweek"];
                            dashboardResponse_.Estimated_last_day_of_work = Convert.ToDateTime(myReader["Estimated_last_day_of_work"]);
                            dashboardResponse_.TOTALCOMPLETE_PERCENT = (Double)(myReader["ACHIVED_PERCENT"]);
                            dashboardResponse_.TOTALISSUE_PERCENT = (Double)(myReader["ISSUE_PERCENT"]);
                            dashboardResponse_.TOTALPENDING_PERCENT = (Double)(myReader["PENDING_PERCENT"]);
                            dashboardResponse_.RESOLVEDISSUE = (Double)(myReader["RESOLVEDISSUE"]);
                            dashboardResponse_.PENDINGISSUE = (Double)(myReader["PENDINGISSUE"]);
                            dashboardResponse_.TARGETPERWEEK = (Double)(myReader["TARGETPERWEEK"]);
                            if (myReader["TARGETCOMPLETIONDATE"] != null)
                                dashboardResponse_.Taget_completion_date = Convert.ToDateTime(myReader["TARGETCOMPLETIONDATE"]);


                            dashboardResponse_.TARGETPERWEEK = (Double)(myReader["TARGETPERWEEK"]);
                            dashboardResponse_.TOTAL_PROCESSED = (int)myReader["TOTAL_PROCESSED"];

                            dashboardResponse_.TOTAL_DRAWN = (int)myReader["TOTAL_DRAWN"];
                            dashboardResponse_.QA_INPROGRESS_D = (int)myReader["QA_INPROGRESS_D"];
                            dashboardResponse_.QA_COMPLETED_D = (int)myReader["QA_COMPLETED_D"];

                            dashboardResponse_.NOT_DRAWABLE = (int)myReader["NOT_DRAWABLE"];
                            dashboardResponse_.QA_INPROGRESS_ND = (int)myReader["QA_INPROGRESS_ND"];
                            dashboardResponse_.QA_COMPLETED_ND = (int)myReader["QA_COMPLETED_ND"];

                            dashboardResponse_.TOTAL_REVIEWED = (int)myReader["TOTAL_REVIEWED"];
                            dashboardResponse_.QA_COMPLETED_REVIEWED = (int)myReader["QA_COMPLETED_REVIEWED"];
                            dashboardResponse_.QA_INPROGRESS_REVIEWED = (int)myReader["QA_INPROGRESS_REVIEWED"];


                            dashboardResponse_.TOTAL_PROCESSED_WEEKLY = (int)myReader["TOTAL_PROCESSED_WEEKLY"];

                            dashboardResponse_.TOTAL_DRAWN_WEEKLY = (int)myReader["TOTAL_DRAWN_WEEKLY"];
                            dashboardResponse_.QA_INPROGRESS_D_WEEKLY = (int)myReader["QA_INPROGRESS_D_WEEKLY"];
                            dashboardResponse_.QA_COMPLETED_D_WEEKLY = (int)myReader["QA_COMPLETED_D_WEEKLY"];

                            dashboardResponse_.NOT_DRAWABLE_WEEKLY = (int)myReader["NOT_DRAWABLE_WEEKLY"];
                            dashboardResponse_.QA_COMPLETED_ND_WEEKLY = (int)myReader["QA_COMPLETED_ND_WEEKLY"];
                            dashboardResponse_.QA_INPROGRESS_ND_WEEKLY = (int)myReader["QA_INPROGRESS_ND_WEEKLY"];

                            dashboardResponse_.TOTAL_REVIEWED_WEEKLY = (int)myReader["TOTAL_REVIEWED_WEEKLY"];
                            dashboardResponse_.QA_COMPLETED_REVIEWED_WEEKLY = (int)myReader["QA_COMPLETED_REVIEWED_WEEKLY"];
                            dashboardResponse_.QA_INPROGRESS_REVIEWED_WEEKLY = (int)myReader["QA_INPROGRESS_REVIEWED_WEEKLY"];

                        }
                    }
                    if (conn.State == ConnectionState.Open)
                        conn.Close();

                }
                catch (Exception ex)
                {
                    throw new Exception("Error in Dashboard_Data. ", ex);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return dashboardResponse_;

        }
        public List<DashboardBatchReports_Data_respose> Dashboard_Batch_Data(DashboardBatchReports_Data_Request _Data_Request)
        {
            List<DashboardBatchReports_Data_respose> dashboardBatches = new List<DashboardBatchReports_Data_respose>();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_GET_BATCHDETAILS_DASHBOARD", conn);
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@PROFILEID", _Data_Request.PROFILEID);
                        cmd.Parameters.AddWithValue("@FROMDATE", _Data_Request.FROMDATE);
                        cmd.Parameters.AddWithValue("@TODATE", _Data_Request.TODATE);
                        using (SqlDataReader myReader = cmd.ExecuteReader())
                        {
                            do
                            {
                                while (myReader.Read())
                                {
                                    DashboardBatchReports_Data_respose obj = new DashboardBatchReports_Data_respose();
                                    obj.STATUS = myReader["CARD_DESCRIPTION"].ToString();
                                    obj.CARDSCOUNT = (int)myReader["TOTAL_COUNT"];
                                    dashboardBatches.Add(obj);
                                }
                            }
                            while (myReader.NextResult());
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in Dashboard_Batch_Data. ", ex);
                }
            }
            return dashboardBatches;
        }
        public List<DashboardIssueReports_Data> Dashboard_Issue_Data(DashboardIssueReports_Data_Request _Data_Request)
        {
            List<DashboardIssueReports_Data> issues = new List<DashboardIssueReports_Data>();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_GET_ISSUEDETAILS", conn);
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@FROMDATE", _Data_Request.FROMDATE);
                        cmd.Parameters.AddWithValue("@TODATE", _Data_Request.TODATE);
                        cmd.Parameters.AddWithValue("@PROFILEID", _Data_Request.PROFILEID);
                        using (SqlDataReader myReader = cmd.ExecuteReader())
                        {
                            do
                            {
                                while (myReader.Read())
                                {
                                    DashboardIssueReports_Data obj = new DashboardIssueReports_Data();
                                    obj.CARDSCOUNT = (int)myReader["CARDSCOUNT"];
                                    obj.ISSUENAME = myReader["ISSUENAME"].ToString();
                                    obj.ISSUEID = (int)myReader["CATEGORY_ID"];
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
        public List<Dashboardresponse_Progressive> DASHBOARD_PROGRESSIVE_CHART_DATA(DashboardRequest_progressive request)
        {
            List<Dashboardresponse_Progressive> dashboardResponse_ = new List<Dashboardresponse_Progressive>();

            string query = string.Empty;
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_DASHBOARD_PROGRESSIVE_CHART_DATA", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ROLE", request.role);
                    cmd.Parameters.AddWithValue("@ProfileID", request.ProfileID);
                    cmd.Parameters.AddWithValue("@REPORT", request.Chartreport);
                    cmd.Parameters.AddWithValue("@FROMDATE", request.datefrom);
                    cmd.Parameters.AddWithValue("@TODATE", request.dateto);



                    using (SqlDataReader myReader = cmd.ExecuteReader())
                    {
                        while (myReader.Read())
                        {

                            Dashboardresponse_Progressive dashboardresponse_Progressive = new Dashboardresponse_Progressive();

                            if (request.Chartreport.ToUpper() == "YEAR_WISE")
                            {
                                if (!myReader.IsDBNull(0))
                                {
                                    dashboardresponse_Progressive.ENTRYYEAR = Convert.ToInt32(myReader[0]);
                                }
                                if (!myReader.IsDBNull(1))
                                {
                                    dashboardresponse_Progressive.TOTALCOUNT = Convert.ToInt32(myReader[1]);
                                }

                                if (!myReader.IsDBNull(2))
                                {
                                    dashboardresponse_Progressive.TOTALCOMPLETED = Convert.ToInt32(myReader[2]);
                                }
                                if (!myReader.IsDBNull(3))
                                {
                                    dashboardresponse_Progressive.TOTALPENDING = Convert.ToInt32(myReader[3]);
                                }
                            }

                            if (request.Chartreport.ToUpper() == "WEEK_WISE")
                            {
                                if (!myReader.IsDBNull(0))
                                {
                                    dashboardresponse_Progressive.TARGETPERWEEK = Convert.ToInt32(myReader[0]);
                                }
                                if (!myReader.IsDBNull(1))
                                {
                                    dashboardresponse_Progressive.LASTWEEKCOMPLETED = Convert.ToInt32(myReader[1]);
                                }
                                if (!myReader.IsDBNull(2))
                                {
                                    dashboardresponse_Progressive.WEEKLYCOMPLETE_PERCENT = Convert.ToInt32(myReader[2]);
                                }
                            }
                            dashboardResponse_.Add(dashboardresponse_Progressive);



                        }
                    }
                    if (conn.State == ConnectionState.Open)
                        conn.Close();

                }
                catch (Exception ex)
                {
                    throw new Exception("Error in DASHBOARD_PROGRESSIVE_CHART_DATA. ", ex);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return dashboardResponse_;

        }
        public List<Dashboardresponse_Weekcount> LAST4WEEKDATA(DashboardRequest_progressive request)
        {
            List<Dashboardresponse_Weekcount> dashboardResponse_ = new List<Dashboardresponse_Weekcount>();

            string query = string.Empty;
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_LAST4WEEKDATA", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ROLE", request.role);
                    cmd.Parameters.AddWithValue("@ProfileID", request.ProfileID);
                    cmd.Parameters.AddWithValue("@FROMDATE", request.datefrom);
                    cmd.Parameters.AddWithValue("@TODATE", request.dateto);


                    using (SqlDataReader myReader = cmd.ExecuteReader())
                    {
                        while (myReader.Read())
                        {

                            Dashboardresponse_Weekcount LAST4WEEKDATA = new Dashboardresponse_Weekcount();

                            if (!myReader.IsDBNull(0) && !myReader.IsDBNull(1))
                            {
                                LAST4WEEKDATA.week = Convert.ToInt16(myReader["WEEKCOUNT"].ToString());
                                LAST4WEEKDATA.data = Convert.ToInt16(myReader["WEEKDATA"].ToString());
                                dashboardResponse_.Add(LAST4WEEKDATA);
                            }
                        }
                    }
                    if (conn.State == ConnectionState.Open)
                        conn.Close();

                }
                catch (Exception ex)
                {
                    throw new Exception("Error in Dashboard_Data. ", ex);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return dashboardResponse_;

        }
        public DashboardResponse_All Dashboard_Data_Hist(DashboardRequest request)
        {
            DashboardResponse_All dashboardResponse_ = new DashboardResponse_All();
            string query = string.Empty;
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_DASHBOAD_DATA_HIST", conn);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ROLE", request.role);
                    cmd.Parameters.AddWithValue("@ProfileID", request.ProfileID);
                    cmd.Parameters.AddWithValue("@FROMDATE", request.FROMDATE);
                    cmd.Parameters.AddWithValue("@TODATE", request.TODATE);

                    cmd.CommandType = CommandType.StoredProcedure;
                    DataSet dsdata = new DataSet();
                    SqlDataAdapter sdadata = new SqlDataAdapter(cmd);
                    sdadata.Fill(dsdata);
                    foreach (DataTable table in dsdata.Tables)
                    {
                        foreach (DataRow myReader in table.Rows)
                        {

                            dashboardResponse_.total_card_process = (int)myReader["total_card_process"];
                            dashboardResponse_.total_qa_completed = (int)myReader["total_qa_completed"];
                            dashboardResponse_.total_card_draw_complete = (int)myReader["total_card_draw_complete"];
                            dashboardResponse_.total_card_process_lastweek = (int)myReader["total_card_process_lastweek"];
                            dashboardResponse_.total_qa_completed_lastweek = (int)myReader["total_qa_completed_lastweek"];
                            dashboardResponse_.total_card_draw_complete_lastweek = (int)myReader["total_card_draw_complete_lastweek"];
                            dashboardResponse_.total_issue_card = (int)myReader["total_issue_card"];
                            dashboardResponse_.total_issue_card_lastweek = (int)myReader["total_issue_card_lastweek"];
                            dashboardResponse_.Estimated_last_day_of_work = Convert.ToDateTime(myReader["Estimated_last_day_of_work"]);
                            dashboardResponse_.TOTALCOMPLETE_PERCENT = (Double)(myReader["ACHIVED_PERCENT"]);
                            dashboardResponse_.TOTALISSUE_PERCENT = (Double)(myReader["ISSUE_PERCENT"]);
                            dashboardResponse_.TOTALPENDING_PERCENT = (Double)(myReader["PENDING_PERCENT"]);
                            dashboardResponse_.RESOLVEDISSUE = (Double)(myReader["RESOLVEDISSUE"]);
                            dashboardResponse_.PENDINGISSUE = (Double)(myReader["PENDINGISSUE"]);
                            dashboardResponse_.TARGETPERWEEK = (Double)(myReader["TARGETPERWEEK"]);
                            if (myReader["TARGETCOMPLETIONDATE"] != null)
                                dashboardResponse_.Taget_completion_date = Convert.ToDateTime(myReader["TARGETCOMPLETIONDATE"]);


                            dashboardResponse_.TARGETPERWEEK = (Double)(myReader["TARGETPERWEEK"]);
                            dashboardResponse_.TOTAL_PROCESSED = (int)myReader["TOTAL_PROCESSED"];

                            dashboardResponse_.TOTAL_DRAWN = (int)myReader["TOTAL_DRAWN"];
                            dashboardResponse_.QA_INPROGRESS_D = (int)myReader["QA_INPROGRESS_D"];
                            dashboardResponse_.QA_COMPLETED_D = (int)myReader["QA_COMPLETED_D"];

                            dashboardResponse_.NOT_DRAWABLE = (int)myReader["NOT_DRAWABLE"];
                            dashboardResponse_.QA_INPROGRESS_ND = (int)myReader["QA_INPROGRESS_ND"];
                            dashboardResponse_.QA_COMPLETED_ND = (int)myReader["QA_COMPLETED_ND"];

                            dashboardResponse_.TOTAL_REVIEWED = (int)myReader["TOTAL_REVIEWED"];
                            dashboardResponse_.QA_COMPLETED_REVIEWED = (int)myReader["QA_COMPLETED_REVIEWED"];
                            dashboardResponse_.QA_INPROGRESS_REVIEWED = (int)myReader["QA_INPROGRESS_REVIEWED"];


                            dashboardResponse_.TOTAL_PROCESSED_WEEKLY = (int)myReader["TOTAL_PROCESSED_WEEKLY"];

                            dashboardResponse_.TOTAL_DRAWN_WEEKLY = (int)myReader["TOTAL_DRAWN_WEEKLY"];
                            dashboardResponse_.QA_INPROGRESS_D_WEEKLY = (int)myReader["QA_INPROGRESS_D_WEEKLY"];
                            dashboardResponse_.QA_COMPLETED_D_WEEKLY = (int)myReader["QA_COMPLETED_D_WEEKLY"];

                            dashboardResponse_.NOT_DRAWABLE_WEEKLY = (int)myReader["NOT_DRAWABLE_WEEKLY"];
                            dashboardResponse_.QA_COMPLETED_ND_WEEKLY = (int)myReader["QA_COMPLETED_ND_WEEKLY"];
                            dashboardResponse_.QA_INPROGRESS_ND_WEEKLY = (int)myReader["QA_INPROGRESS_ND_WEEKLY"];

                            dashboardResponse_.TOTAL_REVIEWED_WEEKLY = (int)myReader["TOTAL_REVIEWED_WEEKLY"];
                            dashboardResponse_.QA_COMPLETED_REVIEWED_WEEKLY = (int)myReader["QA_COMPLETED_REVIEWED_WEEKLY"];
                            dashboardResponse_.QA_INPROGRESS_REVIEWED_WEEKLY = (int)myReader["QA_INPROGRESS_REVIEWED_WEEKLY"];

                        }
                    }
                    if (conn.State == ConnectionState.Open)
                        conn.Close();

                }
                catch (Exception ex)
                {
                    throw new Exception("Error in Dashboard_Data. ", ex);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return dashboardResponse_;

        }
        public List<DashboardBatchReports_Data_respose> Dashboard_Batch_Data_Hist(DashboardBatchReports_Data_Request _Data_Request)
        {
            List<DashboardBatchReports_Data_respose> dashboardBatches = new List<DashboardBatchReports_Data_respose>();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_GET_BATCHDETAILS_DASHBOARD_HIST", conn);
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@PROFILEID", _Data_Request.PROFILEID);
                        cmd.Parameters.AddWithValue("@FROMDATE", _Data_Request.FROMDATE);
                        cmd.Parameters.AddWithValue("@TODATE", _Data_Request.TODATE);
                        using (SqlDataReader myReader = cmd.ExecuteReader())
                        {
                            do
                            {
                                while (myReader.Read())
                                {
                                    DashboardBatchReports_Data_respose obj = new DashboardBatchReports_Data_respose();
                                    obj.STATUS = myReader["CARD_DESCRIPTION"].ToString();
                                    obj.CARDSCOUNT = (int)myReader["TOTAL_COUNT"];
                                    dashboardBatches.Add(obj);
                                }
                            }
                            while (myReader.NextResult());
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in Dashboard_Batch_Data. ", ex);
                }
            }
            return dashboardBatches;
        }
        public List<DashboardIssueReports_Data> Dashboard_Issue_Data_HIST(DashboardIssueReports_Data_Request _Data_Request)
        {
            List<DashboardIssueReports_Data> issues = new List<DashboardIssueReports_Data>();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_GET_ISSUEDETAILS_HIST", conn);
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@FROMDATE", _Data_Request.FROMDATE);
                        cmd.Parameters.AddWithValue("@TODATE", _Data_Request.TODATE);
                        cmd.Parameters.AddWithValue("@PROFILEID", _Data_Request.PROFILEID);
                        using (SqlDataReader myReader = cmd.ExecuteReader())
                        {
                            do
                            {
                                while (myReader.Read())
                                {
                                    DashboardIssueReports_Data obj = new DashboardIssueReports_Data();
                                    obj.CARDSCOUNT = (int)myReader["CARDSCOUNT"];
                                    obj.ISSUENAME = myReader["ISSUENAME"].ToString();
                                    obj.ISSUEID = (int)myReader["CATEGORY_ID"];
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
        public List<Dashboardresponse_Progressive> DASHBOARD_PROGRESSIVE_CHART_DATA_HIST(DashboardRequest_progressive request)
        {
            List<Dashboardresponse_Progressive> dashboardResponse_ = new List<Dashboardresponse_Progressive>();

            string query = string.Empty;
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_DASHBOARD_PROGRESSIVE_CHART_DATA_HIST", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ROLE", request.role);
                    cmd.Parameters.AddWithValue("@ProfileID", request.ProfileID);
                    cmd.Parameters.AddWithValue("@REPORT", request.Chartreport);
                    cmd.Parameters.AddWithValue("@FROMDATE", request.datefrom);
                    cmd.Parameters.AddWithValue("@TODATE", request.dateto);



                    using (SqlDataReader myReader = cmd.ExecuteReader())
                    {
                        while (myReader.Read())
                        {

                            Dashboardresponse_Progressive dashboardresponse_Progressive = new Dashboardresponse_Progressive();

                            if (request.Chartreport.ToUpper() == "YEAR_WISE")
                            {
                                if (!myReader.IsDBNull(0))
                                {
                                    dashboardresponse_Progressive.ENTRYYEAR = Convert.ToInt32(myReader[0]);
                                }
                                if (!myReader.IsDBNull(1))
                                {
                                    dashboardresponse_Progressive.TOTALCOUNT = Convert.ToInt32(myReader[1]);
                                }

                                if (!myReader.IsDBNull(2))
                                {
                                    dashboardresponse_Progressive.TOTALCOMPLETED = Convert.ToInt32(myReader[2]);
                                }
                                if (!myReader.IsDBNull(3))
                                {
                                    dashboardresponse_Progressive.TOTALPENDING = Convert.ToInt32(myReader[3]);
                                }
                            }

                            if (request.Chartreport.ToUpper() == "WEEK_WISE")
                            {
                                if (!myReader.IsDBNull(0))
                                {
                                    dashboardresponse_Progressive.TARGETPERWEEK = Convert.ToInt32(myReader[0]);
                                }
                                if (!myReader.IsDBNull(1))
                                {
                                    dashboardresponse_Progressive.LASTWEEKCOMPLETED = Convert.ToInt32(myReader[1]);
                                }
                                if (!myReader.IsDBNull(2))
                                {
                                    dashboardresponse_Progressive.WEEKLYCOMPLETE_PERCENT = Convert.ToInt32(myReader[2]);
                                }
                            }
                            dashboardResponse_.Add(dashboardresponse_Progressive);



                        }
                    }
                    if (conn.State == ConnectionState.Open)
                        conn.Close();

                }
                catch (Exception ex)
                {
                    throw new Exception("Error in DASHBOARD_PROGRESSIVE_CHART_DATA. ", ex);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return dashboardResponse_;

        }
        public List<Dashboardresponse_Weekcount> LAST4WEEKDATA_HIST(DashboardRequest_progressive request)
        {
            List<Dashboardresponse_Weekcount> dashboardResponse_ = new List<Dashboardresponse_Weekcount>();

            string query = string.Empty;
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_LAST4WEEKDATA_HIST", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ROLE", request.role);
                    cmd.Parameters.AddWithValue("@ProfileID", request.ProfileID);
                    cmd.Parameters.AddWithValue("@FROMDATE", request.datefrom);
                    cmd.Parameters.AddWithValue("@TODATE", request.dateto);


                    using (SqlDataReader myReader = cmd.ExecuteReader())
                    {
                        while (myReader.Read())
                        {

                            Dashboardresponse_Weekcount LAST4WEEKDATA = new Dashboardresponse_Weekcount();

                            if (!myReader.IsDBNull(0) && !myReader.IsDBNull(1))
                            {
                                LAST4WEEKDATA.week = Convert.ToInt16(myReader["WEEKCOUNT"].ToString());
                                LAST4WEEKDATA.data = Convert.ToInt16(myReader["WEEKDATA"].ToString());
                                dashboardResponse_.Add(LAST4WEEKDATA);
                            }
                        }
                    }
                    if (conn.State == ConnectionState.Open)
                        conn.Close();

                }
                catch (Exception ex)
                {
                    throw new Exception("Error in Dashboard_Data. ", ex);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return dashboardResponse_;

        }
        public TASKSUMMARYCOUNT_RESPONSE TASKSUMMARYCOUNT()
        {

            TASKSUMMARYCOUNT_RESPONSE _RESPONSE = new TASKSUMMARYCOUNT_RESPONSE();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_CASECOUNT_TASK", conn);
                    {


                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader myReader = cmd.ExecuteReader())
                        {


                            _RESPONSE._DAY_RESPONSEs = _cardsDl.DataReaderMapToList<TASKSUMMARYCOUNT_DAY_RESPONSE>(myReader);
                            myReader.NextResult();
                            _RESPONSE._WEEK_RESPONSEs = _cardsDl.DataReaderMapToList<TASKSUMMARYCOUNT_WEEK_RESPONSE>(myReader);
                            myReader.NextResult();
                            _RESPONSE._WEEK1_RESPONSEs = _cardsDl.DataReaderMapToList<TASKSUMMARYCOUNT_WEEK1_RESPONSE>(myReader);
                            myReader.NextResult();
                            _RESPONSE._TOTAL_RESPONSEs = _cardsDl.DataReaderMapToList<TASKSUMMARYCOUNT_TOTAL_RESPONSE>(myReader);
                            myReader.NextResult();
                            _RESPONSE._HEADER_RESPONSEs_DAY = _cardsDl.DataReaderMapToList<TASKSUMMARYCOUNT_HEADER_DAY>(myReader);
                            myReader.NextResult();
                            _RESPONSE._HEADER_RESPONSEs_WEEK = _cardsDl.DataReaderMapToList<TASKSUMMARYCOUNT_HEADER_WEEK>(myReader);
                            myReader.NextResult();
                            _RESPONSE._HEADER_RESPONSEs_WEEK1 = _cardsDl.DataReaderMapToList<TASKSUMMARYCOUNT_HEADER_WEEK1>(myReader);
                            myReader.NextResult();
                            _RESPONSE._HEADER_RESPONSEs_TOTAL = _cardsDl.DataReaderMapToList<TASKSUMMARYCOUNT_HEADER_TOTAL>(myReader);



                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in TASKSUMMARYCOUNT. ", ex);
                }
            }
            return _RESPONSE;
        }
        public List<WORKTYPESUMMARYCOUNT_RESPONSE> WORKTYPESUMMARYCOUNT(WORKTYPESUMMARYCOUNT_RESQUEST rESQUEST)
        {
            List<WORKTYPESUMMARYCOUNT_RESPONSE> rESPONSEs = new List<WORKTYPESUMMARYCOUNT_RESPONSE>();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_casecount_status", conn);
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@taskid", rESQUEST.TASKID);
                        cmd.Parameters.AddWithValue("@COUNT_TYPE", rESQUEST.COUNT_TYPE);

                        using (SqlDataReader myReader = cmd.ExecuteReader())
                        {
                            do
                            {
                                while (myReader.Read())
                                {
                                    WORKTYPESUMMARYCOUNT_RESPONSE obj = new WORKTYPESUMMARYCOUNT_RESPONSE();
                                    obj.card_status = myReader["card_status"].ToString();
                                    obj.casecount = (int)myReader["casecount"];
                                    obj.card_Description = myReader["card_Description"].ToString();
                                    rESPONSEs.Add(obj);
                                }
                            }
                            while (myReader.NextResult());
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in WORKTYPESUMMARYCOUNT. ", ex);
                }
            }
            return rESPONSEs;
        }



    }
}
