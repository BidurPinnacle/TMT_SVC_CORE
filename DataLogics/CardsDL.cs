using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Xml.Linq;
using TMT_Code_Migration1.DataLogics.Utility;
using TMT_Code_Migration1.Models.Cards;
using TMT_Code_Migration1.Models.Common;
using Common = TMT_Code_Migration1.Models.Cards.Common;

namespace TMT_Code_Migration1.DataLogics
{
    public class CardsDL
    {
        private readonly CommonDl _commonDl;
        private readonly IConfiguration _configuration;
        
        public CardsDL(CommonDl commonDl, [FromServices] IConfiguration configuration)
        {
            _commonDl = commonDl;
            _configuration = configuration;
        }
        public AddCardResponse Addcard(AddCardRequest request)
        {
            AddCardResponse card = new AddCardResponse();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("spCards", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CardsID", request.CardsID);

                    // convert to date 
                    string DateString = request.EntryDate;
                    DateTime dateVal = DateTime.ParseExact(DateString, "dd/MM/yyyy", null);

                    cmd.Parameters.AddWithValue("@EntryDate", dateVal);
                    cmd.Parameters.AddWithValue("@Address", request.Address);
                    cmd.Parameters.AddWithValue("@CardStatus", request.CardStatus);
                    cmd.Parameters.AddWithValue("@SessionName", request.SessionName);
                    cmd.Parameters.AddWithValue("@Premise", request.Premise);
                    cmd.Parameters.AddWithValue("@WorkOrderNo", request.WorkOrderNo);
                    cmd.Parameters.AddWithValue("@Work_Type", request.Work_Type);
                    cmd.Parameters.AddWithValue("@State", request.State);
                    cmd.Parameters.AddWithValue("@StatementType", request.StatementType);
                    cmd.Parameters.AddWithValue("@CheckedUGP", request.CheckedUGP);
                    cmd.Parameters.AddWithValue("@UpdatedBY", request.UpdatedBY == "" ? DBNull.Value : (object)request.UpdatedBY);
                    cmd.Parameters.AddWithValue("@Remark", request.Remark == "" ? DBNull.Value : (object)request.Remark);
                    cmd.Parameters.AddWithValue("@CardUrl", request.CardUrl == "" ? DBNull.Value : (object)request.CardUrl);
                    cmd.Parameters.AddWithValue("@Record_count", request.RecordCount);
                    cmd.Parameters.AddWithValue("@rootCauseId", request.RootCauseId);

                    cmd.Parameters.Add("@VOUTPUT", SqlDbType.Char, 100);
                    cmd.Parameters["@VOUTPUT"].Direction = ParameterDirection.Output;


                    int k = cmd.ExecuteNonQuery();
                    string str = (string)cmd.Parameters["@VOUTPUT"].Value;
                    //if (k != 0)
                    //{
                    card.Message = str;
                    card.Code = k;

                    //}

                }
                catch (Exception ex)
                {
                    throw new Exception("Error in Cards. ", ex);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return card;
        }
        public List<GatAllCards> GetCardAll()
        {
            List<GatAllCards> allCards = new List<GatAllCards>();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("SP_SELECT_ALLCARDS", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader myReader = cmd.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                GatAllCards obj = new GatAllCards();
                                obj.EntryDate = (DateTime)myReader["EntryDate"];
                                obj.CardsID = (Int32)myReader["CardsID"];
                                obj.SessionName = myReader["SessionName"].ToString() == null ? "" : myReader["SessionName"].ToString();
                                obj.Remark = myReader["Comment"].ToString() == null ? "" : myReader["Comment"].ToString();
                                obj.CName = myReader["CName"].ToString() == null ? "" : myReader["CName"].ToString();
                                obj.worktypename = myReader["worktypename"].ToString() == null ? "" : myReader["worktypename"].ToString();
                                obj.Premise = myReader["Premise"].ToString() == null ? "" : myReader["Premise"].ToString();
                                //obj.CheckedUGP = Convert.ToBoolean(myReader["CheckedUGP"]);
                                obj.Address = myReader["card_address"].ToString() == null ? "" : myReader["card_address"].ToString();
                                obj.Status = myReader["CardStatus"].ToString() == null ? "" : myReader["CardStatus"].ToString();
                                obj.UpdatedBY = myReader["updatedby"].ToString() == null ? "" : myReader["updatedby"].ToString();
                                obj.desc = myReader["card_Description"].ToString() == null ? "" : myReader["card_Description"].ToString();
                                obj.cardUrl = myReader["cardUrl"].ToString() == null ? "" : myReader["cardUrl"].ToString();
                                obj.FullName = myReader["FullName"].ToString() == null ? "" : myReader["FullName"].ToString();
                                obj.superreview = myReader["superreview"].ToString() == null ? "" : myReader["superreview"].ToString();
                                allCards.Add(obj);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in GetCardsAllDL. ", ex);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return allCards;
        }
        //public List<GatAllCards> GetCardAllwithFilter(GetCardRequest request)
        //{
        //    List<GatAllCards> allCards = new List<GatAllCards>();
        //    CommonDl dl = new CommonDl();
        //    using (SqlConnection conn = dl.connect())
        //    {
        //        try
        //        {
        //            SqlCommand myCMD = new SqlCommand("SELECT EntryDate, Status, CardsID " +
        //                "SessionName, CName, worktypename, Premise, CheckedUGP FROM F_cards() where EntryDate = @EntryDate and cid=@CId", conn);
        //            SqlParameter[] param = new SqlParameter[2];
        //            param[0] = new SqlParameter("@EntryDate", request.entrydate.ToString("MM/dd/yyyy"));
        //            param[1] = new SqlParameter("@CId", request.CompanyID);
        //            myCMD.Parameters.Add(param[0]);
        //            myCMD.Parameters.Add(param[1]);
        //            using (SqlDataReader myReader = myCMD.ExecuteReader())
        //            {
        //                while (myReader.Read())
        //                {
        //                    GatAllCards obj = new GatAllCards();
        //                    obj.EntryDate = myReader.GetDateTime(0);
        //                    obj.Status = myReader.GetString(1);
        //                    obj.CardsID = myReader.GetInt32(2);
        //                    obj.SessionName = myReader.GetString(3);
        //                    obj.CName = myReader.GetString(4);
        //                    obj.worktypename = myReader.GetString(5);
        //                    obj.Premise = myReader.GetString(6);
        //                    obj.CheckedUGP = Convert.ToBoolean(myReader.GetInt32(7));
        //                    allCards.Add(obj);
        //                }

        //            }
        //        }
        //        catch (Exception ex)
        //        {

        //            ExceptionLogging.SendErrorToText(ex);
        //            throw new Exception("Error in GetCardAllwithFilter. ", ex);
        //        }
        //    }
        //    return allCards;
        //}
        public GetCardbyCid getCardbyCid(int Cid)
        {
            GetCardbyCid obj = new GetCardbyCid();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_SELECT_CARDS_ID", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        SqlParameter[] param = new SqlParameter[1];
                        param[0] = new SqlParameter("@CARDSID", Cid);
                        myCMD.Parameters.Add(param[0]);

                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {

                            while (myReader.Read())
                            {
                                obj.EntryDate = (DateTime)myReader["EntryDate"];
                                obj.CardsID = (int)myReader["CardsID"];
                                obj.SessionName = myReader["SessionName"].ToString() == "" ? null : myReader["SessionName"].ToString();
                                obj.Premise = myReader["Premise"].ToString() == "" ? null : myReader["Premise"].ToString();
                                obj.WorkOrderNo = myReader["SERV_ORDER_NO"].ToString() == "" ? null : myReader["SERV_ORDER_NO"].ToString();
                                obj.Address = myReader["card_address"].ToString() == "" ? null : myReader["card_address"].ToString();
                                obj.Work_Type = myReader["Work_Type"].ToString() == "" ? null : myReader["Work_Type"].ToString();
                                obj.State = (int)myReader["card_state"];
                                obj.Remark = myReader["comment"].ToString() == "" ? null : myReader["comment"].ToString();
                                obj.CardUrl = myReader["CardUrl"].ToString() == "" ? null : myReader["CardUrl"].ToString();
                                obj.CardStatus = myReader["CARDSTATUS"].ToString() == "" ? null : myReader["CARDSTATUS"].ToString();
                                obj.CheckedUGP = (int)myReader["CHECKEDUGP"];
                                obj.updatedby = myReader["updatedby"].ToString() == "" ? null : myReader["updatedby"].ToString();
                                obj.RecordCount = (int)myReader["record_count"];
                                obj.rootCauseId = myReader["rootCauseId"] != DBNull.Value ? (int)myReader["rootCauseId"] : 0;
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception("Error in getCardbyCid. ", ex);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return obj;
        }
        public Common ArchiveCardbyID(int Cid)
        {
            Common commonmsg = new Common();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_UPDT_CARD_WITH_ID", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        SqlParameter[] param = new SqlParameter[1];
                        param[0] = new SqlParameter("@CId", Cid);
                        myCMD.Parameters.Add(param[0]);
                        myCMD.Parameters.Add("@VOUTPUT", SqlDbType.VarChar, 100);
                        myCMD.Parameters["@VOUTPUT"].Direction = ParameterDirection.Output;
                        int k = myCMD.ExecuteNonQuery();
                        commonmsg.Message = myCMD.Parameters["@VOUTPUT"].Value.ToString();
                        commonmsg.Code = k;
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception("Error in ArchiveCardbyID. ", ex);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return commonmsg;
        }
        public AssignQAResponse AssignQA(List<AssignQARequest> request1)
        {
            AssignQAResponse card = new AssignQAResponse();
            string query = string.Empty;
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    foreach (AssignQARequest request in request1)
                    {
                        using (SqlCommand cmd = new SqlCommand("spCardsComment", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@CardsID", request.CardsID);
                            cmd.Parameters.AddWithValue("@statementType", request.statementType);
                            cmd.Parameters.AddWithValue("@Comment", request.Remark);
                            cmd.Parameters.AddWithValue("@updatedBy", request.UpdatedBY);
                            cmd.Parameters.AddWithValue("@CardStatus", request.CardStatus);
                            cmd.Parameters.AddWithValue("@QCId", request.AssignTo);
                            cmd.Parameters.AddWithValue("@VENTRY_DATE", request.VENTRY_DATE);
                            cmd.Parameters.AddWithValue("@PROFILEID", request.PROFILEID);
                            int k = cmd.ExecuteNonQuery();
                            if (k != 0)
                            {
                                card.Message = "Operation Successfully Completed";
                                card.Code = k;
                            }
                            else
                            {
                                card.Message = "Not Assigned, Please try again";
                                card.Code = k;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in AssignQA DL ", ex);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return card;

        }
        public List<GatAllCards> GetCardAllByUser(CardBYUserRequest request)
        {
            List<GatAllCards> allCards = new List<GatAllCards>();
            //string DateFROMSTR = request.start_date;
            //DateTime DateFROMDT = DateTime.ParseExact(DateFROMSTR, "dd/MM/yyyy",
            //                                    CultureInfo.InvariantCulture);
            //string DateTOSTR = request.end_date;
            //DateTime DateTODT = DateTime.ParseExact(DateTOSTR, "dd/MM/yyyy",
            //                                    CultureInfo.InvariantCulture);
            //DateTime.ParseExact(DateTOSTR, "MM/dd/yyyy", null);

            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_GETCARDBYUSER", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        SqlParameter[] param = new SqlParameter[3];
                        param[0] = new SqlParameter("@UpdatedBy", request.UserId);
                        //param[1] = new SqlParameter("@FROMDATE", DateFROMDT);
                        //param[2] = new SqlParameter("@TODATE", DateTODT);
                        myCMD.Parameters.Add(param[0]);
                        //myCMD.Parameters.Add(param[1]);
                        //myCMD.Parameters.Add(param[2]);
                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                GatAllCards obj = new GatAllCards();
                                obj.EntryDate = (DateTime)myReader["EntryDate"];
                                obj.CardsID = (Int32)myReader["CardsID"];
                                obj.SessionName = myReader["SessionName"].ToString() == "" ? null : myReader["SessionName"].ToString(); // ISSUE DESCRIPTION
                                obj.Remark = myReader["Comment"].ToString() == "" ? null : myReader["Comment"].ToString(); //COMMENT 
                                obj.CName = myReader["CName"].ToString() == "" ? null : myReader["CName"].ToString(); // TASKTYPE
                                obj.worktypename = myReader["worktypename"].ToString() == "" ? null : myReader["worktypename"].ToString(); //WORKTYPE
                                obj.Premise = myReader["Premise"].ToString() == "" ? null : myReader["Premise"].ToString(); //BCEM
                                obj.CheckedUGP = (int)myReader["CheckedUGP"]; // TASKTYPEID
                                obj.Address = myReader["card_address"].ToString() == "" ? null : myReader["card_address"].ToString(); // WORK TYPE DESCRIPTION
                                obj.Status = myReader["CardStatus"].ToString() == "" ? null : myReader["CardStatus"].ToString(); // STATUS / RESOLUTION
                                obj.desc = myReader["card_Description"].ToString() == "" ? null : myReader["card_Description"].ToString(); // STATUS DESCRIPTION
                                obj.cardUrl = myReader["cardUrl"].ToString() == "" ? null : myReader["cardUrl"].ToString(); // CONTRACT ACCOUNT
                                obj.superreview = myReader["superreview"].ToString() == "" ? null : myReader["superreview"].ToString();
                                obj.serv_order_no = myReader["SERV_ORDER_NO"].ToString() == "" ? null : myReader["SERV_ORDER_NO"].ToString();
                                obj.RecordCount = (int)myReader["record_count"];
                                obj.userCreated = myReader["fullname"].ToString();
                                allCards.Add(obj);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in GetCardByUser. ", ex);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }

            }
            return allCards;
        }


        public List<GatAllCards> GetAllCardByUserAndKeywords(CardBYUserRequestAndKeywords request)
        {
            List<GatAllCards> allCards = new List<GatAllCards>();
            string DateFROMSTR = request.start_date;

            if (DateFROMSTR != null)
            {
                DateTime DateFROMDT = DateTime.ParseExact(DateFROMSTR, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateFROMSTR = DateFROMDT.ToString("yyyy-MM-dd");
                System.Diagnostics.Debug.WriteLine("You click me .................." + DateFROMDT.ToString("yyyy-MM-dd"));
            }

            string DateTOSTR = request.end_date;
            if (DateTOSTR != null)
            {
                DateTime DateTODT = DateTime.ParseExact(DateTOSTR, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTOSTR = DateTODT.ToString("yyyy-MM-dd");
                System.Diagnostics.Debug.WriteLine("You click me .................." + DateTODT.ToString("yyyy-MM-dd"));
            }




            string tasktype = request.tasktype;
            string wtype = request.wtype;
            string premise = request.premise;
            string CardUrl = request.CardUrl;
            string serviceorder = request.serviceorder;
            string Resolutiontype = request.Resolutiontype;
            string SessionName = request.SessionName;
            string remark = request.remark;



            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {

                    using (SqlCommand myCMD = new SqlCommand("SP_GETCARDBYUSERANDKEYWORDS", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        SqlParameter[] param = new SqlParameter[13];
                        param[0] = new SqlParameter("@UpdatedBy", request.UserId);
                        param[1] = new SqlParameter("@FROMDATE", DateFROMSTR);
                        param[2] = new SqlParameter("@TODATE", DateTOSTR);
                        param[3] = new SqlParameter("@tasktype", tasktype);
                        param[4] = new SqlParameter("@wtype", wtype);
                        param[5] = new SqlParameter("@premise", premise);
                        param[6] = new SqlParameter("@CardUrl", CardUrl);
                        param[7] = new SqlParameter("@serviceorder", serviceorder);
                        param[8] = new SqlParameter("@Resolutiontype", Resolutiontype);
                        param[9] = new SqlParameter("@SessionName", SessionName);
                        param[10] = new SqlParameter("@remark", remark);
                        param[11] = new SqlParameter("@userName", request.UserName);
                        param[12] = new SqlParameter("@rootCauseId", request.RootCauseId);
                        myCMD.Parameters.Add(param[0]); myCMD.Parameters.Add(param[1]);
                        myCMD.Parameters.Add(param[2]); myCMD.Parameters.Add(param[3]);
                        myCMD.Parameters.Add(param[4]); myCMD.Parameters.Add(param[5]);
                        myCMD.Parameters.Add(param[6]); myCMD.Parameters.Add(param[7]);
                        myCMD.Parameters.Add(param[8]); myCMD.Parameters.Add(param[9]);
                        myCMD.Parameters.Add(param[10]);
                        myCMD.Parameters.Add(param[11]);
                        myCMD.Parameters.Add(param[12]);


                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                GatAllCards obj = new GatAllCards();
                                obj.EntryDate = (DateTime)myReader["EntryDate"];
                                obj.CardsID = (Int32)myReader["CardsID"];
                                obj.SessionName = myReader["SessionName"].ToString() == "" ? null : myReader["SessionName"].ToString(); // ISSUE DESCRIPTION
                                obj.Remark = myReader["Comment"].ToString() == "" ? null : myReader["Comment"].ToString(); //COMMENT 
                                obj.CName = myReader["CName"].ToString() == "" ? null : myReader["CName"].ToString(); // TASKTYPE
                                obj.worktypename = myReader["worktypename"].ToString() == "" ? null : myReader["worktypename"].ToString(); //WORKTYPE
                                obj.Premise = myReader["Premise"].ToString() == "" ? null : myReader["Premise"].ToString(); //BCEM
                                obj.CheckedUGP = (int)myReader["CheckedUGP"]; // TASKTYPEID
                                obj.Address = myReader["card_address"].ToString() == "" ? null : myReader["card_address"].ToString(); // WORK TYPE DESCRIPTION
                                obj.Status = myReader["CardStatus"].ToString() == "" ? null : myReader["CardStatus"].ToString(); // STATUS / RESOLUTION
                                obj.desc = myReader["card_Description"].ToString() == "" ? null : myReader["card_Description"].ToString(); // STATUS DESCRIPTION
                                obj.cardUrl = myReader["cardUrl"].ToString() == "" ? null : myReader["cardUrl"].ToString(); // CONTRACT ACCOUNT
                                obj.superreview = myReader["superreview"].ToString() == "" ? null : myReader["superreview"].ToString();
                                obj.serv_order_no = myReader["SERV_ORDER_NO"].ToString() == "" ? null : myReader["SERV_ORDER_NO"].ToString();
                                obj.RecordCount = (int)myReader["record_count"];
                                obj.userCreated = myReader["fullname"].ToString();

                                allCards.Add(obj);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in GetCardByUser. ", ex);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }

            }
            return allCards;
        }

        public List<GatAllCards> GetCardAllByQC(CardBYQCRequest request)
        {
            List<GatAllCards> allCards = new List<GatAllCards>();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("Sp_getcard_by_qc", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        SqlParameter[] param = new SqlParameter[5];
                        param[0] = new SqlParameter("@QcBy", request.QCId);
                        param[1] = new SqlParameter("@updatedby", request.EditorID);
                        param[2] = new SqlParameter("@EntryDate", Convert.ToDateTime(request.Entrydate));
                        param[3] = new SqlParameter("@cardstatus", request.CardStatus);
                        param[4] = new SqlParameter("@filterText", request.FilterValue);
                        myCMD.Parameters.Add(param[0]);
                        myCMD.Parameters.Add(param[1]);
                        myCMD.Parameters.Add(param[2]);
                        myCMD.Parameters.Add(param[3]);
                        myCMD.Parameters.Add(param[4]);

                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                GatAllCards obj = new GatAllCards();
                                obj.EntryDate = (DateTime)myReader["EntryDate"];
                                obj.CardsID = (Int32)myReader["CardsID"];
                                obj.SessionName = myReader["SessionName"].ToString();
                                obj.Remark = myReader["Comment"].ToString();
                                obj.CName = myReader["CName"].ToString();
                                obj.worktypename = myReader["worktypename"].ToString();
                                obj.Premise = myReader["Premise"].ToString();
                                //obj.CheckedUGP = Convert.ToBoolean(myReader["CheckedUGP"]);
                                obj.Address = myReader["card_Address"].ToString();
                                obj.IssueID = (int)myReader["ISSUEID"];
                                obj.Status = myReader["CardStatus"].ToString();
                                obj.UpdatedBY = myReader["UpdatedBY"].ToString();
                                obj.CID = (int)myReader["CID"];
                                obj.cardUrl = myReader["cardUrl"].ToString();
                                obj.superreview = myReader["superreview"].ToString();
                                obj.IssueDesc = myReader["ISSUENAME"].ToString();
                                obj.QC_FOR_CURRENT_EDITOR = (Int32)myReader["EDITOR_QC_COUNT"];
                                obj.EDITOR_NAME = myReader["EDITOR_NAME"].ToString();
                                obj.Image_id = (int)myReader["IMAGE_ID"];
                                obj.duplicateCard = (int)myReader["count_card"];
                                obj.CARD_URL_PR = myReader["CARD_URL_PR"].ToString();
                                obj.serv_order_no = myReader["serv_order_no"].ToString();
                                allCards.Add(obj);

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in GetCardByQC. ", ex);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return allCards;
        }
        public List<CommentDetailsResponse> GetAllComment(CommentDetailsRequest request)
        {
            List<CommentDetailsResponse> allComments = new List<CommentDetailsResponse>();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_GETALLCOMMENTS ", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        SqlParameter[] param = new SqlParameter[1];
                        param[0] = new SqlParameter("@CId", request.CardsID);
                        myCMD.Parameters.Add(param[0]);
                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                CommentDetailsResponse obj = new CommentDetailsResponse();
                                obj.updatedOn = (DateTime)myReader["updatedOn"];
                                obj.UpdatedBy = myReader["UpdatedBy"].ToString();
                                obj.Comment = myReader["Comment"].ToString();
                                obj.QcBy = myReader["QcBy"].ToString();
                                obj.CardStatus = myReader["CardStatus"].ToString();
                                allComments.Add(obj);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in GetAllComment dl. ", ex);
                }
            }
            return allComments;
        }
        public List<GetCardsforsupervisorResponse> CardsforSupervisor(GetCardsforsupervisorRequest request)
        {
            List<GetCardsforsupervisorResponse> cards = new List<GetCardsforsupervisorResponse>();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("SP_GetCardsByFilter", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@statementType", request.StatementType);
                    cmd.Parameters.AddWithValue("@cardstatus", request.cardstatus);
                    cmd.Parameters.AddWithValue("@userid", request.userid);
                    cmd.Parameters.AddWithValue("@companyid", request.companyid);
                    cmd.CommandTimeout = 240; //in seconds
                    using (SqlDataReader myReader = cmd.ExecuteReader())
                    {
                        while (myReader.Read())
                        {
                            GetCardsforsupervisorResponse obj = new GetCardsforsupervisorResponse();
                            obj.Submiton = (DateTime)myReader["SubmittedOn"];
                            obj.CardCount = (Int32)myReader["CardCount"];
                            obj.EditorName = myReader["EditorName"].ToString();
                            obj.EditorId = myReader["userid"].ToString();
                            obj.Cardstatus = myReader["Cardstatus"].ToString();
                            obj.QcBy = myReader["QcBy"].ToString();
                            obj.Companyid = (int)myReader["CompanyID"];
                            obj.superreview = myReader["superreview"].ToString();
                            cards.Add(obj);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in CardsforSupervisor. ", ex);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return cards;
        }
        public Un_AssignQAResponse UnAssignQA(Un_AssignQARequest request)
        {
            Un_AssignQAResponse un_AssignQA = new Un_AssignQAResponse();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_UN_ASSIGN_UPD", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@updatedBy", request.UpdatedBY);
                    cmd.Parameters.AddWithValue("@VENTRY_DATE", request.VENTRY_DATE);
                    int k = cmd.ExecuteNonQuery();
                    if (k != 0)
                    {
                        un_AssignQA.Message = "Record Inserted Succesfully into the Database";
                        un_AssignQA.Code = k;
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception("Error in CardsforSupervisor. ", ex);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return un_AssignQA;
        }
        public List<FeedbackResponse> GetFeedback(FeedbackRequest request)
        {
            List<FeedbackResponse> feedbackResponses = new List<FeedbackResponse>();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("sp_GetFeedBack", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        SqlParameter[] param = new SqlParameter[1];
                        param[0] = new SqlParameter("@V_CARDSID", 4);
                        myCMD.Parameters.Add(param[0]);

                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                FeedbackResponse feedbackResponse = new FeedbackResponse();
                                // feedbackResponse.FeedbackID = (int)myReader["FeedbackID"];
                                //feedbackResponse.CardsID = (int)myReader["CardsID"];
                                //feedbackResponse.CommentID = (int)myReader["CommentID"];
                                feedbackResponse.FeedBackText = myReader["FeedBackText"].ToString();
                                //feedbackResponse.UpdatedBy = myReader["UpdatedBy"].ToString();
                                //feedbackResponse.UpdatedOn = (DateTime)myReader["UpdatedOn"];

                                using (SqlCommand myCMD1 = new SqlCommand("sp_GetFeedBackDetails", conn))
                                {
                                    myCMD1.CommandType = CommandType.StoredProcedure;
                                    SqlParameter[] param1 = new SqlParameter[1];
                                    param1[0] = new SqlParameter("@V_FeedbackID", (int)myReader["FeedbackID"]);
                                    myCMD1.Parameters.Add(param1[0]);


                                    SqlDataReader myReader1 = myCMD1.ExecuteReader();
                                    List<FeedbackDetailResponse> feedbackDetailResponses = new List<FeedbackDetailResponse>();

                                    while (myReader1.Read())
                                    {
                                        FeedbackDetailResponse feedbackDetailRespons = new FeedbackDetailResponse();
                                        //feedbackDetailResponse.FeedBackText = myReader["FeedBackText"].ToString();
                                        //feedbackDetailRespons.CrossFeedID = (int)myReader1["CrossFeedID"];
                                        //feedbackDetailRespons.FeedbackID = (int)myReader1["FeedbackID"];
                                        feedbackDetailRespons.CrossFeedText = myReader1["CrossFeedText"].ToString();
                                        //feedbackDetailRespons.UpdatedBy = myReader1["UpdatedBy"].ToString();
                                        //feedbackDetailRespons.UpdatedOn = (DateTime)myReader1["UpdatedOn"];
                                        // feedbackDetailRespons.CrossFeedText = myReader1["CrossFeedText"].ToString();
                                        feedbackDetailResponses.Add(feedbackDetailRespons);
                                    }
                                    feedbackResponse.FeedbackDetailResponse = feedbackDetailResponses;

                                }


                                feedbackResponses.Add(feedbackResponse);

                            }

                        }



                    }

                }
                catch (Exception ex)
                {
                    throw new Exception("Error in GetFeedback. ", ex);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return feedbackResponses;
        }
        public List<FeedbackDetailResponse> GetFeedbackDetails(FeedbackDetailRequest request)
        {
            List<FeedbackDetailResponse> feedbackDetailResponses = new List<FeedbackDetailResponse>();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("sp_GetFeedBackDetails", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        SqlParameter[] param = new SqlParameter[1];
                        param[0] = new SqlParameter("@V_CARDSID", request.CardId);
                        myCMD.Parameters.Add(param[0]);

                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {

                            while (myReader.Read())
                            {
                                FeedbackDetailResponse feedbackDetailResponse = new FeedbackDetailResponse();
                                //feedbackDetailResponse.FeedBackText = myReader["FeedBackText"].ToString();
                                feedbackDetailResponse.CrossFeedText = myReader["CrossFeedText"].ToString();
                                feedbackDetailResponses.Add(feedbackDetailResponse);
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception("Error in GetFeedback. ", ex);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return feedbackDetailResponses;
        }
        public ExcelDataResponse PushExcelData(List<CSV_EXCEL_data> request)
        {
            ExcelDataResponse excelDataResponse = new ExcelDataResponse();
            DataTable DT_TblImportedCards = new DataTable();

            try
            {

                DT_TblImportedCards.Columns.Add(new DataColumn("SERV_ORDER_NO", typeof(string)));
                DT_TblImportedCards.Columns.Add(new DataColumn("URL_LINK", typeof(string)));
                DT_TblImportedCards.Columns.Add(new DataColumn("SERVICE_LINE_STATUS_CD", typeof(string)));
                DT_TblImportedCards.Columns.Add(new DataColumn("SERVICE_LINE_INSTALL_DT", typeof(string)));
                DT_TblImportedCards.Columns.Add(new DataColumn("FREQ_CODE", typeof(string)));
                DT_TblImportedCards.Columns.Add(new DataColumn("INSP_TYPE", typeof(string)));
                DT_TblImportedCards.Columns.Add(new DataColumn("COMPANY_NO", typeof(string)));
                DT_TblImportedCards.Columns.Add(new DataColumn("LO_OFFICE", typeof(string)));
                DT_TblImportedCards.Columns.Add(new DataColumn("ROUTE_NO", typeof(string)));
                DT_TblImportedCards.Columns.Add(new DataColumn("READ_SEQ", typeof(string)));
                DT_TblImportedCards.Columns.Add(new DataColumn("PREMISE_NO", typeof(string)));
                DT_TblImportedCards.Columns.Add(new DataColumn("SPECIAL_INST", typeof(string)));
                DT_TblImportedCards.Columns.Add(new DataColumn("MTR_READ_INST2", typeof(string)));
                DT_TblImportedCards.Columns.Add(new DataColumn("MTR_LOCATION", typeof(string)));
                DT_TblImportedCards.Columns.Add(new DataColumn("HOUSE_NO", typeof(string)));
                DT_TblImportedCards.Columns.Add(new DataColumn("DIRECTION", typeof(string)));
                DT_TblImportedCards.Columns.Add(new DataColumn("STREET", typeof(string)));
                DT_TblImportedCards.Columns.Add(new DataColumn("STREET_SUFFIX", typeof(string)));
                DT_TblImportedCards.Columns.Add(new DataColumn("DIRECTION_SUFFIX", typeof(string)));
                DT_TblImportedCards.Columns.Add(new DataColumn("APARTMENT_TYPE", typeof(string)));
                DT_TblImportedCards.Columns.Add(new DataColumn("APARTMENT", typeof(string)));
                DT_TblImportedCards.Columns.Add(new DataColumn("CITY", typeof(string)));
                DT_TblImportedCards.Columns.Add(new DataColumn("ZIP_CODE", typeof(string)));
                DT_TblImportedCards.Columns.Add(new DataColumn("ADDRESS_OVERFLOW", typeof(string)));
                DT_TblImportedCards.Columns.Add(new DataColumn("METER_STATUS", typeof(string)));
                DT_TblImportedCards.Columns.Add(new DataColumn("METER_ID", typeof(string)));
                DT_TblImportedCards.Columns.Add(new DataColumn("ACCOUNT_NO", typeof(string)));
                DT_TblImportedCards.Columns.Add(new DataColumn("LAT", typeof(string)));
                DT_TblImportedCards.Columns.Add(new DataColumn("LONG1", typeof(string)));
                DT_TblImportedCards.Columns.Add(new DataColumn("GAS_LEAK_SURVEY_DT", typeof(string)));
                DT_TblImportedCards.Columns.Add(new DataColumn("SUBDIVISION_DESC", typeof(string)));
                DT_TblImportedCards.Columns.Add(new DataColumn("WORK_TYPE", typeof(string)));
                DT_TblImportedCards.Columns.Add(new DataColumn("COMPANY_ID", typeof(string)));


                foreach (CSV_EXCEL_data excelDataRequest in request)
                {
                    DataRow dr = DT_TblImportedCards.NewRow();
                    dr["SERV_ORDER_NO"] = excelDataRequest.SERV_ORDER_NO;
                    dr["URL_LINK"] = excelDataRequest.URL_LINK;
                    dr["SERVICE_LINE_STATUS_CD"] = excelDataRequest.SERVICE_LINE_STATUS_CD;
                    dr["SERVICE_LINE_INSTALL_DT"] = excelDataRequest.SERVICE_LINE_INSTALL_DT;
                    dr["FREQ_CODE"] = excelDataRequest.FREQ_CODE;
                    dr["INSP_TYPE"] = excelDataRequest.INSP_TYPE;
                    dr["COMPANY_NO"] = excelDataRequest.COMPANY_NO;
                    dr["LO_OFFICE"] = excelDataRequest.LO_OFFICE;
                    dr["ROUTE_NO"] = excelDataRequest.ROUTE_NO;
                    dr["READ_SEQ"] = excelDataRequest.READ_SEQ;
                    dr["PREMISE_NO"] = excelDataRequest.PREMISE_NO;
                    dr["SPECIAL_INST"] = excelDataRequest.SPECIAL_INST;
                    dr["MTR_READ_INST2"] = excelDataRequest.MTR_READ_INST2;
                    dr["MTR_LOCATION"] = excelDataRequest.MTR_LOCATION;
                    dr["HOUSE_NO"] = excelDataRequest.HOUSE_NO;
                    dr["DIRECTION"] = excelDataRequest.DIRECTION;
                    dr["STREET"] = excelDataRequest.STREET;
                    dr["STREET_SUFFIX"] = excelDataRequest.STREET_SUFFIX;
                    dr["DIRECTION_SUFFIX"] = excelDataRequest.DIRECTION_SUFFIX;
                    dr["APARTMENT_TYPE"] = excelDataRequest.APARTMENT_TYPE;
                    dr["APARTMENT"] = excelDataRequest.APARTMENT;
                    dr["CITY"] = excelDataRequest.CITY;
                    dr["ZIP_CODE"] = excelDataRequest.ZIP_CODE;
                    dr["ADDRESS_OVERFLOW"] = excelDataRequest.ADDRESS_OVERFLOW;
                    dr["METER_STATUS"] = excelDataRequest.METER_STATUS;
                    dr["METER_ID"] = excelDataRequest.METER_ID;
                    dr["ACCOUNT_NO"] = excelDataRequest.ACCOUNT_NO;
                    dr["LAT"] = excelDataRequest.LAT;
                    dr["LONG1"] = excelDataRequest.LONG1;
                    dr["GAS_LEAK_SURVEY_DT"] = excelDataRequest.GAS_LEAK_SURVEY_DT;
                    dr["SUBDIVISION_DESC"] = excelDataRequest.SUBDIVISION;
                    dr["WORK_TYPE"] = excelDataRequest.WORK_TYPE;
                    dr["COMPANY_ID"] = excelDataRequest.PREMISE_NO;

                    DT_TblImportedCards.Rows.Add(dr);
                }

                // DataTable dt = (DataTable)JsonConvert.DeserializeObject(request, (typeof(DataTable)));
                if (DT_TblImportedCards.Rows.Count > 0)
                {

                    using (SqlConnection conn = _commonDl.Connect())
                    {
                        string sql = "SP_TBLRawData_ADD_BULK";
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@TBL_TYPE_RAWDATA", DT_TblImportedCards);
                            cmd.Parameters.Add("@VOUTCODE", SqlDbType.Int);
                            cmd.Parameters["@VOUTCODE"].Direction = ParameterDirection.Output;
                            excelDataResponse.Code = cmd.ExecuteNonQuery();
                            //Storing the output parameters value in 3 different variables.  
                            excelDataResponse.Code = Convert.ToInt16(cmd.Parameters["@VOUTCODE"].Value);

                            if (excelDataResponse.Code == (int)Responsecode.Successfull)
                            {
                                excelDataResponse.Message = Responsecode.Successfull.ToString();
                            }

                            conn.Close();
                        }
                        conn.Close();
                        conn.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in Cards. ", ex);
            }
            return excelDataResponse;

        }
        public List<GetRawDataResponse> GetUnfilteredData(GetRawDataRequest request)
        {

            List<GetRawDataResponse> getRawDataResponses = new List<GetRawDataResponse>();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_GETRAWDATA_COMPANY", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        SqlParameter[] param = new SqlParameter[10];

                        param[0] = new SqlParameter("@V_COMPANYID", request.CompanyID);
                        param[1] = new SqlParameter("@V_ROWCOUNT", request.rowcount);
                        param[2] = new SqlParameter("@V_STATUS", request.status == "" ? null : request.status);
                        param[3] = new SqlParameter("@V_CITY", request.city == "" ? null : request.city);
                        param[4] = new SqlParameter("@V_HOUSENUMBER", request.housenumber == "" ? DBNull.Value.ToString() : request.housenumber);
                        param[5] = new SqlParameter("@V_STREET", request.street == "" ? null : request.street);
                        param[6] = new SqlParameter("@V_YEAR", request.year);
                        param[7] = new SqlParameter("@V_WORK_TYPE", request.wtype);
                        param[8] = new SqlParameter("@V_USERID", request.userid);
                        param[9] = new SqlParameter("@V_CARD_STATUS", request.Card_status);
                        foreach (SqlParameter str in param)
                        {
                            myCMD.Parameters.Add(str);
                        }


                        myCMD.CommandType = CommandType.StoredProcedure;
                        DataSet dsdata = new DataSet();
                        SqlDataAdapter sdadata = new SqlDataAdapter(myCMD);
                        sdadata.Fill(dsdata);
                        foreach (DataTable table in dsdata.Tables)
                        {
                            foreach (DataRow myReader in table.Rows)
                            {

                                GetRawDataResponse obj = new GetRawDataResponse();
                                obj.CREATED_DATE = (DateTime)myReader["CREATED_DATE"];
                                obj.HOUSE_NO = myReader["HOUSE_NO"].ToString();
                                obj.STREET = myReader["STREET"].ToString();
                                obj.STREET_SUFFIX = myReader["STREET_SUFFIX"].ToString();
                                obj.ZIP_CODE = myReader["ZIP_CODE"].ToString();
                                obj.SUBDIVISION_DESC = myReader["SUBDIVISION_DESC"].ToString();
                                obj.CARD_URL_LINK = myReader["CARDURL"].ToString();
                                obj.CARD_PREMISE_NO = myReader["PREMISE_NO"].ToString();
                                obj.CARD_SERV_ORDER_NO = myReader["SERV_ORDER_NO"].ToString();
                                obj.CITY = myReader["CITY"].ToString();
                                obj.WORK_TYPE = myReader["WORK_TYPE"].ToString();
                                obj.CARD_STATUS = myReader["CARD_STATUS"].ToString();
                                obj.CARD_YEAR = Convert.ToInt32(myReader["CARD_YEAR"].ToString());
                                obj.FULLNAME = myReader["FULLNAME"].ToString();
                                obj.UNASSIGNEDCOUNT = Convert.ToInt32(myReader["NOTASSIGNEDCARD"].ToString());
                                obj.CARD_DESCRIPTION = myReader["CARD_DESCRIPTION"].ToString();
                                obj.RAW_DATA_ID = (int)myReader["RAW_DATA_ID"];
                                obj.CARD_URL_PR = myReader["CARD_URL_PR"].ToString();
                                getRawDataResponses.Add(obj);

                            }
                        }



                        //using (SqlDataReader myReader = myCMD.ExecuteReader())
                        //{
                        //    while (myReader.Read())
                        //    {
                        //        GetRawDataResponse obj = new GetRawDataResponse();
                        //        obj.CREATED_DATE = (DateTime)myReader["CREATED_DATE"];
                        //        obj.HOUSE_NO = myReader["HOUSE_NO"].ToString();
                        //        obj.STREET = myReader["STREET"].ToString();
                        //        obj.STREET_SUFFIX = myReader["STREET_SUFFIX"].ToString();
                        //        obj.ZIP_CODE = myReader["ZIP_CODE"].ToString();
                        //        obj.SUBDIVISION_DESC = myReader["SUBDIVISION_DESC"].ToString();
                        //        obj.CARD_URL_LINK = myReader["CARDURL"].ToString();
                        //        obj.CARD_PREMISE_NO = myReader["PREMISE_NO"].ToString();
                        //        obj.CARD_SERV_ORDER_NO = myReader["SERV_ORDER_NO"].ToString();
                        //        obj.CITY = myReader["CITY"].ToString();
                        //        obj.WORK_TYPE = myReader["WORK_TYPE"].ToString();
                        //        obj.CARD_STATUS = myReader["CARD_STATUS"].ToString();
                        //        obj.CARD_YEAR = Convert.ToInt32(myReader["CARD_YEAR"].ToString());
                        //        obj.FULLNAME = myReader["FULLNAME"].ToString();
                        //        obj.UNASSIGNEDCOUNT = Convert.ToInt32(myReader["NOTASSIGNEDCARD"].ToString());
                        //        obj.CARD_DESCRIPTION = myReader["CARD_DESCRIPTION"].ToString();
                        //        obj.RAW_DATA_ID = (int) myReader["RAW_DATA_ID"];

                        //        getRawDataResponses.Add(obj);
                        //    }
                        //}

                        //using (SqlDataReader myReader = myCMD.ExecuteReader())
                        //{
                        //    getRawDataResponses = DataReaderMapToList<GetRawDataResponse>(myReader);
                        //}
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in GetUnfilteredData. ", ex);

                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return getRawDataResponses;
        }

        // public Common getSystemIssueGrid(getSystemIssueGridResponse request)
        //{
        //throw new NotImplementedException();
        //}

        public AssgnEditorResponse AssignEditorData(List<AssgnEditorRequest> request)
        {
            AssgnEditorResponse excelDataResponse = new AssgnEditorResponse();
            DataTable DT_TblImportedCards = new DataTable();

            try
            {

                using (SqlConnection conn = _commonDl.Connect())
                {
                    int Assingedcount = 0;
                    int NotAssignedcount = 0;
                    foreach (AssgnEditorRequest assgn in request)
                    {
                        string sql = "SP_DIRECT_CARD_ASSIGN";
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@EDITORID", assgn.Editorid);
                            cmd.Parameters.AddWithValue("@VPREMISE", assgn.CARD_PREMISE_NO);
                            cmd.Parameters.AddWithValue("@VCARDSERVICE", assgn.CARD_SERV_ORDER_NO);
                            cmd.Parameters.AddWithValue("@RAWID", assgn.RAWID);
                            cmd.Parameters.AddWithValue("@PROFILEID", assgn.PROFILEID);
                            cmd.Parameters.Add("@VOUTPUT", SqlDbType.Int);
                            cmd.Parameters["@VOUTPUT"].Direction = ParameterDirection.Output;
                            cmd.ExecuteNonQuery();
                            excelDataResponse.Code = Convert.ToInt32(cmd.Parameters["@VOUTPUT"].Value);
                            if (excelDataResponse.Code == 1)
                            {
                                //excelDataResponse.Message = "Assigned/Unassign Successfully";
                                Assingedcount += 1;

                            }
                            else
                            {
                                // excelDataResponse.Message = "Not Assigned due to assignement revoke issue";
                                NotAssignedcount += 1;
                            }
                        }
                    }

                    excelDataResponse.Message = "Total assigned -- " + Assingedcount + " total not assigned -- " + NotAssignedcount + "";
                    conn.Close();
                    conn.Dispose();
                }
            }
            catch (Exception ex)
            {
                excelDataResponse.Message = "Error in Cards. " + ex.Message;

                throw new Exception("Error in Cards. ", ex);

            }
            return excelDataResponse;
        }
        public List<GetRawDataResponseUser> GetAssignedUserData(GetRawDataRequestUser request)
        {

            List<GetRawDataResponseUser> getRawDataResponses = new List<GetRawDataResponseUser>();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_GetAssigneddataUser", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        myCMD.Parameters.AddWithValue("@V_ASSIGNTO", request._UId);
                        myCMD.Parameters.AddWithValue("@VSTATUS", request._CardStatus);
                        myCMD.Parameters.AddWithValue("@V_CITY", request._city);
                        myCMD.Parameters.AddWithValue("@V_HOUSENUMBER", request._housenumber);
                        myCMD.Parameters.AddWithValue("@V_STREET", request._street);
                        myCMD.Parameters.AddWithValue("@V_YEAR", request._year);
                        myCMD.Parameters.AddWithValue("@V_WORK_TYPE", request._worktype);
                        myCMD.Parameters.AddWithValue("@v_PREMISE", request._Premise);




                        myCMD.CommandType = CommandType.StoredProcedure;
                        DataSet dsdata = new DataSet();
                        SqlDataAdapter sdadata = new SqlDataAdapter(myCMD);
                        sdadata.Fill(dsdata);

                        foreach (DataTable table in dsdata.Tables)
                        {
                            foreach (DataRow myReader in table.Rows)
                            {
                                GetRawDataResponseUser obj = new GetRawDataResponseUser();
                                obj.cardsid = (int)myReader["CARDSID"];
                                obj.CREATED_DATE = (DateTime)myReader["CREATED_DATE"];
                                obj.HOUSE_NO = myReader["HOUSE_NO"].ToString();
                                obj.STREET = myReader["STREET"].ToString();
                                obj.STREET_SUFFIX = myReader["STREET_SUFFIX"].ToString();
                                obj.ZIP_CODE = myReader["ZIP_CODE"].ToString();
                                obj.SUBDIVISION_DESC = myReader["SUBDIVISION_DESC"].ToString();
                                obj.CARD_URL_LINK = myReader["CARDURL"].ToString();
                                obj.CARD_PREMISE_NO = myReader["PREMISE"].ToString();
                                obj.CARD_SERV_ORDER_NO = myReader["SERV_ORDER_NO"].ToString();
                                obj.CITY = myReader["CITY"].ToString();
                                obj.WORK_TYPE = myReader["WORK_TYPE"].ToString();
                                obj.CARD_STATUS = myReader["CARD_STATUS"].ToString();
                                obj.ISSUENAME = myReader["ISSUENAME"].ToString();
                                obj.CARD_DESCRIPTION = myReader["CARD_DESCRIPTION"].ToString();
                                obj.CARD_YEAR = (DateTime)myReader["CARD_YEAR"];
                                //obj.CARD_YEAR = Convert.ToInt32(myReader["CARD_YEAR"].ToString() == null ? 0 : myReader["CARD_YEAR"]);
                                obj.SessionName = myReader["SessionName"].ToString();
                                obj.SUPERREVIEW = myReader["SUPERREVIEW"].ToString();
                                obj.CARDURL = myReader["CARDURL"].ToString();
                                obj.BILLED = myReader["BILLED"].ToString();
                                obj.CARD_COUNT = (int)myReader["count_card"];
                                obj.IMAGE_ID = (int)myReader["IMAGE_ID"];
                                obj.CARD_URL_PR = myReader["CARD_URL_PR"].ToString();

                                getRawDataResponses.Add(obj);
                            }
                        }


                        //using (SqlDataReader myReader = myCMD.ExecuteReader())
                        //{

                        //    while (myReader.Read())
                        //    {
                        //        GetRawDataResponseUser obj = new GetRawDataResponseUser();
                        //        obj.cardsid = (int)myReader["CARDSID"];
                        //        obj.CREATED_DATE = (DateTime)myReader["CREATED_DATE"];
                        //        obj.HOUSE_NO = myReader["HOUSE_NO"].ToString();
                        //        obj.STREET = myReader["STREET"].ToString();
                        //        obj.STREET_SUFFIX = myReader["STREET_SUFFIX"].ToString();
                        //        obj.ZIP_CODE = myReader["ZIP_CODE"].ToString();
                        //        obj.SUBDIVISION_DESC = myReader["SUBDIVISION_DESC"].ToString();
                        //        obj.CARD_URL_LINK = myReader["CARDURL"].ToString();
                        //        obj.CARD_PREMISE_NO = myReader["PREMISE"].ToString();
                        //        obj.CARD_SERV_ORDER_NO = myReader["SERV_ORDER_NO"].ToString();
                        //        obj.CITY = myReader["CITY"].ToString();
                        //        obj.WORK_TYPE = myReader["WORK_TYPE"].ToString();
                        //        obj.CARD_STATUS = myReader["CARD_STATUS"].ToString();
                        //        obj.ISSUENAME = myReader["ISSUENAME"].ToString();
                        //        obj.CARD_DESCRIPTION = myReader["CARD_DESCRIPTION"].ToString();
                        //        obj.CARD_YEAR = (DateTime)myReader["CARD_YEAR"];
                        //        //obj.CARD_YEAR = Convert.ToInt32(myReader["CARD_YEAR"].ToString() == null ? 0 : myReader["CARD_YEAR"]);
                        //        obj.SessionName = myReader["SessionName"].ToString();
                        //        obj.SUPERREVIEW = myReader["SUPERREVIEW"].ToString();
                        //        obj.CARDURL = myReader["CARDURL"].ToString();
                        //        obj.BILLED = myReader["BILLED"].ToString();
                        //        obj.CARD_COUNT = (int)myReader["count_card"];
                        //        obj.IMAGE_ID = (int)myReader["IMAGE_ID"];

                        //        getRawDataResponses.Add(obj);
                        //    }
                        //}
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
        public AssgnEditorBucketResponse AssignEditorbucket(List<AssgnEditorbucketRequest> request)
        {
            AssgnEditorBucketResponse bucketResponse = new AssgnEditorBucketResponse();
            try
            {

                using (SqlConnection conn = _commonDl.Connect())
                {
                    foreach (AssgnEditorbucketRequest assgnEditorbucketRequest in request)
                    {
                        string sql = "SP_TO_EDITORS_BUCKET";
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@EDITORID", assgnEditorbucketRequest.EDITORID);
                            cmd.Parameters.AddWithValue("@VPREMISE", assgnEditorbucketRequest.PREMISE_NO);
                            cmd.Parameters.AddWithValue("@VCARDSERVICE", assgnEditorbucketRequest.SERV_ORDER_NO == "" ? null : assgnEditorbucketRequest.SERV_ORDER_NO);
                            cmd.Parameters.AddWithValue("@CardStatus", assgnEditorbucketRequest.CARD_STATUS == "" ? null : assgnEditorbucketRequest.CARD_STATUS);
                            cmd.Parameters.AddWithValue("@VISSUEID", assgnEditorbucketRequest.ISSUEID);
                            cmd.Parameters.AddWithValue("@VCARDCOMMENT", assgnEditorbucketRequest.FeedbackText);
                            cmd.Parameters.AddWithValue("@VSESSIONTEXT", assgnEditorbucketRequest.SessionText);
                            cmd.Parameters.AddWithValue("@CARDS_ID", assgnEditorbucketRequest.cardsid);

                            bucketResponse.Code = cmd.ExecuteNonQuery();
                            if (bucketResponse.Code != 0)
                            {
                                bucketResponse.Message = "Operation succcessful";
                            }
                            else
                            {
                                bucketResponse.Message = "Operation succcessful";
                            }
                        }
                    }
                    conn.Close();
                    conn.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in Cards. ", ex);

            }
            return bucketResponse;
        }
        public UserCardCount_Response UsercardCount(UserCardCount_Request request)
        {
            UserCardCount_Response response = new UserCardCount_Response();

            try
            {

                using (SqlConnection conn = _commonDl.Connect())
                {

                    string sql = "SP_USERCARDCOUNT";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UPDATEDBY", request.userid);
                        cmd.Parameters.Add("@V_COUNT", SqlDbType.BigInt);
                        cmd.Parameters["@V_COUNT"].Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        //Storing the output parameters value in 3 different variables.  
                        response.Cardcount = Convert.ToInt16(cmd.Parameters["@V_COUNT"].Value);


                    }
                    conn.Close();
                    conn.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in Cards. ", ex);

            }
            return response;
        }
        public CardTransfer_Response CardTransfer(CardTransfer_Request request)
        {
            CardTransfer_Response response = new CardTransfer_Response();
            try
            {

                using (SqlConnection conn = _commonDl.Connect())
                {

                    string sql = "Sp_usertransfer";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FROMUSER", request.FromUser);
                        cmd.Parameters.AddWithValue("@TOUSER", request.ToUser);
                        cmd.Parameters.AddWithValue("@CARDCOUNT", request.CardCount);
                        cmd.Parameters.AddWithValue("@CHANGEBY", request.ChangeBy);
                        cmd.Parameters.Add("@V_OUTPUT", SqlDbType.VarChar, 100);


                        cmd.Parameters["@V_OUTPUT"].Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        //Storing the output parameters value in 3 different variables.  
                        response.message = cmd.Parameters["@V_OUTPUT"].Value.ToString();


                    }
                    conn.Close();
                    conn.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in Cards. ", ex);

            }
            return response;
        }
        public List<PremiseDetails_Response> PremiseDetails(PremiseDetails_Request request)
        {
            List<PremiseDetails_Response> allCards = new List<PremiseDetails_Response>();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("sp_GetPremiseDetails", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        SqlParameter[] param = new SqlParameter[1];
                        param[0] = new SqlParameter("@PREMISE", request.premiseQuery);
                        myCMD.Parameters.Add(param[0]);
                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                PremiseDetails_Response response = new PremiseDetails_Response();
                                response.cardsid = Convert.ToInt32(myReader["CardsID"]);
                                response.CARD_URL_LINK = myReader["CARDURL"].ToString() == "" ? null : myReader["CARDURL"].ToString();
                                response.WORK_TYPE = myReader["WORK_TYPE"].ToString() == "" ? null : myReader["WORK_TYPE"].ToString();
                                response.ZIP_CODE = myReader["ZIP_CODE"].ToString() == "" ? null : myReader["ZIP_CODE"].ToString();
                                response.SUPERREVIEW = myReader["SUPERREVIEW"].ToString() == "" ? null : myReader["SUPERREVIEW"].ToString();
                                response.SUBDIVISION_DESC = myReader["SUBDIVISION_DESC"].ToString() == "" ? null : myReader["SUBDIVISION_DESC"].ToString();
                                response.STREET_SUFFIX = myReader["STREET_SUFFIX"].ToString() == "" ? null : myReader["STREET_SUFFIX"].ToString();
                                response.STREET = myReader["STREET"].ToString() == "" ? null : myReader["STREET"].ToString();
                                response.SessionName = myReader["SessionName"].ToString() == "" ? null : myReader["SessionName"].ToString();
                                response.ISSUENAME = myReader["ISSUENAME"].ToString() == "" ? null : myReader["ISSUENAME"].ToString();
                                response.HOUSE_NO = myReader["HOUSE_NO"].ToString() == "" ? null : myReader["HOUSE_NO"].ToString();
                                response.CARD_DESCRIPTION = myReader["DESCRIPTION"].ToString() == "" ? null : myReader["DESCRIPTION"].ToString();
                                response.CARD_PREMISE_NO = myReader["PREMISE"].ToString() == "" ? null : myReader["PREMISE"].ToString();
                                response.CARD_STATUS = myReader["CardStatus"].ToString() == "" ? null : myReader["CardStatus"].ToString();
                                response.CARD_SERV_ORDER_NO = myReader["SERV_ORDER_NO"].ToString() == "" ? null : myReader["SERV_ORDER_NO"].ToString();
                                if (myReader["SERVICE_LINE_INSTALL_DT"].ToString() != null && !string.IsNullOrWhiteSpace(myReader["SERVICE_LINE_INSTALL_DT"].ToString()))
                                    response.CARD_YEAR = Convert.ToDateTime(myReader["SERVICE_LINE_INSTALL_DT"].ToString());
                                response.CITY = myReader["CITY"].ToString() == "" ? null : myReader["CITY"].ToString();
                                response.CREATED_DATE = Convert.ToDateTime(myReader["EntryDate"].ToString());
                                response.EditorName = myReader["UpdatedBy"].ToString() == "" ? null : myReader["UpdatedBy"].ToString();

                                allCards.Add(response);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in GetCardByUser. ", ex);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return allCards;
        }
        public List<PremiseDetails_Response> PremiseDetails_insight(PremiseDetails_Request request)
        {
            List<PremiseDetails_Response> allCards = new List<PremiseDetails_Response>();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_GETPREMISEDETAILS_INFO", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        SqlParameter[] param = new SqlParameter[1];
                        param[0] = new SqlParameter("@PREMISE", request.premiseQuery);
                        myCMD.Parameters.Add(param[0]);
                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                PremiseDetails_Response response = new PremiseDetails_Response();
                                response.cardsid = Convert.ToInt32(myReader["CardsID"]);
                                response.CARD_PREMISE_NO = myReader["PREMISE_NO"].ToString() == "" ? null : myReader["PREMISE_NO"].ToString();
                                response.CARD_SERV_ORDER_NO = myReader["SERV_ORDER_NO"].ToString() == "" ? null : myReader["SERV_ORDER_NO"].ToString();
                                response.CITY = myReader["CITY"].ToString() == "" ? null : myReader["CITY"].ToString();
                                if (myReader["SERVICE_LINE_INSTALL_DT"].ToString() != null && !string.IsNullOrWhiteSpace(myReader["SERVICE_LINE_INSTALL_DT"].ToString()))
                                    response.CARD_YEAR = Convert.ToDateTime(myReader["SERVICE_LINE_INSTALL_DT"].ToString());
                                response.HOUSE_NO = myReader["HOUSE_NO"].ToString() == "" ? null : myReader["HOUSE_NO"].ToString();
                                response.STREET = myReader["STREET"].ToString() == "" ? null : myReader["STREET"].ToString();
                                response.STREET_SUFFIX = myReader["STREET_SUFFIX"].ToString() == "" ? null : myReader["STREET_SUFFIX"].ToString();


                                response.CARD_URL_LINK = myReader["URL_LINK"].ToString() == "" ? null : myReader["URL_LINK"].ToString();

                                response.ZIP_CODE = myReader["ZIP_CODE"].ToString() == "" ? null : myReader["ZIP_CODE"].ToString();
                                response.CARD_DESCRIPTION = myReader["card_Description"].ToString() == "" ? null : myReader["card_Description"].ToString();
                                response.WORK_TYPE = myReader["worktypename"].ToString() == "" ? null : myReader["worktypename"].ToString();













                                allCards.Add(response);
                            }
                        }
                    }
                }
                catch (Exception ex) { 
                    throw new Exception("Error in GetCardByUser. ", ex);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return allCards;
        }
        public List<cardDetailsResponse> GetAllCardDetails(cardDetailsRequest request)
        {
            List<cardDetailsResponse> allCards = new List<cardDetailsResponse>();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("SP_GET_ALL_CARD_DETAILS", conn)) //change SP name
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CURRENTUSERID", request.currentUserID);
                        cmd.Parameters.AddWithValue("@BYQA", request.byqa);
                        using (SqlDataReader myReader = cmd.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                cardDetailsResponse obj = new cardDetailsResponse();
                                obj.EntryDate = (DateTime)myReader["EntryDate"];
                                obj.CardsID = (Int32)myReader["CardsID"];
                                obj.SessionName = myReader["SessionName"].ToString() == null ? "" : myReader["SessionName"].ToString();
                                obj.Remark = myReader["Comment"].ToString() == null ? "" : myReader["Comment"].ToString();
                                obj.CName = myReader["CName"].ToString() == null ? "" : myReader["CName"].ToString();
                                obj.worktypename = myReader["worktypename"].ToString() == null ? "" : myReader["worktypename"].ToString();
                                obj.Premise = myReader["Premise"].ToString() == null ? "" : myReader["Premise"].ToString();
                                obj.Address = myReader["card_address"].ToString() == null ? "" : myReader["card_address"].ToString();
                                obj.Status = myReader["CardStatus"].ToString() == null ? "" : myReader["CardStatus"].ToString();
                                obj.UpdatedBY = myReader["updatedby"].ToString() == null ? "" : myReader["updatedby"].ToString();
                                obj.desc = myReader["card_Description"].ToString() == null ? "" : myReader["card_Description"].ToString();
                                obj.cardUrl = myReader["cardUrl"].ToString() == null ? "" : myReader["cardUrl"].ToString();
                                obj.FullName = myReader["FullName"].ToString() == null ? "" : myReader["FullName"].ToString();
                                obj.superreview = myReader["superreview"].ToString() == null ? "" : myReader["superreview"].ToString();
                                obj.QCBYNAME = myReader["QCBYNAME"].ToString() == null ? "" : myReader["QCBYNAME"].ToString();
                                obj.card_url_pr = myReader["card_url_pr"].ToString() == null ? "" : myReader["card_url_pr"].ToString();
                                obj.serv_order_no = myReader["serv_order_no"].ToString() == null ? "" : myReader["serv_order_no"].ToString();
                                allCards.Add(obj);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in GetAllCardDetails. ", ex);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return allCards;
        }
        public List<Batch_response> GetallBatch(Batch_request request)
        {

            List<Batch_response> batch_Responses = new List<Batch_response>();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_SELECT_BATCH", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        SqlParameter[] param = new SqlParameter[2];
                        param[0] = new SqlParameter("@BATCHSTATUS", request.BatchStatus);
                        param[1] = new SqlParameter("@PROFILEID", request.profileid);
                        myCMD.Parameters.Add(param[0]);
                        myCMD.Parameters.Add(param[1]);

                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            batch_Responses = DataReaderMapToList<Batch_response>(myReader);
                        }




                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in GetallBatch dl. ", ex);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return batch_Responses;
        }
        public Common UpdateBatch(Batch_update_request request)
        {
            Common commonmsg = new Common();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_UDPATE_BATCH", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;

                        SqlParameter[] param = new SqlParameter[4];
                        param[0] = new SqlParameter("@BATCHID", request.BATCH_ID);
                        param[1] = new SqlParameter("@BATCHSTATUS", request.BatchStatus);
                        param[2] = new SqlParameter("@PROFILEID", request.ProfileID);
                        param[3] = new SqlParameter("@BATCHCOMMENT", request.BatchComment);
                        myCMD.Parameters.Add(param[0]);
                        myCMD.Parameters.Add(param[1]);
                        myCMD.Parameters.Add(param[2]);
                        myCMD.Parameters.Add(param[3]);

                        myCMD.Parameters.Add("@VOUTPUT", SqlDbType.VarChar, 100);
                        myCMD.Parameters["@VOUTPUT"].Direction = ParameterDirection.Output;

                        int k = myCMD.ExecuteNonQuery();
                        commonmsg.Code = k;
                        //if (k == 1)
                        //{
                        commonmsg.Message = myCMD.Parameters["@VOUTPUT"].Value.ToString();

                        //}
                        //else
                        //    commonmsg.Message = "Batch could not updated. ";

                    }

                }
                catch (Exception ex)
                {
                    throw new Exception("Error in UpdateBatch . ", ex);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return commonmsg;
        }
        public Common Deletecard_supervisor(Supervisor_delete_request request)
        {
            Common commonmsg = new Common();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_DELETE_CARD_SUPERVISOR", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        SqlParameter[] param = new SqlParameter[2];
                        param[0] = new SqlParameter("@CARDID", request.CardId);
                        param[1] = new SqlParameter("@DELETEDBY", request.DeletedBY);
                        myCMD.Parameters.Add(param[0]);
                        myCMD.Parameters.Add(param[1]);

                        myCMD.Parameters.Add("@VOUTPUT", SqlDbType.VarChar, 100);
                        myCMD.Parameters["@VOUTPUT"].Direction = ParameterDirection.Output;
                        int k = myCMD.ExecuteNonQuery();


                        string str = (string)myCMD.Parameters["@VOUTPUT"].Value;

                        commonmsg.Message = str;
                        commonmsg.Code = k;
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception("Error in Deletecard_supervisor . ", ex);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return commonmsg;
        }
        public Batch_summary_response BatchSummary(Batch_summary_request request)
        {
            Batch_summary_response batch_Responses = new Batch_summary_response();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_BATCHSUMMARY", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        SqlParameter[] param = new SqlParameter[2];

                        param[0] = new SqlParameter("@PROFILEID", request.ProfileID); // STATIC FIELD FOR ONLY NORTH CAROLINA
                        param[1] = new SqlParameter("@BATCHID", request.BatchID);

                        myCMD.Parameters.Add(param[0]);
                        myCMD.Parameters.Add(param[1]);


                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            while (myReader.Read())
                            {

                                batch_Responses.V_COMPLETED = Convert.ToInt32(myReader["V_COMPLETED"]);
                                batch_Responses.V_VERIFIED = Convert.ToInt32(myReader["V_VERIFIED"]);
                                batch_Responses.V_RETURNED = Convert.ToInt32(myReader["V_RETURNED"]);
                                batch_Responses.V_REVIEWED = Convert.ToInt32(myReader["V_REVIEWED"]);
                                batch_Responses.V_FIXED = Convert.ToInt32(myReader["V_FIXED"]);

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in BatchSummary  ", ex);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return batch_Responses;

        }
        public Common BatchProcess(Batch_process_request request)
        {
            Common commonmsg = new Common();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_BATCH_PROCESS", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        SqlParameter[] param = new SqlParameter[3];


                        //request.profile = Convert.ToInt32( ConfigurationManager.AppSettings["COMPANY"]);
                        
                        request.ROWS_FOR_BATCH = Convert.ToInt32(_configuration.GetSection("storedProcedures")["ROWS_FOR_BATCH"]);
                        request.YEAR = Convert.ToInt32(_configuration.GetSection("storedProcedures")["YEAR"]);

                        myCMD.Parameters.Add("@V_OUTPUT", SqlDbType.Char, 100);
                        myCMD.Parameters["@V_OUTPUT"].Direction = ParameterDirection.Output;
                        param[0] = new SqlParameter("@PROFILEID", request.profile); // STATIC FIELD FOR ONLY NORTH CAROLINA
                        param[1] = new SqlParameter("@ROWS_FOR_BATCH", request.ROWS_FOR_BATCH);
                        param[2] = new SqlParameter("@YEAR", request.YEAR);
                        myCMD.Parameters.Add(param[0]);
                        myCMD.Parameters.Add(param[1]);
                        myCMD.Parameters.Add(param[2]);

                        int k = myCMD.ExecuteNonQuery();
                        string str = (string)myCMD.Parameters["@V_OUTPUT"].Value;
                        commonmsg.Message = str;
                        commonmsg.Code = k;



                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in BatchProcess  ", ex);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return commonmsg;



        }
        public List<New_batch_cards_response> GetallbatchCards(New_batch_cards_request request)
        {
            List<New_batch_cards_response> new_Batch_Cards_Responses = new List<New_batch_cards_response>();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_SELECT_BATCH_CARDS", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        SqlParameter[] param = new SqlParameter[2];
                        int rowcount = Convert.ToInt32(_configuration.GetSection("storedProcedures")["ROWS_FOR_BATCH"]);
                        param[0] = new SqlParameter("@profileid", request.profileid); //
                        param[1] = new SqlParameter("@rowcount", rowcount); //
                        myCMD.Parameters.Add(param[0]);
                        myCMD.Parameters.Add(param[1]);

                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            new_Batch_Cards_Responses = DataReaderMapToList<New_batch_cards_response>(myReader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in GetallBatch dl. ", ex);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return new_Batch_Cards_Responses;
        }
        public Common UpdateSupervisorCard(Update_Supervisor_Request request)
        {
            Common card = new Common();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_UDPATE_SUPERVISOR_CARD", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CARDSID", request.CARDSID);
                    cmd.Parameters.AddWithValue("@EditedBy", request.EditedBy);
                    cmd.Parameters.AddWithValue("@CardUrl", request.CardUrl);
                    cmd.Parameters.AddWithValue("@SERVICE_LINE_INSTALL_DT", request.SERVICE_LINE_INSTALL_DT);
                    cmd.Parameters.AddWithValue("@CITY", request.CITY == "" ? DBNull.Value : (object)request.CITY);
                    cmd.Parameters.AddWithValue("@ZIP_CODE ", request.ZIP_CODE == "" ? DBNull.Value : (object)request.ZIP_CODE);
                    cmd.Parameters.AddWithValue("@STREET_SUFFIX", request.STREET_SUFFIX == "" ? DBNull.Value : (object)request.STREET_SUFFIX);
                    cmd.Parameters.AddWithValue("@HOUSE_NO", request.HOUSE_NO == "" ? DBNull.Value : (object)request.HOUSE_NO);
                    cmd.Parameters.AddWithValue("@STREET", request.STREET);
                    cmd.Parameters.AddWithValue("@SERVICEORDER", request.ServiceOrder == "" ? DBNull.Value : (object)request.ServiceOrder);
                    cmd.Parameters.AddWithValue("@PREMISE", request.PremiseNumber == "" ? DBNull.Value : (object)request.PremiseNumber);
                    cmd.Parameters.AddWithValue("@SUBDIVISION_DESC", request.SUBDIVISION_DESC == "" ? DBNull.Value : (object)request.SUBDIVISION_DESC);


                    cmd.Parameters.Add("@VOUTPUT", SqlDbType.Char, 100);
                    cmd.Parameters["@VOUTPUT"].Direction = ParameterDirection.Output;


                    int k = cmd.ExecuteNonQuery();
                    string str = (string)cmd.Parameters["@VOUTPUT"].Value;

                    card.Message = str;
                    card.Code = k;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in GetallBatch dl. ", ex);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return card;
        }
        public List<cardTransferFetchDetailsResponse> supervisorDialogCardDetails(cardTransferFetchDetailsRequest request)
        {
            List<cardTransferFetchDetailsResponse> allCards = new List<cardTransferFetchDetailsResponse>();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_SELECT_CARD_TRANSFER_DETAILS", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        SqlParameter[] param = new SqlParameter[1];
                        param[0] = new SqlParameter("@CARDSID", request.CardsID);
                        myCMD.Parameters.Add(param[0]);
                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {

                            allCards = DataReaderMapToList<cardTransferFetchDetailsResponse>(myReader);

                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in fetching details...  supervisorDialogCardDetails---", ex);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return allCards;
        }
        public List<T> DataReaderMapToList<T>(IDataReader dr)
        {
            List<T> list = new List<T>();
            T obj = default(T);
            while (dr.Read())
            {
                obj = Activator.CreateInstance<T>();
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    if (!object.Equals(dr[prop.Name], DBNull.Value))
                    {
                        prop.SetValue(obj, dr[prop.Name], null);
                    }
                }
                list.Add(obj);
            }
            return list;
        }
        public List<ExcelDetails_Response> BatchDetails_for_client(ExcelDetails_Request request)
        {
            List<ExcelDetails_Response> allCards = new List<ExcelDetails_Response>();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_BATCH_SELECT_CLIENT", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        SqlParameter[] param = new SqlParameter[2];
                        param[0] = new SqlParameter("@BATCHID", request.BatchId);
                        param[1] = new SqlParameter("@PROFILE_ID", request.ProfileID);
                        myCMD.Parameters.Add(param[0]);
                        myCMD.Parameters.Add(param[1]);
                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            allCards = DataReaderMapToList<ExcelDetails_Response>(myReader);
                            string mapurl = _configuration.GetSection("storedProcedures")["Map_Url"].ToString();
                            foreach (var obj in allCards)
                            {
                                if (!string.IsNullOrEmpty(obj.MAP_URL))
                                    obj.FULL_MAP_URL = mapurl + "&center=" + obj.MAP_URL + "&level=20";

                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in fetching details...  BatchDetails_for_client---", ex);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return allCards;
        }
        public Common ManualTransferCard(singleCardTransferRequest request)
        {
            Common commonmsg = new Common();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_MANUALCARDS_TRANSFER", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        myCMD.Parameters.AddWithValue("@TOUSER", request.ToUser);
                        myCMD.Parameters.AddWithValue("@CARDSID", request.CARDSID);
                        myCMD.Parameters.AddWithValue("@CHANGEBY", request.ChangeBy);
                        myCMD.Parameters.Add("@VOUTPUT", SqlDbType.VarChar, 100);
                        myCMD.Parameters["@VOUTPUT"].Direction = ParameterDirection.Output;
                        int k = myCMD.ExecuteNonQuery();


                        string str = (string)myCMD.Parameters["@VOUTPUT"].Value;

                        commonmsg.Message = str;
                        commonmsg.Code = k;
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception("Error in ManualTransferCard . ", ex);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return commonmsg;
        }
        public Common supervisorAddCard(supervisorAddCardRequest request)
        {
            Common commonmsg = new Common();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_SUPERVISOR_ADD_CARD", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;


                        myCMD.Parameters.AddWithValue("@Premise", request.Premise);
                        myCMD.Parameters.AddWithValue("@WorkOrderNo", request.WorkOrderNo);
                        myCMD.Parameters.AddWithValue("@Work_Type", request.Work_Type);
                        myCMD.Parameters.AddWithValue("@UpdatedBY", request.UpdatedBy == "" ? DBNull.Value : (object)request.UpdatedBy);
                        myCMD.Parameters.AddWithValue("@CardUrl", request.CardUrl == "" ? DBNull.Value : (object)request.CardUrl);


                        myCMD.Parameters.AddWithValue("@SERVICE_LINE_INSTALL_DT", request.SERVICE_LINE_INSTALL_DT);
                        myCMD.Parameters.AddWithValue("@CITY", request.CITY == "" ? DBNull.Value : (object)request.CITY);
                        myCMD.Parameters.AddWithValue("@ZIP_CODE ", request.ZIP_CODE == "" ? DBNull.Value : (object)request.ZIP_CODE);
                        myCMD.Parameters.AddWithValue("@STREET_SUFFIX", request.STREET_SUFFIX == "" ? DBNull.Value : (object)request.STREET_SUFFIX);
                        myCMD.Parameters.AddWithValue("@HOUSE_NO", request.HOUSE_NO == "" ? DBNull.Value : (object)request.HOUSE_NO);
                        myCMD.Parameters.AddWithValue("@STREET", request.STREET == "" ? DBNull.Value : (object)request.STREET);
                        myCMD.Parameters.AddWithValue("@SUBDIVISION_DESC", request.SUBDIVISION_DESC == "" ? DBNull.Value : (object)request.SUBDIVISION_DESC);
                        myCMD.Parameters.AddWithValue("@ChangeBY", request.SUBDIVISION_DESC == "" ? DBNull.Value : (object)request.ChangeBY);


                        myCMD.Parameters.Add("@VOUTPUT", SqlDbType.VarChar, 100);
                        myCMD.Parameters["@VOUTPUT"].Direction = ParameterDirection.Output;
                        int k = myCMD.ExecuteNonQuery();


                        string str = (string)myCMD.Parameters["@VOUTPUT"].Value;

                        commonmsg.Message = str;
                        commonmsg.Code = k;
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception("Error in supervisorAddCard . ", ex);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return commonmsg;
        }
        public List<clientcardResponse> Client_batchview(Models.Cards.ClintBatchRequest request)
        {
            List<clientcardResponse> clientcardResponse = new List<clientcardResponse>();
            int clientcompany = 0;
            clientcompany = Convert.ToInt32(_configuration.GetSection("storedProcedures")["ClientCompany"]);

            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {

                    using (SqlCommand myCMD = new SqlCommand("SP_BATCH_CLIENT_VIEW", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        myCMD.Parameters.AddWithValue("@batchID", request.BatchID);
                        myCMD.Parameters.AddWithValue("@COMPANY", clientcompany);
                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            clientcardResponse = DataReaderMapToList<clientcardResponse>(myReader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in Client_batchview . ", ex);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return clientcardResponse;
        }
        public Common Batchdata_update(Batchdata_update_request request)
        {
            Common commonmsg = new Common();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_BATCHC_INS", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        myCMD.Parameters.AddWithValue("@PROFILE_ID", request.PROFILE_ID);
                        myCMD.Parameters.AddWithValue("@BATCH_CARDID", request.BATCH_CARDID);
                        myCMD.Parameters.AddWithValue("@REMARK", request.REMARK);
                        myCMD.Parameters.AddWithValue("@CARDSTATUS", request.CARDSTATUS == "" ? DBNull.Value : (object)request.CARDSTATUS);

                        myCMD.Parameters.Add("@VOUT", SqlDbType.VarChar, 100);
                        myCMD.Parameters["@VOUT"].Direction = ParameterDirection.Output;
                        int k = myCMD.ExecuteNonQuery();


                        string str = (string)myCMD.Parameters["@VOUT"].Value;

                        commonmsg.Message = str;
                        commonmsg.Code = k;
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception("Error in Batchdata_update . ", ex);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return commonmsg;
        }
        public List<batchQACommentTableResponse> GetAllBatchQACommentTable(batchQACommentTableRequest request)
        {
            List<batchQACommentTableResponse> allComments = new List<batchQACommentTableResponse>();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_GETALLBATCHQACOMMENTS ", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        SqlParameter[] param = new SqlParameter[1];
                        param[0] = new SqlParameter("@CId", request.CardsID);
                        myCMD.Parameters.Add(param[0]);
                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                batchQACommentTableResponse obj = new batchQACommentTableResponse();
                                obj.UpdatedOn = (DateTime)myReader["updatedon"];
                                obj.UpdatedBy = myReader["updatedby"].ToString();
                                obj.Comment = myReader["Comment"].ToString();
                                obj.cardID = (Int32)myReader["cardsid"];
                                allComments.Add(obj);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in GetAllComment dl. ", ex);
                }
            }
            return allComments;
        }
        public Common BatchQACommentSubmit(batchQACommentSubmitRequest request)
        {
            Common commonmsg = new Common();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_UDPATE_BATCH", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        myCMD.Parameters.AddWithValue("@BATCHID", request.batchID);
                        myCMD.Parameters.AddWithValue("@BATCHCOMMENT", request.remark);
                        myCMD.Parameters.AddWithValue("@PROFILEID", request.updatedBy);
                        myCMD.Parameters.AddWithValue("@BATCHSTATUS", request.cardStatus);
                        myCMD.Parameters.Add("@VOUTPUT", SqlDbType.VarChar, 100);
                        myCMD.Parameters["@VOUTPUT"].Direction = ParameterDirection.Output;
                        int k = myCMD.ExecuteNonQuery();
                        string str = (string)myCMD.Parameters["@VOUTPUT"].Value;

                        commonmsg.Message = str;
                        commonmsg.Code = k;
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception("Error in BatchQACommentSubmit . ", ex);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return commonmsg;
        }
        public Common AssignBatchToQA(batchAssignQARequest request)
        {
            Common commonmsg = new Common();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_ASSIGN_BATCH_TO_QA", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        myCMD.Parameters.AddWithValue("@batchID", request.batchID);
                        myCMD.Parameters.AddWithValue("@ASSIGNTO", request.assignTo);
                        myCMD.Parameters.Add("@VOUT", SqlDbType.VarChar, 100);
                        myCMD.Parameters["@VOUT"].Direction = ParameterDirection.Output;
                        int k = myCMD.ExecuteNonQuery();
                        string str = (string)myCMD.Parameters["@VOUT"].Value;

                        commonmsg.Message = str;
                        commonmsg.Code = k;
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception("Error in AssignBatchToQA . ", ex);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return commonmsg;
        }
        public Common send_invoice(GETINVOICE_DETAILS_REQUEST request)
        {
            Common commonmsg = new Common();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_GENERATE_INVOICE", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        myCMD.Parameters.AddWithValue("@BATCHID", request.BATCHID);
                        myCMD.Parameters.AddWithValue("@INVOICEDBY", request.PROFILE);
                        myCMD.Parameters.Add("@VOUT", SqlDbType.VarChar, 100);
                        myCMD.Parameters["@VOUT"].Direction = ParameterDirection.Output;
                        int k = myCMD.ExecuteNonQuery();
                        string str = (string)myCMD.Parameters["@VOUT"].Value;

                        commonmsg.Message = str;
                        commonmsg.Code = k;
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception("Error in SP_SEND_INVOICE . ", ex);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return commonmsg;
        }
        public commonCardCountsResponse getCommonCardCount(commonCardCountsRequest request)
        {
            commonCardCountsResponse resp = new commonCardCountsResponse();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_COMMON_CARD_COUNT", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        myCMD.Parameters.AddWithValue("@PROFILEID", request.profileID);
                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            while (myReader.Read())
                            {

                                resp.todaysEditorCards = Convert.ToInt32(myReader["Editor_submit_count"]);
                                resp.todayQACards = Convert.ToInt32(myReader["qa_complete_count"]);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in commonCardCount  ", ex);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return resp;

        }
        public List<editorError> getEditorErrors()
        {
            List<editorError> issues = new List<editorError>();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_GET_ERROR_ALL", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            do
                            {
                                while (myReader.Read())
                                {
                                    editorError obj = new editorError();
                                    obj.errorID = (int)myReader["error_id"];
                                    obj.errorName = myReader["error_name"].ToString();
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
        public GET_CLIENT_BATCH_INVOICE_DATA GETINVOICE_DETAILS(GETINVOICE_DETAILS_REQUEST request)
        {



            GET_CLIENT_BATCH_INVOICE_DATA gET_CLIENT_BATCH_INVOICE_DATA = new GET_CLIENT_BATCH_INVOICE_DATA();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_GETINVOICE_DETAILS", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        SqlParameter[] param = new SqlParameter[3];
                        param[0] = new SqlParameter("@PROFILE", request.PROFILE);
                        param[1] = new SqlParameter("@BATCHID", request.BATCHID);
                        param[2] = new SqlParameter("@INVOICEID", request.INVOICEID);
                        myCMD.Parameters.Add(param[0]);
                        myCMD.Parameters.Add(param[1]);
                        myCMD.Parameters.Add(param[2]);
                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {

                            gET_CLIENT_BATCH_INVOICE_DATA.GETINVOICE_DETAILS_s = DataReaderMapToList<GETINVOICE_DETAILS_RESPONSE>(myReader);
                            myReader.NextResult();
                            gET_CLIENT_BATCH_INVOICE_DATA.GETINVOICE_DATA_s = DataReaderMapToList<GETINVOICE_DATA_DETAILS_RESPONSE>(myReader);
                        }

                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in GetAllComment dl. ", ex);
                }
            }
            return gET_CLIENT_BATCH_INVOICE_DATA;
        }
        //public List<ExcelDetails_Response> BatchDetails_for_QA(BatchDetails_for_QA_Request request)
        //{
        //    List<ExcelDetails_Response> allCards = new List<ExcelDetails_Response>();
        //    CommonDl dl = new CommonDl();
        //    using (SqlConnection conn = dl.connect())
        //    {
        //        try
        //        {
        //            using (SqlCommand myCMD = new SqlCommand("SP_SELECT_CARD_QA", conn))
        //            {
        //                myCMD.CommandType = CommandType.StoredProcedure;
        //                SqlParameter[] param = new SqlParameter[1];
        //                param[0] = new SqlParameter("@BATCHID", request.BatchId);
        //                myCMD.Parameters.Add(param[0]);
        //                using (SqlDataReader myReader = myCMD.ExecuteReader())
        //                {
        //                    allCards = DataReaderMapToList<ExcelDetails_Response>(myReader);
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            ExceptionLogging.SendErrorToText(ex);
        //            throw new Exception("Error in fetching details...  supervisorDialogCardDetails---", ex);
        //        }
        //        finally
        //        {
        //            conn.Close();
        //            conn.Dispose();
        //        }
        //    }
        //    return allCards;
        //}
        public List<quickReportResponse> getAllQuickReportSP()
        {
            List<quickReportResponse> quick_report_response = new List<quickReportResponse>();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_ALL_REPORT_SPS", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            quick_report_response = DataReaderMapToList<quickReportResponse>(myReader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in getAllQuickReportSP. ", ex);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return quick_report_response;
        }
        public List<quickReportResponse> getAllQuickReportSPParam()
        {
            List<quickReportResponse> quick_report_response = new List<quickReportResponse>();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_ALL_REPORT_SPSPARAM", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            quick_report_response = DataReaderMapToList<quickReportResponse>(myReader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in getAllQuickReportSP. ", ex);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return quick_report_response;
        }
        public LAST_REVIEWED_RESPONSE GET_LAST_REVIEWED(LAST_REVIEWED_REQUEST request)
        {

            LAST_REVIEWED_RESPONSE lAST_ = new LAST_REVIEWED_RESPONSE();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_GET_LAST_REVIEWED_CLIENT", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        SqlParameter[] param = new SqlParameter[1];
                        param[0] = new SqlParameter("@BATCH_ID", request.BATCHID);
                        myCMD.Parameters.Add(param[0]);

                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                lAST_.CARDSID = Convert.ToInt32(myReader["CARDSID"]);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in GetAllComment dl. ", ex);
                }
            }
            return lAST_;
        }
        public Store_image_Response Store_images(Store_image_Request request)
        {

            Store_image_Response store_Image_Response = new Store_image_Response();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_ISSUE_IMAGE_STORE", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        myCMD.Parameters.AddWithValue("@ISSUE_ID", request.ID);
                        myCMD.Parameters.AddWithValue("@STORED_FILE_NAME", request.STORED_FILE_NAME);
                        myCMD.Parameters.AddWithValue("@STORED_FILE_PATH", request.STORED_FILE_PATH);
                        myCMD.Parameters.AddWithValue("@UPDATED_BY", request.UDPATED_BY);

                        myCMD.Parameters.Add("@VOUTPUT", SqlDbType.VarChar, 100);
                        myCMD.Parameters["@VOUTPUT"].Direction = ParameterDirection.Output;
                        int k = myCMD.ExecuteNonQuery();


                        string str = (string)myCMD.Parameters["@VOUTPUT"].Value;

                        store_Image_Response.Message = str;
                        store_Image_Response.Code = k;


                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in GetAllComment dl. ", ex);
                }
            }
            return store_Image_Response;
        }
        public List<Get_Store_image_Response> GET_IMAGES(Get_Store_image_Request request)
        {

            List<Get_Store_image_Response> get_Store_Image_ResponsesList = new List<Get_Store_image_Response>();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_GET_IMAGES", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        SqlParameter[] param = new SqlParameter[1];
                        param[0] = new SqlParameter("@ISSUEID", request.Issue_id);
                        myCMD.Parameters.Add(param[0]);

                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                Get_Store_image_Response _Store_Image_Response = new Get_Store_image_Response();
                                _Store_Image_Response.STORED_FILE_NAME = myReader["STORED_FILE_NAME"].ToString() == null ? "" : myReader["STORED_FILE_NAME"].ToString();
                                _Store_Image_Response.CARDSID = (Int32)myReader["ISSUE_ID"];
                                _Store_Image_Response.STORED_FILE_PATH = myReader["STORED_FILE_PATH"].ToString() == null ? "" : myReader["STORED_FILE_PATH"].ToString();
                                get_Store_Image_ResponsesList.Add(_Store_Image_Response);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in GetAllComment dl. ", ex);
                }
            }
            return get_Store_Image_ResponsesList;
        }


        public JsonResult GET_IMAGES_JSON(Get_Store_image_Request request)
        {
            List<Get_Store_image_Response> get_Store_Image_ResponsesList = new List<Get_Store_image_Response>();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_GET_IMAGES", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        SqlParameter[] param = new SqlParameter[1];
                        param[0] = new SqlParameter("@ISSUEID", request.Issue_id);
                        myCMD.Parameters.Add(param[0]);
                        DataSet dsdata = new DataSet();
                        SqlDataAdapter sdadata = new SqlDataAdapter(myCMD);
                        sdadata.Fill(dsdata);

                        var responseObject = new
                        {
                            Data = dsdata,
                        };
                        return new JsonResult(responseObject);


                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in fetchQuickReport. ", ex);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }

        }
        public JsonResult GET_GRAPH_VALUES()
        {
            List<Get_Store_image_Response> get_Store_Image_ResponsesList = new List<Get_Store_image_Response>();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_PRODUCTIVITY_COEFFICIENT_DATE", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        DataSet dsdata = new DataSet();
                        SqlDataAdapter sdadata = new SqlDataAdapter(myCMD);
                        sdadata.Fill(dsdata);


                        var responseObject = new
                        {
                            Data = dsdata,
                        };
                        return new JsonResult(responseObject);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in GET_GRAPH_VALUES. ", ex);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }

        }
        public GET_INVOICE_PRINT_DATA_RESPONSE GETINVOICE_PRINT_DETAILS(GETINVOICE_PRINT_DETAILS_REQUEST request)
        {
            GET_INVOICE_PRINT_DATA_RESPONSE gET_INVOICE_PRINT_DATA = new GET_INVOICE_PRINT_DATA_RESPONSE();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_PRINT_INVOICE", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        SqlParameter[] param = new SqlParameter[3];
                        param[0] = new SqlParameter("@PROFILE", request.PROFILE);
                        param[1] = new SqlParameter("@REFERENCENUMBER", request.REFERENCENUMBER);
                        param[2] = new SqlParameter("@INVOICE_NUMBER", request.INVOICE_NUMBER);
                        myCMD.Parameters.Add(param[0]);
                        myCMD.Parameters.Add(param[1]);
                        myCMD.Parameters.Add(param[2]);
                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {

                            gET_INVOICE_PRINT_DATA.GETINVOICE_DETAILS_s = DataReaderMapToList<GETINVOICE_DETAILS_RESPONSE>(myReader);

                            myReader.NextResult();
                            while (myReader.Read())
                            {
                                //gET_INVOICE_PRINT_DATA.PRINT_DETAIL = DataReaderMapToList<GETINVOICE_PRINT_DETAIL_RESPONSE>(myReader)

                                gET_INVOICE_PRINT_DATA.PRINT_DETAIL.BillingAddress1 = (myReader["BillingAddress1"].ToString());
                                gET_INVOICE_PRINT_DATA.PRINT_DETAIL.BillingAddress2 = (myReader["BillingAddress2"].ToString());
                                gET_INVOICE_PRINT_DATA.PRINT_DETAIL.BillingAddress3 = (myReader["BillingAddress3"].ToString());
                                gET_INVOICE_PRINT_DATA.PRINT_DETAIL.BillingAddress4 = (myReader["BillingAddress4"].ToString());
                                gET_INVOICE_PRINT_DATA.PRINT_DETAIL.invoiceNumber = (myReader["invoiceNumber"].ToString());
                                gET_INVOICE_PRINT_DATA.PRINT_DETAIL.COMPANY = (myReader["COMPANY"].ToString());
                                gET_INVOICE_PRINT_DATA.PRINT_DETAIL.FROMDATE = (myReader["FROMDATE"].ToString());
                                gET_INVOICE_PRINT_DATA.PRINT_DETAIL.TODATE = (myReader["TODATE"].ToString());
                                gET_INVOICE_PRINT_DATA.PRINT_DETAIL.GENERATEDATE = (myReader["GENERATEDATE"].ToString());
                                gET_INVOICE_PRINT_DATA.PRINT_DETAIL.REFERENCENUMBER = (myReader["REFERENCENUMBER"].ToString());
                                gET_INVOICE_PRINT_DATA.PRINT_DETAIL.DrawnRate = (myReader["DrawnRate"].ToString());
                                gET_INVOICE_PRINT_DATA.PRINT_DETAIL.NoNDrawnRate = (myReader["NoNDrawnRate"].ToString());
                                gET_INVOICE_PRINT_DATA.PRINT_DETAIL.PayAddress1 = (myReader["PayAddress1"].ToString());
                                gET_INVOICE_PRINT_DATA.PRINT_DETAIL.PayAddress2 = (myReader["PayAddress2"].ToString());
                                gET_INVOICE_PRINT_DATA.PRINT_DETAIL.PayAddress3 = (myReader["PayAddress3"].ToString());
                                gET_INVOICE_PRINT_DATA.PRINT_DETAIL.ceo = (myReader["ceo"].ToString());
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in GetAllComment dl. ", ex);
                }
            }
            return gET_INVOICE_PRINT_DATA;
        }
        public JsonResult fetchQuickReport(fetchQuickReportRequest request)
        {
            List<string> listOfDicts = new List<string>();
            Dictionary<string, List<string>> returnDict = new Dictionary<string, List<string>>();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    string reportName = request.reportName.ToString();
                    using (SqlCommand myCMD = new SqlCommand(reportName, conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        DataSet dsdata = new DataSet();
                        SqlDataAdapter sdadata = new SqlDataAdapter(myCMD);
                        sdadata.Fill(dsdata);

                        var responseObject = new
                        {
                            Data = dsdata,
                        };
                        return new JsonResult(responseObject);


                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in fetchQuickReport. ", ex);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }

        }
        public JsonResult fetchQuickReportparam(fetchQuickReporParamtRequest request)
        {
            List<string> listOfDicts = new List<string>();
            Dictionary<string, List<string>> returnDict = new Dictionary<string, List<string>>();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    string reportName = request.reportHead.ToString();



                    using (SqlCommand myCMD = new SqlCommand(reportName, conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        myCMD.Parameters.AddWithValue("@COMPANY", request.companyid);
                        myCMD.Parameters.AddWithValue("@ROLE", request.roleid);
                        myCMD.Parameters.AddWithValue("@USER", request.userid);
                        myCMD.Parameters.AddWithValue("@DATEFROM", request.DateFrom?.ToString("yyyy-MM-dd"));
                        myCMD.Parameters.AddWithValue("@DATETO", request.DateTo?.ToString("yyyy-MM-dd"));
                        DataSet dsdata = new DataSet();
                        SqlDataAdapter sdadata = new SqlDataAdapter(myCMD);
                        sdadata.Fill(dsdata);
                        var responseObject = new
                        {
                            Data = dsdata,
                        };
                        return new JsonResult(responseObject);


                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in fetchQuickReport. ", ex);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }

        }
        public List<fetchDashboardDetailsReportResponse> fetchDashboardDetailsReport(fetchDashboardDetailsReportRequest request)
        {
            List<fetchDashboardDetailsReportResponse> respo = new List<fetchDashboardDetailsReportResponse>();



            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    string reportName = request.reportName.ToString();
                    using (SqlCommand myCMD = new SqlCommand("SP_GET_DASHBOARD_REPORT_DETAILS", conn))
                    {




                        string fromstring = request.FromDate?.ToString("MM/dd/yyyy");
                        string tostring = request.ToDate?.ToString("MM/dd/yyyy");

                        myCMD.CommandType = CommandType.StoredProcedure;
                        myCMD.Parameters.AddWithValue("@REPORT_HEAD", request.reportName);
                        myCMD.Parameters.AddWithValue("@FROMDATE", fromstring);
                        myCMD.Parameters.AddWithValue("@TODATE", tostring);
                        myCMD.Parameters.AddWithValue("@PROFILEID", request.Profile);
                        myCMD.Parameters.AddWithValue("@ISSUEID", request.IssueID);


                        //DataSet dsdata = new DataSet();
                        //SqlDataAdapter sdadata = new SqlDataAdapter(myCMD);
                        //sdadata.Fill(dsdata);

                        //return new JsonResult()
                        //{
                        //    Data = dsdata,
                        //    JsonRequestBehavior = JsonRequestBehavior.DenyGet
                        //};


                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                fetchDashboardDetailsReportResponse obj = new fetchDashboardDetailsReportResponse();
                                obj.premise = myReader["PREMISE"].ToString();
                                obj.serviceorder = myReader["SERV_ORDER_NO"].ToString();
                                obj.cardurl = myReader["CardUrl"].ToString() == null ? "" : myReader["CardUrl"].ToString();
                                obj.address = myReader["CARDADDRESS"].ToString() == null ? "" : myReader["CARDADDRESS"].ToString();
                                respo.Add(obj);

                            }
                        }


                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in fetchQuickReport. ", ex);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }

                return respo;
            }

        }
        public Common insertEditorErrorToMasterTable(insertEditorErrorRequest request)
        {
            Common commonmsg = new Common();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_ADD_EDITOR_ERROR", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        myCMD.Parameters.AddWithValue("@ERRORNAME", request.errorTypeName);
                        myCMD.Parameters.AddWithValue("@ERRORSUBTYPE", request.errorSubtypeName);
                        myCMD.Parameters.AddWithValue("@CREATEDBY", request.createdBy);
                        myCMD.Parameters.AddWithValue("@SUBTYPE_STATUS", request.subtypeStatus);
                        myCMD.Parameters.AddWithValue("@ERROR_DESC", request.errorTypeDesc);
                        myCMD.Parameters.AddWithValue("@SUBTYPE_DESC", request.errorSubtypeDesc);
                        myCMD.Parameters.AddWithValue("@CRUDACTION", request.requestType);
                        myCMD.Parameters.AddWithValue("@SUBTYPE_EDIT_ID", (int)request.subtypeEditID);
                        myCMD.Parameters.AddWithValue("@COMPANYID", (int)request.companyID);
                        myCMD.Parameters.Add("@VOUTPUT", SqlDbType.VarChar, 100);
                        myCMD.Parameters["@VOUTPUT"].Direction = ParameterDirection.Output;
                        int k = myCMD.ExecuteNonQuery();


                        string str = (string)myCMD.Parameters["@VOUTPUT"].Value;


                        commonmsg.Message = str;
                        commonmsg.Code = k;


                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in insertEditorError ", ex);
                }

            }
            return commonmsg;
        }

        public List<editorErrorListResponse> fetchEditorErrorList()
        {

            List<editorErrorListResponse> resp = new List<editorErrorListResponse>();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_GET_EDITOR_ERROR", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                editorErrorListResponse obj = new editorErrorListResponse();
                                obj.errorName = myReader["ERROR_TYPE_NAME"].ToString() == null ? "" : myReader["ERROR_TYPE_NAME"].ToString();
                                obj.subtypeName = myReader["ERROR_SUBTYPE_NAME"].ToString() == null ? "" : myReader["ERROR_SUBTYPE_NAME"].ToString();
                                obj.errorID = (int)myReader["ERROR_TYPE_ID"];
                                obj.subtypeID = (int)myReader["ERROR_SUBTYPE_ID"];
                                obj.subtypeStatus = myReader["SUBTYPE_STATUS"].ToString() == null ? "" : myReader["SUBTYPE_STATUS"].ToString();
                                obj.subtypeDesc = myReader["SUBTYPE_DESCR"].ToString() == null ? "" : myReader["SUBTYPE_DESCR"].ToString();
                                obj.errorDesc = myReader["DESCR"].ToString() == null ? "" : myReader["DESCR"].ToString();
                                obj.companyID = (int)myReader["COMPANYID"];
                                resp.Add(obj);

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in GetAllComment dl. ", ex);
                }
            }
            return resp;
        }
        public List<systemErrorDropdownList> getSystemIssueDropDown()
        {

            List<systemErrorDropdownList> resp = new List<systemErrorDropdownList>();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_GET_SYSTEM_APPISSUETYPE", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                systemErrorDropdownList obj = new systemErrorDropdownList();
                                obj.ISSUE_ID = (int)myReader["ISSUE_ID"];
                                obj.ISSUE_NAME = myReader["ISSUE_NAME"].ToString() == null ? "" : myReader["ISSUE_NAME"].ToString();
                                resp.Add(obj);

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in getIssueDropDown. ", ex);
                }
            }
            return resp;
        }

        public List<workTypeResponse> getMyWorkTypes()
        {

            List<workTypeResponse> resp = new List<workTypeResponse>();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_GET_WORK_TYPES", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {

                            while (myReader.Read())
                            {


                                workTypeResponse obj = new workTypeResponse();
                                obj.worktypevalue = (int)myReader["worktypevalue"];
                                obj.slnumber = (int)myReader["slnumber"];
                                obj.worktypename = (string)myReader["worktypename"];
                                obj.TASKID = (int)myReader["TASKID"];
                                obj.WORKTYPEDESCRIPTION = (string)myReader["WORKTYPEDESCRIPTION"].ToString();

                                resp.Add(obj);

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in getMyWorkTypes. ", ex);
                }
            }
            return resp;
        }
        public testScriptingResponseById GetScriptingRecordById(int scriptId)
        {
            testScriptingResponseById response = null;
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("SP_GET_SCRIPTING_RECORD_BY_ID", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@scriptId", scriptId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                response = new testScriptingResponseById
                                {
                                    TS_ID = reader.IsDBNull(reader.GetOrdinal("ts_id")) ? 0 : reader.GetInt32(reader.GetOrdinal("ts_id")),
                                    TS_DATE = (DateTime)reader["TS_DATE"],
                                    PROJECTNAME = reader.IsDBNull(reader.GetOrdinal("PROJECTNAME")) ? string.Empty : reader["PROJECTNAME"].ToString(),
                                    TASK_ID = reader.IsDBNull(reader.GetOrdinal("TASK_ID")) ? string.Empty : reader["TASK_ID"].ToString(),
                                    TESTPLANNAME = reader.IsDBNull(reader.GetOrdinal("TESTPLANNAME")) ? string.Empty : reader["TESTPLANNAME"].ToString(),
                                    NUM_STEPS_ADDED_REVIEWED = reader.IsDBNull(reader.GetOrdinal("NUM_STEPS_ADDED_REVIEWED")) ? 0 : reader.GetInt32(reader.GetOrdinal("NUM_STEPS_ADDED_REVIEWED")),
                                    NUM_CONFIG_ADDED_REVIEWED = reader.IsDBNull(reader.GetOrdinal("NUM_CONFIG_ADDED_REVIEWED")) ? 0 : reader.GetInt32(reader.GetOrdinal("NUM_CONFIG_ADDED_REVIEWED")),
                                    TIME_SPENT_IN_HOURS = reader.IsDBNull(reader.GetOrdinal("TIME_SPENT_IN_HOURS")) ? 0m : reader.GetDecimal(reader.GetOrdinal("TIME_SPENT_IN_HOURS")),
                                    REMARK = reader.IsDBNull(reader.GetOrdinal("REMARK")) ? string.Empty : reader["REMARK"].ToString(),
                                    STATE_ID = reader.IsDBNull(reader.GetOrdinal("state_id")) ? 0 : reader.GetInt32(reader.GetOrdinal("state_id")),
                                    TEST_CASE_STATUSID = reader.IsDBNull(reader.GetOrdinal("TEST_CASE_STATUSID")) ? 0 : reader.GetInt32(reader.GetOrdinal("TEST_CASE_STATUSID"))
                                };
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in GetScriptingRecordById. ", ex);
                }
            }

            return response;
        }
        public List<testScriptingResponse> getTestScriptingRecords(testScriptingRequest req)
        {

            List<testScriptingResponse> resp = new List<testScriptingResponse>();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    //using (SqlCommand myCMD = new SqlCommand("SP_GET_TEST_SCRIPTING_RECORDS", conn))
                    using (SqlCommand myCMD = new SqlCommand("SP_getTestScrpitRecordsInTest", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        myCMD.Parameters.AddWithValue("@created_by", req.created_by);
                        myCMD.Parameters.AddWithValue("@startDate", req.startDate);
                        myCMD.Parameters.AddWithValue("@endDate", req.endDate);
                        myCMD.Parameters.AddWithValue("@projectName", req.PROJECTNAME);
                        myCMD.Parameters.AddWithValue("@taskId", req.TASK_ID);
                        myCMD.Parameters.AddWithValue("@testPlanName", req.TESTPLANNAME);
                        myCMD.Parameters.AddWithValue("@numStepsAddedReviewed", req.NUM_STEPS_ADDED_REVIEWED);
                        myCMD.Parameters.AddWithValue("@numConfigAddedReviewed", req.NUM_CONFIG_ADDED_REVIEWED);
                        myCMD.Parameters.AddWithValue("@timeSpentInHours", req.TIME_SPENT_IN_HOURS);
                        myCMD.Parameters.AddWithValue("@remark", req.REMARK);
                        myCMD.Parameters.AddWithValue("@sts", req.status);
                        myCMD.Parameters.AddWithValue("@businessUnit", req.businessUnit);
                        myCMD.Parameters.AddWithValue("@userId", req.userSelection != null ? req.userSelection : "");
                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {

                            while (myReader.Read())
                            {
                                testScriptingResponse obj = new testScriptingResponse();
                                obj.TS_ID = myReader.IsDBNull(myReader.GetOrdinal("TS_ID")) ? 0 : myReader.GetInt32(myReader.GetOrdinal("TS_ID"));
                                obj.TS_DATE = myReader.IsDBNull(myReader.GetOrdinal("TS_DATE")) ? string.Empty : myReader["TS_DATE"].ToString();
                                obj.TASK_ID = myReader.IsDBNull(myReader.GetOrdinal("TASK_ID")) ? string.Empty : myReader["TASK_ID"].ToString();
                                obj.PROJECTNAME = myReader.IsDBNull(myReader.GetOrdinal("PROJECTNAME")) ? string.Empty : myReader["PROJECTNAME"].ToString();
                                obj.TESTPLANNAME = myReader.IsDBNull(myReader.GetOrdinal("TESTPLANNAME")) ? string.Empty : myReader["TESTPLANNAME"].ToString();
                                obj.NUM_STEPS_ADDED_REVIEWED = myReader.IsDBNull(myReader.GetOrdinal("NUM_STEPS_ADDED_REVIEWED")) ? 0 : myReader.GetInt32(myReader.GetOrdinal("NUM_STEPS_ADDED_REVIEWED"));
                                obj.NUM_CONFIG_ADDED_REVIEWED = myReader.IsDBNull(myReader.GetOrdinal("NUM_CONFIG_ADDED_REVIEWED")) ? 0 : myReader.GetInt32(myReader.GetOrdinal("NUM_CONFIG_ADDED_REVIEWED"));
                                obj.TIME_SPENT_IN_HOURS = myReader.IsDBNull(myReader.GetOrdinal("TIME_SPENT_IN_HOURS")) ? 0m : myReader.GetDecimal(myReader.GetOrdinal("TIME_SPENT_IN_HOURS"));
                                obj.REMARK = myReader.IsDBNull(myReader.GetOrdinal("REMARK")) ? string.Empty : myReader["REMARK"].ToString();
                                obj.CREATED_BY = myReader.IsDBNull(myReader.GetOrdinal("CREATED_BY")) ? string.Empty : myReader["CREATED_BY"].ToString();
                                obj.BusinessUnit = myReader.IsDBNull(myReader.GetOrdinal("BUSINESS_UNIT")) ? string.Empty : myReader["BUSINESS_UNIT"].ToString();
                                obj.STATE_ID = myReader.IsDBNull(myReader.GetOrdinal("STATE_ID")) ? 0 : myReader.GetInt32(myReader.GetOrdinal("STATE_ID"));
                                obj.StatusID = myReader.IsDBNull(myReader.GetOrdinal("CARD_STATUS")) ? 0 : myReader.GetInt32(myReader.GetOrdinal("CARD_STATUS"));
                                obj.StatusName = myReader.IsDBNull(myReader.GetOrdinal("CARD_DESCRIPTION")) ? string.Empty : myReader["CARD_DESCRIPTION"].ToString();
                                resp.Add(obj);
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in getTestScriptingRecords. ", ex);
                }
            }
            return resp;
        }
        //Only to selectc the all the data on the first load
        public List<executionTestResponse> getExecutionTestRecords(executionTestRequest req)
        {

            List<executionTestResponse> resp = new List<executionTestResponse>();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_GET_EXECUTION_TEST_RECORDS", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        myCMD.Parameters.AddWithValue("@created_by", req.created_by);
                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                executionTestResponse obj = new executionTestResponse();
                                obj.Et_Id = (int)myReader["id"];
                                obj.Project_Name = (string)myReader["project_name"].ToString();
                                obj.Entry_Date = (string)myReader["entry_date"];
                                obj.Task_Id = (int)myReader["task_id"];
                                obj.Task_Text = myReader["Task_name"].ToString();
                                obj.Test_Plan_Name = (string)myReader["test_plan_name"].ToString();
                                obj.Total_Num_Steps = (int)myReader["total_num_steps"];
                                obj.Num_Steps_Executed = (int)myReader["num_steps_executed"];

                                obj.Num_Defects_Logged = (int)myReader["num_defects_logged"];
                                if (!myReader.IsDBNull(8))
                                    obj.Num_Sev1_Defects = (int)myReader["num_sev1_defects"];
                                if (!myReader.IsDBNull(9))
                                    obj.Num_Sev2_Defects = (int)myReader["num_sev2_defects"];
                                if (!myReader.IsDBNull(10))
                                    obj.Num_Sev3_Defects = (int)myReader["num_sev3_defects"];
                                obj.Time_Spent_In_Hours = (decimal)myReader["time_spent_in_hours"];
                                obj.Remark = (string)myReader["remark"].ToString();
                                obj.created_by = (string)myReader["created_by"].ToString();
                                obj.BusinessUnit = (string)myReader["BUSINESS_UNIT"].ToString();
                                obj.STATE_ID = (int)myReader["STATE_ID"];
                                obj.StatusName = (string)myReader["StatusName"].ToString();
                                obj.StatusID = Convert.ToInt32(myReader["StatusID"].ToString() == "" ? "0" : myReader["StatusID"]);


                                resp.Add(obj);

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in getIssueDropDown. ", ex);
                }
            }
            return resp;
        }
        //Only to execute when the search button is clicked
        public List<executionTestResponse> getFilteredExecutionTestRecords(executionTestRequestForSearch req)
        {

            List<executionTestResponse> resp = new List<executionTestResponse>();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_SEARCH_TEST_EXECUTION_RECORDS", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        myCMD.Parameters.AddWithValue("@created_by", req.created_by);
                        myCMD.Parameters.AddWithValue("@TODATE", req.endDate);
                        myCMD.Parameters.AddWithValue("@FROMDATE", req.startDate);
                        myCMD.Parameters.AddWithValue("@testPlanName", req.test_plan_name);
                        myCMD.Parameters.AddWithValue("@projectName", req.project_name);
                        myCMD.Parameters.AddWithValue("@totalNumSteps", req.total_num_steps);
                        myCMD.Parameters.AddWithValue("@taskId", req.task_id);
                        myCMD.Parameters.AddWithValue("@numberOfDefectsLogged", req.num_defects_logged);
                        myCMD.Parameters.AddWithValue("@numberOfStepsExecuted", req.num_steps_executed);
                        myCMD.Parameters.AddWithValue("@numOfSev3Defects", req.num_sev3_defects);
                        myCMD.Parameters.AddWithValue("@numOfSev2Defects", req.num_sev2_defects);
                        myCMD.Parameters.AddWithValue("@numOfSev1Defects", req.num_sev1_defects);
                        myCMD.Parameters.AddWithValue("@remark", req.remark);
                        myCMD.Parameters.AddWithValue("@timeSpentInHours", req.time_spent_in_hours);
                        myCMD.Parameters.AddWithValue("@sts", req.Test_case_statusID);
                        myCMD.Parameters.AddWithValue("@bid", req.Bid);
                        myCMD.Parameters.AddWithValue("@userId", req.userSearch != null ? req.userSearch : "");
                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                executionTestResponse obj = new executionTestResponse();
                                obj.Et_Id = (int)myReader["id"];
                                obj.Project_Name = (string)myReader["project_name"].ToString();
                                obj.Entry_Date = (string)myReader["entry_date"];
                                obj.Task_Id = (int)myReader["task_id"];
                                obj.Task_Text = (string)myReader["Task_name"].ToString();
                                obj.Test_Plan_Name = (string)myReader["test_plan_name"].ToString();
                                obj.Total_Num_Steps = (int)myReader["total_num_steps"];
                                obj.Num_Steps_Executed = (int)myReader["num_steps_executed"];

                                obj.Num_Defects_Logged = (int)myReader["num_defects_logged"];
                                if (!myReader.IsDBNull(8))
                                    obj.Num_Sev1_Defects = (int)myReader["num_sev1_defects"];
                                if (!myReader.IsDBNull(9))
                                    obj.Num_Sev2_Defects = (int)myReader["num_sev2_defects"];
                                if (!myReader.IsDBNull(10))
                                    obj.Num_Sev3_Defects = (int)myReader["num_sev3_defects"];
                                obj.Time_Spent_In_Hours = (decimal)myReader["time_spent_in_hours"];
                                obj.Remark = (string)myReader["remark"].ToString();
                                obj.created_by = (string)myReader["created_by"].ToString();
                                obj.BusinessUnit = (string)myReader["BUSINESS_UNIT"].ToString();
                                obj.STATE_ID = (int)myReader["STATE_ID"];
                                obj.StatusName = (string)myReader["StatusName"].ToString();
                                obj.StatusID = Convert.ToInt32(myReader["StatusID"].ToString() == "" ? "0" : myReader["StatusID"]);


                                resp.Add(obj);

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in search of test execution data. ", ex);
                }
            }
            return resp;
        }
        public List<executionTestResponseById> getTextExecutionById(int etid)
        {

            List<executionTestResponseById> resp = new List<executionTestResponseById>();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_GET_TEST_EXECUTION_RECORD_BY_ID", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        myCMD.Parameters.AddWithValue("@testExecutionId", etid);

                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                executionTestResponseById obj = new executionTestResponseById();
                                obj.id = (int)myReader["id"];
                                obj.project_name = (int)myReader["project_name"];
                                obj.entry_date = (DateTime)myReader["entry_date"];
                                obj.task_id = (int)myReader["task_id"];
                                obj.test_plan_name = (string)myReader["test_plan_name"].ToString();
                                obj.total_num_steps = (int)myReader["total_num_steps"];
                                obj.num_steps_executed = (int)myReader["num_steps_executed"];

                                obj.num_defects_logged = (int)myReader["num_defects_logged"];
                                if (!myReader.IsDBNull(8))
                                    obj.num_sev1_defects = (int)myReader["num_sev1_defects"];
                                if (!myReader.IsDBNull(9))
                                    obj.num_sev2_defects = (int)myReader["num_sev2_defects"];
                                if (!myReader.IsDBNull(10))
                                    obj.num_sev3_defects = (int)myReader["num_sev3_defects"];
                                obj.time_spent_in_hours = (decimal)myReader["time_spent_in_hours"];
                                obj.remark = (string)myReader["remark"].ToString();
                                obj.created_by = (string)myReader["created_by"].ToString();
                                obj.bid = (int)myReader["bid"];
                                obj.StatusID = Convert.ToInt32(myReader["StatusID"].ToString() == "" ? "0" : myReader["StatusID"]);


                                resp.Add(obj);

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in search of test execution data. ", ex);
                }
            }
            return resp;
        }

        public List<caseRecordResponse> getCaseRecords(caseRecordRequest req)
        {

            List<caseRecordResponse> resp = new List<caseRecordResponse>();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_GET_CASE_RECORD_TASK", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {

                            while (myReader.Read())
                            {

                                caseRecordResponse obj = new caseRecordResponse();
                                obj.RECORD_TASK_ID = (int)myReader["RECORD_TASK_ID"];
                                if (!myReader.IsDBNull(1))
                                    obj.RECORD_ID = (int)myReader["RECORD_ID"];
                                obj.TASK_ID = (int)myReader["TASK_ID"];

                                obj.RECORD_COUNT = (int)myReader["RECORD_COUNT"];
                                obj.RECORD_ADDITION_DATE = (string)myReader["RECORD_ADDITION_DATE"].ToString();

                                resp.Add(obj);

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in getIssueDropDown. ", ex);
                }
            }
            return resp;
        }

        public List<masterSetupList> getMasterSetupTables()
        {

            List<masterSetupList> resp = new List<masterSetupList>();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_master_setup", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                masterSetupList obj = new masterSetupList();
                                obj.tablename = myReader["tablename"].ToString();
                                resp.Add(obj);

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in getIssueDropDown. ", ex);
                }
            }
            return resp;
        }
        public List<ppnmResponse> getPpnmRecords()
        {

            List<ppnmResponse> resp = new List<ppnmResponse>();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_GET_PPNM_RECORDS", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                ppnmResponse obj = new ppnmResponse();
                                obj.PPNM_ID = (int)myReader["ID"];
                                obj.plan_name = myReader["plan_name"].ToString() == null ? "" : myReader["plan_name"].ToString();



                                resp.Add(obj);

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in getPpnmRecords. ", ex);
                }
            }
            return resp;
        }
        public List<taskDropdownList> getTaskList()
        {

            List<taskDropdownList> resp = new List<taskDropdownList>();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_GETTASKLIST", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                taskDropdownList obj = new taskDropdownList();
                                obj.TASKID = (int)myReader["TASKID"];
                                obj.TASKTEXT = myReader["TASKTEXT"].ToString() == null ? "" : myReader["TASKTEXT"].ToString();
                                resp.Add(obj);

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in getIssueDropDown. ", ex);
                }
            }
            return resp;
        }
        public List<systemIssueStatusrDropdownList> systemIssueStatusrDropdownList()
        {

            List<systemIssueStatusrDropdownList> resp = new List<systemIssueStatusrDropdownList>();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_GET_SYSTEM_ISSUE_STATUS", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                systemIssueStatusrDropdownList obj = new systemIssueStatusrDropdownList();
                                obj.SYID = (int)myReader["SYID"];
                                obj.SName = myReader["SNAME"].ToString() == null ? "" : myReader["SName"].ToString();



                                resp.Add(obj);

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in getIssueDropDown. ", ex);
                }
            }
            return resp;
        }

        public List<getSystemIssueGridResponse> GetSystemIssueGrid(getSystemIssueGridRequest req)
        {
            List<getSystemIssueGridResponse> response = new List<getSystemIssueGridResponse>();
            try
            {
                using (SqlConnection conn = _commonDl.Connect())
                using (SqlCommand cmd = new SqlCommand("SP_GET_SYSTEM_ISSUE_DATA", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserId", req.UserId);
                    System.Diagnostics.Debug.Write("mysrid===" + req.UserId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            getSystemIssueGridResponse obj = new getSystemIssueGridResponse();
                            obj.ISSUE_ID = (int)reader["ISSUE_ID"];
                            obj.ISSUE_TYPE = (int)reader["ISSUE_TYPE"];
                            obj.ISSUE_NAME = reader["ISSUE_NAME"].ToString() ?? "";
                            obj.ISSUE_RAISED_BY = reader["ISSUE_RAISED_BY"].ToString() ?? "";
                            obj.PRIORITY_ISSUE = reader["PRIORITY_ISSUE"].ToString() ?? "";
                            obj.IssueRegisterdate = reader["IssueRegisterdate"].ToString() ?? "";
                            // obj.SYSTEM_STATUS = reader["SYSTEM_STATUS_NAME"].ToString() ?? "";

                            obj.IssueRegisterdate = reader["IssueRegisterdate"].ToString() ?? "";
                            obj.slnumber = (int)reader["slnumber"];
                            obj.ISSUE_ID = (int)reader["ISSUE_ID"];
                            obj.slnumber = (int)reader["slnumber"];

                            obj.COMPANY = (int)reader["COMPANY"];
                            obj.SName = reader["SName"].ToString() ?? "";
                            obj.ISSUE_DESC = reader["ISSUE_DESC"].ToString() ?? "";
                            obj.ISSUE_ASSIGN_TO = reader["ISSUE_ASSIGN_TO"].ToString() ?? "";
                            obj.FullName = reader["FullName"].ToString() ?? "";
                            obj.CName = reader["CName"].ToString() ?? "";
                            obj.firstName = reader["firstName"].ToString() ?? "";
                            obj.countOfPicures = (int)(reader["CountOfPictures"] ?? 0);
                            System.Diagnostics.Debug.Write("myobj===" + obj);

                            response.Add(obj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GetSystemIssueGrid.", ex);
            }

            return response;
        }
        public string fetchEditorErrorsForQA(fetchEditorErrorsForQARequest request)
        {
            string json = "";
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_GET_EDITOR_ERRORS_QA_PAGE", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        myCMD.Parameters.AddWithValue("@COMPANYID", request.companyID);
                        DataTable table = new DataTable();
                        SqlDataAdapter adapter = new SqlDataAdapter(myCMD);
                        adapter.Fill(table);



                        List<errorNestedJson> mainObjects = new List<errorNestedJson>();
                        foreach (DataRow row in table.Rows)
                        {
                            string ename = row["ERROR_TYPE_NAME"].ToString();

                            errorNestedJson mainObject = mainObjects.FirstOrDefault(obj => obj.errorName == ename);

                            if (mainObject == null)
                            {
                                mainObject = new errorNestedJson
                                {
                                    errorName = ename,
                                    subtypeArray = new List<subtypeArray>()
                                };

                                mainObjects.Add(mainObject);
                            }

                            subtypeArray nestedObject = new subtypeArray
                            {
                                subtypeID = (int)row["ERROR_SUBTYPE_ID"],
                                subtypeName = row["ERROR_SUBTYPE_NAME"].ToString(),
                            };

                            mainObject.subtypeArray.Add(nestedObject);
                        }
                        json = JsonConvert.SerializeObject(mainObjects);
                    }
                    return json;

                }
                catch (Exception ex)
                {
                    throw new Exception("Error in fetchEditorErrorsForQA. ", ex);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }

        }
        public Common sendEditorErrors(qaPageEditorErrorRequest request)
        {
            Common commonmsg = new Common();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_SEND_EDITOR_ERROR_BY_QA", conn))
                    {
                        int[] numbers = request.errorArray;
                        string[] result = Array.ConvertAll(numbers, x => x.ToString());
                        var subtypeString = string.Empty;
                        foreach (var item in result)
                        {
                            subtypeString += item + ",";
                        }
                        subtypeString = subtypeString.TrimEnd(',');
                        myCMD.CommandType = CommandType.StoredProcedure;
                        myCMD.Parameters.AddWithValue("@CARDID", (int)request.card_id);
                        myCMD.Parameters.AddWithValue("@EDITORID", request.editorID);
                        myCMD.Parameters.AddWithValue("@QC_BY", request.QC_BY);
                        myCMD.Parameters.AddWithValue("@REMARK", request.txtRemark);
                        myCMD.Parameters.AddWithValue("@CARDSTATUS", request.CardStatus);
                        myCMD.Parameters.AddWithValue("@LOGGEDINUSER", request.LoggedInUser);
                        myCMD.Parameters.AddWithValue("@SUBTYPE_ARRAY", subtypeString);
                        myCMD.Parameters.Add("@VOUTPUT", SqlDbType.VarChar, 100);
                        myCMD.Parameters["@VOUTPUT"].Direction = ParameterDirection.Output;
                        int k = myCMD.ExecuteNonQuery();


                        string str = (string)myCMD.Parameters["@VOUTPUT"].Value;


                        commonmsg.Message = str;
                        commonmsg.Code = k;


                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in sendEditorErrors ", ex);
                }

            }
            return commonmsg;
        }


        public Common addTaskTypeRecord(TaskTypeInsertUpdateRequest request)
        {
            Common commonmsg = new Common();
            using (SqlConnection conn = _commonDl.Connect())
            {
                using (SqlCommand myCMD = new SqlCommand("sp_task_type_ins_update", conn))
                {
                    myCMD.CommandType = CommandType.StoredProcedure;
                    myCMD.Parameters.AddWithValue("@TID", request.TID);
                    myCMD.Parameters.AddWithValue("@TASKTEXT", request.TASKTEXT);
                    myCMD.Parameters.AddWithValue("@STATEMENT", request.STATEMENT);

                    System.Diagnostics.Debug.Write("Helmy rec add date isss22!" +
             request.TID + request.TASKTEXT + request.STATEMENT);
                    //     myCMD.Parameters.AddWithValue("@CREATION_DATE", "2023-11-13");
                    myCMD.Parameters.Add("@VOUTPUT", SqlDbType.VarChar, 100);
                    myCMD.Parameters["@VOUTPUT"].Direction = ParameterDirection.Output;

                    int k = myCMD.ExecuteNonQuery();

                    commonmsg.Message = myCMD.Parameters["@VOUTPUT"].Value.ToString();

                    //     System.Diagnostics.Debug.Write("Hello via Debug!" + k);
                    //     
                    commonmsg.Code = k;
                }
            }
            return commonmsg;
        }
        public Common addWorkTypeRecord(WorkTypeInsertUpdateRequest request)
        {
            Common commonmsg = new Common();
            using (SqlConnection conn = _commonDl.Connect())
            {
                using (SqlCommand myCMD = new SqlCommand("sp_work_type_ins_update", conn))
                {
                    myCMD.CommandType = CommandType.StoredProcedure;
                    myCMD.Parameters.AddWithValue("@WTV", request.WTV);
                    myCMD.Parameters.AddWithValue("@TASKID", request.TASKID);
                    myCMD.Parameters.AddWithValue("@worktypename", request.worktypename);
                    myCMD.Parameters.AddWithValue("@WORKTYPEDESCRIPTION", request.WORKTYPEDESCRIPTION);
                    myCMD.Parameters.AddWithValue("@STATEMENT", request.STATEMENT);

                    System.Diagnostics.Debug.Write("Helmy rec add date isss22!" +
             request.WTV + request.TASKID + request.worktypename + request.WORKTYPEDESCRIPTION + request.STATEMENT);
                    //     myCMD.Parameters.AddWithValue("@CREATION_DATE", "2023-11-13");
                    myCMD.Parameters.Add("@VOUTPUT", SqlDbType.VarChar, 100);
                    myCMD.Parameters["@VOUTPUT"].Direction = ParameterDirection.Output;

                    int k = myCMD.ExecuteNonQuery();

                    commonmsg.Message = myCMD.Parameters["@VOUTPUT"].Value.ToString();

                    //     System.Diagnostics.Debug.Write("Hello via Debug!" + k);
                    //     
                    commonmsg.Code = k;
                }
            }
            return commonmsg;
        }
        public Common addCaseRecord(CaseRecordInsertUpdateRequest request)
        {
            Common commonmsg = new Common();
            using (SqlConnection conn = _commonDl.Connect())
            {
                using (SqlCommand myCMD = new SqlCommand("sp_case_record_ins_update", conn))
                {
                    myCMD.CommandType = CommandType.StoredProcedure;
                    myCMD.Parameters.AddWithValue("@RTASKID", request.rtaskid);
                    myCMD.Parameters.AddWithValue("@TASK_ID", request.task_id);
                    myCMD.Parameters.AddWithValue("@RECORD_ID", request.record_id);
                    myCMD.Parameters.AddWithValue("@RECORD_COUNT", request.record_count);
                    myCMD.Parameters.AddWithValue("@STATEMENT", request.statement);
                    myCMD.Parameters.AddWithValue("@RECORD_ADDITION_DATE", request.record_addition_date);
                    System.Diagnostics.Debug.Write("Helmy rec add date isss22!" + request.record_addition_date);
                    //     myCMD.Parameters.AddWithValue("@CREATION_DATE", "2023-11-13");
                    myCMD.Parameters.Add("@VOUTPUT", SqlDbType.VarChar, 100);
                    myCMD.Parameters["@VOUTPUT"].Direction = ParameterDirection.Output;

                    int k = myCMD.ExecuteNonQuery();

                    commonmsg.Message = myCMD.Parameters["@VOUTPUT"].Value.ToString();

                    //     System.Diagnostics.Debug.Write("Hello via Debug!" + k);
                    //     
                    commonmsg.Code = k;
                }
            }
            return commonmsg;
        }

        public Common addPpnmRecord(ppnmInsertUpdateRequest request)
        {
            Common commonmsg = new Common();
            using (SqlConnection conn = _commonDl.Connect())
            {
                using (SqlCommand myCMD = new SqlCommand("sp_ppnm_record_ins_update", conn))
                {
                    myCMD.CommandType = CommandType.StoredProcedure;
                    myCMD.Parameters.AddWithValue("@PPNM_ID", request.PPNM_ID);
                    myCMD.Parameters.AddWithValue("@PLAN_NAME", request.PLAN_NAME);
                    myCMD.Parameters.AddWithValue("@STATEMENT", request.STATEMENT);
                    System.Diagnostics.Debug.Write("Helmy rec add date isss22!" + request.PLAN_NAME);
                    myCMD.Parameters.Add("@VOUTPUT", SqlDbType.VarChar, 100);
                    myCMD.Parameters["@VOUTPUT"].Direction = ParameterDirection.Output;
                    int k = myCMD.ExecuteNonQuery();
                    commonmsg.Message = myCMD.Parameters["@VOUTPUT"].Value.ToString();
                    commonmsg.Code = k;
                }
            }
            return commonmsg;
        }

        public Common addExecutionTestRecord(ExecutionTestInsertUpdateRequest request)
        {
            Common commonmsg = new Common();
            using (SqlConnection conn = _commonDl.Connect())
            {
                using (SqlCommand myCMD = new SqlCommand("sp_execution_test_ins_update", conn))
                {
                    myCMD.CommandType = CommandType.StoredProcedure;
                    myCMD.Parameters.AddWithValue("@ET_ID", request.et_id);
                    myCMD.Parameters.AddWithValue("@TASK_ID", request.task_id);
                    myCMD.Parameters.AddWithValue("@PROJECT_NAME", request.project_name);
                    myCMD.Parameters.AddWithValue("@ENTRY_DATE", request.entry_date);
                    myCMD.Parameters.AddWithValue("@TEST_PLAN_NAME", request.test_plan_name);
                    myCMD.Parameters.AddWithValue("@TOTAL_NUM_STEPS", request.total_num_steps);
                    myCMD.Parameters.AddWithValue("@NUM_STEPS_EXECUTED", request.num_steps_executed);
                    myCMD.Parameters.AddWithValue("@NUM_DEFECTS_LOGGED", request.num_defects_logged);
                    myCMD.Parameters.AddWithValue("@NUM_SEV1_DEFECTS", request.num_sev1_defects);
                    myCMD.Parameters.AddWithValue("@NUM_SEV2_DEFECTS", request.num_sev2_defects);
                    myCMD.Parameters.AddWithValue("@NUM_SEV3_DEFECTS", request.num_sev3_defects);
                    myCMD.Parameters.AddWithValue("@Bid", request.Bid);
                    myCMD.Parameters.AddWithValue("@TIME_SPENT_IN_HOURS", request.time_spent_in_hours);
                    myCMD.Parameters.AddWithValue("@REMARK", request.remark);
                    myCMD.Parameters.AddWithValue("@CREATED_BY", request.created_by);
                    myCMD.Parameters.AddWithValue("@STATEMENT", request.statement);
                    myCMD.Parameters.AddWithValue("@Test_case_statusID", request.Test_case_statusID);

                    myCMD.Parameters.Add("@VOUTPUT", SqlDbType.VarChar, 100);
                    myCMD.Parameters["@VOUTPUT"].Direction = ParameterDirection.Output;
                    int k = myCMD.ExecuteNonQuery();
                    commonmsg.Message = myCMD.Parameters["@VOUTPUT"].Value.ToString();
                    commonmsg.Code = k;
                }
            }
            return commonmsg;
        }

        public Common addTestScriptingRecord(TestScriptingInsertUpdateRequest request)
        {
            Common commonmsg = new Common();
            using (SqlConnection conn = _commonDl.Connect())
            {
                using (SqlCommand myCMD = new SqlCommand("sp_test_scripting_ins_update", conn))
                {
                    myCMD.CommandType = CommandType.StoredProcedure;
                    myCMD.Parameters.AddWithValue("@TS_ID", request.TS_ID);
                    myCMD.Parameters.AddWithValue("@TASK_ID", request.TASK_ID);
                    myCMD.Parameters.AddWithValue("@TS_DATE", request.TS_DATE);
                    myCMD.Parameters.AddWithValue("@PROJECTNAME", request.PROJECTNAME);
                    myCMD.Parameters.AddWithValue("@STATEMENT", request.STATEMENT);
                    myCMD.Parameters.AddWithValue("@TESTPLANNAME", request.TESTPLANNAME);
                    myCMD.Parameters.AddWithValue("@NUM_STEPS_ADDED_REVIEWED", request.NUM_STEPS_ADDED_REVIEWED);
                    myCMD.Parameters.AddWithValue("@NUM_CONFIG_ADDED_REVIEWED", request.NUM_CONFIG_ADDED_REVIEWED);
                    myCMD.Parameters.AddWithValue("@TIME_SPENT_IN_HOURS", request.TIME_SPENT_IN_HOURS);
                    myCMD.Parameters.AddWithValue("@REMARK", request.REMARK);
                    myCMD.Parameters.AddWithValue("@CREATED_BY", request.created_by);
                    myCMD.Parameters.AddWithValue("@BID", request.BID);
                    myCMD.Parameters.AddWithValue("@Test_case_statusID", request.Test_case_statusID);
                    myCMD.Parameters.Add("@VOUTPUT", SqlDbType.VarChar, 100);
                    myCMD.Parameters["@VOUTPUT"].Direction = ParameterDirection.Output;
                    int k = myCMD.ExecuteNonQuery();
                    commonmsg.Message = myCMD.Parameters["@VOUTPUT"].Value.ToString();
                    commonmsg.Code = k;
                }
            }
            return commonmsg;
        }
        public Common InsertUpdateIssueData(SystemErrorInsertRequest request)
        {
            Common commonmsg = new Common();
            using (SqlConnection conn = _commonDl.Connect())
            {
                using (SqlCommand myCMD = new SqlCommand("Sp_System_Issues_Register", conn))
                {
                    myCMD.CommandType = CommandType.StoredProcedure;
                    myCMD.Parameters.AddWithValue("@ISSUEID", request.issueID);
                    myCMD.Parameters.AddWithValue("@ISSUE_TYPE", request.IssueType);
                    myCMD.Parameters.AddWithValue("@ISSUE_DESC", request.IssueDesc);
                    myCMD.Parameters.AddWithValue("@PRIORITY_ISSUE", request.Priority);
                    myCMD.Parameters.AddWithValue("@SYSTEM_STATUS", request.status);
                    myCMD.Parameters.AddWithValue("@ISSUE_ASSIGN_TO", request.AssignTo);
                    myCMD.Parameters.AddWithValue("@ISSUE_RAISED_BY", request.IssueRaisedBy);
                    myCMD.Parameters.AddWithValue("@COMPANY", request.CompanyId);
                    myCMD.Parameters.AddWithValue("@STATEMENT", request.operation);
                    myCMD.Parameters.Add("@VOUTPUT", SqlDbType.VarChar, 100);
                    myCMD.Parameters["@VOUTPUT"].Direction = ParameterDirection.Output;

                    int k = myCMD.ExecuteNonQuery();

                    commonmsg.Message = myCMD.Parameters["@VOUTPUT"].Value.ToString();
                    commonmsg.Code = k;
                }
            }
            return commonmsg;
        }
        public string showErrorsForEditor(showEditorErrorRequest request)
        {
            string json = "";
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_GET_EDITOR_ERRORS_FROM_TRANSACTION", conn))
                    {

                        myCMD.CommandType = CommandType.StoredProcedure;
                        SqlParameter[] param = new SqlParameter[3];
                        param[0] = new SqlParameter("@CARDID", request.cardid);
                        myCMD.Parameters.Add(param[0]);
                        DataTable table = new DataTable();
                        SqlDataAdapter adapter = new SqlDataAdapter(myCMD);
                        adapter.Fill(table);



                        List<errorNestedJson> mainObjects = new List<errorNestedJson>();
                        foreach (DataRow row in table.Rows)
                        {
                            string ename = row["ERROR_TYPE_NAME"].ToString();

                            errorNestedJson mainObject = mainObjects.FirstOrDefault(obj => obj.errorName == ename);

                            if (mainObject == null)
                            {
                                mainObject = new errorNestedJson
                                {
                                    errorName = ename,
                                    subtypeArray = new List<subtypeArray>()
                                };

                                mainObjects.Add(mainObject);
                            }

                            subtypeArray nestedObject = new subtypeArray
                            {
                                subtypeID = (int)row["ESUBTYPE_ID"],
                                subtypeName = row["ERROR_SUBTYPE_NAME"].ToString(),
                            };

                            mainObject.subtypeArray.Add(nestedObject);
                        }
                        json = JsonConvert.SerializeObject(mainObjects);
                    }
                    return json;

                }
                catch (Exception ex)
                {
                    throw new Exception("Error in fetchEditorErrorsForQA. ", ex);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }

        }
        public Common updateCardIssueByQA(updateCardIssueByQARequest request)
        {
            Common response = new Common();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_UPDATE_CARD_ISSUE_QA", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        myCMD.Parameters.AddWithValue("@CARDSID", request.cardId);
                        myCMD.Parameters.AddWithValue("@ISSUEID", request.issueId);
                        myCMD.Parameters.AddWithValue("@UPDATEDBY", request.updatedBy);
                        myCMD.Parameters.Add("@VOUTPUT", SqlDbType.VarChar, 100);
                        myCMD.Parameters["@VOUTPUT"].Direction = ParameterDirection.Output;
                        int k = myCMD.ExecuteNonQuery();
                        string str = (string)myCMD.Parameters["@VOUTPUT"].Value;

                        response.Message = str;
                        response.Code = k;


                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in GetAllComment dl. ", ex);
                }
            }
            return response;
        }

        public List<InvestigationReportResponse> InvestigationReport(InvestigationReportRequest request)
        {
            List<InvestigationReportResponse> respo = new List<InvestigationReportResponse>();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {

                    using (SqlCommand cmd = new SqlCommand("SP_INVESTIGATION_REPORT", conn))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@V_COMPANYID", request.companyid);
                        cmd.Parameters.AddWithValue("@V_ROLEID", request.roleid);
                        cmd.Parameters.AddWithValue("@V_USERID", request.userid);
                        cmd.Parameters.AddWithValue("@V_DateFrom", request.DateFrom);
                        cmd.Parameters.AddWithValue("@V_DateTo", request.DateTo);
                        cmd.Parameters.AddWithValue("@V_ROWCOUNT", request.toprows);
                        cmd.Parameters.AddWithValue("@V_CARD_STATUS", request.CARDSTATUS);
                        cmd.Parameters.AddWithValue("@V_CITY", request.City);
                        cmd.Parameters.AddWithValue("@V_STREET", request.Street);
                        cmd.Parameters.AddWithValue("@V_HOUSENUMBER", request.House);


                        using (SqlDataReader myReader = cmd.ExecuteReader())
                        {
                            //respo = DataReaderMapToList<InvestigationReportResponse>(myReader);

                            while (myReader.Read())
                            {
                                InvestigationReportResponse obj = new InvestigationReportResponse();
                                obj.CardsID = myReader["CardsID"].ToString();
                                obj.CARD_ADDRESS = myReader["CARD_ADDRESS"].ToString();
                                obj.card_Description = myReader["card_Description"].ToString() == null ? "" : myReader["card_Description"].ToString();
                                obj.fullname = myReader["fullname"].ToString() == null ? "" : myReader["fullname"].ToString();
                                obj.premiseurl = myReader["premiseurl"].ToString() == null ? "" : myReader["premiseurl"].ToString();
                                obj.serviceurl = myReader["serviceurl"].ToString() == null ? "" : myReader["serviceurl"].ToString();
                                obj.serv_order_no = myReader["serv_order_no"].ToString() == null ? "" : myReader["serv_order_no"].ToString();
                                obj.WORK_TYPE = myReader["WORK_TYPE"].ToString() == null ? "" : myReader["WORK_TYPE"].ToString();
                                obj.CARD_YEAR = myReader["CARD_YEAR"].ToString() == null ? "" : myReader["CARD_YEAR"].ToString();
                                obj.PREMISE_NO = myReader["PREMISE_NO"].ToString() == null ? "" : myReader["PREMISE_NO"].ToString();
                                respo.Add(obj);
                            }
                        }
                    }



                }
                catch (Exception ex)
                {
                    throw new Exception("Error in InvestigationReport. ", ex);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }

                return respo;
            }

        }


        public systemIssueInformation GetRaisedIssueDataForMail(int? issueId)
        {
            systemIssueInformation issueInfo = null;

            using (SqlConnection conn = _commonDl.Connect())
            {
                using (SqlCommand cmd = new SqlCommand("SP_GET_RAISED_ISSUE_DATA_FOR_MAIL", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Input parameter
                    cmd.Parameters.AddWithValue("@issueId", issueId);

                    try
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                issueInfo = new systemIssueInformation
                                {
                                    IssueId = reader.GetInt32(reader.GetOrdinal("ISSUE_ID")),
                                    UserEmail = reader.IsDBNull(reader.GetOrdinal("userEmail")) ? string.Empty : reader.GetString(reader.GetOrdinal("userEmail")),
                                    IssueRegisterDate = reader.GetString(reader.GetOrdinal("IssueRegisterDate")),
                                    IssueDescription = reader.GetString(reader.GetOrdinal("issueDescription")),
                                    IssuePriority = reader.GetString(reader.GetOrdinal("issuePriority")),
                                    IssueType = reader.GetString(reader.GetOrdinal("issueType")),
                                    UserCreated = reader.GetString(reader.GetOrdinal("userCreated")),
                                    CompanyName = reader.GetString(reader.GetOrdinal("companyName")),
                                    IssueStatus = reader.GetString(reader.GetOrdinal("issueStatus")),
                                    FirstName = reader.GetString(reader.GetOrdinal("firstName"))
                                };
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle the exception
                        Console.WriteLine("An error occurred: " + ex.Message);
                    }
                }
            }

            return issueInfo;
        }

        public async Task<Common> InsertUpdateIssueDataAsync(SystemErrorInsertRequest request)
        {
            Common commonmsg = new Common();
            using (SqlConnection conn = _commonDl.Connect())
            {
                using (SqlCommand myCMD = new SqlCommand("Sp_System_Issues_Register", conn))
                {
                    myCMD.CommandType = CommandType.StoredProcedure;
                    myCMD.Parameters.AddWithValue("@ISSUEID", request.issueID);
                    myCMD.Parameters.AddWithValue("@ISSUE_TYPE", request.IssueType);
                    myCMD.Parameters.AddWithValue("@ISSUE_DESC", request.IssueDesc);
                    myCMD.Parameters.AddWithValue("@PRIORITY_ISSUE", request.Priority);
                    myCMD.Parameters.AddWithValue("@SYSTEM_STATUS", request.status);
                    myCMD.Parameters.AddWithValue("@ISSUE_ASSIGN_TO", request.AssignTo);
                    myCMD.Parameters.AddWithValue("@ISSUE_RAISED_BY", request.IssueRaisedBy);
                    myCMD.Parameters.AddWithValue("@COMPANY", request.CompanyId);
                    myCMD.Parameters.AddWithValue("@STATEMENT", request.operation);
                    myCMD.Parameters.Add("@VOUTPUT", SqlDbType.VarChar, 100);
                    myCMD.Parameters["@VOUTPUT"].Direction = ParameterDirection.Output;

                    int k = myCMD.ExecuteNonQuery();

                    //string mailString = "Issue Id: "+request.issueID+" , Issue Type: "+request.IssueType+" , Issue Priority"
                    //SendCompletionEmail(GetRaisedIssueDataForMail(request.issueID), ConfigurationManager.AppSettings["mailSubject"]);
                    if (_configuration.GetSection("emailConfiguration")["emailGateKeeper"] == "true")
                    {
                        systemIssueInformation info = GetRaisedIssueDataForMail(request.issueID);
                        if (info != null)
                        {
                            SendCompletionEmail(htmlTemplateForMail(info), _configuration.GetSection("emailConfiguration")["mailSubject"] , (info.UserEmail != string.Empty) ? ";" + info.UserEmail : "");
                        }

                    }
                    //await SendMailAsync();


                    commonmsg.Message = myCMD.Parameters["@VOUTPUT"].Value.ToString();
                    commonmsg.Code = k;
                }
            }
            return commonmsg;
        }
        public string htmlTemplateForMail(systemIssueInformation data)
        {
            string htmlBody = $@"
                    <!DOCTYPE html>
                    <html lang='en'>
                      <head>
                        <meta charset='UTF-8' />
                        <meta name='viewport' content='width=device-width, initial-scale=1.0' />
                        <title>Issue Details</title>
                        <style>
                          body {{
                            background-color: #cce9f1;
                          }}
                          table {{
                            width: 70%;
                            border-collapse: collapse;
                            margin: 20px auto;
                          }}
                          th,
                          td {{
                            padding: 10px;
                            border: 1px solid #000000;
                            text-align: left;
                          }}
                          th {{
                            background-color: #0b97b7;
                            color: azure;
                          }}
                            th:first-child{{
                              width: 40%; 
                            }}
                        </style>
                      </head>
                      <body>
                        <div>
                          <center><h5>Hello, thank you for raising your concern</h5></center>
                        </div>
                        <table>
                          <tr>
                            <th>Field</th>
                            <th>Details</th>
                          </tr>
                          <tr>
                            <td>Issue Id</td>
                            <td>{data.IssueId}</td>
                          </tr>
                          <tr>
                            <td>Issue Type</td>
                            <td>{data.IssueType}</td>
                          </tr>
                          <tr>
                            <td>Priority</td>
                            <td>{data.IssuePriority}</td>
                          </tr>
                          <tr>
                            <td>Status</td>
                            <td>{data.IssueStatus}</td>
                          </tr>
                          <tr>
                            <td>Created By</td>
                            <td>{data.UserCreated}</td>
                          </tr>
                          <tr>
                            <td>Assigned</td>
                            <td>{data.FirstName}</td>
                          </tr>
                          
                          <tr>
                            <td>Comment</td>
                            <td>{data.IssueDescription}</td>
                          </tr>
                        </table>
                        <div>
                          <center>This is an auto-generated email</center>
                        </div>
                      </body>
                    </html>";

            return htmlBody;
        }

        public async Task SendMailAsync()
        {
            try
            {
                string smtpAddress = _configuration.GetSection("emailConfiguration")["SmtpAddress"];
                int portNumber = Convert.ToInt32(_configuration.GetSection("emailConfiguration")["PortNumber"]);
                bool enableSSL = true;
                string emailFrom = _configuration.GetSection("emailConfiguration")["MailFrom"];
                string password = _configuration.GetSection("emailConfiguration")["Password"];
                string emailTo = _configuration.GetSection("emailConfiguration")["MailTo"];
                string subject = "Test Email";
                string body = "Hello,\n\nThis is a test email sent from a C# application.\n\nRegards,\nYour Name";

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(emailFrom);
                    mail.To.Add(emailTo);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                    {
                        smtp.Credentials = new NetworkCredential(emailFrom, password);
                        smtp.EnableSsl = enableSSL;
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.UseDefaultCredentials = false;

                        try
                        {
                            await smtp.SendMailAsync(mail);
                            Console.WriteLine("Email sent successfully.");
                        }
                        catch (SmtpException smtpEx)
                        {
                            Console.WriteLine($"SMTP Exception: {smtpEx.StatusCode} - {smtpEx.Message}");
                            Console.WriteLine($"Inner Exception: {smtpEx.InnerException?.Message}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Failed to send email: " + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        private static bool SendCompletionEmail(string strMessage, string strSsubject, string generatorEmail)
        {
            bool blnSuccess = false;
            var configuration = new ConfigurationBuilder()
.AddJsonFile("appsettings.json")
.Build();
            try
            {
                string strMailCompletionTo = configuration.GetSection("emailConfiguration")["MailCompletionTo"];
                //string strMailCompletionTo = string.Empty;
                string strMailErrorTo = configuration.GetSection("emailConfiguration")["MailTo"] ;
                string strMailFrom = configuration.GetSection("emailConfiguration")["MailFrom"];
                string strPassword = configuration.GetSection("emailConfiguration")["Password"];
                int strPortNumber = Convert.ToInt16(configuration.GetSection("emailConfiguration")["PortNumber"]);
                string strSmtpAddress = configuration.GetSection("emailConfiguration")["SmtpAddress"];
                string strMailServer = configuration.GetSection("emailConfiguration")["MailServer"];
                string emailCC = configuration.GetSection("emailConfiguration")["mailCC"];
                bool enableSSL = true;
                emailCC += generatorEmail;

                using (MailMessage mail = new MailMessage())
                {
                    if (!string.IsNullOrEmpty(strMailCompletionTo))
                    {
                        string[] aryMailCompletionTo = strMailCompletionTo.Split(';');


                        for (Int16 i = 0; i <= aryMailCompletionTo.Length - 1; i++)
                        {
                            mail.To.Add(aryMailCompletionTo[i].Trim());
                        }
                    }
                    if (!string.IsNullOrEmpty(emailCC))
                    {
                        string[] ccAddresses = emailCC.Split(';');
                        foreach (string ccAddress in ccAddresses)
                        {
                            mail.CC.Add(ccAddress.Trim());
                        }
                    }
                    mail.From = new MailAddress(strMailFrom);
                    mail.To.Add(strMailErrorTo);
                    mail.Subject = strSsubject;
                    mail.Body = strMessage;
                    mail.IsBodyHtml = true;
                    //mail.Attachments.Add(new Attachment("D:\\TestFile.txt"));//--Uncomment this to send any attachment  
                    using (SmtpClient smtp = new SmtpClient(strSmtpAddress, strPortNumber))
                    {
                        //smtp.UseDefaultCredentials = false;
                        smtp.EnableSsl = enableSSL;
                        smtp.Credentials = new NetworkCredential(strMailFrom, strPassword);
                        smtp.Send(mail);
                    }
                }
                blnSuccess = true;
            }
            catch (Exception ex)
            {
                blnSuccess = false;
            }
            return blnSuccess;
        }


        public JsonResult FetchBPEMDetails(BPEMDetailRequest request)
        {
            List<string> listOfDicts = new List<string>();
            Dictionary<string, List<string>> returnDict = new Dictionary<string, List<string>>();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {

                    using (SqlCommand myCMD = new SqlCommand("SP_BPEM_DETAIL_USER", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        myCMD.Parameters.AddWithValue("@ID", request.cardsid);

                        DataSet dsdata = new DataSet();
                        SqlDataAdapter sdadata = new SqlDataAdapter(myCMD);
                        sdadata.Fill(dsdata);
                        var responseObject = new
                        {
                            Data = dsdata,
                        };
                        return new JsonResult(responseObject);


                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in FetchBPEMDetails. ", ex);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }

        }
        //public CasesTotalsResponse GetCountData(CountDataRequest request)
        //{
        //    CasesTotalsResponse response = new CasesTotalsResponse();
        //    CommonDl dl = new CommonDl();

        //    using (SqlConnection conn = dl.connect())
        //    {
        //        try
        //        {
        //            using (SqlCommand myCMD = new SqlCommand("SP_GET_COUNT_DATA", conn))
        //            {
        //                myCMD.CommandType = CommandType.StoredProcedure;

        //                // Add input parameters from request
        //                myCMD.Parameters.Add(new SqlParameter("@pageName", string.IsNullOrEmpty(request.PageName) ? (object)DBNull.Value : request.PageName));
        //                myCMD.Parameters.Add(new SqlParameter("@user", string.IsNullOrEmpty(request.User) ? (object)DBNull.Value : request.User));
        //                myCMD.Parameters.Add(new SqlParameter("@fromDate", request.FromDate.HasValue ? (object)request.FromDate.Value : DBNull.Value));
        //                myCMD.Parameters.Add(new SqlParameter("@toDate", request.ToDate.HasValue ? (object)request.ToDate.Value : DBNull.Value));

        //                // Add output parameter
        //                SqlParameter outputParam = new SqlParameter("@outPutData", SqlDbType.NVarChar, -1) // -1 for MAX
        //                {
        //                    Direction = ParameterDirection.Output
        //                };
        //                myCMD.Parameters.Add(outputParam);

        //                // Execute the command
        //                myCMD.ExecuteNonQuery();

        //                // Retrieve output parameter value and parse JSON
        //                string jsonData = outputParam.Value.ToString();
        //                if (!string.IsNullOrEmpty(jsonData))
        //                {
        //                    JObject json = JObject.Parse(jsonData);
        //                    response.TodayTotal = json["TodayCount"]?.ToObject<int>() ?? 0;
        //                    response.WeekTotal = json["WeekCount"]?.ToObject<int>() ?? 0;
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            ExceptionLogging.SendErrorToText(ex);
        //            throw new Exception("Error in GetCountData. ", ex);
        //        }
        //    }

        //    return response;
        //}
        public CasesTotalsResponse GetCountData(CountDataRequest request)
        {
            CasesTotalsResponse response = new CasesTotalsResponse();

            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("sp_get_cases_totals", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        myCMD.Parameters.Add(new SqlParameter("@user", string.IsNullOrEmpty(request.User) ? (object)DBNull.Value : request.User));
                        myCMD.Parameters.Add(new SqlParameter("@searchUser", string.IsNullOrEmpty(request.SearchUser) ? (object)DBNull.Value : request.SearchUser));
                        myCMD.Parameters.Add(new SqlParameter("@fromDate", request.FromDate.HasValue ? (object)request.FromDate.Value : DBNull.Value));
                        myCMD.Parameters.Add(new SqlParameter("@toDate", request.ToDate.HasValue ? (object)request.ToDate.Value : DBNull.Value));
                        myCMD.Parameters.Add(new SqlParameter("@taskId", request.TaskId.HasValue ? (object)request.TaskId.Value : DBNull.Value));
                        myCMD.Parameters.Add(new SqlParameter("@workId", request.WorkId.HasValue ? (object)request.WorkId.Value : DBNull.Value));
                        myCMD.Parameters.Add(new SqlParameter("@statusId", string.IsNullOrEmpty(request.StatusId) ? (object)DBNull.Value : request.StatusId));

                        // Add output parameter
                        SqlParameter outputParam = new SqlParameter("@result", SqlDbType.NVarChar, -1) // -1 for MAX
                        {
                            Direction = ParameterDirection.Output
                        };
                        myCMD.Parameters.Add(outputParam);

                        // Execute the command
                        myCMD.ExecuteNonQuery();

                        // Retrieve output parameter value and parse JSON
                        string jsonData = outputParam.Value.ToString();
                        if (!string.IsNullOrEmpty(jsonData))
                        {
                            JObject json = JObject.Parse(jsonData);
                            response.TodayDate = json["todayDate"]?.ToObject<DateTime>() ?? DateTime.MinValue;
                            response.StartingWeekDate = json["startingWeekDate"]?.ToObject<DateTime>() ?? DateTime.MinValue;
                            response.EndingWeekDate = json["endingWeekDate"]?.ToObject<DateTime>() ?? DateTime.MinValue;
                            response.TodayCount = json["todayCount"]?.ToObject<int>() ?? 0;
                            response.WeeklyCount = json["weeklyCount"]?.ToObject<int>() ?? 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in GetCountData. ", ex);
                }
            }

            return response;
        }

        public CasesTotalsResponse GetQaNumbersData(QaNumbersRequest request)
        {
            CasesTotalsResponse response = new CasesTotalsResponse();

            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("sp_get_qa_numbers", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        myCMD.Parameters.Add(new SqlParameter("@selfUser", string.IsNullOrEmpty(request.SelfUser) ? "" : request.SelfUser));
                        myCMD.Parameters.Add(new SqlParameter("@user", string.IsNullOrEmpty(request.User) ? "" : request.User));
                        myCMD.Parameters.Add(new SqlParameter("@qaUser", string.IsNullOrEmpty(request.QaUser) ? "" : request.QaUser));
                        myCMD.Parameters.Add(new SqlParameter("@fromDate", string.IsNullOrEmpty(request.FromDate) ? (object)DBNull.Value : request.FromDate));
                        myCMD.Parameters.Add(new SqlParameter("@toDate", string.IsNullOrEmpty(request.ToDate) ? (object)DBNull.Value : request.ToDate));
                        myCMD.Parameters.Add(new SqlParameter("@taskId", request.TaskId.HasValue ? (object)request.TaskId.Value : DBNull.Value));
                        myCMD.Parameters.Add(new SqlParameter("@workId", string.IsNullOrEmpty(request.WorkId) ? "" : request.WorkId));
                        myCMD.Parameters.Add(new SqlParameter("@installationNum", string.IsNullOrEmpty(request.InstallationNum) ? (object)DBNull.Value : request.InstallationNum));

                        // Add output parameter
                        SqlParameter outputParam = new SqlParameter("@result", SqlDbType.NVarChar, -1) // -1 for MAX
                        {
                            Direction = ParameterDirection.Output
                        };
                        myCMD.Parameters.Add(outputParam);

                        // Execute the command
                        myCMD.ExecuteNonQuery();

                        // Retrieve output parameter value and parse JSON
                        string jsonData = outputParam.Value.ToString();
                        if (!string.IsNullOrEmpty(jsonData))
                        {
                            JObject json = JObject.Parse(jsonData);
                            response.TodayDate = json["todayDate"]?.ToObject<DateTime>() ?? DateTime.MinValue;
                            response.StartingWeekDate = json["startingWeekDate"]?.ToObject<DateTime>() ?? DateTime.MinValue;
                            response.EndingWeekDate = json["endingWeekDate"]?.ToObject<DateTime>() ?? DateTime.MinValue;
                            response.TodayCount = json["todayCount"]?.ToObject<int>() ?? 0;
                            response.WeeklyCount = json["weeklyCount"]?.ToObject<int>() ?? 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in GetQaNumbersData.", ex);
                }
            }

            return response;
        }

        public JsonResult GetCaseHistoryInformation(int cardId)
        {
            var caseHistoryList = new List<Dictionary<string, object>>();
            string voutput = string.Empty;

            using (SqlConnection conn = _commonDl.Connect())
            {
                using (SqlCommand cmd = new SqlCommand("SP_GET_CASE_HISTORY_INFORMATION", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cardId", cardId);

                    // Add the output parameter
                    var outputParam = new SqlParameter("@voutput", SqlDbType.VarChar, 100)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputParam);

                    try
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var row = new Dictionary<string, object>();
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    row[reader.GetName(i)] = reader.IsDBNull(i) ? null : reader.GetValue(i);
                                }
                                caseHistoryList.Add(row);
                            }
                        }

                        // Get the value of the output parameter after executing the command
                        voutput = outputParam.Value.ToString();
                    }
                    catch (Exception ex)
                    {
                        var responseObject = new
                        {
                            Data = new { error = "An error occurred: " + ex.Message },
                        };
                        return new JsonResult(responseObject);
                    }
                }
            }

            if (string.IsNullOrEmpty(voutput))
            {
                var responseObject = new
                {
                    Data = new
                    {
                        JsonArray = caseHistoryList,
                        JsonOutputResponse = ""
                    },
                };
                return new JsonResult(responseObject);
            }
            else
            {
                var responseObject = new
                {
                    Data = new
                    {
                        JsonArray = new List<Dictionary<string, object>>(),
                        JsonOutputResponse = voutput
                    },
                };
                return new JsonResult(responseObject);
            }
        }
    }
}
