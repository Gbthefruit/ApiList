using ApiList.Context;
using ApiList.Models;
using ApiList.Pagination;
using Microsoft.EntityFrameworkCore;

namespace ApiList.Repositories;

public class TarefasRepository : ITarefasRepository {

    private readonly TarefaDbContext _context;

    public TarefasRepository(TarefaDbContext context) {

        _context = context; 
    }
    public IEnumerable<Tarefas> GetTarefas(TarefasParameters tarefasParams) {

        return _context.Tarefas.OrderBy(p => p.Name)
            .Skip((tarefasParams.pageNumber - 1) * tarefasParams.PageSize)
            .Take(tarefasParams.PageSize).ToList();
    }

    public Tarefas GetTarefasId(int id) {
        
        return _context.Tarefas.FirstOrDefault(t => t.Id == id);
    }

    public Tarefas Create(Tarefas tarefa) {

        if (tarefa is null) {
        
            throw new ArgumentNullException(nameof(tarefa));
        }
        _context.Tarefas.Add(tarefa);
        return tarefa;
    }

    public Tarefas Update(Tarefas tarefa) {

        if (tarefa is null) {

            throw new ArgumentNullException(nameof(tarefa));
        }
        _context.Entry(tarefa).State = EntityState.Modified;
        return tarefa;
    }

    public Tarefas Delete(int id) {

        var tarefa = _context.Tarefas.Find(id);

        if (tarefa is null) {

            throw new ArgumentNullException(nameof(tarefa));
        }
        _context.Tarefas.Remove(tarefa);
        return tarefa;
    }
}
