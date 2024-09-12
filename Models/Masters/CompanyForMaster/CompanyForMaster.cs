namespace TMT_Code_Migration1.Models.Masters.CompanyForMaster
{
    public class CompanyForMasterData
    {
        public int? companyId { get; set; }
        public string? companyName { get; set; }
        public string? status { get; set; }
        public string? companyDesc { get; set; }
    }
    public class InsertUpdateCompanyForMasterData
    {
        public int? returnCode { get; set; }
        public string? returnText { get; set; }
    }
    public class InsUpdateCompanyForMasterRequest
    {
        public string? operationType { get; set; }
        public int? companyId { get; set; }
        public string? companyName { get; set; }
        public string? status { get; set; }
        public string? companyDesc { get; set; }
    }
}
