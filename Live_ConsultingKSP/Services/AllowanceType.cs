using Live_ConsultingKSP.Models;
using Live_ConsultingKSP.Services.IServices;

namespace Live_ConsultingKSP.Services
{
    public class AllowanceType : IAllowanceType
    {
        private readonly KsperpDbContext context;

        public AllowanceType(KsperpDbContext context)
        {
            this.context = context;
        }
        public void AddAllowanceType(AcAllowanceType acAllowance)
        {
            bool isDuplicate = context.AcAllowanceTypes
           .Any(c => c.Title == acAllowance.Title);

            if (isDuplicate)
            {
                throw new Exception("Data Already Exists!");
            }


            acAllowance.IsDisplay = acAllowance.IsDisplay;
            acAllowance.Uuid = Guid.NewGuid().ToString();
            acAllowance.IsActive = true;
            acAllowance.IsAddedOn = DateTime.Now;
            acAllowance.IsAddedBy = "1";
            context.AcAllowanceTypes.Add(acAllowance);
            context.SaveChanges();
        }

        public void DeleteAllowanceType(Guid uuid)
        {
            var result = context.AcAllowanceTypes.FirstOrDefault(c => c.Uuid == uuid.ToString());
            if (result != null)
            {
                result.IsDeleteOn = DateTime.Now;
                result.IsDeletedBy = "1";
                result.IsActive = false;
                context.AcAllowanceTypes.Update(result);
                context.SaveChanges();
            }
        }

        public List<AcAllowanceType> GetAllAllowanceType(string cmpuuid, string envuuid)
        {
            return context.AcAllowanceTypes.Where(c => c.IsActive == true && c.MasterCompanyUuid == cmpuuid && c.MasterEnvironmentUuid == envuuid)
                .OrderByDescending(c => c.AllowanceTypeId).ToList();
        }

        public AcAllowanceType GetByAllowanceType(Guid uuid)
        {
            throw new NotImplementedException();
        }

        public void UpdateAllowanceType(AcAllowanceType acAllowance)
        {
            bool isDuplicate = context.AcAllowanceTypes
        .Any(c => c.Title == acAllowance.Title 
                  && c.Uuid != acAllowance.Uuid);

            if (isDuplicate)
            {
                throw new Exception("Data Already Exists!");
            }
            var existingEnvironment = context.AcAllowanceTypes.FirstOrDefault(c => c.Uuid == acAllowance.Uuid);
            if (existingEnvironment != null)
            {
                existingEnvironment.Title = acAllowance.Title;
                existingEnvironment.Code = acAllowance.Code;
                existingEnvironment.Type = acAllowance.Type;
                existingEnvironment.Remark = acAllowance.Remark;
                existingEnvironment.IsDisplay = acAllowance.IsDisplay;
                existingEnvironment.IsUpdatedOn = DateTime.Now;
                existingEnvironment.IsUpdateBy = "1";

                context.SaveChanges();
            }
        }
    }
}
