using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;

namespace PSTDotNetCore.RestApiWithNLayer.Features.BaganMap
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaganMapController : ControllerBase
    {
        private async Task<BaganMap> GetDataAsync()
        {
            string jsonStr = await System.IO.File.ReadAllTextAsync("data.json");
            var model = JsonConvert.DeserializeObject<BaganMap>(jsonStr);
            return model;
        }

        [HttpGet]
        public async Task<IActionResult> TravelRoutes()
        {
            var model = await GetDataAsync();
            return Ok(model.Tbl_TravelRouteListData
                .Select(x => new 
                {
                    x.TravelRouteName,
                    x.TravelRouteDescription
                }));
        }

        [HttpGet("travelroutes/{travelroutename}")]
        public async Task<IActionResult> TravelRoutes(string travelroutename)
        {
            var model = await GetDataAsync();
            var item = from route in model.Tbl_TravelRouteListData
                         from pagodaId in route.PagodaList
                         join pagoda in model.Tbl_BaganMapInfoData
                         on pagodaId equals pagoda.Id
                         where route.TravelRouteName == travelroutename
                         select new
                         {
                             //route.TravelRouteName,
                             pagoda.Id,
                             pagoda.PagodaMmName,
                             pagoda.PagodaEngName,
                             pagoda.Latitude,
                             pagoda.Longitude
                         };

            return Ok(item);
        }

        [HttpGet("baganinfo/{pagodaid}")]
        public async Task<IActionResult> BaganmapInfoDetails(string pagodaid)
        {
            var model = await GetDataAsync();
            var item = from info in model.Tbl_BaganMapInfoData
                       join details in model.Tbl_BaganMapInfoDetailData
                       on info.Id equals details.Id
                       where info.Id == pagodaid
                       select new
                       {
                           PagodaInfo = info.PagodaMmName + " - " + pagodaid,
                           details.Description
                       };

            return Ok(item);
        }

    }

    public class BaganMap
    {
        public Tbl_Baganmapinfodata[] Tbl_BaganMapInfoData { get; set; }
        public Tbl_Baganmapinfodetaildata[] Tbl_BaganMapInfoDetailData { get; set; }
        public Tbl_Travelroutelistdata[] Tbl_TravelRouteListData { get; set; }
    }

    public class Tbl_Baganmapinfodata
    {
        public string Id { get; set; }
        public string PagodaMmName { get; set; }
        public string PagodaEngName { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
    }

    public class Tbl_Baganmapinfodetaildata
    {
        public string Id { get; set; }
        public string Description { get; set; }
    }

    public class Tbl_Travelroutelistdata
    {
        public string TravelRouteId { get; set; }
        public string TravelRouteName { get; set; }
        public string TravelRouteDescription { get; set; }
        public string[] PagodaList { get; set; }
    }
}
