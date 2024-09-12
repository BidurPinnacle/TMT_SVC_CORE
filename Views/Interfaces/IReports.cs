using TMT_Code_Migration1.Models.Reports;

namespace TMT_Code_Migration1.Views.Interfaces
{
    public interface IReports
    {
        public List<GetClientReportResponse> GetClientReport(GetClientReportRequest request);
        public List<ReportsCardsDetailsResponse> GetCardsDetails(ReportsCardsDetailsRequest request);
        public List<GetReportResponse> GetReport(GetReportRequest request);
    }
}
