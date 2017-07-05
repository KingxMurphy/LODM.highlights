using System.Security.Claims;
using System.Threading.Tasks;

namespace LODM.highlights.Services.Interfaces
{
    public interface IHighlightService
    {
        Task<HighlightList> GetOverwatchHighlightsAsync(string gamerTag, ClaimsPrincipal user, bool disableCache);

        //Task<HighlightList> GetDestinyHighlightsAsync(ClaimsPrincipal user, bool disableCache);
    }
}
