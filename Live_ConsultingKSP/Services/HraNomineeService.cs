using Live_ConsultingKSP.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Live_ConsultingKSP.Services
{
    public class HraNomineeService : IHraNomineeService
    {
        private readonly KsperpDbContext _context;

        public HraNomineeService(KsperpDbContext context)
        {
            _context = context;
        }

        public void AddNominee(HraNominee nominee)
        {
            if (_context.HraNominees.Any(n => n.NomineeName == nominee.NomineeName))
                throw new Exception("Nominee already exists!");

            nominee.IsActive = true;
            nominee.IsAddedOn = DateTime.Now;
            nominee.IsAddedBy = "1";

            _context.HraNominees.Add(nominee);
            _context.SaveChanges();
        }

        public List<HraNominee> GetAllNominees()
        {
            return _context.HraNominees
                .Where(n => n.IsActive == true)
                .OrderByDescending(n => n.NomineeId)
                .ToList();
        }

        public HraNominee GetNomineeByUUID(string uuid)
        {
            return _context.HraNominees.FirstOrDefault(n => n.Uuid == uuid);
        }

        public void UpdateNominee(HraNominee nominee)
        {
            var existingNominee = _context.HraNominees.FirstOrDefault(n => n.Uuid == nominee.Uuid);
            if (existingNominee == null) throw new Exception("Nominee not found!");

            if (_context.HraNominees.Any(n => n.NomineeName == nominee.NomineeName && n.Uuid != nominee.Uuid))
                throw new Exception("Nominee with the same name already exists!");

            existingNominee.NomineeName = nominee.NomineeName;
            existingNominee.IsDisplay = nominee.IsDisplay;
            existingNominee.IsUpdatedOn = DateTime.Now;
            existingNominee.IsUpdatedBy = "1";

            _context.SaveChanges();
        }

        public void DeleteNominee(string uuid)
        {
            var nominee = _context.HraNominees.FirstOrDefault(n => n.Uuid == uuid);
            if (nominee == null) throw new Exception("Nominee not found!");

            nominee.IsActive = false;
            nominee.IsDeletedOn = DateTime.Now;
            nominee.IsDeletedBy = "1";

            _context.SaveChanges();
        }
    }
}
