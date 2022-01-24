using FindMyLocation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindMyLocation.Service.Contract
{
    public interface IFourSqaureVenues
    {
        void AddResult(FourSqaureVenues modelFour);
        Task<IEnumerable<FourSqaureVenues>> GetAllExisting(double lat, double lon);
        Task<IEnumerable<FourSqaureVenues>> GetAll();
    }
}
