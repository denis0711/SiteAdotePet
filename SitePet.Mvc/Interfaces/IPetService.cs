using SitePet.Mvc.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SitePet.Mvc.Interfaces
{
    public interface IPetService 
    {
        Task<IEnumerable<PetViewModel>> MostrarPets();
        Task<PetViewModel> GetById(int id);

        Task<IEnumerable<PetViewModel>> MostrarCaes();
        Task<IEnumerable<PetViewModel>> MostrarGatos();
        Task<IEnumerable<PetViewModel>> MostrarPetUsuario(string email);
        Task AdicionarPet(PetViewModel pet);
        Task RemoverPet(int id);
        Task AtualizarPet(PetViewModel pet);

    }
}
