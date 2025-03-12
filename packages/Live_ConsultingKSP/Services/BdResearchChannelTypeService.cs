using Live_ConsultingKSP.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Live_ConsultingKSP.Services
{
    public class BdResearchChannelTypeService : IBdResearchChannelTypeService
    {
        private readonly KsperpDbContext _context;

        public BdResearchChannelTypeService(KsperpDbContext context)
        {
            _context = context;
        }

        public void AddChannelType(BdResearchChannelType channelType)
        {
            if (_context.BdResearchChannelTypes.Any(c => c.ResearchChannelName == channelType.ResearchChannelName))
                throw new Exception("Channel Type already exists!");

            channelType.Uuid = Guid.NewGuid().ToString();
            channelType.IsActive = true;
            channelType.IsAddedOn = DateTime.Now;
            channelType.IsAddedBy = "1";

            _context.BdResearchChannelTypes.Add(channelType);
            _context.SaveChanges();
        }

        public List<BdResearchChannelType> GetAllChannelTypes(string cmpuuid, string envuuid)
        {
            return _context.BdResearchChannelTypes
                .Where(c => (bool)c.IsActive && c.MasterCompanyUuid == cmpuuid && c.MasterEnvironmentUuid == envuuid)
                .OrderByDescending(c => c.ResearchChannelId)
                .ToList();
        }

        public BdResearchChannelType GetChannelTypeByUUID(string uuid)
        {
            return _context.BdResearchChannelTypes.FirstOrDefault(c => c.Uuid == uuid);
        }

        public void UpdateChannelType(BdResearchChannelType channelType)
        {
            var existingChannelType = _context.BdResearchChannelTypes.FirstOrDefault(c => c.Uuid == channelType.Uuid);
            if (existingChannelType == null) throw new Exception("Channel Type not found!");

            if (_context.BdResearchChannelTypes.Any(c => c.ResearchChannelName == channelType.ResearchChannelName && c.Uuid != channelType.Uuid))
                throw new Exception("Channel Type name already exists!");

            existingChannelType.ResearchChannelName = channelType.ResearchChannelName;
            existingChannelType.IsDisplay = channelType.IsDisplay;
            existingChannelType.IsUpdatedOn = DateTime.Now;
            existingChannelType.IsUpdatedBy = "1";

            _context.SaveChanges();
        }


        public void DeleteChannelType(string uuid)
        {
            var channelType = _context.BdResearchChannelTypes.FirstOrDefault(c => c.Uuid == uuid);
            if (channelType == null) throw new Exception("Channel Type not found!");

            channelType.IsActive = false;
            channelType.IsDeletedOn = DateTime.Now;
            channelType.IsDeletedBy = "1";

            _context.SaveChanges();
        }
    }
}