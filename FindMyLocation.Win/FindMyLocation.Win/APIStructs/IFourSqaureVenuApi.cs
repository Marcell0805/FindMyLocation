using System.Collections.Generic;
using System.Threading.Tasks;
using FindMyLocation.Domain.Entities;
using Refit;

namespace FindMyLocation.Win.APIStructs
{
    public interface IFourSqaureVenuApi
    {

        [Post("/FourSqaureVenuesApi")]
        Task<IEnumerable<FourSqaureVenues>> AddResult(FourSqaureVenues modelFour);
        [Get("/FourSqaureVenuesApi/GetAllResults/")]
        Task<IEnumerable<FourSqaureVenues>> GetAllResults();
       
    }
}