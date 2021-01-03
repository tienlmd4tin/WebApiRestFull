using Microsoft.AspNetCore.Mvc;
using dotnet_rpg.Service.CharacterService;
using System.Threading.Tasks;
using dotnet_rpg.Dtos.Character;
using System.Linq;
using dotnet_rpg.Models;
using System.Collections.Generic;

namespace dotnet_rpg.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            var lsCharacter = await _characterService.GetAllCharacter();
            
            if (!lsCharacter.Data.Any())
            {
                return NotFound(lsCharacter);
            }

            return Ok(lsCharacter);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            ServiceResponse<GetCharacterDto> character = await _characterService.GetCharacterById(id);

            if (character.Data == null)
            {
                return NotFound(character);
            }

            return Ok(character);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddCharacter(AddCharacterDto newCharacter)
        {
            ServiceResponse<List<GetCharacterDto>> lsCharacter = await _characterService.AddCharacter(newCharacter);
            
            if (!lsCharacter.Data.Any())
            {
                return NotFound(lsCharacter);
            }

            return Ok(lsCharacter);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateCharacter(UpdateCharacterDto updateCharacter)
        {
            ServiceResponse<List<GetCharacterDto>> lsCharacter = await _characterService.UpdateCharacter(updateCharacter);
            
            if (!lsCharacter.Data.Any())
            {
                return NotFound(lsCharacter);
            }

            return Ok(lsCharacter);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Deleteharacter(int id)
        {
            ServiceResponse<List<GetCharacterDto>> lsCharacter = await _characterService.DeleteCharacter(id);
            
            if (!lsCharacter.Data.Any())
            {
                return NotFound(lsCharacter);
            }

            return Ok(lsCharacter);
        }
    }
}