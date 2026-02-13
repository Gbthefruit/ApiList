using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiList.Migrations
{
    /// <inheritdoc />
    public partial class PopulaProgresso : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("Insert into Progresso(Name) Value('Not Finished')");
			mb.Sql("Insert into Progresso(Name) Value('Finished')");
		}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from Progresso");
        }
    }
}
