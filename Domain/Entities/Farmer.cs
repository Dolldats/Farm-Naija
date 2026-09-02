using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Farmer
    {
        public int Id { get; set; }
        public int UserId {  get; set; }
        public User? User { get; set; }
        public string FarmName { get; set; } = string.Empty;
        public string FarmDescription { get; set; } = string.Empty;
        public bool IsVerified  { get; set; } = false;  
        public string FarmLocation { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set;}
    }
}
