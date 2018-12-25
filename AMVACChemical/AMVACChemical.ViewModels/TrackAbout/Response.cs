using System.Collections.Generic;
using AMVACChemical.ViewModels.TrackAbout.Assets;

namespace AMVACChemical.Controllers.Api
{
    public class Response<T>
    {
        public string TotalRows { get; set; }
        public List<AssetsVM> Rows { get; set; }
    }
}