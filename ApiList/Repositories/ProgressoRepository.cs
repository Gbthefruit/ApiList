using ApiList.Context;
using ApiList.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiList.Repositories;

public class ProgressoRepository : IProgressoRepository {

    private readonly TarefaDbContext _context;

    public ProgressoRepository(TarefaDbContext context) {

        _context = context;
    }

    public IEnumerable<Progresso> GetProgressos() {

        return _context.Progresso.AsNoTracking().ToList();
    }

    public IEnumerable<Progresso> GetProgressoTarefas() {
        
        return _context.Progresso.Include(t => t.Tarefas).AsNoTracking().ToList();
    }

    public IEnumerable<Progresso> GetProgressoTarefasId(int id) {

        return _context.Progresso.Include(t => t.Tarefas).Where(p => p.Id == id).AsNoTracking().ToList();
    }
}
