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
        Task<IEnumerable<ModelFour>> GetAll(string locationName, decimal lat, decimal lon, int count);
        Task<IEnumerable<ModelFour>> GetGeo(decimal lat, decimal lon, int count = 5);
        Task<IEnumerable<ModelFour>> GetName(string locationName,int count = 5);
        Task<IEnumerable<ImageModel>> GetPictures(ModelFour modelFour);
        //void AddResult(FourSqaureVenues modelFour);
        Task DeleteLocation(int locationId);
    }
}
