using Live_ConsultingKSP.Models;
using Live_ConsultingKSP.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Live_ConsultingKSP.Services
{
    public class ProjectCredentialService : IProjectCredentialService
    {
        private readonly KsperpDbContext _context;

        public ProjectCredentialService(KsperpDbContext context)
        {
            _context = context;
        }

        public void AddProjectCredential(ProjectProjectCrendential credential)
        {
            bool isDuplicate = _context.ProjectProjectCrendentials
                     .Any(c => c.Title == credential.Title && c.ProjectUuid == credential.ProjectUuid);

            if (isDuplicate)
            {
                throw new Exception("Data Already Exists!");
            }

            credential.Uuid = Guid.NewGuid().ToString();
            credential.Title = credential.Title;
            credential.CredentialsDetails = credential.CredentialsDetails;
            credential.IsAddedBy = "1";
            credential.IsActive = true;
            credential.IsAddedOn = DateTime.Now;

            _context.ProjectProjectCrendentials.Add(credential);
            _context.SaveChanges();
        }

       

        public List<ProjectProjectCrendential> GetAllProjectCredentials(string cmpuuid, string envuuid)
        {
            return _context.ProjectProjectCrendentials.Where(c => c.IsActive == true && c.MasterCompanyUuid == cmpuuid && c.MasterEnvironmentUuid == envuuid)
                .OrderByDescending(c => c.Id).ToList();
        }

        public ProjectProjectCrendential GetByProjectCredential(Guid uuid)
        {
            return _context.ProjectProjectCrendentials.FirstOrDefault(c => c.Uuid == uuid.ToString());
        }

        public void UpdateProjectCredential(ProjectProjectCrendential credential)
        {
            bool isDuplicate = _context.ProjectProjectCrendentials
                .Any(c => c.Title == credential.Title && c.ProjectUuid == credential.ProjectUuid && c.Uuid != credential.Uuid);

            if (isDuplicate)
            {
                throw new Exception("Data Already Exists!");
            }

            var existingCredential = _context.ProjectProjectCrendentials.FirstOrDefault(c => c.Uuid == credential.Uuid);
            if (existingCredential != null)
            {
                existingCredential.Title = credential.Title;
                existingCredential.CredentialsDetails = credential.CredentialsDetails;
                existingCredential.IsDisplay = credential.IsDisplay;
                existingCredential.IsUpdatedOn = DateTime.Now;
                existingCredential.IsUpdatedBy = "1";

                _context.SaveChanges();
            }
        }

        public void DeleteProjectCredential(Guid uuid)
        {
            var result = _context.ProjectProjectCrendentials.FirstOrDefault(c => c.Uuid == uuid.ToString());
            if (result != null)
            {
                result.IsDeletedOn = DateTime.Now;
                result.IsDeletedBy = "1";
                result.IsActive = false;
                _context.ProjectProjectCrendentials.Update(result);
                _context.SaveChanges();
            }
        }
    }
}
