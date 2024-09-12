using TMT_Code_Migration1.Models.Masters.Root_Cause;

namespace TMT_Code_Migration1.Views.Interfaces.Masters
{
    public interface IRootCause
    {
        List<RootCauseForMasterData> GetRootCauseForMasterData();
        List<RootCauseForMasterDataById> GetRootCauseForMasterDataById(int causeId);
        InsertUpdateRootCauseForMasterData insertUpdateRootCauseForMaster(InsUpdateRootCauseForMasterRequest rqst);
    }
}
