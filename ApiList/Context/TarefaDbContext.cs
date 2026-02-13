using ApiList.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiList.Context; 
public class TarefaDbContext : DbContext {

	public DbSet<Tarefas>? Tarefas { get; set; }
	public DbSet<Progresso>? Progresso { get; set; }

	public TarefaDbContext(DbContextOptions<TarefaDbContext> Options) : base(Options) 
	{
	}

}
