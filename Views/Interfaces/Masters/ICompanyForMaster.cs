using TMT_Code_Migration1.Models.Masters.CompanyForMaster;

namespace TMT_Code_Migration1.Views.Interfaces.Masters
{
    public interface ICompanyForMaster
    {
        List<CompanyForMasterData> GetCompanyForMasterData();
        List<CompanyForMasterData> GetCompanyForMasterDataById(int companyId);
        InsertUpdateCompanyForMasterData insertUpdateCompanyForMaster(InsUpdateCompanyForMasterRequest rqst);
    }
}
