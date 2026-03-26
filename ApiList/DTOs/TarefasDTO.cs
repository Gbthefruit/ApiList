using ApiList.Enum;
using System.ComponentModel.DataAnnotations;

namespace ApiList.DTOs {
    public class TarefasDTO {

        public int Id { get; set; }

        [Required(ErrorMessage = "Nome obrigatório")]
        [StringLength(50, ErrorMessage = "Número de caracteres excedidos!")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Descrição obrigatória")]
        [StringLength(150, ErrorMessage = "Número de caracteres excedidos!")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Nível de importancia obrigatória.")]

        public Importance Importance { get; set; }

        [Required]
        public int ProgressoId { get; set; }
    }
}
