using TMT_Code_Migration1.Models.Masters.Status;

namespace TMT_Code_Migration1.Views.Interfaces.Masters
{
    public interface IStatus
    {
        List<StatusData> GetStatusData();
        List<StatusData> GetStatusDataById(int statusId);
        InsertUpdateStatusData insertUpdateStatus(InsUpdateStatusRequest rqst);
    }
}
