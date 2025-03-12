using System.ComponentModel.DataAnnotations;

namespace Live_ConsultingKSP.Models
{
    public class Login
    {
        
        public string UserName { get; set; }

        public string Password { get; set; }
        public bool IsLoginActive { get; set; }
       

    }
}
