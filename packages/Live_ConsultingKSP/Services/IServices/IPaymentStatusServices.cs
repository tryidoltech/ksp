using Live_ConsultingKSP.Models;

namespace Live_ConsultingKSP.Services.IServices
{
    public interface IPaymentStatusServices
    {
        void AddPaymentStatus(AcPaymentStatus paymentStatus);
        List<AcPaymentStatus> GetAllPaymentStatus(string cmpuuid, string envuuid);
        AcPaymentStatus GetByPaymentStatus(Guid uuid);
        void UpdatePaymentStatus(AcPaymentStatus acPaymentStatus);
        void DeletePaymentStatus(Guid uuid);
    }
}
