using ApiList.Models;
using System.ComponentModel.DataAnnotations;

namespace ApiList.DTOs; 
public class ProgressoDTO {

    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
    public ICollection<Tarefas>? Tarefas { get; set; }
}