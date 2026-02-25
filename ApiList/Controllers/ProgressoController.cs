using ApiList.Context;
using ApiList.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Diagnostics;

namespace ApiList.Controllers; 

[ApiController]
[Route("[controller]")]
public class ProgressoController : ControllerBase {

	private readonly TarefaDbContext _context;

	public ProgressoController(TarefaDbContext context) {

		_context = context;
	}

	[HttpGet]
	public ActionResult<IEnumerable<Progresso>> Get() {

		try {
			var progresso = _context.Progresso.ToList();

			if (progresso is null) {

				return NotFound();
			}

			return Ok(progresso);
		}
		catch (Exception) {

			return StatusCode(StatusCodes.Status500InternalServerError, "Ops... Ocorreu um erro interno no servidor, Contate o suporte!");
		}
	}

	[HttpGet("progressotarefas")]
	public async Task<ActionResult<IEnumerable>> GetAllProgressoTarefasAsync() {

		try {
			return await _context.Progresso.Include(t => t.Tarefas).AsNoTracking().ToListAsync();
		}
		catch (Exception) {

			return StatusCode(StatusCodes.Status500InternalServerError, "Ops... Ocorreu um erro interno no servidor, Contate o suporte!");
		}
	}

	[HttpGet("{id:int:min(1)}")]
	public async Task<ActionResult<IEnumerable<Progresso>>> GetIdProgressoTarefasAsync(int id) {

		try {
			var progressoAndTarefas = await _context.Progresso.Include(t => t.Tarefas).Where(p => p.Id == id).AsNoTracking().ToListAsync();

			if (progressoAndTarefas is null) {

				return NotFound("Id Inválido. Tente 1 para os Não Finalizados e 2 para os Finalizado.");
			}

			return Ok(progressoAndTarefas);
		}
		catch (Exception) {

			return StatusCode(StatusCodes.Status500InternalServerError, "Ops... Ocorreu um erro interno no servidor, Contate o suporte!");
		}	
	}
}
