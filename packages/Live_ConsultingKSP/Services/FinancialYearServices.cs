using Live_ConsultingKSP.Models;
using Live_ConsultingKSP.Services.IServices;
using Microsoft.VisualBasic;
using System;

namespace Live_ConsultingKSP.Services
{
    public class FinancialYearServices : IFinancialYearServices
    {
        private readonly KsperpDbContext _context;

        public FinancialYearServices(KsperpDbContext context)
        {
            _context = context;
        }

        public void AddFinancialYear(AcFinancialYear acFinancialYear)
        {
            bool isDuplicate = _context.AcFinancialYears
           .Any(c => c.Title == acFinancialYear.Title);

            if (isDuplicate)
            {
                throw new Exception("Data Already Exists!");
            }
            var existingEnvironment = _context.AcFinancialYears.FirstOrDefault(c => c.Uuid == acFinancialYear.Uuid);
            
            acFinancialYear.IsDisplay = acFinancialYear.IsDisplay;
            
            acFinancialYear.IsActive = true;
            acFinancialYear.IsAddedOn = DateTime.Now;
            acFinancialYear.IsAddedBy = "1";
            _context.AcFinancialYears.Add(acFinancialYear);
            _context.SaveChanges();
        }
        

        public void DeleteFinancialYear(Guid uuid)
        {
            var result = _context.AcFinancialYears.FirstOrDefault(c => c.Uuid == uuid.ToString());
            if (result != null)
            {
                result.IsDeleteOn = DateTime.Now;
                result.IsDeletedBy = "1";
                result.IsActive = false;
                _context.AcFinancialYears.Update(result);
                _context.SaveChanges();
            }
        }

        public List<AcFinancialYear> GetAllFinancialYear(string cmpuuid, string envuuid)
        {
            return _context.AcFinancialYears.Where(c => c.IsActive == true && c.MasterCompanyUuid == cmpuuid && c.MasterEnvironmentUuid == envuuid)
                .OrderByDescending(c => c.FinancialYearId).ToList();
        }

        public AcFinancialYear GetByFinancialYear(Guid uuid)
        {
            throw new NotImplementedException();
        }

        public void UpdateFinancialYear(AcFinancialYear acFinancial)
        {
            bool isDuplicate = _context.AcFinancialYears
      .Any(c => c.Title == acFinancial.Title
                && c.Uuid != acFinancial.Uuid);

            if (isDuplicate)
            {
                throw new Exception("Data Already Exists!");
            }
            var existingEnvironment = _context.AcFinancialYears.FirstOrDefault(c => c.Uuid == acFinancial.Uuid);
            if (existingEnvironment != null)
            {
                existingEnvironment.Title = acFinancial.Title;
                existingEnvironment.StartDate = acFinancial.StartDate;
                existingEnvironment.EndDate = acFinancial.EndDate;
                existingEnvironment.IsDisplay = acFinancial.IsDisplay;
                existingEnvironment.IsUpdatedOn = DateTime.Now;
                existingEnvironment.IsUpdateBy = "1";

                _context.SaveChanges();
            }
        }
    }
}
