using FindMyLocation.Domain.Entities;
using FindMyLocation.Persistence;
using FindMyLocation.Service.Contract;
using Microsoft.EntityFrameworkCore;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FindMyLocation.Service.Implementation
{
    
    public class FourSquareService:IFourSqaureService
    {
        private readonly IRepository<ModelFour> _repository;
        private readonly DbSet<ModelFour> _entities;
        public FourSquareService(IRepository<ModelFour> repository, ApplicationDbContext context)
        {
            _repository = repository;
            _entities = context.Set<ModelFour>();
        }

        

        public async Task DeleteLocation(int locationId)
        {
            try
            {
                var result = _entities.FirstOrDefault(a => a.locationId == locationId);
                _entities.Remove(result);
                _repository.SaveChanges();
            }
            catch (DbException e)
            {
                Console.WriteLine(e.ErrorCode);
                throw;
            }
        }

        public async Task<IEnumerable<ModelFour>> GetAll(string locationName, decimal lat, decimal lon, int count)
        {
            string api1 = $"https://api.foursquare.com/v3/places/search?near={locationName}&limit={count}";
            RestClient clientS;
            RestRequest requestS;
            Requestbuilder(api1, out clientS, out requestS);
            RestResponse responseS = await clientS.ExecuteGetAsync(requestS);
            var resultMapped = JsonSerializer.Deserialize<ModelFour>(responseS.Content);
            List<ModelFour> modelFour = buildList(resultMapped);
            return modelFour;
        }

        private static List<ModelFour> buildList(ModelFour resultMapped)
        {
            List<ModelFour> modelFour = new List<ModelFour>();
            ModelFour modelList = new ModelFour();
            var results = resultMapped.results.ToArray();
            foreach (var item in results)
            {
                modelList.results = resultMapped.results;
                modelFour.Add(modelList);
            }

            return modelFour;
        }

        private static void Requestbuilder(string api1, out RestClient clientS, out RestRequest requestS)
        {
            clientS = new RestClient(api1);
            requestS = new RestRequest();
            requestS.AddHeader("Accept", "application/json");
            requestS.AddHeader("Authorization", "fsq3ScPvf/YHtlzvGBR0t8tH6CRZevePMnaiAprxVIvLFGs=");
        }

        public async Task<IEnumerable<ModelFour>> GetGeo(decimal lat, decimal lon, int count = 5)
        {
            string api1 = $"https://api.foursquare.com/v3/places/nearby?ll={lon},{lat}&limit={count}";
            RestClient clientS;
            RestRequest requestS;
            Requestbuilder(api1, out clientS, out requestS);
            RestResponse responseS = await clientS.ExecuteGetAsync(requestS);
            var resultMapped = JsonSerializer.Deserialize<ModelFour>(responseS.Content);
            List<ModelFour> modelFour = buildList(resultMapped);
            return modelFour;
        }

        public async Task<IEnumerable<ModelFour>> GetName(string locationName, int count = 5)
        {
            string api1 = $"https://api.foursquare.com/v3/places/search?near={locationName}&limit={count}";
            RestClient clientS;
            RestRequest requestS;
            Requestbuilder(api1, out clientS, out requestS);
            RestResponse responseS = await clientS.ExecuteGetAsync(requestS);
            var resultMapped = JsonSerializer.Deserialize<ModelFour>(responseS.Content);
            List<ModelFour> modelFour = buildList(resultMapped);
            return modelFour;
        }

        public async Task<IEnumerable<ImageModel>> GetPictures(ModelFour modelFour)
        {
            int index = 0;
            string api1 = $"https://api.foursquare.com/v3/places/{modelFour.results[index].fsq_id}/photos?limit={1}";
            RestClient clientS;
            RestRequest requestS;
            Requestbuilder(api1, out clientS, out requestS);
            List<string> pictureUrls = new List<string>();
            RestResponse responseP = await clientS.ExecuteGetAsync(requestS);
            List<ImageModel> imageModels = JsonSerializer.Deserialize<List<ImageModel>>(responseP.Content);
            return imageModels;
        }
    }
    
}
