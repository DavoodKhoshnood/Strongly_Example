using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Strongly_Myself.Models
{
    public class PersonViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        
        public string CarModel { get; set; }
        public string CarPlaque { get; set; }
    }
}