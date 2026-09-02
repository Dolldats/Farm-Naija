using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Farmer
{
    public class FarmerResponseDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string FarmName { get; set; } = string.Empty;
        public string FarmDescription { get; set; } = string.Empty;
        public bool IsVerified { get; set; }
        public string FarmLocation { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
