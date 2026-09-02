using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Farmer
{
    public class UpdateFarmerDto
    {
        public string FarmName { get; set; } = string.Empty;
        public string FarmDescription { get; set;} = string.Empty;
        public string FarmLocation {  get; set; } = string.Empty;
    }
}
