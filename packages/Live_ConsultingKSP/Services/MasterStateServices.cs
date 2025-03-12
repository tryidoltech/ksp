using Live_ConsultingKSP.Models;

namespace Live_ConsultingKSP.Services
{
    public class MasterStateServices : IMasterStateServices
    {
        private readonly KsperpDbContext _context;
        public MasterStateServices(KsperpDbContext context)
        {
            _context = context;
        }
        public IEnumerable<MasterState> GetAllStates(string cmpuuid, string envuuid)
        {
            return _context.MasterStates.Where(c => (bool)c.IsActive && c.MasterCompanyUuid == cmpuuid
            && c.MasterEnvironmentUuid == envuuid).ToList();
        }
        public void AddState(MasterState state)
        {

            bool isDuplicate = _context.MasterStates
                        .Any(c => c.StateName == state.StateName);

            if (isDuplicate)
            {
                throw new Exception("Data Already Exists!");
            }
            state.Uuid ??= Guid.NewGuid().ToString();
            state.IsActive = true;
            state.IsAddedOn = DateTime.Now;
            state.IsAddedBy = "1";
            _context.MasterStates.Add(state);
            _context.SaveChanges();
        }

        /*public List<MasterState> GetAllStates()
        {
            return _context.MasterStates.Where(c => c.IsActive).OrderByDescending(c => c.Id).ToList();
        }*/
        public IEnumerable<MasterState> GetStatesByCountry(string countryUuid)
        {
            return _context.MasterStates
                .Where(s => s.CountryUuid == countryUuid && s.IsActive == true)
                .ToList();
        }

        public MasterState GetStatesByUUID(Guid uuid)
        {
            return _context.MasterStates.FirstOrDefault(c => c.Uuid == uuid.ToString());
        }
        
        public void UpdateState(MasterState state)
        {
            bool isDuplicate = _context.MasterStates
        .Any(c => (c.StateName == state.StateName)
                  && c.Uuid != state.Uuid);

            if (isDuplicate)
            {
                throw new Exception("Data Already Exists!");
            }
            var existingState = _context.MasterStates.FirstOrDefault(c => c.Uuid == state.Uuid);
            if (existingState != null)
            {
                existingState.StateName = state.StateName;
                existingState.IsDisplay = state.IsDisplay;
                existingState.IsUpdatedOn = DateTime.Now;
                existingState.IsUpdateBy = "1";

                _context.SaveChanges();
            }
        }

        public void DeleteState(Guid uuid)
        {
            var state = _context.MasterStates.FirstOrDefault(c => c.Uuid == uuid.ToString());
            if (state != null)
            {
                state.IsDeleteOn = DateTime.Now;
                state.IsDeletedBy = "1";
                state.IsActive = false;
                _context.MasterStates.Update(state);
                _context.SaveChanges();
            }
        }
    }
}
