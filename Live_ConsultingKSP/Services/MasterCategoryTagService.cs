using Live_ConsultingKSP.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Live_ConsultingKSP.Services
{
    public class MasterCategoryTagService : IMasterCategoryTagService
    {
        private readonly KsperpDbContext _context;

        public MasterCategoryTagService(KsperpDbContext context)
        {
            _context = context;
        }

        public List<MasterCategorTag> GetAllCategoryTags(string cmpuuid, string envuuid)
        {
            return _context.MasterCategorTags
                .Where(c => (bool)c.IsActive && c.MasterCompanyUuid == cmpuuid && c.MasterEnvironmentUuid == envuuid) // Only active tags
                .OrderByDescending(c => c.CategoryTagId)
                .ToList();
        }

        public MasterCategorTag GetCategoryTagByUUID(Guid uuid)
        {
            return _context.MasterCategorTags
                .FirstOrDefault(c => c.Uuid == uuid.ToString());
        }

        public void AddCategoryTag(MasterCategorTag categoryTag)
        {
            if (_context.MasterCategorTags.Any(c => c.CategoryTagName == categoryTag.CategoryTagName))
                throw new Exception("Category Tag already exists!");

            categoryTag.Uuid = Guid.NewGuid().ToString();
            categoryTag.IsActive = true;
            categoryTag.IsAddedOn = DateTime.Now;
            categoryTag.IsAddedBy = "1";

            _context.MasterCategorTags.Add(categoryTag);
            _context.SaveChanges();
        }

        public void UpdateCategoryTag(MasterCategorTag categoryTag)
        {
            var existingCategoryTag = _context.MasterCategorTags
                .FirstOrDefault(c => c.Uuid == categoryTag.Uuid);

            if (existingCategoryTag == null)
                throw new Exception("Category Tag not found!");

            // Check for duplicate category tag names
            if (_context.MasterCategorTags.Any(c => c.CategoryTagName == categoryTag.CategoryTagName && c.Uuid != categoryTag.Uuid))
                throw new Exception("Category Tag name already exists!");

            // Update existing record
            existingCategoryTag.CategoryTagName = categoryTag.CategoryTagName;
            existingCategoryTag.IsDisplay = categoryTag.IsDisplay;
            existingCategoryTag.IsUpdatedOn = categoryTag.IsUpdatedOn; // Timestamp
            existingCategoryTag.IsUpdatedBy = categoryTag.IsUpdatedBy; // User ID

            _context.SaveChanges(); // Commit changes to the database
        }


        public void DeleteCategoryTag(Guid uuid)
        {
            var categoryTag = _context.MasterCategorTags
                .FirstOrDefault(c => c.Uuid == uuid.ToString());
            if (categoryTag == null) throw new Exception("Category Tag not found!");

            categoryTag.IsActive = false; // Soft delete by marking inactive
            categoryTag.IsDeletedOn = DateTime.Now;
            categoryTag.IsDeletedBy = "1";

            _context.SaveChanges();
        }
    }
}
