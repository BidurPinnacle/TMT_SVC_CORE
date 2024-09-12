
namespace TMT_Code_Migration1.Models.Masters.Status
{
    public class StatusData
    {
        public int? statusId { get; set; }
        public string? statusText { get; set; }
        public string? statusActive { get; set; }
        public int? statusRank { get; set; }
    }
    public class InsertUpdateStatusData
    {
        public int? returnCode { get; set; }
        public string? returnText { get; set; }
    }
    public class InsUpdateStatusRequest
    {
        public string? operationType { get; set; }
        public int? statusId { get; set; } // Nullable int
        public string? statusText { get; set; }
        public string? statusActive { get; set; }
        public int? statusRank { get; set; } // Nullable int
    }
}
