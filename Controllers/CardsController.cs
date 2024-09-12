using Microsoft.AspNetCore.Mvc;
using TMT_Code_Migration1.DataLogics.Utility;
using TMT_Code_Migration1.DataLogics;
using TMT_Code_Migration1.Models.Cards;
using Newtonsoft.Json;
using System.Web;
namespace TMT_Code_Migration1.Controllers
{
    public class CardsController : Controller
    {
        private readonly CommonDl _commonDl;
        private readonly CardsDL _cardsDL;
        private readonly IConfiguration _configuration;

        public CardsController(CommonDl commonDl, [FromServices] IConfiguration configuration)
        {
            _commonDl = commonDl;
            _cardsDL = new CardsDL(commonDl, configuration);
            _configuration = configuration;
        }
        [Route("TMTService/AddCard")]
        [HttpPost]
        public IActionResult AddCard(AddCardRequest request)
        {
            AddCardResponse addCardResponse = new AddCardResponse();
            try
            {
                addCardResponse = _cardsDL.Addcard(request);
            }
            catch (Exception)
            {
                throw;
            }
            return Ok(addCardResponse);
        }

        [Route("TMTService/GetAllCard")]
        [HttpGet]
        public IActionResult GetAllCard()
        {
            List<GatAllCards> allCards = _cardsDL.GetCardAll();
            return Ok(allCards);

        }

        [Route("TMTService/GetCardbyId/{cid}")]
        [HttpGet]
        public IActionResult GetCards(int Cid)
        {
            GetCardbyCid getCardbyCid = _cardsDL.getCardbyCid(Cid);
            return Ok(getCardbyCid);

        }

        [Route("TMTService/ArchiveCardID")]
        [HttpPost]
        public IActionResult ArchiveCardbyID(Deleterequest deleterequest)
        {
            int Cid = (int)deleterequest.CardId;

            Common commonmsg = _cardsDL.ArchiveCardbyID(Cid);

            return Ok(commonmsg);

        }
        [Route("TMTService/AssignQA")]
        [HttpPost]
        public IActionResult AssignQA(List<AssignQARequest> request)
        {
            AssignQAResponse assignresponse = new AssignQAResponse();
            try
            {
                assignresponse = _cardsDL.AssignQA(request);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in AssignQA controller ", ex);
            }
            return Ok(assignresponse);

        }
        [Route("TMTService/GetAllCardByQC")]
        [HttpPost]
        public IActionResult GetAllCardByQC(CardBYQCRequest request)
        {
            List<GatAllCards> allCards = _cardsDL.GetCardAllByQC(request);
            return Ok(allCards);

        }

        [Route("TMTService/GetAllComment")]
        [HttpPost]
        public IActionResult GetAllComment(CommentDetailsRequest request)
        {
            List<CommentDetailsResponse> allComments = _cardsDL.GetAllComment(request);
            return Ok(allComments);

        }
        [Route("TMTService/GetAllCardByUser")]
        [HttpPost]
        public IActionResult GetAllCardByUser(CardBYUserRequest request)
        {
            List<GatAllCards> allCards = _cardsDL.GetCardAllByUser(request);
            return Ok(allCards);

        }
        [Route("TMTService/GetAllCardByUserAndKeywords")]
        [HttpPost]
        public IActionResult GetAllCardByUserAndKeywords(CardBYUserRequestAndKeywords request)
        {
            List<GatAllCards> allCards = _cardsDL.GetAllCardByUserAndKeywords(request);
            return Ok(allCards);

        }
        [Route("TMTService/GetCardsforSupervisor")]
        [HttpPost]
        public IActionResult CardsforSupervisor(GetCardsforsupervisorRequest request)
        {
            List<GetCardsforsupervisorResponse> getCards = _cardsDL.CardsforSupervisor(request);
            return Ok(getCards);
        }

        [Route("TMTService/UnAssignQAUpdate")]
        [HttpPost]
        public IActionResult UnAssignQAUpdate(Un_AssignQARequest request)
        {
            Un_AssignQAResponse un_AssignQAResponse = null;
            try
            {
                un_AssignQAResponse = _cardsDL.UnAssignQA(request);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in UnAssignQAUpdate controller ", ex);
            }
            return Ok(un_AssignQAResponse); 

        }

        [Route("TMTService/PushExcelData")]
        [HttpPost]
        public IActionResult PushExcelData(pushExcelDataRequest request)
        {
            CSV_EXCEL_data cSV_EXCEL_Data = new CSV_EXCEL_data();
            List<CSV_EXCEL_data> datas = new List<CSV_EXCEL_data>();
            ExcelDataResponse excelDataResponse = new ExcelDataResponse();
            if (ModelState.IsValid)
            {
                int companyID = (int)request.CompanyID;
                try
                {
                    int objcount = ((Newtonsoft.Json.Linq.JContainer)request.spdshtData).Count;

                    for (int i = 0; i < objcount; i++)
                    {
                        string str = ((Newtonsoft.Json.Linq.JToken)request.spdshtData).Root[i].ToString(), results;
                        cSV_EXCEL_Data = null;
                        cSV_EXCEL_Data = JsonConvert.DeserializeObject<CSV_EXCEL_data>(str);
                        datas.Add(cSV_EXCEL_Data);
                    }
                    excelDataResponse = _cardsDL.PushExcelData(datas);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in PushExcelData controller ", ex);
                }

            }
            else
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();
            }
            return Ok(excelDataResponse);

        }

        [Route("TMTService/GetRawdata")]
        [HttpPost]
        public List<GetRawDataResponse> GetRawdata(GetRawDataRequest request)
        {
            List<GetRawDataResponse> getRawDataResponses = _cardsDL.GetUnfilteredData(request);

            return getRawDataResponses;

        }
        [Route("TMTService/AssignEditorData")]
        [HttpPost]
        public IActionResult AssignEditorData(List<AssgnEditorRequest> request)
        {
            AssgnEditorResponse assgn = new AssgnEditorResponse();
            try
            {
                assgn = _cardsDL.AssignEditorData(request);

            }
            catch (Exception ex)
            {
                throw new Exception("Error in AssignEditorData controller ", ex);
            }

            return Ok(assgn);
        }

        [Route("TMTService/GetAssignedUserData")]
        [HttpPost]
        public List<GetRawDataResponseUser> GetAssignedUserData(GetRawDataRequestUser request)
        {
            List<GetRawDataResponseUser> getRawDataResponses = new List<GetRawDataResponseUser>();
            if (ModelState.IsValid)
            {
                getRawDataResponses = _cardsDL.GetAssignedUserData(request);
            }
            else
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();
            }


            return getRawDataResponses;
        }

        [Route("TMTService/AssignEditorbucket")]
        [HttpPost]
        public IActionResult AssignEditorbucket(List<AssgnEditorbucketRequest> request)
        {
            AssgnEditorBucketResponse bucketResponse = new AssgnEditorBucketResponse();
            try
            {
                if (ModelState.IsValid)
                {
                    bucketResponse = _cardsDL.AssignEditorbucket(request);
                }
                else
                {
                    var errors = ModelState.Select(x => x.Value.Errors)
                               .Where(y => y.Count > 0)
                               .ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in AssignEditorbucket controller ", ex);
            }

            return Ok(bucketResponse);
        }

        #region DON'T DELETE
        //[Route("Dominion/UploadExcel")]
        //[HttpPost]
        //public ExcelDataResponse  UploadExcel()
        //{
        //    string message = string.Empty;          
        //    List<CSV_EXCEL_data> eXCEL_Datas = new List<CSV_EXCEL_data>();
        //    ExcelDataResponse excelDataResponse = new ExcelDataResponse();

        //    try
        //    {
        //        #region Variable Declaration
        //        HttpResponseMessage ResponseMessage = null;
        //        var httpRequest = HttpContext.Current.Request;
        //        DataSet dsexcelRecords = new DataSet();
        //        IExcelDataReader reader = null;
        //        HttpPostedFile Inputfile = null;
        //        Stream FileStream = null;
        //        #endregion

        //        #region Save Student Detail From Excel

        //        if (httpRequest.Files.Count > 0)
        //        {
        //            Inputfile = httpRequest.Files[0];
        //            FileStream = Inputfile.InputStream;

        //            if (Inputfile != null && FileStream != null)
        //            {
        //                if (Inputfile.FileName.EndsWith(".xls"))
        //                    reader = ExcelReaderFactory.CreateBinaryReader(FileStream);
        //                else if (Inputfile.FileName.EndsWith(".xlsx"))
        //                    reader = ExcelReaderFactory.CreateReader(FileStream);
        //                else
        //                    message = "The file format is not supported.";

        //                dsexcelRecords = reader.AsDataSet();
        //                reader.Close();

        //                if (dsexcelRecords != null && dsexcelRecords.Tables.Count > 0)
        //                {
        //                    DataTable dtsheet = dsexcelRecords.Tables[0];
        //                    for (int i = 1; i < dtsheet.Rows.Count; i++)
        //                    {
        //                        CSV_EXCEL_data cSV_EXCEL_Data = new CSV_EXCEL_data();

        //                        cSV_EXCEL_Data.SERV_ORDER_NO = string.IsNullOrEmpty(dtsheet.Rows[i][0].ToString()) ?string.Empty: dtsheet.Rows[i][0].ToString();
        //                        cSV_EXCEL_Data.URL_LINK = string.IsNullOrEmpty(dtsheet.Rows[i][1].ToString()) ? string.Empty : dtsheet.Rows[i][1].ToString();
        //                        cSV_EXCEL_Data.SERVICE_LINE_STATUS_CD = string.IsNullOrEmpty(dtsheet.Rows[i][2].ToString()) ? string.Empty : dtsheet.Rows[i][2].ToString();
        //                        cSV_EXCEL_Data.SERVICE_LINE_INSTALL_DT = string.IsNullOrEmpty(dtsheet.Rows[i][3].ToString()) ? string.Empty : dtsheet.Rows[i][3].ToString();
        //                        cSV_EXCEL_Data.FREQ_CODE = Convert.ToInt32( string.IsNullOrEmpty(dtsheet.Rows[i][4].ToString()) ? "0" : dtsheet.Rows[i][4].ToString());
        //                        cSV_EXCEL_Data.INSP_TYPE = string.IsNullOrEmpty(dtsheet.Rows[i][5].ToString()) ? string.Empty : dtsheet.Rows[i][5].ToString();
        //                        cSV_EXCEL_Data.COMPANY_NO = string.IsNullOrEmpty(dtsheet.Rows[i][6].ToString()) ? string.Empty : dtsheet.Rows[i][6].ToString();

        //                        cSV_EXCEL_Data.LO_OFFICE = 0; //7
        //                        //cSV_EXCEL_Data.LO_OFFICE = dtsheet.Rows[i][8].ToString() == "" ? 0 : Convert.ToInt32(dtsheet.Rows[i][8].ToString());
        //                        cSV_EXCEL_Data.ROUTE_NO = string.IsNullOrEmpty(dtsheet.Rows[i][8].ToString()) ? string.Empty : dtsheet.Rows[i][8].ToString();
        //                        cSV_EXCEL_Data.READ_SEQ = string.IsNullOrEmpty(dtsheet.Rows[i][9].ToString()) ? string.Empty : dtsheet.Rows[i][9].ToString();
        //                        cSV_EXCEL_Data.PREMISE_NO = string.IsNullOrEmpty(dtsheet.Rows[i][10].ToString()) ? string.Empty : dtsheet.Rows[i][10].ToString();
        //                        cSV_EXCEL_Data.SPECIAL_INST = string.IsNullOrEmpty(dtsheet.Rows[i][11].ToString()) ? string.Empty : dtsheet.Rows[i][11].ToString();
        //                        cSV_EXCEL_Data.MTR_READ_INST2 = string.IsNullOrEmpty(dtsheet.Rows[i][12].ToString()) ? string.Empty : dtsheet.Rows[i][12].ToString();
        //                        cSV_EXCEL_Data.MTR_LOCATION = string.IsNullOrEmpty(dtsheet.Rows[i][13].ToString()) ? string.Empty : dtsheet.Rows[i][13].ToString(); ;
        //                        cSV_EXCEL_Data.HOUSE_NO = string.IsNullOrEmpty(dtsheet.Rows[i][14].ToString()) ? string.Empty : dtsheet.Rows[i][14].ToString();
        //                        cSV_EXCEL_Data.DIRECTION = string.IsNullOrEmpty(dtsheet.Rows[i][15].ToString()) ? string.Empty : dtsheet.Rows[i][15].ToString();
        //                        cSV_EXCEL_Data.STREET = string.IsNullOrEmpty(dtsheet.Rows[i][16].ToString()) ? string.Empty : dtsheet.Rows[i][16].ToString();
        //                        cSV_EXCEL_Data.STREET_SUFFIX = string.IsNullOrEmpty(dtsheet.Rows[i][17].ToString()) ? string.Empty : dtsheet.Rows[i][17].ToString();
        //                        cSV_EXCEL_Data.DIRECTION_SUFFIX = string.IsNullOrEmpty(dtsheet.Rows[i][18].ToString()) ? string.Empty : dtsheet.Rows[i][18].ToString();
        //                        cSV_EXCEL_Data.APARTMENT_TYPE = string.IsNullOrEmpty(dtsheet.Rows[i][19].ToString()) ? string.Empty : dtsheet.Rows[i][19].ToString();
        //                        cSV_EXCEL_Data.APARTMENT = string.IsNullOrEmpty(dtsheet.Rows[i][20].ToString()) ? string.Empty : dtsheet.Rows[i][20].ToString();
        //                        cSV_EXCEL_Data.CITY = string.IsNullOrEmpty(dtsheet.Rows[i][22].ToString()) ? string.Empty : dtsheet.Rows[i][22].ToString();
        //                        cSV_EXCEL_Data.ZIP_CODE = string.IsNullOrEmpty(dtsheet.Rows[i][22].ToString()) ? string.Empty : dtsheet.Rows[i][22].ToString();
        //                        cSV_EXCEL_Data.ADDRESS_OVERFLOW = string.IsNullOrEmpty(dtsheet.Rows[i][23].ToString()) ? string.Empty : dtsheet.Rows[i][23].ToString();
        //                        cSV_EXCEL_Data.METER_STATUS = string.IsNullOrEmpty(dtsheet.Rows[i][24].ToString()) ? string.Empty : dtsheet.Rows[i][24].ToString();
        //                        cSV_EXCEL_Data.METER_ID = string.IsNullOrEmpty(dtsheet.Rows[i][25].ToString()) ? string.Empty : dtsheet.Rows[i][25].ToString();
        //                        cSV_EXCEL_Data.ACCOUNT_NO = string.IsNullOrEmpty(dtsheet.Rows[i][26].ToString()) ? string.Empty : dtsheet.Rows[i][26].ToString();
        //                        cSV_EXCEL_Data.LAT = string.IsNullOrEmpty(dtsheet.Rows[i][27].ToString()) ? string.Empty : dtsheet.Rows[i][27].ToString();
        //                        cSV_EXCEL_Data.LONG1 = string.IsNullOrEmpty(dtsheet.Rows[i][28].ToString()) ? string.Empty : dtsheet.Rows[i][28].ToString();
        //                        cSV_EXCEL_Data.GAS_LEAK_SURVEY_DT = string.IsNullOrEmpty(dtsheet.Rows[i][29].ToString()) ? string.Empty : dtsheet.Rows[i][29].ToString();
        //                        cSV_EXCEL_Data.SUBDIVISION_DESC = string.IsNullOrEmpty(dtsheet.Rows[i][30].ToString()) ? string.Empty : dtsheet.Rows[i][30].ToString();                               
        //                        cSV_EXCEL_Data.WORK_TYPE = string.IsNullOrEmpty(dtsheet.Rows[i][31].ToString()) ? string.Empty : dtsheet.Rows[i][31].ToString();
        //                        eXCEL_Datas.Add(cSV_EXCEL_Data);
        //                    }
        //                    CardsDL dl = new CardsDL();
        //                    excelDataResponse = dl.PushExcelData(eXCEL_Datas);
        //                }
        //                else
        //                    message = "Selected file is empty.";
        //            }
        //            else
        //                message = "Invalid File.";
        //        }
        //        else
        //            ResponseMessage = Request.CreateResponse(HttpStatusCode.BadRequest);

        //        return excelDataResponse;
        //        #endregion
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return excelDataResponse;
        //}
        #endregion


        [Route("TMTService/UserCardCount")]
        [HttpPost]
        public IActionResult USERCARDCOUNT(UserCardCount_Request request)
        {
            UserCardCount_Response userCardCount_Response = new UserCardCount_Response();
            try
            {
                if (ModelState.IsValid)
                {
                    userCardCount_Response = _cardsDL.UsercardCount(request);
                }
                else
                {
                    var errors = ModelState.Select(x => x.Value.Errors)
                               .Where(y => y.Count > 0)
                               .ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in UserCardCount controller ", ex);
            }

            return Ok(userCardCount_Response);
        }

        [Route("TMTService/transferCards")]
        [HttpPost]
        public IActionResult CardTransfer(CardTransfer_Request request)
        {
            CardTransfer_Response cardTransfer_Response = new CardTransfer_Response();
            try
            {
                if (ModelState.IsValid)
                {
                    cardTransfer_Response = _cardsDL.CardTransfer(request);
                }
                else
                {
                    var errors = ModelState.Select(x => x.Value.Errors)
                               .Where(y => y.Count > 0)
                               .ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in transferCards controller ", ex);
            }

            return Ok(cardTransfer_Response);
        }



        [Route("TMTService/GetpremiseDetails")]
        [HttpPost]
        public IActionResult PremiseDetails(PremiseDetails_Request request)
        {
            PremiseDetails_Response premiseDetails_Response = new PremiseDetails_Response();
            List<PremiseDetails_Response> allCards = null;
            try
            {
                if (ModelState.IsValid)
                {
                    allCards = _cardsDL.PremiseDetails(request);
                    //return Ok(allCards);
                }
                else
                {
                    var errors = ModelState.Select(x => x.Value.Errors)
                               .Where(y => y.Count > 0)
                               .ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in PremiseDetails controller ", ex);
            }

            return Ok(allCards);

            // return Request.CreateResponse<PremiseDetails_Response>(HttpStatusCode.OK, premiseDetails_Response);
        }


        [Route("TMTService/PremiseDetails_insigt")]
        [HttpPost]
        public IActionResult PremiseDetails_insigt(PremiseDetails_Request request)
        {
            PremiseDetails_Response premiseDetails_Response = new PremiseDetails_Response();
            List<PremiseDetails_Response> allCards = null;
            try
            {
                if (ModelState.IsValid)
                {
                    allCards = _cardsDL.PremiseDetails_insight(request);
                    //return Ok(allCards);
                }
                else
                {
                    var errors = ModelState.Select(x => x.Value.Errors)
                               .Where(y => y.Count > 0)
                               .ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in PremiseDetails_insigt controller ", ex);
            }

            return Ok(allCards);

            // return Request.CreateResponse<PremiseDetails_Response>(HttpStatusCode.OK, premiseDetails_Response);
        }


        [Route("TMTService/GetBatchData")]
        [HttpPost]
        public List<Batch_response> GetBatchData(Batch_request request)
        {
            List<Batch_response> batch_Responses = _cardsDL.GetallBatch(request);
            return batch_Responses;
        }


        [Route("TMTService/getAllCardDetails")]
        [HttpPost]

        public IActionResult GetAllCardDetails(cardDetailsRequest request)
        {
            cardDetailsResponse cardDetails_Response = new cardDetailsResponse();
            List<cardDetailsResponse> allCards = null;
            try
            {
                if (ModelState.IsValid)
                {
                    allCards = _cardsDL.GetAllCardDetails(request);
                    //return Ok(allCards);
                }
                else
                {
                    var errors = ModelState.Select(x => x.Value.Errors)
                               .Where(y => y.Count > 0)
                               .ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in getAllCardDetails controller ", ex);
            }

            return Ok(allCards);

            // return Request.CreateResponse<PremiseDetails_Response>(HttpStatusCode.OK, premiseDetails_Response);
        }


        [Route("TMTService/UpdateBatchid")]
        [HttpPost]
        public IActionResult UpdateBatchid(Batch_update_request request)
        {
            Common commonmsg = new Common();
            // string BatchID = request.BATCH_ID.Replace("undefined", "").TrimEnd(',');


            commonmsg = _cardsDL.UpdateBatch(request);
            return Ok(commonmsg);
        }

        [Route("TMTService/SupervisorDelete")]
        [HttpPost]
        public IActionResult SupervisorDelete(Supervisor_delete_request request)
        {
            Common commonmsg = new Common();

            //int BatchID = request.card_id;

            commonmsg = _cardsDL.Deletecard_supervisor(request);

            return Ok(commonmsg);

        }

        [Route("TMTService/BatchSummary")]
        [HttpPost]
        public IActionResult BatchSummary(Batch_summary_request request)
        {
            Batch_summary_response commonmsg = new Batch_summary_response();

            commonmsg = _cardsDL.BatchSummary(request);
            return Ok(commonmsg);
        }

        [Route("TMTService/BatchProcess")]
        [HttpPost]
        public IActionResult BatchProcess(Batch_process_request request)
        {
            Common commonmsg = new Common();
            commonmsg = _cardsDL.BatchProcess(request);
            return Ok(commonmsg);
        }

        [Route("TMTService/GetallbatchCards")]
        [HttpPost]
        public IActionResult GetallbatchCards(New_batch_cards_request request)
        {
            List<New_batch_cards_response> new_Batch_Cards_Responses = new List<New_batch_cards_response>();
            new_Batch_Cards_Responses = _cardsDL.GetallbatchCards(request);
            return Ok(new_Batch_Cards_Responses);
        }


        [Route("TMTService/UpdateSupervisorCard")]
        [HttpPost]
        public IActionResult UpdateSupervisorCard(Update_Supervisor_Request update_Supervisor_Request)
        {

            Common common = new Common();
            common = _cardsDL.UpdateSupervisorCard(update_Supervisor_Request);
            return Ok(common);
        }

        [Route("TMTService/getDetailsByCardID")]
        [HttpPost]
        public IActionResult getDetailsByCardID(cardTransferFetchDetailsRequest cardRequest)
        {
            List<cardTransferFetchDetailsResponse> allCards = _cardsDL.supervisorDialogCardDetails(cardRequest);
            return Ok(allCards);
        }


        [Route("TMTService/BatchDetails_for_client")]
        [HttpPost]
        public IActionResult BatchDetails_for_client(ExcelDetails_Request request)
        {
            List<ExcelDetails_Response> excelDetails = _cardsDL.BatchDetails_for_client(request);
            return Ok(excelDetails);

        }

        [Route("TMTService/manualTransferCard")]
        [HttpPost]
        public IActionResult ManualTransferCard(singleCardTransferRequest request)
        {
            Common common = new Common();
            try
            {
                if (ModelState.IsValid)
                {
                    common = _cardsDL.ManualTransferCard(request);
                }
                else
                {
                    var errors = ModelState.Select(x => x.Value.Errors)
                               .Where(y => y.Count > 0)
                               .ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in manualTransferCard controller ", ex);
            }

            return Ok(common);

        }

        [Route("TMTService/supervisorAddCard")]
        [HttpPost]
        public IActionResult supervisorAddCard(supervisorAddCardRequest request)
        {
            Common common = new Common();
            try
            {
                if (ModelState.IsValid)
                {
                    common = _cardsDL.supervisorAddCard(request);
                }
                else
                {
                    var errors = ModelState.Select(x => x.Value.Errors)
                               .Where(y => y.Count > 0)
                               .ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in supervisorAddCard controller ", ex);
            }

            return Ok(common);

        }


        [Route("TMTService/Clientbatchview")]
        [HttpPost]
        public IActionResult Clientbatchview(ClintBatchRequest request)
        {
            List<clientcardResponse> clientcardResponse = new List<clientcardResponse>();
            try
            {
                if (ModelState.IsValid)
                {
                    clientcardResponse = _cardsDL.Client_batchview(request);
                }
                else
                {
                    var errors = ModelState.Select(x => x.Value.Errors)
                               .Where(y => y.Count > 0)
                               .ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in Clientbatchview controller ", ex);
            }

            return Ok(clientcardResponse);

        }

        [Route("TMTService/Batchdata_update")]
        [HttpPost]
        public IActionResult Batchdata_update(Batchdata_update_request request)
        {
            Common commonmsg = new Common();
            try
            {
                commonmsg = _cardsDL.Batchdata_update(request);
            }
            catch (Exception)
            {
                throw;
            }
            return Ok(commonmsg);
        }


        [Route("TMTService/GetAllQABatchTableComment")]
        [HttpPost]
        public IActionResult GetAllBatchQACommentTable(batchQACommentTableRequest request)
        {
            List<batchQACommentTableResponse> allComments = _cardsDL.GetAllBatchQACommentTable(request);
            return Ok(allComments);

        }

        [Route("TMTService/sendBatchQAComment")]
        [HttpPost]
        public IActionResult BatchQACommentSubmit(batchQACommentSubmitRequest request)
        {
            Common common = _cardsDL.BatchQACommentSubmit(request);
            return Ok(common);
        }

        [Route("TMTService/assignBatchToQA")]
        [HttpPost]
        public IActionResult AssignBatchToQA(batchAssignQARequest request)
        {
            Common common = _cardsDL.AssignBatchToQA(request);
            return Ok(common);
        }



        [Route("TMTService/getQuickReport")]
        [HttpGet]
        public IActionResult getAllQuickReportSP()
        {
            //Common common = new Common();
            List<quickReportResponse> response = new List<quickReportResponse>();
            response = _cardsDL.getAllQuickReportSP();
            return Ok(response);
        }
        [Route("TMTService/getQuickReportparam")]
        [HttpGet]
        public IActionResult getAllQuickReportSPparam()
        {
            //Common common = new Common();
            List<quickReportResponse> response = _cardsDL.getAllQuickReportSPParam();
            return Ok(response);
        }



        [Route("TMTService/GetInvoice_Details")]
        [HttpPost]
        public IActionResult GetInvoice_Details(GETINVOICE_DETAILS_REQUEST request)
        {
            GET_CLIENT_BATCH_INVOICE_DATA _DETAILS_RESPONSE = new GET_CLIENT_BATCH_INVOICE_DATA();
            try
            {
                if (ModelState.IsValid)
                {
                    _DETAILS_RESPONSE = _cardsDL.GETINVOICE_DETAILS(request);
                }
                else
                {
                    var errors = ModelState.Select(x => x.Value.Errors)
                               .Where(y => y.Count > 0)
                               .ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return Ok(_DETAILS_RESPONSE);
        }

        [Route("TMTService/GenerateInvoice")]
        [HttpPost]
        public IActionResult GenerateInvoice(GETINVOICE_DETAILS_REQUEST request)
        {
            Common common = _cardsDL.send_invoice(request);
            return Ok(common);
        }


        [Route("TMTService/getCommonCardCount")]
        [HttpPost]
        public IActionResult getCommonCardCount(commonCardCountsRequest request)
        {
            commonCardCountsResponse obj = _cardsDL.getCommonCardCount(request);
            return Ok(obj);
        }


        [Route("TMTService/GetEditorErrors")]
        [HttpGet]
        public IActionResult getEditorErrors()
        {
            List<editorError> issues = _cardsDL.getEditorErrors();
            return Ok(issues);
        }



        [Route("TMTService/GET_LAST_REVIEWED")]
        [HttpPost]
        public IActionResult GET_LAST_REVIEWED(LAST_REVIEWED_REQUEST request)
        {
            LAST_REVIEWED_RESPONSE obj = _cardsDL.GET_LAST_REVIEWED(request);
            return Ok(obj);
        }



        [Route("TMTService/GET_ISSUE_IMAGES")]
        [HttpPost]
        //public IHttpActionResult GET_IMAGES(Get_Store_image_Request request)
        //{
        //    CardsDL cardsdl = new CardsDL();
        //    List<Get_Store_image_Response> get_Store_Image_Responses = new List<Get_Store_image_Response>();
        //    get_Store_Image_Responses = cardsdl.GET_IMAGES(request);
        //    return Ok(get_Store_Image_Responses);
        //}

        public IActionResult GET_IMAGES_JSON(Get_Store_image_Request request)
        {
            //Dictionary<string, List<string>> response = new Dictionary<string, List<string>>();
            var response = _cardsDL.GET_IMAGES_JSON(request);
            return Ok(response);
        }


        public class FileAPIController : Controller
        {
            private readonly CommonDl _commonDl;
            private readonly CardsDL _cardsDL;
            private readonly IConfiguration _configuration;

            public FileAPIController(CommonDl commonDl, [FromServices] IConfiguration configuration)
            {
                _commonDl = commonDl;
                _cardsDL = new CardsDL(commonDl, configuration);
                _configuration = configuration;
            }
            [Route("TMTService/UploadIssueFiles")]
            [HttpPost]
            public IActionResult UploadFiles([FromForm] IFormFile file, [FromForm] string fileName, [FromForm] int issueId, [FromForm] string profileID)
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest("No file was uploaded.");
                }

                // Get the file extension and combine it with the provided file name
                string completeFileName = fileName + Path.GetExtension(file.FileName);

                // Retrieve the upload path from configuration (appsettings.json or environment variables)
                string path = _configuration.GetSection("FileManagement")["uploadPath"];  // Ensure that 'uploadPath' is set in appsettings.json
                if (string.IsNullOrEmpty(path))
                {
                    return BadRequest("Upload path is not configured.");
                }

                // Ensure the directory exists
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                // Full file path
                string fullPath = Path.Combine(path, completeFileName);

                // Save the uploaded file to the specified path
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                // Create the request object for the data layer
                Store_image_Request request = new Store_image_Request
                {
                    ID = issueId,
                    STORED_FILE_NAME = completeFileName,
                    STORED_FILE_PATH = path,
                    UDPATED_BY = profileID
                };

                // Call the data layer to store image metadata in the database
                Store_image_Response response = _cardsDL.Store_images(request);

                // Send OK response with the response object
                return Ok(response);
            }

        }


        [Route("TMTService/fetchQuickReport")]
        [HttpPost]

        public IActionResult fetchQuickReport(fetchQuickReportRequest request)
        {
            //Dictionary<string, List<string>> response = new Dictionary<string, List<string>>();
            var response = _cardsDL.fetchQuickReport(request);
            return Ok(response);
        }

        [Route("TMTService/fetchQuickReportParam")]
        [HttpPost]

        public IActionResult fetchQuickReportParam(fetchQuickReporParamtRequest request)
        {
            var response = _cardsDL.fetchQuickReportparam(request);
            return Ok(response);
        }

        [Route("TMTService/GETINVOICE_PRINT_DETAILS")]
        [HttpPost]

        public IActionResult GETINVOICE_PRINT_DETAILS(GETINVOICE_PRINT_DETAILS_REQUEST request)
        {
            GET_INVOICE_PRINT_DATA_RESPONSE gET_INVOICE_PRINT_DATA = _cardsDL.GETINVOICE_PRINT_DETAILS(request);
            return Ok(gET_INVOICE_PRINT_DATA);
        }



        [Route("TMTService/insertEditorErrorToMasterTable")]
        [HttpPost]
        public IActionResult insertEditorErrorToMasterTable(insertEditorErrorRequest request)
        {
            Common common = _cardsDL.insertEditorErrorToMasterTable(request);
            return Ok(common);
        }


        [Route("TMTService/sendEditorErrors")]
        [HttpPost]
        public IActionResult sendEditorErrors(qaPageEditorErrorRequest request)
        {
            Common common = _cardsDL.sendEditorErrors(request);
            return Ok(common);
        }

        [Route("TMTService/showErrorsForEditor")]
        [HttpPost]
        public IActionResult showErrorsForEditor(showEditorErrorRequest request)
        {
            string response = _cardsDL.showErrorsForEditor(request);
            return Ok(response);
        }

        [Route("TMTService/fetchEditorErrorList")]
        [HttpGet]
        public IActionResult fetchEditorErrorList()
        {
            List<editorErrorListResponse> response = _cardsDL.fetchEditorErrorList();
            return Ok(response);
        }

        //[Route("TMTService/getIssueDropDown")]
        [Route("TMTService/fetchEditorErrorsForQA")]
        [HttpPost]
        public IActionResult fetchEditorErrorsForQA(fetchEditorErrorsForQARequest request)
        {
            string response = _cardsDL.fetchEditorErrorsForQA(request);
            return Ok(response);
        }

        [Route("TMTService/getIssueDropDown")]
        [HttpGet]
        public IActionResult getIssueDropDown()
        {
            List<systemErrorDropdownList> response = _cardsDL.getSystemIssueDropDown();
            return Ok(response);
        }

        [Route("TMTService/getTaskList")]
        [HttpGet]
        public IActionResult getTaskList()
        {
            List<taskDropdownList> response = _cardsDL.getTaskList();
            return Ok(response);
        }

        [Route("TMTService/getIssuestatusDropDown")]
        [HttpGet]
        public IActionResult systemIssueStatusrDropdownList()
        {
            List<systemIssueStatusrDropdownList> response = _cardsDL.systemIssueStatusrDropdownList();
            return Ok(response);
        }


        // [Route("Dominion/getSystemIssueGrid")]
        // [ResponseType(typeof(List<getSystemIssueGridResponse>))]
        //[HttpPost]
        //public IHttpActionResult getSystemIssueGrid()
        //{
        //  List<getSystemIssueGridResponse> response = new List<getSystemIssueGridResponse>();
        //CardsDL cardsdl = new CardsDL();
        //response = cardsdl.getSystemIssueGrid();
        //return Ok(response);
        //}

        [Route("TMTService/getSystemIssueGrid")]
        [HttpPost]
        public IActionResult getSystemIssueGrid(getSystemIssueGridRequest request)
        {
            List<getSystemIssueGridResponse> response = _cardsDL.GetSystemIssueGrid(request);
            return Ok(response);
        }

        [Route("TMTService/getMyWorkTypes")]
        [HttpPost]
        public IActionResult getMyWorkTypes()
        {
            List<workTypeResponse> response = _cardsDL.getMyWorkTypes();
            return Ok(response);
        }

        [Route("TMTService/getTestScriptingRecords")]
        [HttpPost]
        public IActionResult getTestScriptingRecords(testScriptingRequest req)
        {
            List<testScriptingResponse> response = _cardsDL.getTestScriptingRecords(req);
            return Ok(response);
        }
        [Route("TMTService/getTestScriptingRecordsById/{scriptId}")]
        [HttpGet]
        public IActionResult getTestScriptingRecordsById(int scriptId)
        {
            testScriptingResponseById response = _cardsDL.GetScriptingRecordById(scriptId);
            return Ok(response);
        }
        [Route("TMTService/getExecutionTestRecords")]
        [HttpPost]
        public IActionResult getExecutionTestRecords(executionTestRequest req)
        {
            List<executionTestResponse> response = _cardsDL.getExecutionTestRecords(req);
            return Ok(response);
        }


        [Route("TMTService/getFilteredExecutionTestRecords")]
        [HttpPost]
        public IActionResult getFilteredExecutionTestRecords(executionTestRequestForSearch req)
        {
            List<executionTestResponse> response = _cardsDL.getFilteredExecutionTestRecords(req);
            return Ok(response);
        }

        [Route("TMTService/getExecutionTestRecordsById/{etid}")]
        [HttpGet]
        public IActionResult getExecutionTestRecordsById(int etid)
        {
            List<executionTestResponseById> response = _cardsDL.getTextExecutionById(etid);
            return Ok(response);
        }


        [Route("TMTService/getCaseRecords")]
        [HttpPost]
        public IActionResult getCaseRecords(caseRecordRequest request)
        {
            List<caseRecordResponse> response = _cardsDL.getCaseRecords(request);
            return Ok(response);
        }

        [Route("TMTService/getPpnmRecords")]
        [HttpPost]
        public IActionResult getPpnmRecords()
        {
            List<ppnmResponse> response = _cardsDL.getPpnmRecords();
            return Ok(response);
        }

        [Route("TMTService/getMasterSetupTables")]
        [HttpPost]
        public IActionResult getMasterSetupTables()
        {
            List<masterSetupList> response = _cardsDL.getMasterSetupTables();
            return Ok(response);
        }

        [Route("TMTService/addExecutionTestRecord")]
        [HttpPost]
        public IActionResult InsertUpdateIssueData(ExecutionTestInsertUpdateRequest request)
        {
            Common common = _cardsDL.addExecutionTestRecord(request);
            return Ok(common);
        }
        [Route("TMTService/addPpnmRecord")]
        [HttpPost]
        public IActionResult InsertUpdateIssueData(ppnmInsertUpdateRequest request)
        {
            Common common = _cardsDL.addPpnmRecord(request);
            return Ok(common);
        }

        [Route("TMTService/addCaseRecord")]
        [HttpPost]
        public IActionResult InsertUpdateIssueData(CaseRecordInsertUpdateRequest request)
        {
            Common common = _cardsDL.addCaseRecord(request);
            return Ok(common);
        }
        [Route("TMTService/addTestScriptingRecord")]
        [HttpPost]
        public IActionResult InsertUpdateIssueData(TestScriptingInsertUpdateRequest request)
        {
            Common common = _cardsDL.addTestScriptingRecord(request);
            return Ok(common);
        }
        [Route("TMTService/addWorkTypeRecord")]
        [HttpPost]
        public IActionResult InsertUpdateIssueData(WorkTypeInsertUpdateRequest request)
        {
            Common common = _cardsDL.addWorkTypeRecord(request);
            return Ok(common);
        }

        [Route("TMTService/addTaskTypeRecord")]
        [HttpPost]
        public IActionResult InsertUpdateIssueData(TaskTypeInsertUpdateRequest request)
        {
            Common common = _cardsDL.addTaskTypeRecord(request);
            return Ok(common);
        }

        //[Route("TMTService/addSystemIssue")]
        //[ResponseType(typeof(HttpResponseMessage))]
        //[HttpPost]
        //public IHttpActionResult InsertUpdateIssueData(SystemErrorInsertRequest request)
        //{
        //    CardsDL cardsdl = new CardsDL();
        //    Common common = cardsdl.InsertUpdateIssueData(request);
        //    return Ok(common);
        //}
        [Route("TMTService/addSystemIssue")]
        [HttpPost]
        public async Task<IActionResult> InsertUpdateIssueData(SystemErrorInsertRequest request)
        {

            try
            {
                Common common = await _cardsDL.InsertUpdateIssueDataAsync(request);
                return Ok(common);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [Route("TMTService/updateCardIssueByQA")]
        [HttpPost]
        public IActionResult updateCardIssueByQA(updateCardIssueByQARequest request)
        {
            Common resp = _cardsDL.updateCardIssueByQA(request);
            return Ok(resp);
        }


        [Route("TMTService/fetchDashboardDetailsReport")]
        [HttpPost]

        public IActionResult fetchDashboardDetailsReport(fetchDashboardDetailsReportRequest request)
        {
            List<fetchDashboardDetailsReportResponse> respo = _cardsDL.fetchDashboardDetailsReport(request);
            return Ok(respo);
        }

        [Route("TMTService/InvestigationReport")]
        [HttpPost]

        public IActionResult InvestigationReport(InvestigationReportRequest request)
        {
            List<InvestigationReportResponse> respo = _cardsDL.InvestigationReport(request);
            return Ok(respo);
        }


        [Route("TMTService/GETBPEMDETAILS")]
        [HttpPost]

        public IActionResult GETBPEMDETAILS(BPEMDetailRequest request)
        {
            var response = _cardsDL.FetchBPEMDetails(request);
            return Ok(response);
        }

        [Route("TMTService/GetCasesTotals")]
        [HttpPost]

        //public IHttpActionResult GetCasesTotals(CountDataRequest rqst)
        //{
        //    CardsDL cardsdl = new CardsDL();
        //    var response = cardsdl.GetCountData(rqst);
        //    return Ok(response);
        //}
        public IActionResult GetCasesTotals(CountDataRequest rqst)
        {
            CasesTotalsResponse response = _cardsDL.GetCountData(rqst);
            return Ok(response);
        }

        [Route("TMTService/GetQACounts")]
        [HttpPost]
        public IActionResult GetQaNumbersData(QaNumbersRequest rqst)
        {
            CasesTotalsResponse response = _cardsDL.GetQaNumbersData(rqst);
            return Ok(response);
        }


        [Route("TMTService/GetCaseHistoryInformation/{cardId}")]
        [HttpGet]
        public IActionResult GetCaseHistoryInformation(int cardId)
        {
            JsonResult response = _cardsDL.GetCaseHistoryInformation(cardId);
            return Ok(response);
        }
    }
}
