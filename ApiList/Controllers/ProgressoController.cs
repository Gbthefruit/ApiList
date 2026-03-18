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

	private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<TarefasController> _logger;

    public ProgressoController(IUnitOfWork unitOfWork, ILogger<TarefasController> logger) {

        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
	public ActionResult<IEnumerable<Progresso>> Get() {

		var progressos = _unitOfWork.ProgressoRepository.GetProgressos();
		return Ok(progressos);
	}

	[HttpGet("progressotarefas")]
	[ServiceFilter(typeof(ApiLoggingFilter))]
	public ActionResult<IEnumerable> GetAllProgressoTarefas() {
	
		var progressoTarefas = _unitOfWork.ProgressoRepository.GetProgressoTarefas();
		
		if (progressoTarefas is null) {

            _logger.LogWarning("Nenhum progresso encontrado");
            return NotFound("Nenhuma progresso encontrado");
        }
		return Ok(progressoTarefas);
	
	}

	[HttpGet("{id:int:min(1)}")]
	public async Task<ActionResult<IEnumerable<Progresso>>> GetIdProgressoTarefasAsync(int id) {

		var progressoTarefas = _unitOfWork.ProgressoRepository.GetProgressoTarefasId(id);

		if (progressoTarefas is null) {

            _logger.LogWarning($"Nenhum progresso com id= {id} encontrado");
            return NotFound($"Nenhuma progresso com id= {id} encontrado");
        }

		return Ok(progressoTarefas);
	}
}
