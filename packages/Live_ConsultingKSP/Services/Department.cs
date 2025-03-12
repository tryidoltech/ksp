using Live_ConsultingKSP.Models;
using Live_ConsultingKSP.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Live_ConsultingKSP.Repository
{
    public class Department : IDepartment
    {
        private readonly KsperpDbContext _context;

        public Department(KsperpDbContext context)
        {
            _context = context;
        }

        public void AddDepartment(MasterDepartment department)
        {
            if (_context.MasterDepartments.Any(d => d.DepartmentName == department.DepartmentName))
                throw new Exception("Department already exists!");

            department.Uuid = Guid.NewGuid().ToString();
            department.IsActive = true;
            department.IsAddedOn = DateTime.Now;
            department.IsAddedBy = "1";

            _context.MasterDepartments.Add(department);
            _context.SaveChanges();
        }

        public List<MasterDepartment> GetAllDepartments(string cmpuuid, string envuuid)
        {
            return _context.MasterDepartments
                .Where(d => (bool)d.IsActive && d.MasterCompanyUuid == cmpuuid && d.MasterEnvironmentUuid == envuuid)
                .OrderByDescending(d => d.DepartmentId)
                .ToList();
        }

        public MasterDepartment GetDepartmentByUUID(Guid uuid)
        {
            return _context.MasterDepartments.FirstOrDefault(d => d.Uuid == uuid.ToString());
        }

        public void UpdateDepartment(MasterDepartment department)
        {
            var existingDepartment = _context.MasterDepartments.FirstOrDefault(d => d.Uuid == department.Uuid);
            if (existingDepartment == null) throw new Exception("Department not found!");

            if (_context.MasterDepartments.Any(d => d.DepartmentName == department.DepartmentName && d.Uuid != department.Uuid))
                throw new Exception("Department name already exists!");

            existingDepartment.DepartmentName = department.DepartmentName;
            existingDepartment.IsDisplay = department.IsDisplay;
            existingDepartment.IsUpdatedOn = DateTime.Now;
            existingDepartment.IsUpdatedBy = "1";

            _context.SaveChanges();
        }

        public void DeleteDepartment(Guid uuid)
        {
            var department = _context.MasterDepartments.FirstOrDefault(d => d.Uuid == uuid.ToString());
            if (department == null) throw new Exception("Department not found!");

            department.IsActive = false;
            department.IsDeletedOn = DateTime.Now;
            department.IsDeletedBy = "1";

            _context.MasterDepartments.Update(department);
            _context.SaveChanges();
        }
    }
}
