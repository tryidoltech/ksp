using Live_ConsultingKSP.Models;

namespace Live_ConsultingKSP.Services
{
    public interface ITermsofPaymentServices
    {
        void AddTermsOfPayment(AcTermsOfPayment acTermsOfPayment);
        List<AcTermsOfPayment> GetAllTermsOfPayment(string cmpuuid, string envuuid);
        AcUnit GetByTermsOfPayment(Guid uuid);
        void UpdateTermsOfPayment(AcTermsOfPayment payment);
        void DeleteTermsOfPayment(Guid uuid);
    }
}
