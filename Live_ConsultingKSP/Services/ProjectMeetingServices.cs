using Live_ConsultingKSP.Models;
using Live_ConsultingKSP.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace Live_ConsultingKSP.Services
{
    public class ProjectMeetingServices : IProjectMeetingServices
    {
        private readonly KsperpDbContext _context;

        public ProjectMeetingServices(KsperpDbContext context)
        {
            _context = context;
        }

        public void AddProjectMeeting(ProjectProjectMeeting model)
        {
            bool isDuplicate = _context.ProjectProjectMeetings.Any(c => c.CompanyName == model.CompanyName && c.MeetingDate == model.MeetingDate 
            && c.MeetingTime == model.MeetingTime);
            if (isDuplicate)
            {
                throw new Exception("Data Already Exists!");
            }
            model.CompanyName = model.CompanyName;
            model.ContactPersonName = model.ContactPersonName;
            model.IndustrySectorUuid = model.IndustrySectorUuid;
            model.MeetingDate = model.MeetingDate;
            model.MeetingTime = model.MeetingTime;
            model.MeetingDocument = model.MeetingDocument;
            model.AttendeesUuid = model.AttendeesUuid;
            model.Description = model.Description;
            model.IsActive = true;
            model.Uuid = Guid.NewGuid().ToString();
            model.IsAddedOn = DateTime.Now;
            model.IsAddedBy = "1";
            _context.ProjectProjectMeetings.Add(model);

             _context.SaveChanges();
        }

        public void DeleteProjectMeeting(string uuid)
        {
            var model = _context.ProjectProjectMeetings.FirstOrDefault(c => c.Uuid == uuid.ToString());
            if (model != null)
            {
                model.IsActive = false;
                model.IsDeletedBy = "1";
                model.IsDeletedOn = DateTime.Now;
                _context.ProjectProjectMeetings.Update(model);
                _context.SaveChanges();
            }
        }
        public IEnumerable<ProjectProjectMeeting> GetAllProjectMeeting(string cmpuuid, string envuuid)
        {
            return _context.ProjectProjectMeetings
                .Where(x => (bool)x.IsActive && x.MasterCompanyUuid == cmpuuid && x.MasterEnvironmentUuid == envuuid)
                .OrderByDescending(x => x.Id)
                .ToList();
        }

        public Task<ProjectProjectMeeting?> GetProjectMeetingByUuid(string uuid)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateProjectMeeting(ProjectProjectMeeting model)
        {
            var result = await _context.ProjectProjectMeetings.FirstOrDefaultAsync(b => b.Uuid == model.Uuid.ToString());

            if (result != null)
            {
                result.CompanyName = model.CompanyName;
                result.ContactPersonName = model.ContactPersonName;
                result.IndustrySectorUuid = model.IndustrySectorUuid;
                result.MeetingDate = model.MeetingDate;
                result.MeetingTime = model.MeetingTime;
                result.MeetingDocument = model.MeetingDocument;
                result.AttendeesUuid = model.AttendeesUuid;
                result.Description = model.Description;
                result.IsDisplay = model.IsDisplay;
                result.IsUpdatedBy = "1";
                result.IsUpdatedOn = DateTime.Now;
                _context.ProjectProjectMeetings.Update(result);
                await _context.SaveChangesAsync();

            }
        }
    }
}
