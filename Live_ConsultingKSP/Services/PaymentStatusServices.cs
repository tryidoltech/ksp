using Live_ConsultingKSP.Models;
using Live_ConsultingKSP.Services.IServices;

namespace Live_ConsultingKSP.Services
{
    public class PaymentStatusServices : IPaymentStatusServices
    {
        private readonly KsperpDbContext _context;

        public PaymentStatusServices(KsperpDbContext context)
        {
            _context = context;
        }
        public void AddPaymentStatus(AcPaymentStatus paymentStatus)
        {
            bool isDuplicate = _context.AcPaymentStatuses
.Any(c => c.Title == paymentStatus.Title);

            if (isDuplicate)
            {
                throw new Exception("Data Already Exists!");
            }


            paymentStatus.IsDisplay = paymentStatus.IsDisplay;
            paymentStatus.Uuid = Guid.NewGuid().ToString();
            paymentStatus.IsActive = true;
            paymentStatus.IsAddedOn = DateTime.Now;
            paymentStatus.IsAddedBy = "1";
            _context.AcPaymentStatuses.Add(paymentStatus);
            _context.SaveChanges();
        }

        public void DeletePaymentStatus(Guid uuid)
        {
            var result = _context.AcPaymentStatuses.FirstOrDefault(c => c.Uuid == uuid.ToString());
            if (result != null)
            {
                result.IsDeleteOn = DateTime.Now;
                result.IsDeletedBy = "1";
                result.IsActive = false;
                _context.AcPaymentStatuses.Update(result);
                _context.SaveChanges();
            }
        }

        public List<AcPaymentStatus> GetAllPaymentStatus(string cmpuuid, string envuuid)
        {
            return _context.AcPaymentStatuses.Where(c => c.IsActive == true && c.MasterCompanyUuid == cmpuuid && c.MasterEnvironmentUuid == envuuid)
                .OrderByDescending(c => c.PaymentStatusId).ToList();
        }

        public AcPaymentStatus GetByPaymentStatus(Guid uuid)
        {
            throw new NotImplementedException();
        }

        public void UpdatePaymentStatus(AcPaymentStatus acPaymentStatus)
        {
            bool isDuplicate = _context.AcPaymentStatuses
       .Any(c => c.Title == acPaymentStatus.Title
                 && c.Uuid != acPaymentStatus.Uuid);

            if (isDuplicate)
            {
                throw new Exception("Data Already Exists!");
            }
            var existingEnvironment = _context.AcAllowanceTypes.FirstOrDefault(c => c.Uuid == acPaymentStatus.Uuid);
            if (existingEnvironment != null)
            {
                existingEnvironment.Title = acPaymentStatus.Title;
                existingEnvironment.Code = acPaymentStatus.Title;
                existingEnvironment.IsDisplay = acPaymentStatus.IsDisplay;
                existingEnvironment.IsUpdatedOn = DateTime.Now;
                existingEnvironment.IsUpdateBy = "1";

                _context.SaveChanges();
            }
        }
    }
}
