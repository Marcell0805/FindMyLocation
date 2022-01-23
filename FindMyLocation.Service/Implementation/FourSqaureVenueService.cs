using FindMyLocation.Domain.Entities;
using FindMyLocation.Persistence;
using FindMyLocation.Service.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindMyLocation.Service.Implementation
{
    public class FourSqaureVenueService : IFourSqaureVenues
    {

        private readonly IRepository<FourSqaureVenues> _repository;
        private readonly DbSet<FourSqaureVenues> _entities;
        public FourSqaureVenueService(IRepository<FourSqaureVenues> repository, ApplicationDbContext context)
        {
            _repository = repository;
            _entities = context.Set<FourSqaureVenues>();
        }
        public async void AddResult(FourSqaureVenues modelFour)
        {
            try
            {
                var exist = await GetAll(modelFour.latitude, modelFour.longitude);
                if (exist == null)
                {
                    _repository.Insert(modelFour);
                    _repository.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<IEnumerable<FourSqaureVenues>> GetAll(double lat, double lon)
        {
            var result = await _entities.FirstOrDefaultAsync(a => a.longitude == lon && a.latitude==lat);
            var  x=result;
            FourSqaureVenues fourSqaureVenues = x;
            List<FourSqaureVenues> l = new();
            l.Add(x);
            return l;

        }
    }
}
