using System.ComponentModel.DataAnnotations;

namespace DavinTI.Application.DTOs {

    public class ContatoCreateDto {
        [Required]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        [Range(0, 200)]
        public int Idade { get; set; }

        public List<TelefoneCreateDto> Telefones { get; set; } = new();
    }

    public class ContatoReadDto {
        public int IdContato { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int Idade { get; set; }
    }

    public class ContatoUpdateDto {
        [Required]
        public int IdContato { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        [Range(0, 200)]
        public int Idade { get; set; }

        public List<TelefoneUpdateDto> Telefones { get; set; } = new();
    }

    public class ContatoComTelefonesDto {
        public int IdContato { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int Idade { get; set; }
        public List<TelefoneReadDto> Telefones { get; set; } = new();
    }
}
