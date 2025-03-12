using Live_ConsultingKSP.Models;
using Live_ConsultingKSP.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace Live_ConsultingKSP.Services
{
    public class MasterEnvironmentServices : IMasterEnvironmentServices
    {
        private readonly KsperpDbContext _context;
        public MasterEnvironmentServices(KsperpDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<MasterEnvironment>> GetAllEnvironmentsAsync()
        {
            return await _context.MasterEnvironments.Where(x => x.IsActive == true).OrderByDescending(x => x.EnvironmentId).ToListAsync();
        }

        public async Task<MasterEnvironment?> GetEnvironmentByUuidAsync(Guid uuid)
        {
            return await _context.MasterEnvironments.FirstOrDefaultAsync(y => y.Uuid == uuid.ToString());
        }

        public async Task AddEnvironmentAsync(MasterEnvironment environment)
        {
            bool isDuplicate = _context.MasterEnvironments.Any(x => x.EnvironmentName == environment.EnvironmentName);
            if (isDuplicate)
            {
                throw new Exception("Data Already Exists!");
            }
            environment.IsActive = true;
            environment.Uuid = Guid.NewGuid().ToString();
            environment.IsAdddedOn = DateTime.Now;
            environment.IsAddedBy = "1";
            _context.MasterEnvironments.Add(environment);

            await _context.SaveChangesAsync();
        }
        public async Task UpdateEnvironmentAsync(MasterEnvironment model)
        {
            var environment = await _context.MasterEnvironments.FirstOrDefaultAsync(b => b.Uuid == model.Uuid.ToString());

            if (environment != null)
            {
                environment.EnvironmentName = model.EnvironmentName;
                environment.IsDisplay = model.IsDisplay;
                environment.IsUpdatedBy = "1";
                environment.IsUpdatedOn = DateTime.Now;
                _context.MasterEnvironments.Update(environment);
                await _context.SaveChangesAsync();

            }
        }
        public void DeleteEnvironment(Guid uuid)
        {
            var environment = _context.MasterEnvironments.FirstOrDefault(c => c.Uuid == uuid.ToString());
            if (environment != null)
            {
                environment.IsActive = false;
                environment.IsDeletedBy = "1";
                environment.IsDeletedOn = DateTime.Now;
                _context.MasterEnvironments.Update(environment);
                _context.SaveChanges();
            }
        }





    }
}
