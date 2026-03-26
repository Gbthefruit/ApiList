using ApiList.Models;

namespace ApiList.DTOs.Mappings {
    public static class TarefaDTOMappingExtensions {

        public static TarefasDTO? ToTarefaDto(this Tarefas tarefas) {

            if (tarefas is null) return null;

            return new TarefasDTO {

                Id = tarefas.Id,
                Name = tarefas.Name,
                Description = tarefas.Description,
                Importance = tarefas.Importance,
                ProgressoId = tarefas.ProgressoId
            };
        }

        public static Tarefas? ToTarefa(this TarefasDTO tarefasDto) {

            if (tarefasDto is null) return null;

            return new Tarefas {
            
                Id = tarefasDto.Id,
                Name = tarefasDto.Name,
                Description = tarefasDto.Description,
                Importance = tarefasDto.Importance,
                ProgressoId = tarefasDto.ProgressoId
            };
        }

        public static IEnumerable<TarefasDTO?> ToTarefasDtoList(this IEnumerable<Tarefas> tarefas) {

            if (tarefas is null || !tarefas.Any()) return null;

            List<TarefasDTO> tarefasDtoList = new List<TarefasDTO>();

            foreach (Tarefas tarefa in tarefas) {

                var tarefaDto = new TarefasDTO {

                    Id = tarefa.Id,
                    Name = tarefa.Name,
                    Description = tarefa.Description,
                    Importance = tarefa.Importance,
                    ProgressoId = tarefa.ProgressoId
                };
                tarefasDtoList.Add(tarefaDto);
            }

            return tarefasDtoList;
        }
    }
}
