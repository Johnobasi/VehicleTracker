using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleTracker.Core.Entities;
using VehicleTracker.Core.Interfaces;
using VehicleTracker.Web.Dto;
using VehicleTracker.Web.Response;

namespace VehicleTracker.Web.Controllers
{
    [Route("api/v1/vehicle")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<VehicleController> _logger;

        public VehicleController(IVehicleRepository vehicleRepository,IMapper mapper,ILogger<VehicleController> logger)
        {
            _vehicleRepository = vehicleRepository;
            _mapper = mapper;
            _logger = logger;
        }
        [HttpPost("addvehicle")]
        public async Task<ActionResult> Post(VehicleRequestDto request)
        {
            try
            {
                var vehicle = await _vehicleRepository.GetVehicleByNumber(request.Number);
                if (vehicle != null)               
                    return BadRequest(new BaseResponse<VehicleResponseDto> { Body = null, Code = "400", IsSuccessful = false, Message = "Vehicle already exist" });
                
                var model = _mapper.Map<VehicleRequestDto,Vehicles>(request);
                if (model is null)
                    return StatusCode(StatusCodes.Status500InternalServerError, 
                        new BaseResponse<string> { IsSuccessful = false, Body = null, Code ="500",Message = "internal server error" });

                await _vehicleRepository.AddAsync(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,$"{ex.Message} {ex.InnerException} {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                        new BaseResponse<string> { IsSuccessful = false, Body = null, Code = "500", Message = "internal server error" });
            }
            return CreatedAtAction(nameof(Post), new BaseResponse<VehicleRequestDto> { Body =request , Message = "new position has been created", IsSuccessful = true, Code = "201" });
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id )
        {
            VehicleResponseDto model = null;
            try
            {
                var vehicle = await _vehicleRepository.GetByIdAsync<Vehicles>(id);
                if (vehicle is null)
                    return NotFound(new BaseResponse<VehicleResponseDto> { Body = null, Code = "404", IsSuccessful = false, Message = "Vehicle Not found"});

                model = _mapper.Map<Vehicles, VehicleResponseDto>(vehicle);

                if (model is null)
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        new BaseResponse<string> { IsSuccessful = false, Body = null, Code = "500", Message = "internal server error" });

              
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.Message} {ex.InnerException} {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                        new BaseResponse<string> { IsSuccessful = false, Body = null, Code = "500", Message = "internal server error" });
            }
            return Ok(new BaseResponse<VehicleResponseDto> { Body = model, Code = "200", IsSuccessful = false, Message = "Vehicle enquiry successful" });
        }

     
    
    [HttpPost("getPosition")]
    public async Task<ActionResult> Get(VehiclePositionDateDto request)
    {
        VehicleResponseDto model = null;
        try
        {
                request.StartDate = DateTime.Now;
                request.EndDate = DateTime.Now.AddDays(5);
            var vehicle = await _vehicleRepository.GetVehicles(request.VehicleId, request.StartDate, request.EndDate,true);
            if (vehicle is null)
                return NotFound(new BaseResponse<VehicleResponseDto> { Body = null, Code = "404", IsSuccessful = false, Message = "Vehicle Not found" });

            model = _mapper.Map<Vehicles, VehicleResponseDto>(vehicle);

            if (model is null)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new BaseResponse<string> { IsSuccessful = false, Body = null, Code = "500", Message = "internal server error" });


        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{ex.Message} {ex.InnerException} {ex.StackTrace}");
            return StatusCode(StatusCodes.Status500InternalServerError,
                    new BaseResponse<string> { IsSuccessful = false, Body = null, Code = "500", Message = "internal server error" });
        }
        return Ok(new BaseResponse<VehicleResponseDto> { Body = model, Code = "200", IsSuccessful = false, Message = "Vehicle enquiry successful" });
    }


}
}
