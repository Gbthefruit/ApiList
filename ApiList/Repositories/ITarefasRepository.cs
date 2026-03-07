using ApiList.Models;

namespace ApiList.Repositories; 
public interface ITarefasRepository {

    IEnumerable<Tarefas> GetTarefas();
    Tarefas GetTarefasId(int id);
    Tarefas Create(Tarefas tarefas);
    Tarefas Update(Tarefas tarefas);
    Tarefas Delete(int id);
}
