using Live_ConsultingKSP.Models;
using System.Diagnostics.Metrics;

namespace Live_ConsultingKSP.Services
{
    public class ModeofPaymentServices : IModeofPaymentServices
    {
        private readonly KsperpDbContext context;

        public ModeofPaymentServices(KsperpDbContext context)
        {
            this.context = context;
        }
        public void AddModeofPayment(AcModeOfPayment modeOfPayment)
        {
            bool isDuplicate = context.AcModeOfPayments
                      .Any(c => c.Title == modeOfPayment.Title && c.Description == modeOfPayment.Description);

            if (isDuplicate)
            {
                throw new Exception("Data Already Exists!");
            }


            modeOfPayment.IsDisplay = modeOfPayment.IsDisplay;
            modeOfPayment.Uuid = Guid.NewGuid().ToString();
            modeOfPayment.IsActive = true;
            modeOfPayment.IsAddedOn = DateTime.Now;
            modeOfPayment.IsAddedBy = "1";
            context.AcModeOfPayments.Add(modeOfPayment);
            context.SaveChanges();
        }

        public void DeleteModeofPayment(Guid uuid)
        {
            var result = context.AcModeOfPayments.FirstOrDefault(c => c.Uuid == uuid.ToString());
            if (result != null)
            {
                result.IsDeleteOn = DateTime.Now;
                result.IsDeletedBy = "1";
                result.IsActive = false;
                context.AcModeOfPayments.Update(result);
                context.SaveChanges();
            }
        }

        public List<AcModeOfPayment> GetAllModeofPayment(string cmpuuid, string envuuid)
        {
            return context.AcModeOfPayments.Where(c => c.IsActive == true && c.MasterCompanyUuid == cmpuuid && c.MasterEnvironmentUuid == envuuid)
                .OrderByDescending(c => c.ModeOfPaymentId).ToList();
        }

        public AcModeOfPayment GetByModeofPayment(Guid uuid)
        {
            return context.AcModeOfPayments.FirstOrDefault(c => c.Uuid == uuid.ToString());
        }

        public void UpdateModeofPayment(AcModeOfPayment payment)
        {
            bool isDuplicate = context.AcModeOfPayments
       .Any(c => (c.Title == payment.Title)
                 && c.Uuid != payment.Uuid);

            if (isDuplicate)
            {
                throw new Exception("Data Already Exists!");
            }
            var existingEnvironment = context.AcModeOfPayments.FirstOrDefault(c => c.Uuid == payment.Uuid);
            if (existingEnvironment != null)
            {
                existingEnvironment.Title = payment.Title;
                existingEnvironment.Uuid = payment.Uuid;
                existingEnvironment.IsDisplay = payment.IsDisplay;
                existingEnvironment.IsUpdatedOn = DateTime.Now;
                existingEnvironment.IsUpdateBy = "1";

                context.SaveChanges();
            }
        }
    }
}
