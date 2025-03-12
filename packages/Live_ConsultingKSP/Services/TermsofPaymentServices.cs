using Live_ConsultingKSP.Models;

namespace Live_ConsultingKSP.Services
{
    public class TermsofPaymentServices : ITermsofPaymentServices
    {
        private readonly KsperpDbContext context;

        public TermsofPaymentServices(KsperpDbContext context)
        {
            this.context = context;
        }

        public void AddTermsOfPayment(AcTermsOfPayment acTermsOfPayment)
        {
            bool isDuplicate = context.AcTermsOfPayments
            .Any(c => c.Title == acTermsOfPayment.Title && c.Description == acTermsOfPayment.Description);

            if (isDuplicate)
            {
                throw new Exception("Data Already Exists!");
            }


            acTermsOfPayment.IsDisplay = acTermsOfPayment.IsDisplay;
            acTermsOfPayment.Uuid = Guid.NewGuid().ToString();
            acTermsOfPayment.IsActive = true;
            acTermsOfPayment.IsAddedOn = DateTime.Now;
            acTermsOfPayment.IsAddedBy = "1";
            context.AcTermsOfPayments.Add(acTermsOfPayment);
            context.SaveChanges();
        }

        public void DeleteTermsOfPayment(Guid uuid)
        {
            var result = context.AcTermsOfPayments.FirstOrDefault(c => c.Uuid == uuid.ToString());
            if (result != null)
            {
                result.IsDeleteOn = DateTime.Now;
                result.IsDeletedBy = "1";
                result.IsActive = false;
                context.AcTermsOfPayments.Update(result);
                context.SaveChanges();
            }
        }

        public List<AcTermsOfPayment> GetAllTermsOfPayment(string cmpuuid, string envuuid)
        {
            return context.AcTermsOfPayments.Where(c => c.IsActive == true && c.MasterCompanyUuid == cmpuuid && c.MasterEnvironmentUuid == envuuid)
                .OrderByDescending(c => c.TermsOfPaymentId).ToList();
        }

        public AcUnit GetByTermsOfPayment(Guid uuid)
        {
            throw new NotImplementedException();
        }

        public void UpdateTermsOfPayment(AcTermsOfPayment payment)
        {
            bool isDuplicate = context.AcTermsOfPayments
        .Any(c => c.Title == payment.Title && c.Description == payment.Description
                  && c.Uuid != payment.Uuid);

            if (isDuplicate)
            {
                throw new Exception("Data Already Exists!");
            }
            var existingEnvironment = context.AcTermsOfPayments.FirstOrDefault(c => c.Uuid == payment.Uuid);
            if (existingEnvironment != null)
            {
                existingEnvironment.Title = payment.Title;
                existingEnvironment.Description = payment.Description;
                existingEnvironment.IsDisplay = payment.IsDisplay;
                existingEnvironment.IsUpdatedOn = DateTime.Now;
                existingEnvironment.IsUpdateBy = "1";

                context.SaveChanges();
            }
        }
    }
}
