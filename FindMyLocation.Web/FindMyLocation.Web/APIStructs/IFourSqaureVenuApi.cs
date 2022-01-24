using System.Collections.Generic;
using System.Threading.Tasks;
using FindMyLocation.Domain.Entities;
using Refit;

namespace FindMyLocation.Web.APIStructs
{
    public interface IFourSquareVenuApi
    {

        [Post("/FourSqaureVenuesApi/AddResult/")]
        Task<IEnumerable<ImageModel>> AddResult(ModelFour modelFour);
        [Get("/FourSqaureVenuesApi/GetAllResults/")]
        Task<IEnumerable<FourSqaureVenues>> GetAllResults();
       
    }
}