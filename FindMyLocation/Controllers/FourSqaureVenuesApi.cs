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
    public class FourSqaureVenuesApi : ControllerBase
    {
        private readonly IFourSqaureVenues _fourSqaureService;
        public FourSqaureVenuesApi(IFourSqaureVenues fourSqaureService)
        {
            this._fourSqaureService = fourSqaureService;
        }
        [HttpPost("AddResult/", Name = "addResult")]
        public async Task<ActionResult<IEnumerable<FourSqaureVenues>>> AddResult(FourSqaureVenues modelFourResult)
        {
            try
            {
                _fourSqaureService.AddResult(modelFourResult);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        [HttpGet("GetAllResults/", Name = "getAllResults")]
        public async Task<IEnumerable<FourSqaureVenues>> GetAllResults()
        {
            try
            {
                var t=_fourSqaureService.GetAll().Result;
                return t;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
