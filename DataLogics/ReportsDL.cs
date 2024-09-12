using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using TMT_Code_Migration1.DataLogics.Utility;
using TMT_Code_Migration1.Models.Reports;
using TMT_Code_Migration1.Views.Interfaces;

namespace TMT_Code_Migration1.DataLogics
{
    public class ReportsDL: IReports
    {
        private readonly CommonDl _commonDl;
        private readonly IConfiguration _config;
        public ReportsDL(CommonDl commonDl, [FromServices] IConfiguration config)
        {
            _commonDl = commonDl;
            _config = config;
        }
        public List<ReportsCardsDetailsResponse> GetCardsDetails(ReportsCardsDetailsRequest request)
        {
            List<ReportsCardsDetailsResponse> allCards = new List<ReportsCardsDetailsResponse>();
            var dtfrom = DateTime.MinValue;
            var dtTo = DateTime.MinValue;
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {

                    using (SqlCommand myCMD = new SqlCommand("SELECT EntryDate,FullName,worktypename,premise," +
                         " CName,CardUrl,Cardstatus,QcBy,CardsID,comment,card_Address,SUPERREVIEW  from F_ReportCountcards(@datefrom ,@dateto ,@companyid ,@EditorName ,@worktype ) ", conn))

                    //using (SqlCommand myCMD = new SqlCommand("SP_ReportCountcards", conn))
                    {
                        if (!string.IsNullOrEmpty(request.datefrom))
                        {
                            dtfrom = DateTime.ParseExact(request.datefrom, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            myCMD.Parameters.Add(new SqlParameter("@datefrom", dtfrom.ToString("yyyy-MM-dd") ?? (object)DBNull.Value));
                        }
                        else
                            myCMD.Parameters.Add(new SqlParameter("@datefrom", request.datefrom ?? (object)DBNull.Value));

                        if (!string.IsNullOrEmpty(request.dateto))
                        {
                            dtTo = DateTime.ParseExact(request.dateto, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            myCMD.Parameters.Add(new SqlParameter("@dateto", dtTo.ToString("yyyy-MM-dd") ?? (object)DBNull.Value));
                        }
                        else
                            myCMD.Parameters.Add(new SqlParameter("@dateto", request.dateto ?? (object)DBNull.Value));

                        myCMD.Parameters.Add(new SqlParameter("@companyid", request.companyid == "0" ? (object)DBNull.Value : request.companyid));
                        myCMD.Parameters.Add(new SqlParameter("@EditorName", request.EditorName == null ? (object)DBNull.Value : request.EditorName));
                        myCMD.Parameters.Add(new SqlParameter("@worktype", request.worktype == "" ? (object)DBNull.Value : request.worktype));

                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {

                            while (myReader.Read())
                            {
                                ReportsCardsDetailsResponse obj = new ReportsCardsDetailsResponse();
                                obj.Date = Convert.ToDateTime(myReader["EntryDate"]).ToString("dd/MM/yyyy");
                                obj.Editor = myReader["FullName"].ToString();
                                obj.WorkType = myReader["worktypename"].ToString();
                                obj.Premise = myReader["premise"].ToString();
                                obj.CompanyName = myReader["CName"].ToString();
                                obj.CardUrl = myReader["CardUrl"].ToString();
                                obj.CardStatus = myReader["Cardstatus"].ToString();
                                obj.Analyst = myReader["QcBy"].ToString();
                                obj.CardsID = myReader["CardsID"].ToString();
                                obj.Comment = myReader["comment"].ToString();
                                obj.Address = myReader["card_address"].ToString();
                                obj.superreview = myReader["SUPERREVIEW"].ToString();

                                allCards.Add(obj);
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in GetCardsDetails. ", ex);
                }

            }
            return allCards;
        }
        public List<GetReportResponse> GetReport(GetReportRequest request)
        {
            var dtfrom = DateTime.MinValue;
            var dtTo = DateTime.MinValue;
            List<GetReportResponse> getRawDataResponses = new List<GetReportResponse>();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_GET_REPORT", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        SqlParameter[] param = new SqlParameter[7];

                        param[0] = new SqlParameter("@V_COMPANY", request.V_COMPANY);
                        param[1] = new SqlParameter("@V_USER", request.V_USER);
                        param[2] = new SqlParameter("@VSTATUS", request.VSTATUS);


                        if (!string.IsNullOrEmpty(request.V_DATEFROM))
                        {
                            dtfrom = DateTime.ParseExact(request.V_DATEFROM, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            param[3] = new SqlParameter("@V_DATEFROM", dtfrom.ToString("yyyy-MM-dd") ?? (object)DBNull.Value);
                        }
                        else
                            param[3] = new SqlParameter("@V_DATEFROM", request.V_DATEFROM ?? (object)DBNull.Value);


                        if (!string.IsNullOrEmpty(request.V_DATETO))
                        {
                            dtTo = DateTime.ParseExact(request.V_DATETO, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            param[4] = new SqlParameter("@V_DATETO", dtTo.ToString("yyyy-MM-dd") ?? (object)DBNull.Value);
                        }
                        else
                            param[4] = new SqlParameter("@V_DATETO", request.V_DATETO ?? (object)DBNull.Value);


                        param[5] = new SqlParameter("@V_ISSUETYPE", request.V_ISSUETYPE);
                        param[6] = new SqlParameter("@V_WORK_TYPE", request.V_WORK_TYPE);

                        foreach (SqlParameter str in param)
                        {
                            myCMD.Parameters.Add(str);
                        }
                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                GetReportResponse obj = new GetReportResponse();

                                if (myReader["COMPLETED_DATE"].ToString() != null && (myReader["COMPLETED_DATE"].ToString() != ""))
                                    obj.COMPLETED_DATE = Convert.ToDateTime(myReader["COMPLETED_DATE"].ToString());
                                //obj.COMPLETED_DATE = Convert.ToDateTime(myReader["COMPLETED_DATE"].ToString());
                                obj.PREMISE_NUMBER = myReader["PREMISE_NUMBER"].ToString();
                                obj.SERV_ORDER_NO = myReader["SERV_ORDER_NO"].ToString();
                                obj.CardUrl = myReader["CardUrl"].ToString();
                                if (myReader["HOUSE_NO"].ToString() != null && myReader["HOUSE_NO"].ToString() != "")
                                    obj.HOUSE_NO = myReader["HOUSE_NO"].ToString();

                                obj.STREET = myReader["STREET"].ToString() == "" ? null : myReader["STREET"].ToString();
                                obj.STREET_SUFFIX = myReader["STREET_SUFFIX"].ToString() == "" ? null : myReader["STREET_SUFFIX"].ToString();
                                obj.WORK_TYPE = myReader["WORK_TYPE"].ToString();

                                obj.FULLNAME = myReader["FULLNAME"].ToString();
                                obj.ISSUENAME = myReader["ISSUENAME"].ToString() == "" ? null : myReader["ISSUENAME"].ToString();
                                obj.CARD_DESCRIPTION = myReader["CARD_DESCRIPTION"].ToString();
                                obj.CITY = myReader["CITY"].ToString() == "" ? null : myReader["CITY"].ToString();
                                obj.CARD_URL_PR = myReader["CARD_URL_PR"].ToString() == "" ? null : myReader["CARD_URL_PR"].ToString();
                                getRawDataResponses.Add(obj);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in GetAssignedUserData. ", ex);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return getRawDataResponses;
        }
        public List<GetClientReportResponse> GetClientReport(GetClientReportRequest request)
        {
            var dtfrom = DateTime.MinValue;
            var dtTo = DateTime.MinValue;
            List<GetClientReportResponse> getRawDataResponses = new List<GetClientReportResponse>();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_GET_CLIENT_REPORT", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        SqlParameter[] param = new SqlParameter[11];

                        param[0] = new SqlParameter("@V_COMPANY", request.V_COMPANY);
                        param[1] = new SqlParameter("@V_USER", request.V_USER);
                        param[2] = new SqlParameter("@VSTATUS", request.VSTATUS);


                        if (!string.IsNullOrEmpty(request.V_DATEFROM))
                        {
                            dtfrom = DateTime.ParseExact(request.V_DATEFROM, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            param[3] = new SqlParameter("@V_DATEFROM", dtfrom.ToString("yyyy-MM-dd") ?? (object)DBNull.Value);
                        }
                        else
                            param[3] = new SqlParameter("@V_DATEFROM", request.V_DATEFROM ?? (object)DBNull.Value);


                        if (!string.IsNullOrEmpty(request.V_DATETO))
                        {
                            dtTo = DateTime.ParseExact(request.V_DATETO, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            param[4] = new SqlParameter("@V_DATETO", dtTo.ToString("yyyy-MM-dd") ?? (object)DBNull.Value);
                        }
                        else
                            param[4] = new SqlParameter("@V_DATETO", request.V_DATETO ?? (object)DBNull.Value);


                        param[5] = new SqlParameter("@V_ISSUETYPE", request.V_ISSUETYPE);
                        param[6] = new SqlParameter("@V_WORK_TYPE", request.V_WORK_TYPE);

                        param[7] = new SqlParameter("@V_STREET", request.V_STREET);
                        param[8] = new SqlParameter("@V_CITY", request.V_CITY);
                        param[9] = new SqlParameter("@V_House_number", request.V_House_number);
                        param[10] = new SqlParameter("@V_PREMISE", request.V_PREMISE);

                        foreach (SqlParameter str in param)
                        {
                            myCMD.Parameters.Add(str);
                        }
                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                GetClientReportResponse obj = new GetClientReportResponse();

                                if (myReader["COMPLETED_DATE"].ToString() != null && (myReader["COMPLETED_DATE"].ToString() != ""))
                                    obj.COMPLETED_DATE = Convert.ToDateTime(myReader["COMPLETED_DATE"].ToString());
                                //obj.COMPLETED_DATE = Convert.ToDateTime(myReader["COMPLETED_DATE"].ToString());
                                obj.PREMISE_NUMBER = myReader["PREMISE_NUMBER"].ToString();
                                obj.SERV_ORDER_NO = myReader["SERV_ORDER_NO"].ToString();
                                obj.CardUrl = myReader["CardUrl"].ToString();
                                obj.CardsID = myReader["CardsID"].ToString();
                                if (myReader["HOUSE_NO"].ToString() != null && myReader["HOUSE_NO"].ToString() != "")
                                    obj.HOUSE_NO = myReader["HOUSE_NO"].ToString();

                                obj.STREET = myReader["STREET"].ToString() == "" ? null : myReader["STREET"].ToString();
                                obj.STREET_SUFFIX = myReader["STREET_SUFFIX"].ToString() == "" ? null : myReader["STREET_SUFFIX"].ToString();
                                obj.WORK_TYPE = myReader["WORK_TYPE"].ToString();
                                obj.FULLNAME = myReader["FULLNAME"].ToString();
                                obj.ISSUENAME = myReader["ISSUENAME"].ToString() == "" ? null : myReader["ISSUENAME"].ToString();
                                obj.CARD_DESCRIPTION = myReader["CARD_DESCRIPTION"].ToString();
                                obj.CITY = myReader["CITY"].ToString();
                                obj.IMAGE_ID = (int)myReader["IMAGE_ID"];
                                getRawDataResponses.Add(obj);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in GetAssignedUserData. ", ex);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return getRawDataResponses;
        }
    }
}
