using TMT_Code_Migration1.Models.Masters.System_Issue;

namespace TMT_Code_Migration1.Views.Interfaces.Masters
{
    public interface ISystemIssueList
    {
        List<SystemIssueListData> GetIssueListDatas();
        List<SystemIssueListData> GetIssueListDatasById(int sid);
        InsertUpdateSystemIssueListData insertUpdateSystemIssue(InsUpdateSystemIssueRequest rqst);
    }
}
