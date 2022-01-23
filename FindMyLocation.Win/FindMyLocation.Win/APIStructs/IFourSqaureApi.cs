using FindMyLocation.Domain.Entities;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindMyLocation.Win.APIStructs
{
    public interface IFourSqaureApi
    {
        [Get("/FourSqaureApi/GetByName/{locationName}")]
        Task<IEnumerable<ModelFour>> GetByName(string locationName, string count);
        [Get("/FourSqaureApi/GetByGeo/")]
        Task<IEnumerable<ModelFour>> GetByGeo(string lon, string lat, string count);
        [Get("/FourSqaureApi/GetByAll/{locationName}/{lat}/{lon}/{count}")]
        Task<IEnumerable<ModelFour>> GetByAll(string locationName, string lon, string lat,string count);
        [Get("/FourSqaureApi/GetImages/{fourModel}")]
        Task<IEnumerable<ImageModel>> GetImages(string fourModel);
    }
}
