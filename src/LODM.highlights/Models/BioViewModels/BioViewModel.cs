using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LODM.highlights.Models.BioViewModels
{
    public class BioViewModel
    {
        public Member PlayerBio { get; set; }
        public List<GameInformation> GameInformationList { get; set; }
    }
}
