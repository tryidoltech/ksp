using Live_ConsultingKSP.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Live_ConsultingKSP.Services
{
    public class HraTaxRegimeService : IHraTaxRegimeService
    {
        private readonly KsperpDbContext _context;

        public HraTaxRegimeService(KsperpDbContext context)
        {
            _context = context;
        }

        public void AddTaxRegime(HraTaxRegime taxRegime)
        {
            if (_context.HraTaxRegimes.Any(tr => tr.TaxRegime == taxRegime.TaxRegime))
                throw new Exception("Tax Regime already exists!");

            taxRegime.IsActive = true;
            taxRegime.IsAddedOn = DateTime.Now;
            taxRegime.IsAddedBy = "1";

            _context.HraTaxRegimes.Add(taxRegime);
            _context.SaveChanges();
        }

        public List<HraTaxRegime> GetAllTaxRegimes()
        {
            return _context.HraTaxRegimes
                .Where(tr => tr.IsActive == true)
                .OrderByDescending(tr => tr.TaxRegimeId)
                .ToList();
        }

        public HraTaxRegime GetTaxRegimeByUUID(string uuid)
        {
            return _context.HraTaxRegimes.FirstOrDefault(tr => tr.Uuid == uuid);
        }

        public void UpdateTaxRegime(HraTaxRegime taxRegime)
        {
            var existingTaxRegime = _context.HraTaxRegimes.FirstOrDefault(tr => tr.Uuid == taxRegime.Uuid);
            if (existingTaxRegime == null) throw new Exception("Tax Regime not found!");

            if (_context.HraTaxRegimes.Any(tr => tr.TaxRegime == taxRegime.TaxRegime && tr.Uuid != taxRegime.Uuid))
                throw new Exception("Tax Regime with the same name already exists!");

            existingTaxRegime.TaxRegime = taxRegime.TaxRegime;
            existingTaxRegime.IsDisplay = taxRegime.IsDisplay;
            existingTaxRegime.IsUpdatedOn = DateTime.Now;
            existingTaxRegime.IsUpdatedBy = "1";

            _context.SaveChanges();
        }

        public void DeleteTaxRegime(string uuid)
        {
            var taxRegime = _context.HraTaxRegimes.FirstOrDefault(tr => tr.Uuid == uuid);
            if (taxRegime == null) throw new Exception("Tax Regime not found!");

            taxRegime.IsActive = false;
            taxRegime.IsDeletedOn = DateTime.Now;
            taxRegime.IsDeletedBy = "1";

            _context.SaveChanges();
        }
    }
}
