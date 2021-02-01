using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    [Route("api/v1/position")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        private readonly ILogger<PositionController> _logger;
        private readonly IPositionReppository _positionReppository;
        private readonly IMapper _mapper;
        private readonly IVehicleRepository _vehicleRepository;

        public PositionController(ILogger<PositionController> logger,IPositionReppository positionReppository,IMapper mapper,IVehicleRepository vehicleRepository)
        {
            _logger = logger;
            _positionReppository = positionReppository;
            _mapper = mapper;
            _vehicleRepository  = vehicleRepository;
        }
        [HttpPost("addposition")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post(PositionRequestDto request)
        {
            try
            {
                var vehicle = await _vehicleRepository.GetByIdAsync<Vehicles>(request.VehicleId);
                if (vehicle is null)
                    return NotFound(new BaseResponse<PositionResponseDto> { Body = null, Code = "404", IsSuccessful = false, Message = "vevhile not found" });
                var model = _mapper.Map<PositionRequestDto, Position>(request);
                model.Entry = DateTime.Now;
                model.Vehicles = vehicle;
               
              
                    await _positionReppository.AddAsync(model);
            }
            catch (Exception ex)
            {
                var model = _mapper.Map<PositionRequestDto, Position>(request);

                _logger.LogError(ex, $"{ex.Message} {ex.InnerException} {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse<PositionResponseDto> { Message = "An error occured while adding position", Body = null, Code = "500", IsSuccessful = false });
            }

            return CreatedAtAction(nameof(Post), new BaseResponse<PositionRequestDto> { Body = request, Code= "200",  IsSuccessful = true, Message ="position enquiry sucessfully"});
        }
        [HttpGet("getposition/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get(int request)
        {
            PositionResponseDto positionResponseDto = null;
            try
            {

                var model = await _positionReppository.GetByIdAsync<Position>(request);
                if (model is null)
                    return NotFound(new BaseResponse<PositionResponseDto> { Body = null, Code = "404", IsSuccessful = false, Message = "postion cannot found" });
                positionResponseDto = _mapper.Map<Position, PositionResponseDto>(model);
              
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, $"{ex.Message} {ex.InnerException} {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse<PositionResponseDto> { Message = "An error occured while adding position", Body = null, Code = "500", IsSuccessful = false });
            }

            return CreatedAtAction(nameof(Post), new BaseResponse<PositionResponseDto> { Body = positionResponseDto, Message = "new position has been created",IsSuccessful = true, Code = "201"});
        }
    }
}
