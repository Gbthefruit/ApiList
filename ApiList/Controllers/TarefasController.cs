using ApiList.Context;
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
	public ActionResult<IEnumerable<Tarefas>> Get() {

		var tarefas = _unitOfWork.TarefasRepository.GetTarefas();
		return Ok(tarefas);
	}

	[HttpGet("{id:int:min(1)}", Name="ObterTarefa")]
	public ActionResult<Tarefas> Get(int id) {

		var tarefa = _unitOfWork.TarefasRepository.GetTarefasId(id);
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

		var tarefaCreated = _unitOfWork.TarefasRepository.Create(tarefa);
		_unitOfWork.Commit();
		return new CreatedAtRouteResult("ObterTarefa", new {

			id = tarefaCreated.Id
		}, tarefaCreated);
	}

	[HttpPut("{id:int}")]
	public ActionResult Put(int id, Tarefas tarefas) {

		if (id != tarefas.Id) {

            _logger.LogWarning("Dados Inválidos.");
            return BadRequest("Dados Inválidos.");
        }

		var tarefaUpdated = _unitOfWork.TarefasRepository.Update(tarefas);
		_unitOfWork.Commit();
		return Ok(tarefaUpdated);
	}

	[HttpDelete("{id:int}")]
	public ActionResult Delete(int id) {

		var tarefaDeleted = _unitOfWork.TarefasRepository.Delete(id);

		if (tarefaDeleted is null) {

            _logger.LogWarning($"Tarefa com id={id} não encontrada.");
            return NotFound($"Tarefa com id={id} não encontrada.");
        }
		_unitOfWork.Commit();
		return Ok(tarefaDeleted);
	}
}
