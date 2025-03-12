using Live_ConsultingKSP.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Live_ConsultingKSP.Services
{
    public class MasterDocumentTagService : IMasterDocumentTagService
    {
        private readonly KsperpDbContext _context;

        public MasterDocumentTagService(KsperpDbContext context)
        {
            _context = context;
        }

        public void AddDocumentCategoryTag(MasterDocumentCategoryTag tag)
        {
            if (_context.MasterDocumentCategoryTags.Any(t => t.DocumentCategoryTag == tag.DocumentCategoryTag))
                throw new Exception("Document category tag already exists!");

            tag.Uuid = Guid.NewGuid().ToString();
            tag.IsActive = true;
            tag.IsAddedOn = DateTime.Now;
            tag.IsAddedBy = "1"; // Example: Replace with actual user ID

            _context.MasterDocumentCategoryTags.Add(tag);
            _context.SaveChanges();
        }

        public List<MasterDocumentCategoryTag> GetAllDocumentCategoryTags(string cmpuuid, string envuuid)
        {
            return _context.MasterDocumentCategoryTags
                .Where(t => (bool)t.IsActive && t.MasterCompanyUuid == cmpuuid && t.MasterEnvironmentUuid == envuuid) // Only active tags
                .OrderByDescending(t => t.DocumentCategoryTag)
                .ToList();
        }


        public MasterDocumentCategoryTag GetDocumentCategoryTagByUUID(Guid uuid)
        {
            return _context.MasterDocumentCategoryTags.FirstOrDefault(t => t.Uuid == uuid.ToString());
        }

        public void UpdateDocumentCategoryTag(MasterDocumentCategoryTag tag)
        {
            var existingTag = _context.MasterDocumentCategoryTags.FirstOrDefault(t => t.Uuid == tag.Uuid);
            if (existingTag == null)
                throw new Exception("Document category tag not found!");

            if (_context.MasterDocumentCategoryTags.Any(t => t.DocumentCategoryTag == tag.DocumentCategoryTag && t.Uuid != tag.Uuid))
                throw new Exception("Document category tag already exists!");

            existingTag.DocumentCategoryTag = tag.DocumentCategoryTag;
            existingTag.IsDisplay = tag.IsDisplay;
            existingTag.IsUpdatedOn = DateTime.Now;
            existingTag.IsUpdatedBy = "1"; // Example: Replace with actual user ID

            _context.SaveChanges();
        }

        public void DeleteDocumentCategoryTag(Guid uuid)
        {
            var tag = _context.MasterDocumentCategoryTags.FirstOrDefault(t => t.Uuid == uuid.ToString());
            if (tag == null) throw new Exception("Document category tag not found!");

            tag.IsActive = false;
            tag.IsDeletedOn = DateTime.Now;
            tag.IsDeletedBy = "1"; // Example: Replace with actual user ID

            _context.SaveChanges();
        }
    }
}
