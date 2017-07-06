using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LODM.highlights.Services.Interfaces
{
    public interface IMember
    {
        IEnumerable<Member> GetAll();
    }
}
