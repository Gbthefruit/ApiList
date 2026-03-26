using ApiList.Models;

namespace ApiList.DTOs.Mappings; 
public static class ProgressoDTOMappingExtensions {

    public static IEnumerable<ProgressoDTO> ToProgressoTarefasDTOList(this IEnumerable<Progresso> progressos) {

        if (progressos is null) return null;

        List<ProgressoDTO> progressosTarefasDtoList = new List<ProgressoDTO>();

        foreach (Progresso progresso in progressos) {
        
            var tarefas = progresso.Tarefas.ToList();

            ProgressoDTO progressoTarefasDto = new ProgressoDTO {

                Id = progresso.Id,
                Name = progresso.Name,
                Tarefas = tarefas
            };

            progressosTarefasDtoList.Add(progressoTarefasDto);
        }

        return progressosTarefasDtoList;
    }

    public static IEnumerable<ProgressoDTO> ToProgressoDTOList(this IEnumerable<Progresso> progressos) {

        if (progressos is null || !progressos.Any()) return new List<ProgressoDTO>(); 

        return progressos.Select(progresso => new ProgressoDTO {

            Id = progresso.Id,
            Name = progresso.Name,
        }).ToList();
    }
}
