using System.ComponentModel.DataAnnotations;

namespace PMS.Backend.Core.Entities
{
    public class Agencies
    {
        public int Id { get; set; }

        [MaxLength(75, ErrorMessage = "{0} can have a max of {1} characters.")]
        public string Name { get; set; } = null!;

        [MaxLength(75, ErrorMessage = "{0} can have a max of {1} characters.")]
        public string SaveDir { get; set; } = null!;
        
        public int ComRate { get; set; }
        
        public int ComExtras { get; set; }
        
        public bool PayCom { get; set; }
        
        public string EmergencyPhone { get; set; }
        
        public Contact Contact { get; set; }
    }
}
