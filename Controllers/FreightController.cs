using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Fletes.Models;
using Fletes.Context;
using Fletes.Models.DTOs;
using AutoMapper;
using Fletes.Services;
using Microsoft.AspNetCore.Authorization;

namespace Fletes.Controllers
{
    [Authorize(Roles = "Admin")] // Only Admins can access this endpoint
    [ApiController]
    [Route("api/[controller]")]
    public class FreightController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly FreightServices _freightServices;


        public FreightController(AppDbContext context, FreightServices freightServices)
        {
            _context = context;
            _freightServices = freightServices;
        }

        // GET: api/Freight
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FreightDTO>>> GetFreights()
        {
            var freights = await _context.Freights
                .Include(f => f.Customer)
                .Include(f => f.Carrier)
                .Include(f => f.Vehicle)
                .Include(f => f.Route)
                .Select(f => new FreightDTO
                {
                    FreightId = f.FreightId,
                    Customer = new CustomerDTO
                    {
                        CustomerId = f.Customer.CustomerId,
                        Name = f.Customer.Name,
                        Address = f.Customer.Address,
                        Phone = f.Customer.Phone,
                        Email = f.Customer.Email,
                    },
                    Carrier = new CarrierDTO
                    {
                        CarrierId = f.Carrier.CarrierId,
                        Name = f.Carrier.Name,
                        Phone = f.Carrier.Phone,
                        Email = f.Carrier.Email,
                    },
                    Vehicle = new VehicleDTO
                    {
                        VehicleId = f.Vehicle.VehicleId,
                        Brand = f.Vehicle.Brand,
                        Model = f.Vehicle.Model,
                        Plate = f.Vehicle.Plate,
                        Status = f.Vehicle.IsAvailable

    },
                    Route = new RouteDTO
                    {
                        RouteId = f.Route.RouteId,
                        Origin = f.Route.Origin,
                        Destination = f.Route.Destination,
                        DistanceKm = f.Route.DistanceKm,
                    },
                    Origin = f.Route.Origin,
                    Destination = f.Route.Destination,
                    DepartureDate = f.DepartureDate,
                    ArrivalDate = f.ArrivalDate,
                    Status = f.Status,
                    Products = f.FreightProducts.Select(fp => new ProductDTO
                    {
                        ProductId = fp.ProductId,
                        Description = fp.Product.Description,
                        WeightKg = fp.Product.WeightKg,
                        VolumeM3 = fp.Product.VolumeM3
                    }).ToList()
                }).ToListAsync();

            return Ok(freights);
        }


        // GET: api/Freight/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<FreightDTO>> GetFreight(int id)
        {
            var freight = await _context.Freights
                .Include(f => f.Customer)
                .Include(f => f.Carrier)
                .Include(f => f.Vehicle)
                .Include(f => f.Route)
                .Include(f => f.FreightProducts)
                .ThenInclude(fp => fp.Product)
                .Where(f => f.FreightId == id)
                .Select(f => new FreightDTO
                {
                    FreightId = f.FreightId,
                    Origin = f.Route.Origin,
                    Destination = f.Route.Destination,
                    DepartureDate = f.DepartureDate,
                    ArrivalDate = f.ArrivalDate,
                    Status = f.Status,
                    Products = f.FreightProducts.Select(fp => new ProductDTO
                    {
                        ProductId = fp.ProductId,
                        Description = fp.Product.Description,
                        WeightKg = fp.Product.WeightKg,
                        VolumeM3 = fp.Product.VolumeM3
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (freight == null)
            {
                return NotFound();
            }

            return Ok(freight);
        }
        /*
        // POST: api/Freight
        [HttpPost]
        public async Task<ActionResult<Freight>> CreateFreight(FreightDTO freightDto)
        {
            var customer = await _context.Customers.FindAsync(freightDto.Customer.CustomerId);
            var carrier = await _context.Carriers.FindAsync(freightDto.Carrier.CarrierId);
            var vehicle = await _context.Vehicles.FindAsync(freightDto.Vehicle.VehicleId);
            var route = await _context.Routes.FindAsync(freightDto.Route.RouteId);

            if (customer == null)
            {
                return BadRequest("Invalid customer.");
            }
            if (carrier == null)
            {
                return BadRequest("Invalid carrier.");
            }
            if (vehicle == null )
            {
                return BadRequest("Invalid vehicle.");
            }
            if (route == null)
            {
                return BadRequest("Invalid route.");
            }

            
             * var freight = new Freight
             *             
             {
                Customer = freightDto.Customer,
                Carrier = freightDto.Carrier,
                Vehicle = freightDto.Vehicle,
                Route = freightDto.Route.RouteId,
                DepartureDate = freightDto.DepartureDate,
                ArrivalDate = freightDto.ArrivalDate,
                Status = freightDto.Status
             };
             
             
            Freight freight = ConvertFreightDtoToFreight(freightDto);

            _context.Freights.Add(freight);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFreight), new { id = freight.FreightId }, freight);
        }
        */

        // POST: api/Freight/create-all
        [HttpPost]
        public async Task<ActionResult<Freight>> CreateAllFreight(FreightDTO freightDto)
        {
            //var customer = await _context.Customers.FindAsync(freightDto.Customer.CustomerId);
            //var carrier = await _context.Carriers.FindAsync(freightDto.Carrier.CarrierId);
            //var vehicle = await _context.Vehicles.FindAsync(freightDto.Vehicle.VehicleId);
            //var route = await _context.Routes.FindAsync(freightDto.Route.RouteId);
            

            Freight? freightStored = await _context.Freights
                .Where(f => f.FreightId == freightDto.FreightId).FirstOrDefaultAsync();


            if(freightStored != null )
            {
                return BadRequest("Freight can not be updated here.");
            }

            if (freightDto.Customer == null)
            {
                return BadRequest("Invalid customer.");
            }
            if (freightDto.Carrier == null)
            {
                return BadRequest("Invalid carrier.");
            }
            if (freightDto.Vehicle == null)
            {
                return BadRequest("Invalid vehicle.");
            }
            if (freightDto.Route == null)
            {
                return BadRequest("Invalid route.");
            }
            

            Freight freight = _freightServices.createFreight(freightDto);

            _context.Freights.Add(freight);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFreight), new { id = freight.FreightId }, freight);
        }


        // PUT: api/Freight/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFreight(int id, FreightDTO freightDto)
        {
            if (id != freightDto.FreightId)
            {
                return BadRequest();
            }

            var freight = await _context.Freights.FindAsync(id);
            if (freight == null)
            {
                return NotFound();
            }

            freight.DepartureDate = freightDto.DepartureDate;
            freight.ArrivalDate = freightDto.ArrivalDate;
            freight.Status = freightDto.Status;

            _context.Entry(freight).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FreightExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Freight/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFreight(int id)
        {
            var freight = await _context.Freights.FindAsync(id);
            if (freight == null)
            {
                return NotFound();
            }

            _context.Freights.Remove(freight);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FreightExists(int id)
        {
            return _context.Freights.Any(e => e.FreightId == id);
        }
    }
}
