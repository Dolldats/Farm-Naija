using Application.DTOs.Farmer;
using Application.Interface.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FarmNigeria.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FarmerController : ControllerBase
    {
        private readonly IFarmerServices _farmerServices;

        public FarmerController(IFarmerServices farmerServices)
        {
            _farmerServices = farmerServices;
        }

        [HttpPost]
        [Authorize(Roles = "Farmer")]
        public async Task<IActionResult> CreateFarmer(
            CreateFarmerDto dto)
        {
            try
            {
                var userId = GetUserId();

                var farmer =
                    await _farmerServices.CreateFarmerAsync(
                        userId,
                        dto);

                return CreatedAtAction(
                    nameof(GetFarmerById),
                    new { id = farmer.Id },
                    farmer);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllFarmer()
        {
            var farmers =
                await _farmerServices.GetAllFarmerAsync();

            return Ok(farmers);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFarmerById(int id)
        {
            var farmer =
                await _farmerServices.GetFarmerByIdAsync(id);

            if (farmer == null)
                return NotFound(new
                {
                    message = "Farmer not found."
                });

            return Ok(farmer);
        }

        [HttpGet("my-profile")]
        [Authorize(Roles = "Farmer")]
        public async Task<IActionResult> GetMyProfile()
        {
            var userId = GetUserId();

            var farmer =
                await _farmerServices.GetFarmerByUserIdAsync(userId);

            if (farmer == null)
                return NotFound(new
                {
                    message = "Farmer profile not found."
                });

            return Ok(farmer);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Farmer")]
        public async Task<IActionResult> Update(
            int id,
            UpdateFarmerDto dto)
        {
            try
            {
                var userId = GetUserId();

                var farmer =
                    await _farmerServices.UpdateFarmerAsync(
                        id,
                        userId,
                        dto);

                if (farmer == null)
                    return NotFound(new
                    {
                        message = "Farmer not found."
                    });

                return Ok(farmer);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid();
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Farmer")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var userId = GetUserId();

                var deleted =
                    await _farmerServices.DeleteFarmerAsync(
                        id,
                        userId);

                if (!deleted)
                    return NotFound(new
                    {
                        message = "Farmer not found."
                    });

                return NoContent();
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
        }

        private int GetUserId()
        {
            var userId =
                User.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? User.FindFirstValue(
                    ClaimTypes.Name);

            if (!int.TryParse(userId, out var id))
                throw new UnauthorizedAccessException(
                    "Invalid user identity.");

            return id;
        }
    }
}