using Live_ConsultingKSP.Models;

namespace Live_ConsultingKSP.Services
{
    public class ModeofTransportServices : IModeofTransportServices
    {
        private readonly KsperpDbContext context;

        public ModeofTransportServices(KsperpDbContext context)
        {
            this.context = context;
        }

        public void AddModeofTransport(AcModeOfTransport acModeOfTransport)
        {
            bool isDuplicate = context.AcTermsOfPayments
            .Any(c => c.Title == acModeOfTransport.Title && c.Description == acModeOfTransport.Description);

            if (isDuplicate)
            {
                throw new Exception("Data Already Exists!");
            }


            acModeOfTransport.IsDisplay = acModeOfTransport.IsDisplay;
            acModeOfTransport.Uuid = Guid.NewGuid().ToString();
            acModeOfTransport.IsActive = true;
            acModeOfTransport.IsAddedOn = DateTime.Now;
            acModeOfTransport.IsAddedBy = "1";
            context.AcModeOfTransports.Add(acModeOfTransport);
            context.SaveChanges();
        }

        public void DeleteModeofTransport(Guid uuid)
        {
            var result = context.AcModeOfTransports.FirstOrDefault(c => c.Uuid == uuid.ToString());
            if (result != null)
            {
                result.IsDeleteOn = DateTime.Now;
                result.IsDeletedBy = "1";
                result.IsActive = false;
                context.AcModeOfTransports.Update(result);
                context.SaveChanges();
            }
        }

        public List<AcModeOfTransport> GetAllModeofTransport(string cmpuuid, string envuuid)
        {
            return context.AcModeOfTransports.Where(c => c.IsActive == true && c.MasterCompanyUuid == cmpuuid && c.MasterEnvironmentUuid == envuuid )
                .OrderByDescending(c => c.ModeOfTransportId).ToList();
        }

        public AcUnit GetByModeofTransport(Guid uuid)
        {
            throw new NotImplementedException();
        }

        public void UpdateModeofTransport(AcModeOfTransport transport)
        {
            bool isDuplicate = context.AcTermsOfPayments
        .Any(c => c.Title == transport.Title && c.Description == transport.Description
                  && c.Uuid != transport.Uuid);

            if (isDuplicate)
            {
                throw new Exception("Data Already Exists!");
            }
            var existingEnvironment = context.AcTermsOfPayments.FirstOrDefault(c => c.Uuid == transport.Uuid);
            if (existingEnvironment != null)
            {
                existingEnvironment.Title = transport.Title;
                existingEnvironment.Description = transport.Description;
                existingEnvironment.IsDisplay = transport.IsDisplay;
                existingEnvironment.IsUpdatedOn = DateTime.Now;
                existingEnvironment.IsUpdateBy = "1";

                context.SaveChanges();
            }
        }
    }
}
