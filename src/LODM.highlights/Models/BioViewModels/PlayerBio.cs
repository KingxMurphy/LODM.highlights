using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LODM.highlights.Models.BioViewModels
{
    public class PlayerBio
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool MaleFemale { get; set; }
    }
}
