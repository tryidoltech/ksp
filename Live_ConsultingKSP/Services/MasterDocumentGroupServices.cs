using Live_ConsultingKSP.Models;
using Live_ConsultingKSP.Service.IService;
using Microsoft.EntityFrameworkCore;

namespace Live_ConsultingKSP.Service
{
    public class MasterDocumentGroupServices : IMasterDocumentGroupServices
    {
        private readonly KsperpDbContext _context;

        public MasterDocumentGroupServices(KsperpDbContext context)
        {
            _context = context;

        }
        public async Task<IEnumerable<MasterDocumentGrop>> GetAllDocumentGropsAsync(string cmpuuid, string envuuid)
        {
            return await _context.MasterDocumentGrops.Where(x => x.IsActive == true && x.MasterCompanyUuid == cmpuuid && x.MasterEnvironmentUuid == envuuid)
                .OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<MasterDocumentGrop?> GetDocumentGropsByUuidAsync(Guid uuid)
        {
            return await _context.MasterDocumentGrops.FirstOrDefaultAsync(g => g.Uuid == uuid.ToString());
        }

        public async Task AddDocumentGropAsync(MasterDocumentGrop documenttype)
        {
            bool isDuplicate = _context.MasterDocumentGrops.Any(x => x.DocumentName == documenttype.DocumentName);
            if (isDuplicate)
            {
                throw new Exception("Data Already Exists!");
            }
            documenttype.IsActive = true;
            documenttype.Uuid = Guid.NewGuid().ToString();
            documenttype.IsAddedOn = DateTime.Now;
            documenttype.IsAddedBy = "1";
            _context.MasterDocumentGrops.Add(documenttype);

            await _context.SaveChangesAsync();
        }
        public async Task UpdateDocumentGropAsync(MasterDocumentGrop model)
        {
            var documenttype = await _context.MasterDocumentGrops.FirstOrDefaultAsync(g => g.Uuid == model.Uuid);
            if (documenttype != null)
            {
                documenttype.DocumentName = model.DocumentName;
                documenttype.IsDisplay = model.IsDisplay;
                documenttype.IsUpdateBy = "1";
                documenttype.IsUpdatedOn = DateTime.Now;
                _context.MasterDocumentGrops.Update(documenttype);
                await _context.SaveChangesAsync();
            }
        }
        public void Deletedocument(Guid uuid)
        {
            var documenttype = _context.MasterDocumentGrops.FirstOrDefault(g => g.Uuid == uuid.ToString());
            if (documenttype != null)
            {
                documenttype.IsActive = false;
                documenttype.IsDeletedBy = "1";
                documenttype.IsDeleteOn = DateTime.Now;
                _context.MasterDocumentGrops.Update(documenttype);
                _context.SaveChanges();
            }

        }
    }
}
