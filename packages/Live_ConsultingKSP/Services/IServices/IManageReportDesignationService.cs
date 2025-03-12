using Live_ConsultingKSP.Models;

namespace Live_ConsultingKSP.Services
{
    public interface IManageReportDesignationService
    {
        void AddManageReportDesignation(ErManageReportDesignation designation);
        Task<List<ErManageReportDesignation>> GetAllManageReportDesignations(string cmpuuid, string envuuid);
        /*Task<IEnumerable<ErManageReportDesignation>> GetAllManageReportDesignationsAsync(string cmpuuid, string envuuid);*/
        ErManageReportDesignation GetManageReportDesignationByUUID(Guid uuid);
        void UpdateManageReportDesignation(ErManageReportDesignation designation);
        void  DeleteManageReportDesignation(Guid uuid);

    }
}
