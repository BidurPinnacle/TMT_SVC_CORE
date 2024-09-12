namespace TMT_Code_Migration1.Models.Masters.Work_Type
{
    public class WorkTypeData
    {
        public int? WorkTypeValue { get; set; }
        public string? WorkypeName { get; set; }
        public string? WorkTypeDescription { get; set; }
        public int? TaskId { get; set; }
        public string? TaskText { get; set; }
        public string? ActiveStatus { get; set; }
        public int? TargetPerDay { get; set; }
    }
    public class InsertUpdateWorkTypeData
    {
        public int? returnCode { get; set; }
        public string? returnText { get; set; }
    }
    public class InsUpdateWorkRequest
    {
        public string? operationType { get; set; }
        public int? taskId { get; set; }
        public string? workName { get; set; }
        public string? workDescription { get; set; }
        public int? workValue { get; set; }
        public string? ActiveStatus { get; set; }
        public int? Target { get; set; }
    }
}
