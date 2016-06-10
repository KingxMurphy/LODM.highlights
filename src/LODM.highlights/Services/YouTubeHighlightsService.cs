using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using LODM.highlights.Models;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.WebEncoders;

namespace LODM.highlights.Services
{
    public class YouTubeHighlightsService : IHighlightService
    {
        public const string CacheKey = nameof(YouTubeHighlightsService);

        private readonly IHostingEnvironment _env;
        private readonly AppSettings _appSettings;
        private readonly IMemoryCache _cache;
        private readonly TelemetryClient _telemetry;
        public YouTubeHighlightsService(
            IHostingEnvironment env,
            IOptions<AppSettings> appSettings,
            IMemoryCache memoryCache,
            TelemetryClient telemetry)
        {
            _env = env;
            _appSettings = appSettings.Value;
            _cache = memoryCache;
            _telemetry = telemetry;
        }

        public async Task<HighlightList> GetOverwatchHighlightsAsync(string gamerTag, ClaimsPrincipal user, bool disableCache)
        {
            var gamerMember = _appSettings.Members.FirstOrDefault(x => x.GamerTag == gamerTag);

            if (string.IsNullOrEmpty(gamerMember?.YouTubeApiKey))
                return new HighlightList()
                {
                    GamerTag = gamerMember?.GamerTag ?? "NoneProvided",
                    Highlights = PlaceHolderData.Highlights,
                    MoreHighlightsUrl = "https://www.youtube.com"
                };

            if (user.Identity.IsAuthenticated && disableCache)
                return await getHighlightsList(gamerMember);

            var result = _cache.Get<HighlightList>(CacheKey);

            if (result != null)
                return result;

            result = await getHighlightsList(gamerMember);

            _cache.Set(CacheKey, result, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)
            });

            return result;
        }

        //public async Task<HighlightList> GetDestinyHighlightsAsync(ClaimsPrincipal user, bool disableCache)
        //{
        //    if (string.IsNullOrEmpty(_appSettings.YouTubeApiKey))
        //        return new HighlightList() { Highlights = PlaceHolderData.Highlights };

        //    if (user.Identity.IsAuthenticated && disableCache)
        //        return await getHighlightsList();

        //    var result = _cache.Get<HighlightList>(CacheKey);

        //    if (result != null)
        //        return result;

        //    result = await getHighlightsList();

        //    _cache.Set(CacheKey, result, new MemoryCacheEntryOptions
        //    {
        //        AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)
        //    });

        //    return result;
        //}

        private async Task<HighlightList> getHighlightsList(Member lodmMember)
        {
            using (var client = new YouTubeService(new BaseClientService.Initializer
            {
                ApplicationName = _appSettings.YouTubeApplicationName,
                ApiKey = lodmMember.YouTubeApiKey
            }))
            {
                var listRequest = client.PlaylistItems.List("snippet");
                listRequest.PlaylistId = lodmMember.YouTubePlaylistId;
                listRequest.MaxResults = 4;

                var requestStart = DateTimeOffset.UtcNow;
                var playlistItems = await listRequest.ExecuteAsync();
                _telemetry.TrackDependency("YouTube.PlayListItemsApi", "List", requestStart, DateTimeOffset.UtcNow - requestStart, true);

                var result = new HighlightList
                {
                    Highlights = playlistItems.Items.Select(item => new Highlight
                    {
                        Provider = "YouTube",
                        ProviderId = item.Snippet.ResourceId.VideoId,
                        Title = getUsefulBitsFromTitle(item.Snippet.Title),
                        Description = item.Snippet.Description,
                        HighlightDate =
                            DateTimeOffset.Parse(item.Snippet.PublishedAtRaw, null, DateTimeStyles.RoundtripKind),
                        ThumbnailUrl = item.Snippet.Thumbnails.High.Url,
                        Url =
                            getVideoUrl(item.Snippet.ResourceId.VideoId, item.Snippet.PlaylistId,
                                item.Snippet.Position ?? 0)
                    }).ToList()
                };


                if (!string.IsNullOrEmpty(playlistItems.NextPageToken))
                    result.MoreHighlightsUrl = getPlaylistUrl(lodmMember.YouTubePlaylistId);

                result.GamerTag = lodmMember.GamerTag;

                return result;
            }
        }

        private static string getUsefulBitsFromTitle(string title)
        {
            if (title.Count(c => c == '-') < 2)
                return string.Empty;

            var lastHyphen = title.LastIndexOf('-');

            if (lastHyphen < 0)
                return string.Empty;

            var result = title.Substring(lastHyphen + 1);

            return result;
        }

        private static string getVideoUrl(string id, string playlistId, long itemIndex)
        {
            var encodedId = UrlEncoder.Default.UrlEncode(id);
            var encodedPlaylistId = UrlEncoder.Default.UrlEncode(playlistId);
            var encodedItemIndex = UrlEncoder.Default.UrlEncode(itemIndex.ToString());

            return $"https://www.youtube.com/watch?v={encodedId}&list={encodedPlaylistId}&index={encodedItemIndex}";
        }

        private static string getPlaylistUrl(string playlistId)
        {
            var encodedPlaylistId = UrlEncoder.Default.UrlEncode(playlistId);

            return $"https://www.youtube.com/playlist?list={encodedPlaylistId}";
        }

        private static class PlaceHolderData
        {
            private static readonly TimeSpan CdtOffset = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time").BaseUtcOffset;

            public static readonly IList<Highlight> Highlights = new List<Highlight>
            {
                new Highlight
                {
                    HighlightDate = new DateTimeOffset(2016, 6, 6, 11, 30, 0, CdtOffset),
                    Title = "Overwatch Highlights - June 6th 2016",
                    Provider = "YouTube",
                    ProviderId = "LoYL3TYAN4E",
                    ThumbnailUrl = "http://img.youtube.com/vi/LoYL3TYAN4E/mqdefault.jpg",
                    Url = "https://www.youtube.com/watch?v=LoYL3TYAN4E"
                },
                new Highlight
                {
                    HighlightDate = new DateTimeOffset(2012, 8, 16, 15, 30, 0, CdtOffset),
                    Title = "Soul Calibur Highlight - Aug 16, 2012",
                    Provider = "YouTube",
                    ProviderId = "l9ztSKb4vT4",
                    ThumbnailUrl = "http://img.youtube.com/vi/l9ztSKb4vT4/mqdefault.jpg",
                    Url = "https://www.youtube.com/watch?v=l9ztSKb4vT4&list=PLA5977930832F4F80&index=1"
                },
                new Highlight
                {
                    HighlightDate = new DateTimeOffset(2012, 8, 20, 18, 30, 0, CdtOffset),
                    Title = "Soul Calibur Highlight - Aug 20, 2012",
                    Provider = "YouTube",
                    ProviderId = "Z6UfoQQqTcc",
                    ThumbnailUrl = "http://img.youtube.com/vi/Z6UfoQQqTcc/mqdefault.jpg",
                    Url = "https://www.youtube.com/watch?v=Z6UfoQQqTcc&index=7&list=PLA5977930832F4F80"
                },
            };
        }
    }
}
