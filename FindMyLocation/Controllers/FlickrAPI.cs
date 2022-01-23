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
    public class FlickrAPI : ControllerBase
    {
        [HttpGet]
        public async void GetPic()
        {

        }
    }
}
