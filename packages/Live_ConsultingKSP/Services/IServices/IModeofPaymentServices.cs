using Live_ConsultingKSP.Models;

namespace Live_ConsultingKSP.Services
{
    public interface IModeofPaymentServices
    {
        void AddModeofPayment(AcModeOfPayment modeOfPayment);
        List<AcModeOfPayment> GetAllModeofPayment(string cmpuuid, string envuuid);
        AcModeOfPayment GetByModeofPayment(Guid uuid);
        void UpdateModeofPayment(AcModeOfPayment payment);
        void DeleteModeofPayment(Guid uuid);
    }
}
