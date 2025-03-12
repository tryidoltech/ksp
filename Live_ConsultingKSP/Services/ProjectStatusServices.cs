using Live_ConsultingKSP.Models;
using Live_ConsultingKSP.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace Live_ConsultingKSP.Services
{
    public class ProjectStatusServices : IProjectStatusServices
    {
        private readonly KsperpDbContext _context;
        public ProjectStatusServices(KsperpDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ProjectProjectStatus>> GetAllProjectStatusAsync(string cmpuuid, string envuuid)
        {
            return await _context.ProjectProjectStatuses.Where(x => x.IsActive == true && x.MasterCompanyUuid == cmpuuid
            && x.MasterEnvironmentUuid == envuuid).OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<ProjectProjectStatus?> GetProjectStatusByUuidAsync(string uuid)
        {
            return await _context.ProjectProjectStatuses.FirstOrDefaultAsync(c => c.Uuid == uuid.ToString());
        }

        public async Task AddProjectStatusAsync(ProjectProjectStatus model)
        {
            bool isDuplicate = _context.ProjectProjectStatuses.Any(x => x.StatusTitle == model.StatusTitle);
            if (isDuplicate)
            {
                throw new Exception("Data Already Exists!");
            }
            model.IsActive = true;
            model.Uuid = Guid.NewGuid().ToString();
            model.IsAddedOn = DateTime.Now;
            model.IsAddedBy = "1";
            _context.ProjectProjectStatuses.Add(model);

            await _context.SaveChangesAsync();
        }
        public async Task UpdateProjectStatusAsync(ProjectProjectStatus model)
        {
            var result = await _context.ProjectProjectStatuses.FirstOrDefaultAsync(b => b.Uuid == model.Uuid.ToString());

            if (result != null)
            {
                result.StatusTitle = model.StatusTitle;
                result.IsDisplay = model.IsDisplay;
                result.IsUpdatedBy = "1";
                result.IsUpdatedOn = DateTime.Now;
                _context.ProjectProjectStatuses.Update(result);
                await _context.SaveChangesAsync();

            }
        }
        public void DeleteProjectStatus(string uuid)
        {
            var model = _context.ProjectProjectStatuses.FirstOrDefault(c => c.Uuid == uuid.ToString());
            if (model != null)
            {
                model.IsActive = false;
                model.IsDeletedBy = "1";
                model.IsDeletedOn = DateTime.Now;
                _context.ProjectProjectStatuses.Update(model);
                _context.SaveChanges();
            }
        }

    }
}
