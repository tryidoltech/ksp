using Live_ConsultingKSP.Models;
using Live_ConsultingKSP.Services.IServices;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Live_ConsultingKSP.Services
{
    public class ProjectMeetingMOMServices : IProjectMeetingMOMServices
    {
        private readonly KsperpDbContext _context;
        public ProjectMeetingMOMServices(KsperpDbContext context)
        {
            _context = context;
        }
        public IEnumerable<SelectListItem> GetEmployeesForDropdown()
        {
            return _context.MasterEmployees
                           .Where(e => e.IsActive == true) // Ensure the employee is active
                           .Select(e => new SelectListItem
                           {
                               Value = e.Uuid, // UUID as the value to be stored
                               Text = $"{e.FirstName} {e.LastName}" // Combined name of employee
                           }).ToList();
        }

        public IEnumerable<ProjectProjectMom> GetAllProjectMeetingMOMAsync(string cmpuuid, string envuuid)
        {
            return _context.ProjectProjectMoms.Where(x => x.IsActive == true && x.MasterCompanyUuid == cmpuuid
            && x.MasterEnvironmentUuid == envuuid).OrderByDescending(x => x.Id).ToList();
        }

        public ProjectProjectMom? GetProjectMeetingMOMByUuidAsync(string uuid)
        {
            return _context.ProjectProjectMoms.FirstOrDefault(c => c.Uuid == uuid.ToString());
        }

        public void AddProjectMeetingMOMAsync(ProjectProjectMom model)
        {
            bool isDuplicate = _context.ProjectProjectMoms.Any(x => x.Company == model.Company 
            && x.MeetingDate == model.MeetingDate 
            && x.MeetingTime == model.MeetingTime
            && x.MeetingType == model.MeetingType
            && x.AttendeesFromOurCompanyName == model.AttendeesFromOurCompanyName);

            if (isDuplicate)
            {
                throw new Exception("Data Already Exists!");
            }
            model.IsActive = true;
            model.Uuid = Guid.NewGuid().ToString();
            model.IsAddedOn = DateTime.Now;
            model.IsAddedBy = "1";
            _context.ProjectProjectMoms.Add(model);

            _context.SaveChangesAsync();
        }
        public void UpdateProjectMeetingMOMAsync(ProjectProjectMom model)
        {
            var result = _context.ProjectProjectMoms.FirstOrDefault(b => b.Uuid == model.Uuid);

            if (result != null)
            {

                result.Company = model.Company;
                result.Company = model.Company;
                result.MeetingDate = model.MeetingDate;
                result.MeetingTime = model.MeetingTime;
                result.MeetingType = model.MeetingType;
                result.AttendeesFromOurCompanyName = model.AttendeesFromOurCompanyName;
                result.MeetingAjenda = model.MeetingAjenda;
                result.MeetingDocument = model.MeetingDocument;
                result.IsDisplay = model.IsDisplay;
                result.IsUpdatedBy = "1";
                result.IsUpdatedOn = DateTime.Now;
                _context.ProjectProjectMoms.Update(result);
                _context.SaveChangesAsync();

            }
        }
        public void DeleteProjectMeetingMOM(string uuid)
        {
            var model = _context.ProjectProjectMoms.FirstOrDefault(c => c.Uuid == uuid.ToString());
            if (model != null)
            {
                model.IsActive = false;
                model.IsDeletedBy = "1";
                model.IsDeletedOn = DateTime.Now;
                _context.ProjectProjectMoms.Update(model);
                _context.SaveChanges();
            }
        }

    }
}
