using System.Collections.Generic;
using System.Threading.Tasks;
using FindMyLocation.Domain.Entities;
using Refit;

namespace FindMyLocation.Win.APIStructs
{
    public interface IFourSqaureVenuApi
    {

        [Post("/FourSqaureVenuesApi/AddResult/")]
        Task<IEnumerable<ImageModel>> AddResult(ModelFour modelFour);
        [Get("/FourSqaureVenuesApi/GetAllResults/")]
        Task<IEnumerable<FourSqaureVenues>> GetAllResults();
       
    }
}