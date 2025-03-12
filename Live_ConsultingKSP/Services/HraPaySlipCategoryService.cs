using Live_ConsultingKSP.Models;
using Live_ConsultingKSP.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Live_ConsultingKSP.Services
{
    public class HraPaySlipCategoryService : IHraPaySlipCategoryService
    {
        private readonly KsperpDbContext _context;

        public HraPaySlipCategoryService(KsperpDbContext context)
        {
            _context = context;
        }

        public void AddPaySlipCategory(HraPaySlipCategory paySlipCategory)
        {
            if (_context.HraPaySlipCategories.Any(p => p.PayslipCategory == paySlipCategory.PayslipCategory))
                throw new Exception("PaySlip Category already exists!");

            paySlipCategory.IsActive = true;
            paySlipCategory.IsAddedOn = DateTime.Now;
            paySlipCategory.IsAddedBy = "1";

            _context.HraPaySlipCategories.Add(paySlipCategory);
            _context.SaveChanges();
        }

        public List<HraPaySlipCategory> GetAllPaySlipCategories()
        {
            return _context.HraPaySlipCategories
                .Where(p => p.IsActive == true)
                .OrderByDescending(p => p.PaySlipCategoryId)
                .ToList();
        }

        public IQueryable<HraPaySlipCategory> GetAllPaySlipCategoriesQueryable()
        {
            return _context.HraPaySlipCategories.Where(p => p.IsActive == true);
        }

        public HraPaySlipCategory GetPaySlipCategoryByUUID(string uuid)
        {
            return _context.HraPaySlipCategories.FirstOrDefault(p => p.Uuid == uuid);
        }

        public void UpdatePaySlipCategory(HraPaySlipCategory paySlipCategory)
        {
            var existingPaySlipCategory = _context.HraPaySlipCategories.FirstOrDefault(p => p.Uuid == paySlipCategory.Uuid);
            if (existingPaySlipCategory == null) throw new Exception("PaySlip Category not found!");

            if (_context.HraPaySlipCategories.Any(p => p.PayslipCategory == paySlipCategory.PayslipCategory && p.Uuid != paySlipCategory.Uuid))
                throw new Exception("PaySlip Category with the same name already exists!");

            existingPaySlipCategory.PayslipCategory = paySlipCategory.PayslipCategory;
            existingPaySlipCategory.IsDisplay = paySlipCategory.IsDisplay;
            existingPaySlipCategory.IsUpdatedOn = DateTime.Now;
            existingPaySlipCategory.IsUpdatedBy = "1";

            _context.SaveChanges();
        }

        public void DeletePaySlipCategory(string uuid)
        {
            var paySlipCategory = _context.HraPaySlipCategories.FirstOrDefault(p => p.Uuid == uuid);
            if (paySlipCategory == null) throw new Exception("PaySlip Category not found!");

            paySlipCategory.IsActive = false;
            paySlipCategory.IsDeletedOn = DateTime.Now;
            paySlipCategory.IsDeletedBy = "1";

            _context.SaveChanges();
        }

       
    }
}
