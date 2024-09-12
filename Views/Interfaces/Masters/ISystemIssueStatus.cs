using TMT_Code_Migration1.Models.Masters.System_Issue;

namespace TMT_Code_Migration1.Views.Interfaces.Masters
{
    public interface ISystemIssueStatus
    {
        List<SystemIssueStatusData> GetSystemIssueStatusData();
        List<SystemIssueStatusData> GetSystemIssueStatusDataById(int statusId);
        InsertUpdateSystemIssueStatusData insertUpdateSystemIssueStatus(InsUpdateSystemIssueStatusRequest rqst);
    }
}
