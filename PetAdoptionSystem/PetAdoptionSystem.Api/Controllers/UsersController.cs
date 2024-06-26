﻿using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetAdoptionSystem.Application.Dtos;
using PetAdoptionSystem.Application.Interfaces;

namespace PetAdoptionSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRequestDto user)
        {
            var createdUser = await _userService.Register(user);

            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserRequestDto loginRequest)
        {
            var token = await _userService.Login(loginRequest.Username, loginRequest.Password);

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized();
            }

            return Ok(token);
        }

        [HttpGet("profile")]
        [Authorize]
        public async Task<IActionResult> GetUserById()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized();
            }

            var user = await _userService.GetUserById(userId);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
    }
}