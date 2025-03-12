using Live_ConsultingKSP.Models;

namespace Live_ConsultingKSP.Services
{
    public interface IModeofTransportServices
    {
        void AddModeofTransport(AcModeOfTransport acModeOfTransport);
        List<AcModeOfTransport> GetAllModeofTransport(string cmpuuid, string envuuid);
        AcUnit GetByModeofTransport(Guid uuid);
        void UpdateModeofTransport(AcModeOfTransport transport);
        void DeleteModeofTransport(Guid uuid);
    }
}
