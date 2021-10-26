using AutoMapper;
using SitePet.Domain.Interfaces;
using SitePet.Domain.Models;
using SitePet.Mvc.Interfaces;
using SitePet.Mvc.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SitePet.Mvc.Services
{
    public class PetService : IPetService
    {
        private readonly IPetRepository _petRepository;
        private readonly IMapper _mapper;

        public PetService(IPetRepository petRepository
            , IMapper mapper)
        {
            _petRepository = petRepository;
            _mapper = mapper;
        }

        public async Task AdicionarPet(PetViewModel pet)
        {
            var petAdd = _mapper.Map<Pet>(pet);
            await _petRepository.Adicionar(petAdd);
        }

        public async Task<PetViewModel> GetById(int id)
        {
            return _mapper.Map<PetViewModel>(await _petRepository.MostrarPorId(id));
        }

        public async Task<IEnumerable<PetViewModel>> MostrarPets()
        {
            return _mapper.Map<IEnumerable<PetViewModel>>(await _petRepository.MostrarTodos());
        }

        public async Task RemoverPet(int id)
        {
            await _petRepository.Remover(id);
        }

        public async Task AtualizarPet(PetViewModel pet)
        {
            var petUp = _mapper.Map<Pet>(pet);
            await _petRepository.Atualizar(petUp);
        }

        public async Task<IEnumerable<PetViewModel>> MostrarCaes()
        {
            return _mapper.Map<IEnumerable<PetViewModel>>(await
                _petRepository.MostrarCaes());
        }

        public async Task<IEnumerable<PetViewModel>> MostrarGatos()
        {
            return _mapper.Map<IEnumerable<PetViewModel>>(await
                _petRepository.MostrarGatos());
        }

        public async Task<IEnumerable<PetViewModel>> MostrarPetUsuario(string email)
        {
            return _mapper.Map<IEnumerable<PetViewModel>>(await _petRepository.MostarPetUsuario(email));
        }

        
    }
}
