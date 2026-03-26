using ApiList.DTOs;
using ApiList.DTOs.Mappings;
using ApiList.Models;
using ApiList.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiList.Controllers;

[ApiController]
[Route("[controller]")]
public class TarefasController : ControllerBase {

	private readonly IUnitOfWork _unitOfWork;
	private readonly ILogger<TarefasController> _logger;

    public TarefasController(IUnitOfWork unitOfWork, ILogger<TarefasController> logger) {

        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
	public ActionResult<IEnumerable<TarefasDTO>> Get() {

		var tarefas = _unitOfWork.TarefasRepository.GetTarefas();

		var tarefasDto = tarefas.ToTarefasDtoList();

		return Ok(tarefasDto);
	}

	[HttpGet("{id:int:min(1)}", Name="ObterTarefa")]
	public ActionResult<Tarefas> Get(int id) {

		var tarefa = _unitOfWork.TarefasRepository.GetTarefasId(id);
		if (tarefa is null) {

			_logger.LogWarning($"Tarefa com id={id} não encontrada.");
			return NotFound($"Tarefa com id={id} não encontrada.");
		}

		var tarefaDto = tarefa.ToTarefaDto();
		return Ok(tarefaDto);
	}

	[HttpPost]
	public ActionResult Post(TarefasDTO tarefaDto) {
		
		if (tarefaDto is null) {

			_logger.LogWarning("Dados Inválidos.");
			return BadRequest("Dados Inválidos.");
		}

		var tarefa = tarefaDto.ToTarefa();
		var tarefaCreated = _unitOfWork.TarefasRepository.Create(tarefa);
		_unitOfWork.Commit();
		var novaTarefaDto = tarefaCreated.ToTarefaDto();
		
		return new CreatedAtRouteResult("ObterTarefa", new {

			id = novaTarefaDto.Id
		}, novaTarefaDto);
	}

	[HttpPut("{id:int}")]
	public ActionResult Put(int id, TarefasDTO tarefaDto) {

		if (id != tarefaDto.Id) {

            _logger.LogWarning("Dados Inválidos.");
            return BadRequest("Dados Inválidos.");
        }

		var tarefas = tarefaDto.ToTarefa();
		var tarefaUpdated = _unitOfWork.TarefasRepository.Update(tarefas);
		_unitOfWork.Commit();
		var novaTarefaDto = tarefaUpdated.ToTarefaDto();

		return Ok(novaTarefaDto);
	}

	[HttpDelete("{id:int}")]
	public ActionResult Delete(int id) {

		var tarefaDeleted = _unitOfWork.TarefasRepository.Delete(id);

		if (tarefaDeleted is null) {

            _logger.LogWarning($"Tarefa com id={id} não encontrada.");
            return NotFound($"Tarefa com id={id} não encontrada.");
        }

		_unitOfWork.Commit();
		var tarefaDto = tarefaDeleted.ToTarefaDto();

		return Ok(tarefaDto);
	}
}