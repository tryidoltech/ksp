using Live_ConsultingKSP.Services;
using Live_ConsultingKSP.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Live_ConsultingKSP.Services
{
    public class HraWeekDayService : IHraWeekDayService
    {
        private readonly KsperpDbContext _context;

        public HraWeekDayService(KsperpDbContext context)
        {
            _context = context;
        }

        public void AddWeekDay(HraWeekDay weekDay)
        {
            if (_context.HraWeekDays.Any(w => w.WeekDayName == weekDay.WeekDayName))
                throw new Exception("Weekday already exists!");

            weekDay.IsActive = true;
            weekDay.IsAddedOn = DateTime.Now;
            weekDay.IsAddedBy = "1";

            _context.HraWeekDays.Add(weekDay);
            _context.SaveChanges();
        }

        public List<HraWeekDay> GetAllWeekDays()
        {
            return _context.HraWeekDays
                .Where(w => w.IsActive == true)
                .OrderByDescending(w => w.WeekDaysId)
                .ToList();
        }

        public IQueryable<HraWeekDay> GetAllWeekDaysQueryable()
        {
            return _context.HraWeekDays.Where(w => w.IsActive == true);
        }

        public HraWeekDay GetWeekDayByUUID(string uuid)
        {
            return _context.HraWeekDays.FirstOrDefault(w => w.Uuid == uuid);
        }

        public void UpdateWeekDay(HraWeekDay weekDay)
        {
            var existingWeekDay = _context.HraWeekDays.FirstOrDefault(w => w.Uuid == weekDay.Uuid);
            if (existingWeekDay == null) throw new Exception("Weekday not found!");

            if (_context.HraWeekDays.Any(w => w.WeekDayName == weekDay.WeekDayName && w.Uuid != weekDay.Uuid))
                throw new Exception("Weekday with the same name already exists!");

            existingWeekDay.WeekDayName = weekDay.WeekDayName;
            existingWeekDay.IsDisplay = weekDay.IsDisplay;
            existingWeekDay.IsUpdatedOn = DateTime.Now;
            existingWeekDay.IsUpdatedBy = "1";

            _context.SaveChanges();
        }

        public void DeleteWeekDay(string uuid)
        {
            var weekDay = _context.HraWeekDays.FirstOrDefault(w => w.Uuid == uuid);
            if (weekDay == null) throw new Exception("Weekday not found!");

            weekDay.IsActive = false;
            weekDay.IsDeletedOn = DateTime.Now;
            weekDay.IsDeletedBy = "1";

            _context.SaveChanges();
        }
    }
}
