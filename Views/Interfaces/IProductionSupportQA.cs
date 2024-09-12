using Microsoft.AspNetCore.Mvc;
using TMT_Code_Migration1.Models.Production_Support_QA;

namespace TMT_Code_Migration1.Views.Interfaces
{
    public interface IProductionSupportQA
    {
        JsonResult GET_GETCARDBYUSERQA_SEARCH(QA_Search_request request, string Dynamic_sp);
        JsonResult GET_GETCARDBYUSERQA_SEARCHById(int request, string Dynamic_sp);
        List<StatusForQAResponse> GetCardStatusDL();
        List<QAEDUsers> GetQAEDByUserDL(string loginUser);
        updateResponse UpdateCardForQA(List<updateCardRequestForQA> requests);
        List<QAStatus> GetDistinctQAStatus();
    }
}
