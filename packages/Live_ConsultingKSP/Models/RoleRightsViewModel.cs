namespace KSPLogin.Models
{
    public class RoleRightsViewModel
    {
       
            public Guid RoleUuid { get; set; }
            public List<MenuRightDto> Rights { get; set; }
        public class MenuRightDto
        {
            public string Uuid { get; set; }
            public bool IsRead { get; set; }
            public bool IsWrite { get; set; }
            public bool IsEdit { get; set; }
            public bool IsDelete { get; set; }
        }

    }
}
