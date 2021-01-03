using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Models;

namespace dotnet_rpg.Service.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private static List<Character> characters = new List<Character> {
            new Character(),
            new Character { Id = 2, Name = "TienLM"},
        };

        private readonly IMapper _mapper;

        public CharacterService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var serviceRs = new ServiceResponse<List<GetCharacterDto>>();

            var characterItem = _mapper.Map<Character>(newCharacter);
            characterItem.Id = characters.Max(m => m.Id) + 1;

            characters.Add(characterItem);
            serviceRs.Data = characters.Select(m => _mapper.Map<GetCharacterDto>(m)).ToList();

            return serviceRs;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var serviceRs = new ServiceResponse<List<GetCharacterDto>>();
            var dltCharacter = characters.FirstOrDefault(m => m.Id.Equals(id));

            if (dltCharacter == null)
            {
                serviceRs.Success = false;
                serviceRs.Message = "Object not found";
            }
            else
            {
                try
                {
                    characters.Remove(dltCharacter);

                    serviceRs.Data = characters.Select(m => _mapper.Map<GetCharacterDto>(m)).ToList();
                }
                catch (Exception ex)
                {
                    serviceRs.Success = false;
                    serviceRs.Message = ex.Message;
                }
            }

            return serviceRs;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacter()
        {
            var serviceRs = new ServiceResponse<List<GetCharacterDto>>();
            serviceRs.Data = characters.Select(m => _mapper.Map<GetCharacterDto>(m)).ToList();

            return serviceRs;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceRs = new ServiceResponse<GetCharacterDto>();
            var dataQuery = _mapper.Map<GetCharacterDto>(characters.FirstOrDefault(m => m.Id.Equals(id)));
            serviceRs.Data = dataQuery;

            return serviceRs;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> UpdateCharacter(UpdateCharacterDto updateCharacter)
        {
            var serviceRs = new ServiceResponse<List<GetCharacterDto>>();
            var updCharacter = characters.FirstOrDefault(m => m.Id.Equals(updateCharacter.Id));

            if (updCharacter != null)
            {
                try
                {
                    updCharacter.Name = updateCharacter.Name;
                    updCharacter.Class = updateCharacter.Class;
                    updateCharacter.Defence = updateCharacter.Defence;
                    updateCharacter.HitPoints = updateCharacter.HitPoints;
                    updateCharacter.Intelligence = updateCharacter.Intelligence;
                    updateCharacter.Strength = updateCharacter.Strength;

                    serviceRs.Data = characters.Select(m => _mapper.Map<GetCharacterDto>(m)).ToList();
                }
                catch (Exception ex)
                {
                    serviceRs.Success = false;
                    serviceRs.Message = ex.Message;
                }
            }
            else
            {
                serviceRs.Success = false;
                serviceRs.Message = "Object not found";
            }

            return serviceRs;
        }
    }
}