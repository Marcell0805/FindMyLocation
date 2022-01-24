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
            if (_entities != null)
            {
                _repository.Insert(modelFour);
                _repository.SaveChanges();
                //var result = await _entities.FirstOrDefaultAsync(a =>
                //    a.longitude == modelFour.longitude && a.latitude == modelFour.latitude);
                //var result =  _entities.Where(a =>
                //    a.id==modelFour.id).ToListAsync().Result;
                //if (result.Count == 0)
                //{
                //    //_repository.Insert(modelFour);
                //    //await _repository.SaveChangesAsync();
                //    await _entities.AddAsync(modelFour);
                //    _entities.. = EntityState.Added;
                //}
            }

            //try
            //{
            //    bool hasData = true;

            //catch (Exception e)
            //{
            //    Console.WriteLine(e);
            //    throw;
            //}
        }

        public Task<IEnumerable<FourSqaureVenues>> GetAll()
        {
            var x=_repository.GetAll();
            return x;
        }

        public async Task<IEnumerable<FourSqaureVenues>> GetAllExisting(double lat, double lon)
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
