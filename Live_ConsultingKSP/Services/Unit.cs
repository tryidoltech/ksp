using Live_ConsultingKSP.Models;
using System.Diagnostics.Metrics;

namespace Live_ConsultingKSP.Services
{
    public class Unit : IUnit
    {
        private readonly KsperpDbContext context;

        public Unit(KsperpDbContext context)
        {
            this.context = context;
        }

        public void AddUnit(AcUnit acUnit)
        {
            bool isDuplicate = context.AcUnits
                     .Any(c => c.UnitName == acUnit.UnitName && c.UnitShortName == acUnit.UnitShortName);

            if (isDuplicate)
            {
                throw new Exception("Data Already Exists!");
            }


            acUnit.IsDisplay = acUnit.IsDisplay;
            acUnit.Uuid = Guid.NewGuid().ToString();
            acUnit.IsActive = true;
            acUnit.IsAddedOn = DateTime.Now;
            acUnit.IsAddedBy = "1";
            context.AcUnits.Add(acUnit);
            context.SaveChanges();
        }

        public void DeleteUnit(Guid uuid)
        {
            var result = context.AcUnits.FirstOrDefault(c => c.Uuid == uuid.ToString());
            if (result != null)
            {
                result.IsDeleteOn = DateTime.Now;
                result.IsDeletedBy = "1";
                result.IsActive = false;
                context.AcUnits.Update(result);
                context.SaveChanges();
            }
        }

        public List<AcUnit> GetAllUnit(string cmpuuid, string envuuid)
        {
            return context.AcUnits.Where(c => c.IsActive == true && c.MasterCompanyUuid == cmpuuid && c.MasterEnvironmentUuid == envuuid)
                .OrderByDescending(c => c.UnitId).ToList();
        }

        public AcUnit GetByUnit(Guid uuid)
        {
            return context.AcUnits.FirstOrDefault(c => c.Uuid == uuid.ToString());
        }

        public void UpdateUnit(AcUnit unit)
        {
            bool isDuplicate = context.AcUnits
        .Any(c => c.UnitName == unit.UnitName && c.UnitShortName == unit.UnitShortName
                  && c.Uuid != unit.Uuid);

            if (isDuplicate)
            {
                throw new Exception("Data Already Exists!");
            }
            var existingEnvironment = context.AcUnits.FirstOrDefault(c => c.Uuid == unit.Uuid);
            if (existingEnvironment != null)
            {
                existingEnvironment.UnitName = unit.UnitName;
                existingEnvironment.UnitShortName = unit.UnitShortName;
                existingEnvironment.Mode = unit.Mode;
                existingEnvironment.IsDisplay = unit.IsDisplay;
                existingEnvironment.IsUpdatedOn = DateTime.Now;
                existingEnvironment.IsUpdateBy = "1";

                context.SaveChanges();
            }
        }
    }
}
