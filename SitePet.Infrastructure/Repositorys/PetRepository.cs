using Microsoft.EntityFrameworkCore;
using SitePet.Domain.Interfaces;
using SitePet.Domain.Models;
using SitePet.Infrastructure.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;

namespace SitePet.Infrastructure.Repositorys
{
    public class PetRepository : IPetRepository
    {
        private readonly MeuDbContext _context;
        

        public PetRepository(MeuDbContext context)
        {
            _context = context;
        }

        public async Task Adicionar(Pet entity)
        {
            _context.Add(entity);
            await SaveChanges();
        }

        public async Task Atualizar(Pet entity)
        {
            _context.Update(entity);
            await SaveChanges();
        }

        public async Task<List<Pet>> MostrarCaes()
        {
            return await _context.Pets.Where(p => p.Tipo == Tipo.Cachorro)
                .AsNoTracking().ToListAsync();
        }

        public async Task<List<Pet>> MostrarGatos()
        {
            return await _context.Pets.Where(p => p.Tipo == Tipo.Gato)
                .AsNoTracking().ToListAsync();
        }

        public async Task<Pet> MostrarPorId(int id)
        {
            return await _context.Pets
                 .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Pet>> MostrarTodos()
        {
            return await _context.Pets
                .AsNoTracking().OrderByDescending(p=> p.Id)
                .ToListAsync();
        }

        public async Task Remover(int id)
        {
            _context.Remove(new Pet { Id = id });
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<List<Pet>> MostarPetUsuario(string user)
        {
            return await _context.Pets.AsNoTracking().Where(p => p.Usuario == user).ToListAsync();
        }

        
    }
}
