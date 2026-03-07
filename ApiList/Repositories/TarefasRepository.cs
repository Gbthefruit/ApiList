using ApiList.Context;
using ApiList.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiList.Repositories;

public class TarefasRepository : ITarefasRepository {

    private readonly TarefaDbContext _context;

    public TarefasRepository(TarefaDbContext context) {

        _context = context;
    }
    public IEnumerable<Tarefas> GetTarefas() {

        return _context.Tarefas.AsNoTracking().ToList();
    }

    public Tarefas GetTarefasId(int id) {
        
        return _context.Tarefas.FirstOrDefault(t => t.Id == id);
    }

    public Tarefas Create(Tarefas tarefa) {

        _context.Tarefas.Add(tarefa);
        _context.SaveChanges();
        return tarefa;
    }

    public Tarefas Update(Tarefas tarefa) {

        _context.Entry(tarefa).State = EntityState.Modified;
        _context.SaveChanges();
        return tarefa;
    }

    public Tarefas Delete(int id) {

        var tarefa = _context.Tarefas.Find(id);
        _context.Tarefas.Remove(tarefa);
        _context.SaveChanges();
        return tarefa;
    }
}
