using ApiList.Context;
using ApiList.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiList.Controllers;

[ApiController]
[Route("[controller]")]
public class TarefasController : ControllerBase {

	private readonly TarefaDbContext _context;

	public TarefasController(TarefaDbContext context) {

		_context = context;
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<Tarefas>>> GetAsync() {

		try {
			var tarefa = await _context.Tarefas.AsNoTracking().Take(5).ToListAsync();
			if (tarefa is null) {

				return NotFound("Nenhum produto encontrado.");
			}
			return Ok(tarefa);
		}
		catch (Exception) {

			return StatusCode(StatusCodes.Status500InternalServerError, "Ops... Ocorreu um erro interno no servidor, Contate o suporte!");
		}
	}

	[HttpGet("{id:int:min(1)}", Name="ObterProduto")]
	public ActionResult<Tarefas> Get(int id) {

		try {
			var tarefa = _context.Tarefas.FirstOrDefault(t => t.Id == id);

			if (tarefa is null) {

				return NotFound("Produto não encontrado.");
			}

			return Ok(tarefa);
		}
		catch (Exception) {

			return StatusCode(StatusCodes.Status500InternalServerError, "Ops... Ocorreu um erro interno no servidor, Contate o suporte!");
		}
	}

	[HttpPost]
	public ActionResult Post(Tarefas tarefa) {

		try {
			if (tarefa is null) {
				return BadRequest("Dados inválidos.");
			}
			_context.Tarefas.Add(tarefa);
			_context.SaveChanges();

			return new CreatedAtRouteResult("ObterProduto",
				new { id = tarefa.Id }, tarefa);
		}
		catch (Exception) {

			return StatusCode(StatusCodes.Status500InternalServerError, "Ops... Ocorreu um erro interno no servidor, Contate o suporte!");
		}
	}

	[HttpPut("{id:int}")]
	public ActionResult Put(int id, Tarefas tarefas) {

		try {
			if (id != tarefas.Id) {

				return BadRequest("Dados inválidos.");
			}

			_context.Entry(tarefas).State = EntityState.Modified;
			_context.SaveChanges();

			return Ok(tarefas);
		}
		catch (Exception) {

			return StatusCode(StatusCodes.Status500InternalServerError, "Ops... Ocorreu um erro interno no servidor, Contate o suporte!");
		}
	}

	[HttpDelete("{id:int}")]
	public ActionResult Delete(int id) {

		try {
			var tarefa = _context.Tarefas.FirstOrDefault(t => t.Id == id);

			if (tarefa is null) {

				return NotFound("Produto não encontrado.");
			}

			_context.Tarefas.Remove(tarefa);
			_context.SaveChanges();

			return Ok($"Tarefa excluida com êxito.");
		}
		catch (Exception) {

			return StatusCode(StatusCodes.Status500InternalServerError, "Ops... Ocorreu um erro interno no servidor, Contate o suporte!");
		}
	}
}
