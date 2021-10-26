using System;
using System.Collections.Generic;

namespace SitePet.Domain.Models
{
    public class Pet
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Raca { get; set; }
        public Tipo Tipo { get; set; }
        public Genero Genero { get; set; }
        public string Imagem { get; set; }
        public int Idade { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Usuario { get; set; }


    }

    public enum Tipo
    {
        Gato = 1,
        Cachorro
    }

    public enum Genero
    {
        Femea = 1,
        Macho
    }
}
