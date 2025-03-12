using Live_ConsultingKSP.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Live_ConsultingKSP.Services
{
    public class DesignationService : IDesignationService
    {
        private readonly KsperpDbContext _context;

        public DesignationService(KsperpDbContext context)
        {
            _context = context;
        }
        public MasterDesignation GetDesignationByUuid(Guid uuid)
        {
            return _context.MasterDesignations.FirstOrDefault(d => d.Uuid == uuid.ToString());
        }

        public void AddDesignation(MasterDesignation designation)
        {
            if (_context.MasterDesignations.Any(d => d.DesignationName == designation.DesignationName))
                throw new Exception("Designation already exists!");

            designation.Uuid = Guid.NewGuid().ToString();
            designation.IsActive = true;
            designation.IsAdddedOn = DateTime.Now;
            designation.IsAddedBy = "1";

            _context.MasterDesignations.Add(designation);
            _context.SaveChanges();
        }

        public List<MasterDesignation> GetAllDesignations(string cmpuuid, string envuuid)
        {
            return _context.MasterDesignations
                .Where(d => (bool)d.IsActive && d.MasterCompanyUuid == cmpuuid && d.MasterEnvironmentUuid == envuuid) 
                .OrderByDescending(d => d.DesignationId)
                .ToList();
        }


      

        public void UpdateDesignation(MasterDesignation designation)
        {
            var existingDesignation = _context.MasterDesignations.FirstOrDefault(d => d.Uuid == designation.Uuid);
            if (existingDesignation == null) throw new Exception("Designation not found!");

            if (_context.MasterDesignations.Any(d => d.DesignationName == designation.DesignationName && d.Uuid != designation.Uuid))
                throw new Exception("Designation name already exists!");

            existingDesignation.DesignationName = designation.DesignationName;
            existingDesignation.DesignationShortName = designation.DesignationShortName;
            existingDesignation.IsDisplay = designation.IsDisplay;
            existingDesignation.IsUpdatedOn = DateTime.Now;
            existingDesignation.IsUpdatedBy = "1";

            _context.SaveChanges();
        }

        public void DeleteDesignation(Guid uuid)
        {
            var designation = _context.MasterDesignations.FirstOrDefault(d => d.Uuid == uuid.ToString());
            if (designation == null) throw new Exception("Designation not found!");

            designation.IsActive = false;
            designation.IsDeletedOn = DateTime.Now;
            designation.IsDeletedBy = "1";

            _context.MasterDesignations.Update(designation);
            _context.SaveChanges();
        }
        public List<MasterDesignation> GetAllEmployeeDesignations()
        {
            return _context.MasterDesignations
                .Where(d => d.IsActive == true && d.IsDisplay == true)
                .OrderBy(d => d.DesignationName)
                .ToList();
        }

     

    }
}
