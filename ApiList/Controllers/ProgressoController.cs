using ApiList.DTOs;
using ApiList.DTOs.Mappings;
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
	public ActionResult<IEnumerable<ProgressoDTO>> Get() {

		var progressos = _unitOfWork.ProgressoRepository.GetProgressos();
		if (progressos is null) {

            _logger.LogWarning("Nenhum progresso encontrado");
            return NotFound("Nenhuma progresso encontrado");
        }

		var progressosDto = progressos.ToProgressoDTOList();

		return Ok(progressosDto);
	}

	[HttpGet("progressotarefas")]
	[ServiceFilter(typeof(ApiLoggingFilter))]
	public ActionResult<IEnumerable<ProgressoDTO>> GetAllProgressosTarefas() {
	
		var progressosTarefas = _unitOfWork.ProgressoRepository.GetProgressoTarefas();
		
		if (progressosTarefas is null) {

            _logger.LogWarning("Nenhum progresso encontrado");
            return NotFound("Nenhuma progresso encontrado");
        }

		var progressosTarefasDto = progressosTarefas.ToProgressoTarefasDTOList();

		return Ok(progressosTarefasDto);
	
	}

	[HttpGet("{id:int:min(1)}")]
	public async Task<ActionResult<IEnumerable<ProgressoDTO>>> GetIdProgressoTarefasAsync(int id) {

		if (id > 2) {

            _logger.LogWarning($"O Progresso com id= {id} não existe.");
            return NotFound($"O Progresso com id= {id} não existe.");
        }

		var progressoTarefas = _unitOfWork.ProgressoRepository.GetProgressoTarefasId(id);

		var progressoTarefasIdDto = progressoTarefas.ToProgressoTarefasDTOList();

		return Ok(progressoTarefasIdDto);
	}
}
