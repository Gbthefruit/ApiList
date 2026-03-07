using ApiList.Models;

namespace ApiList.Repositories; 
public interface IProgressoRepository {

    IEnumerable<Progresso> GetProgressos();
    IEnumerable<Progresso> GetProgressoTarefas();
    IEnumerable<Progresso> GetProgressoTarefasId(int id);
}
