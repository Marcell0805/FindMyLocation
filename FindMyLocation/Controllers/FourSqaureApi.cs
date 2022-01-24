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
        // GET: api/<FindMyLocationController>
        /// <summary>
        /// Gets details from location name 
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetByName/{locationName}", Name = "getByName")]
        public async Task<IEnumerable<ModelFour>> GetByName(string locationName, int count = 5)
        {
            var result = _fourSqaureService.GetName(locationName, count).Result;
            return result;
        }
        // GET: api/<FindMyLocationController>
        /// <summary>
        /// Gets details from geo details
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetByGeo/", Name = "getByGeo")]
        public async Task<IEnumerable<ModelFour>> GetByGeo(decimal lat, decimal lon, int count = 5)
        {
            var result = _fourSqaureService.GetGeo(lat,lon, count).Result;
            return result;
        }
        // GET: api/<FindMyLocationController>
        /// <summary>
        /// Gets details from all details
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetByAll/{locationName}/{lat}/{lon}/{count}", Name = "getByAll")]
        [ProducesResponseType(200)]
        public async Task<IEnumerable<ModelFour>> GetByAll(string locationName, string lat, string lon, string count = "5")
        {
            var result = _fourSqaureService.GetAll(locationName, lat, lon, count).Result;
            return result;
        }
        // GET: api/<FindMyLocationController>
        /// <summary>
        /// Gets picture details from result fetched
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetImages/{modelFour}", Name = "getImages")]
        public async Task<IEnumerable<ImageModel>> GetImages(string modelFour)
        {
            var result = _fourSqaureService.GetPictures(modelFour).Result;
            List<ImageModel> res = new();
            ImageModel m = new ImageModel();
            if(result!=null)
            {
                return result;
            }
            else
            {
                res.Add(m);
                result = res;
            }

            return result;
        }
       
    }

}
    
