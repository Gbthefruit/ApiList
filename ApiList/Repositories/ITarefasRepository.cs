using ApiList.Models;
using ApiList.Pagination;

namespace ApiList.Repositories; 
public interface ITarefasRepository {

    IEnumerable<Tarefas> GetTarefas(TarefasParameters tarefasParams);
    Tarefas GetTarefasId(int id); 
    Tarefas Create(Tarefas tarefas);
    Tarefas Update(Tarefas tarefas);
    Tarefas Delete(int id);
}
