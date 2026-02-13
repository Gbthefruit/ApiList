using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace ApiList.Models {
	public class Progresso {

		public int Id { get; set; }

		[Required]
		public string Name { get; set; }
		public ICollection<Tarefas>? Tarefas { get; set; }

		public Progresso() {

			Tarefas = new Collection<Tarefas>();
		}
	}
}
