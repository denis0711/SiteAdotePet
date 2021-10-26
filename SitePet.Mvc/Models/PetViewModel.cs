using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SitePet.Mvc.Models
{
    public class PetViewModel
    {
        [Key]
        public virtual int Id { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "O {0} dever ter de {1} ate {2} caracteres", MinimumLength = 3)]
        public virtual string Nome { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "O {0} dever ter de {1} ate {2} caracteres", MinimumLength = 3)]
        [DisplayName("Raça")]
        public virtual string Raca { get; set; }

        [Required]
        public virtual int Tipo { get; set; }

        [Required]
        public virtual int Genero { get; set; }

        [DisplayName("Imagem do Pet")]
        public virtual IFormFile ImagemUpload { get; set; }
        public virtual string Imagem { get; set; }

        public virtual string Usuario { get; set; }

        [DisplayName("Data Cadastro")]
        public virtual DateTime DataCadastro { get; set; }

        
        public virtual int Idade { get; set; }
    }
}
