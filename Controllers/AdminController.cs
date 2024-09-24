using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fletes.Models;
using Fletes.Context;
using Fletes.Models.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace Fletes.Controllers
{
    [Authorize(Roles = "Admin")] // Only Admins can access this endpoint
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        [HttpGet("secure-data")]
        public IActionResult GetSecureData()
        {
            return Ok("This is secured data, only accessible by Admins.");
        }
    }
}
