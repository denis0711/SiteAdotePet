using SitePet.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SitePet.Domain.Interfaces
{
    public interface IPetRepository
    {
        Task<List<Pet>> MostrarTodos();

        Task<Pet> MostrarPorId(int id);

        Task Adicionar(Pet entity);

        Task<List<Pet>> MostrarGatos();

        Task<List<Pet>> MostrarCaes();
        Task<List<Pet>> MostarPetUsuario(string user);

        Task Atualizar(Pet entity);

        Task Remover(int id);

        Task<int> SaveChanges();
    }
}
