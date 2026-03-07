using ApiList.Filters;
using ApiList.Models;
using ApiList.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Diagnostics;

namespace ApiList.Controllers; 

[ApiController]
[Route("[controller]")]
public class ProgressoController : ControllerBase {

	private readonly IProgressoRepository _repository;
    private readonly ILogger<TarefasController> _logger;

    public ProgressoController(IProgressoRepository repository, ILogger<TarefasController> logger) {

		_repository = repository;
		_logger = logger;

	}

	[HttpGet]
	public ActionResult<IEnumerable<Progresso>> Get() {

		var progressos = _repository.GetProgressos();
		return Ok(progressos);
	}

	[HttpGet("progressotarefas")]
	[ServiceFilter(typeof(ApiLoggingFilter))]
	public ActionResult<IEnumerable> GetAllProgressoTarefas() {
	
		var progressoTarefas = _repository.GetProgressoTarefas();
		
		if (progressoTarefas is null) {

            _logger.LogWarning("Nenhum progresso encontrado");
            return NotFound("Nenhuma progresso encontrado");
        }
		return Ok(progressoTarefas);
	
	}

	[HttpGet("{id:int:min(1)}")]
	public async Task<ActionResult<IEnumerable<Progresso>>> GetIdProgressoTarefasAsync(int id) {

		var progressoTarefas = _repository.GetProgressoTarefasId(id);

		if (progressoTarefas is null) {

            _logger.LogWarning($"Nenhum progresso com id= {id} encontrado");
            return NotFound($"Nenhuma progresso com id= {id} encontrado");
        }

		return Ok(progressoTarefas);
	}
}
