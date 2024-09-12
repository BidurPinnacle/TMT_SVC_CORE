namespace TMT_Code_Migration1.Models.Editors
{
    public class StatusRequest
    {
        public int? CardsID { get; set; }
        public string? Comment { get; set; }
        public string? updatedBy { get; set; }
        public string? CardStatus { get; set; }
        public string? QCId { get; set; }


    }

    public class EditorssubmitRequest
    {

        public string? updatedBy { get; set; }
        public string? CardStatusCurrent { get; set; }
        public string? CardStatusTobe { get; set; }



    }
    public class StatusResponse
    {
        public int? Code { get; set; }
        public string? Message { get; set; }



    }
}
