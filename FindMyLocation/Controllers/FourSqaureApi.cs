using FindMyLocation.Domain.Entities;
using FindMyLocation.Service.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindMyLocation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FourSqaureApi : ControllerBase
    {
        private readonly IFourSqaureService _fourSqaureService;
        public FourSqaureApi(IFourSqaureService fourSqaureService)
        {
            this._fourSqaureService = fourSqaureService;
        }
        [HttpGet("GetByName/{locationName}", Name = "getByName")]
        public async Task<IEnumerable<ModelFour>> GetByName(string locationName, int count = 5)
        {
            var result = _fourSqaureService.GetName(locationName, count).Result;
            return result;
        }
        [HttpGet("GetByGeo/", Name = "getByGeo")]
        public async Task<IEnumerable<ModelFour>> GetByGeo(decimal lat, decimal lon, int count = 5)
        {
            var result = _fourSqaureService.GetGeo(lat,lon, count).Result;
            return result;
        }
        // GET: api/<UserAccessController>
        /// <summary>
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IEnumerable<ModelFour>> GetByAll(string locationName, decimal lat, decimal lon, int count = 5)
        {
            var result = _fourSqaureService.GetAll(locationName, lat, lon, count).Result;
            return result;
        }
        //[HttpGet]
        //public async List<string> GetImages(string fsq_id)
        //{
        //    return null;
        //}
    }
    
}
