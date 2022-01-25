using FindMyLocation.Domain.Entities;
using FindMyLocation.Win.APIStructs;
using Refit;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace FindMyLocation.Win.Code
{
    //This class handles all the calls to the rest service
    //In production apps I usually use the app config files to set the paths
    public class MainScreenCode
    {
        //https://localhost:44356/api/FourSqaureApi/GetByAll?locationName=Germany&lat=52.3664&lon=9.7369&count=5
        private IFourSqaureApi FourSqaureApi { get; set; }
        private IFourSqaureVenuApi FourSquareVenueApi { get; set; }
        public MainScreenCode()
        {

        }
        public async Task<IEnumerable<ModelFour>> GetResults(string searchTxt,string lon,string lat,string count)
        {
            FourSqaureApi = RestService.For<IFourSqaureApi>("http://localhost:53623/api/");
            var resultApi = await FourSqaureApi.GetByAll(searchTxt,lon,lat, count);
            return resultApi;
        }
        public async Task<IEnumerable<ModelFour>> GetByGeo(string lon, string lat, string count)
        {
            FourSqaureApi = RestService.For<IFourSqaureApi>("http://localhost:53623/api/");
            var resultApi = await FourSqaureApi.GetByGeo(lon,lat, count);
            return resultApi;
        }
        public async Task<IEnumerable<ModelFour>> GetByName(string searchTxt, string count)
        {
            FourSqaureApi = RestService.For<IFourSqaureApi>("http://localhost:53623/api/");
            var resultApi = await FourSqaureApi.GetByName(searchTxt, count);
            return resultApi;
        }
        public async Task<IEnumerable<ImageModel>> GetImages(string fsq)
        {
            FourSqaureApi = RestService.For<IFourSqaureApi>("http://localhost:53623/api/");
            var resultApi = await FourSqaureApi.GetImages(fsq);
            if (resultApi != null)
            {
                return resultApi;
            }
            return null;
        }
        public async Task SaveResults(List<ModelFour> modelFours)
        {
            int index = 0;
            FourSquareVenueApi = RestService.For<IFourSqaureVenuApi>("http://localhost:53623/api/");
            var results = await FourSquareVenueApi.GetAllResults();
            foreach (var item in modelFours)
            {
                var t = results.ToList();
                var b = results.Where(a => a.fsq_id == item.results[index].fsq_id);
                if (!b.Any())
                {
                    FourSqaureVenues itemToAdd = new FourSqaureVenues
                    {
                        fsq_id = item.results[index].fsq_id,
                        country = item.results[index].location.country,
                        address = item.results[index].location.address,
                        cross_street = item.results[index].location.cross_street,
                        postcode = item.results[index].location.postcode,
                        latitude = item.results[index].geocodes.main.latitude,
                        longitude = item.results[index].geocodes.main.longitude,
                        name = item.results[index].name,
                        region = item.results[index].location.region,
                        suffix = item.results[index].categories[0].icon.suffix,
                        prefix = item.results[index].categories[0].icon.prefix,
                        timezone = item.results[index].timezone
                    };

                    await FourSquareVenueApi.AddResult(itemToAdd);
                }
                index++;
            }

        }
    }
}
