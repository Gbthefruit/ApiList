using ApiList.Context;
using ApiList.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiList.Controllers;

[ApiController]
[Route("[controller]")]
public class TarefasController : ControllerBase {

	private readonly TarefaDbContext _context;
	private readonly ILogger<TarefasController> _logger;

	public TarefasController(TarefaDbContext context, ILogger<TarefasController> logger) {

		_context = context;
		_logger = logger;
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<Tarefas>>> GetAsync() {

		return await _context.Tarefas.Take(5).AsNoTracking().ToListAsync();
	}

	[HttpGet("{id:int:min(1)}", Name="ObterTarefa")]
	public ActionResult<Tarefas> Get(int id) {

		var tarefa = _context.Tarefas.FirstOrDefault(t => id == t.Id);
		if (tarefa is null) {

			_logger.LogWarning($"Tarefa com id={id} não encontrada.");
			return NotFound($"Tarefa com id={id} não encontrada.");
		}

		return Ok(tarefa);
	}

	[HttpPost]
	public ActionResult Post(Tarefas tarefa) {
		
		if (tarefa is null) {

			_logger.LogWarning("Dados Inválidos.");
			return BadRequest("Dados Inválidos.");
		}

		_context.Tarefas.Add(tarefa);
		_context.SaveChanges();
		return new CreatedAtRouteResult("ObterTarefa", new {

			id = tarefa.Id
		}, tarefa);
	}

	[HttpPut("{id:int}")]
	public ActionResult Put(int id, Tarefas tarefas) {

		if (id != tarefas.Id) {

            _logger.LogWarning("Dados Inválidos.");
            return BadRequest("Dados Inválidos.");
        }

		_context.Entry(tarefas).State = EntityState.Modified;
		_context.SaveChanges();

		return Ok(tarefas);
	}

	[HttpDelete("{id:int}")]
	public ActionResult Delete(int id) {

		var tarefa = _context.Tarefas.FirstOrDefault(t => id == t.Id);

		if (tarefa is null) {

            _logger.LogWarning($"Tarefa com id={id} não encontrada.");
            return NotFound($"Tarefa com id={id} não encontrada.");
        }

		_context.Tarefas.Remove(tarefa);
		_context.SaveChanges();

		return Ok(tarefa);
	}
}
