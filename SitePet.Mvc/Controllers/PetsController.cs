using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SitePet.Mvc.Interfaces;
using SitePet.Mvc.Models;
using System;
using System.IO;
using System.Threading.Tasks;
using X.PagedList;

namespace SitePet.Mvc.Controllers
{
    [Authorize]
    public class PetsController : Controller
    {
        private readonly IPetService _petService;
  

        public PetsController(IPetService petService)
        {
            _petService = petService;
          
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(int pagina = 1 )
        {
           
            var pets = await _petService.MostrarPets();

       
            return View(pets.ToPagedList(pagina, 6));
        }

    
        [AllowAnonymous]
        [Route("gatos")]
        public async Task<IActionResult> IndexGatos(int pagina = 1)
        {
            var pets = await _petService.MostrarGatos();
            return View(pets.ToPagedList(pagina, 6));
        }

        [AllowAnonymous]
        [Route("caes")]
        public async Task<IActionResult> IndexCaes(int pagina = 1 )
        {
            

            var pets = await _petService.MostrarCaes();
            return View(pets.ToPagedList(pagina, 6));
        }



        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var petViewModel = await ObterPet(id);

            if (petViewModel == null)
            {
                return NotFound();
            }

            return View(petViewModel);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PetViewModel petViewModel)
        {
            if (!ModelState.IsValid) return View(petViewModel);

            var imgPrefixo = Guid.NewGuid() + "_";
            if (!await UploadArquivo(petViewModel.ImagemUpload, imgPrefixo))
            {
                return View(petViewModel);
            }

            petViewModel.Imagem = imgPrefixo + petViewModel.ImagemUpload.FileName;
            petViewModel.DataCadastro = DateTime.Now;

            petViewModel.Usuario = User.Identity.Name;


            await _petService.AdicionarPet(petViewModel);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var pet = await ObterPet(id);

            if (pet == null) return NotFound();

            return View(pet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PetViewModel petViewModel)
        {
            if (id != petViewModel.Id)
            {
                return NotFound();
            }

            var petsAtualizao = await ObterPet(id);

            petViewModel.Imagem = petsAtualizao.Imagem;
            petViewModel.Usuario = petsAtualizao.Usuario;

            if (!ModelState.IsValid) return View(petViewModel);

            if (petViewModel.ImagemUpload != null)
            {
                var imgPrefixo = Guid.NewGuid() + "_";
                if (!await UploadArquivo(petViewModel.ImagemUpload, imgPrefixo))
                {
                    return View(petViewModel);
                }

                petsAtualizao.Imagem = imgPrefixo + petViewModel.ImagemUpload.FileName;

            }

            petsAtualizao.Nome = petViewModel.Nome;
            petsAtualizao.Raca = petViewModel.Raca;
            petsAtualizao.Tipo = petViewModel.Tipo;
            petsAtualizao.Genero = petViewModel.Genero;
            petsAtualizao.Idade = petViewModel.Idade;

            var user = User.Identity.Name;

            if (user == petViewModel.Usuario) 
            {
                await _petService.AtualizarPet(petsAtualizao);

            }
            else if(user == "admin@gmail.com")
            {
                await _petService.AtualizarPet(petsAtualizao);
            }
            else
            {
                return View("Erro");
            }
            
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var pet = await ObterPet(id);
          

           

           

            if (pet == null)
            {
                return NotFound();
            }

            return View(pet);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pet = await ObterPet(id);

            if (pet == null)
            {
                return NotFound();
            }
            var user = User.Identity.Name;

            if (user != pet.Usuario)
            {
              
                return View("Erro");
            }

            await _petService.RemoverPet(id);


            return RedirectToAction(nameof(Index));
        }


        private async Task<PetViewModel> ObterPet(int id)
        {
            var pet = await _petService.GetById(id);
            return pet;
        }

        private async Task<bool> UploadArquivo(IFormFile arquivo, string imgPrefixo)
        {
            if (arquivo.Length <= 0) return false;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens", imgPrefixo + arquivo.FileName);

            if (System.IO.File.Exists(path))
            {
                ModelState.AddModelError(string.Empty, "Já existe um arquivo com este nome!");
                return false;
            }

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await arquivo.CopyToAsync(stream);
            }

            return true;
        }

    

       



    }
}
