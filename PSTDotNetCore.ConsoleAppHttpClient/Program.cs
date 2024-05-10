using Newtonsoft.Json;

string jsonStr = await File.ReadAllTextAsync("data.json");
var model = JsonConvert.DeserializeObject<MainDto>(jsonStr);

foreach(var baganinfo in model.Tbl_BaganMapInfoData)
{
    Console.WriteLine(baganinfo.Id);
}

Console.ReadLine();


public class MainDto
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
