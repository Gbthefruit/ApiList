using ApiList.Context;
using ApiList.Models;
using ApiList.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiList.Controllers;

[ApiController]
[Route("[controller]")]
public class TarefasController : ControllerBase {

	private readonly ITarefasRepository _repository;
	private readonly ILogger<TarefasController> _logger;

	public TarefasController(ITarefasRepository repository, ILogger<TarefasController> logger) {

		_repository = repository;
		_logger = logger;
	}

	[HttpGet]
	public ActionResult<IEnumerable<Tarefas>> Get() {

		var tarefas = _repository.GetTarefas();
		return Ok(tarefas);
	}

	[HttpGet("{id:int:min(1)}", Name="ObterTarefa")]
	public ActionResult<Tarefas> Get(int id) {

		var tarefa = _repository.GetTarefasId(id);
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

		var tarefaCreated = _repository.Create(tarefa);
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

		var tarefaUpdated = _repository.Update(tarefas);

		return Ok(tarefaUpdated);
	}

	[HttpDelete("{id:int}")]
	public ActionResult Delete(int id) {

		var tarefaDeleted = _repository.Delete(id);

		if (tarefaDeleted is null) {

            _logger.LogWarning($"Tarefa com id={id} não encontrada.");
            return NotFound($"Tarefa com id={id} não encontrada.");
        }
		return Ok(tarefaDeleted);
	}
}
