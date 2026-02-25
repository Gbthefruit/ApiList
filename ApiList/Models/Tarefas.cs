using ApiList.Enum;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiList.Models; 
public class Tarefas : IValidatableObject {

	[Key]
	public int Id { get; set; }

	[Required(ErrorMessage = "Nome obrigatório")]
	[StringLength(50, ErrorMessage= "Número de caracteres excedidos!")]
	public string? Name { get; set; }

	[Required(ErrorMessage = "Descrição obrigatória")]
	[StringLength(150, ErrorMessage ="Número de caracteres excedidos!")]
	public string? Description { get; set; }

	[Required(ErrorMessage = "Nível de importancia obrigatória.")]

	public Importance Importance { get; set; }

	[Required(ErrorMessage = "A data de criação é obrigatória.")]
	public DateTime Creation { get; set; }

	[Required]
	public int ProgressoId { get; set; }

	[JsonIgnore]
	public Progresso? Progress { get; set; }

	public Tarefas() { }

	public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) {
	
		if (!string.IsNullOrEmpty(this.Name)) {

			var firstLetter = this.Name.ToString()[0].ToString();
			if (firstLetter != firstLetter.ToUpper()) {

				yield return new
					ValidationResult("O nome da tarefa deve começar com letra maiúscula.",
					new[] {
						nameof(this.Name)
					});
			}
		}
	}

}