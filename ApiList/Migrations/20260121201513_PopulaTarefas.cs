using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiList.Migrations
{
    /// <inheritdoc />
    public partial class PopulaTarefas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("Insert into Tarefas(Name, Description, Importance, Creation, ProgressoId) Value('Quintal', 'Varrer as folhas do quintal todos os dias durante 7 dias consecutivos.', 1, now(), 2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from Tarefas");
        }
    }
}
