using ApiList.Context;
using ApiList.Filters;
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
    private readonly ILogger<TarefasController> _logger;

    public ProgressoController(TarefaDbContext context, ILogger<TarefasController> logger) {

		_context = context;
		_logger = logger;

	}

	[HttpGet]
	public ActionResult<IEnumerable<Progresso>> Get() {

		return _context.Progresso.ToList();
	}

	[HttpGet("progressotarefas")]
	[ServiceFilter(typeof(ApiLoggingFilter))]
	public async Task<ActionResult<IEnumerable>> GetAllProgressoTarefasAsync() {
	
		return await _context.Progresso.Include(t => t.Tarefas).AsNoTracking().ToListAsync();
	
	}

	[HttpGet("{id:int:min(1)}")]
	public async Task<ActionResult<IEnumerable<Progresso>>> GetIdProgressoTarefasAsync(int id) {

        var progressoTarefas = await _context.Progresso.Include(t => t.Tarefas).Where(p => p.Id == id).AsNoTracking().ToListAsync();

		if (progressoTarefas is null) {

			_logger.LogWarning("Nenhuma tarefa encontrada");
			return NotFound("Nenhuma tarefa encontrada");
		}

		return Ok(progressoTarefas);
	}
}
