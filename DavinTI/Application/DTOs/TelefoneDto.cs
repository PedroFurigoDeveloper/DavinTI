using System.ComponentModel.DataAnnotations;

namespace DavinTI.Application.DTOs {
    public class TelefoneCreateDto {
        [Required]
        public int IdContato { get; set; }

        [Required]
        [StringLength(16, ErrorMessage = "O número não pode exceder 16 caracteres.")]
        public string Numero { get; set; } = string.Empty;
    }
    public class TelefoneReadDto {
        public int Id { get; set; }

        public int IdContato { get; set; }

        public string Numero { get; set; } = string.Empty;
    }

    public class TelefoneGetByIdDto {
        [Required]
        public int Id { get; set; }
    }

    public class TelefoneUpdateDto {
        [Required]
        public int Id { get; set; }

        [Required]
        public int IdContato { get; set; }

        [Required]
        [StringLength(16, ErrorMessage = "O número não pode exceder 16 caracteres.")]
        public string Numero { get; set; } = string.Empty;
    }

    public class TelefoneDeleteDto {
        [Required]
        public int Id { get; set; }
    }
}
