using FindMyLocation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindMyLocation.Service.Contract
{
    public interface IFourSqaureService
    {
        Task<IEnumerable<ModelFour>> GetAll(string locationName, string lat, string lon, string count);
        Task<IEnumerable<ModelFour>> GetGeo(decimal lat, decimal lon, int count = 5);
        Task<IEnumerable<ModelFour>> GetName(string locationName,int count = 5);
        Task<IEnumerable<ImageModel>> GetPictures(string modelFour);
        //void AddResult(FourSqaureVenues modelFour);
        Task DeleteLocation(int locationId);
    }
}
