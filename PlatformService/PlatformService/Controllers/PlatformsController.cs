using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatformService.Models.Repositories;
using PlatformService.SyncDataServices;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepository _platformRepository;
        private readonly IMapper _mapper;
        private readonly ICommandDataClient _commandDataClient;

        public PlatformsController(
            IPlatformRepository platformRepository,
            IMapper mapper,
            ICommandDataClient commandDataClient)
        {
            _platformRepository = platformRepository;
            _mapper = mapper;
            _commandDataClient = commandDataClient;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var platforms = await _platformRepository.All().ToListAsync();
            return Ok(_mapper.Map<List<PlatformReadDto>>(platforms));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var platform = await _platformRepository.FindAsync(id);
            return Ok(_mapper.Map<PlatformReadDto>(platform));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PlatformCreateDto platformCreateDto)
        {
            try
            {
                var platform = _mapper.Map<Platform>(platformCreateDto);
                await _platformRepository.CreateAsync(platform);
                await _platformRepository.SaveAsync();
                var readDto = _mapper.Map<PlatformReadDto>(platform);
                await _commandDataClient.SendPlatformToCommandServiceAsync(readDto);
                return CreatedAtRoute("", new { platform.Id }, readDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
