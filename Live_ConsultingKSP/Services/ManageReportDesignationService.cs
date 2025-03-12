using Live_ConsultingKSP.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Live_ConsultingKSP.Services
{
    public class ManageReportDesignationService : IManageReportDesignationService
    {
        private readonly KsperpDbContext _context;

        public ManageReportDesignationService(KsperpDbContext context)
        {
            _context = context;
        }

        public void AddManageReportDesignation(ErManageReportDesignation designation)
        {
            if (_context.ErManageReportDesignations.Any(d => d.DesignationName == designation.DesignationName))
                throw new Exception("Manage Report Designation already exists!");

            designation.Uuid = Guid.NewGuid().ToString();
            designation.IsActive = true;
            designation.IsAddedOn = DateTime.Now;
            designation.IsAddedBy = "1";

            _context.ErManageReportDesignations.Add(designation);
            _context.SaveChanges();
        }

        public async Task<List<ErManageReportDesignation>> GetAllManageReportDesignations(string cmpuuid, string envuuid)
        {
            return  _context.ErManageReportDesignations
                .Where(d => (bool)d.IsActive && d.MasterCompanyUuid == cmpuuid && d.MasterEnvironmentUuid == envuuid)
                .OrderByDescending(d => d.ManageReportDesignationId)
                .ToList();
        }
     /*   public async Task<IEnumerable<ErManageReportDesignation>> GetAllManageReportDesignationsAsync(string cmpuuid, string envuuid)
        {
            return await _context.ErManageReportDesignations
                .Where(d => d.IsActive ==true  && d.MasterCompanyUuid == cmpuuid && d.MasterEnvironmentUuid == envuuid)
                .OrderByDescending(d => d.ManageReportDesignationId)
                .ToListAsync();
        }*/

      

        public ErManageReportDesignation GetManageReportDesignationByUUID(Guid uuid)
        {
            return _context.ErManageReportDesignations.FirstOrDefault(d => d.Uuid == uuid.ToString());
        }

        public void UpdateManageReportDesignation(ErManageReportDesignation designation)
        {
            var existingDesignation = _context.ErManageReportDesignations.FirstOrDefault(d => d.Uuid == designation.Uuid);
            if (existingDesignation == null) throw new Exception("Manage Report Designation not found!");

            if (_context.ErManageReportDesignations.Any(d => d.DesignationName == designation.DesignationName && d.Uuid != designation.Uuid))
                throw new Exception("Manage Report Designation with the same name already exists!");

            existingDesignation.DesignationName = designation.DesignationName;
            existingDesignation.IsReportToManagement = designation.IsReportToManagement;
            existingDesignation.NoOfErApproval = designation.NoOfErApproval;
            existingDesignation.NoOfPafApproval = designation.NoOfPafApproval;
            existingDesignation.IsDisplay = designation.IsDisplay;
            existingDesignation.IsUpdatedOn = DateTime.Now;
            existingDesignation.IsUpdatedBy = "1";

            _context.SaveChanges();
        }

        public void DeleteManageReportDesignation(Guid uuid)
        {
            var designation = _context.ErManageReportDesignations.FirstOrDefault(d => d.Uuid == uuid.ToString());
            if (designation == null) throw new Exception("Manage Report Designation not found!");

            designation.IsActive = false;
            designation.IsDeletedOn = DateTime.Now;
            designation.IsDeletedBy = "1";

            _context.SaveChanges();
        }
    }
}
