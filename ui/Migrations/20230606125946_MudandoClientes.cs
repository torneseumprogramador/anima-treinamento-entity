using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace entity.Migrations
{
    /// <inheritdoc />
    public partial class CriandoFornecedoresEMudandoClientes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Clientes",
                table: "Clientes");

            migrationBuilder.RenameTable(
                name: "Clientes",
                newName: "tb_clientes");

            migrationBuilder.RenameColumn(
                name: "Telefone",
                table: "tb_clientes",
                newName: "cli_telefone");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "tb_clientes",
                newName: "cli_nome");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "tb_clientes",
                newName: "cli_id");

            migrationBuilder.AlterColumn<string>(
                name: "cli_telefone",
                table: "tb_clientes",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                comment: "Este é o número de telefone do cliente.",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Observacao",
                table: "tb_clientes",
                type: "text",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_clientes",
                table: "tb_clientes",
                column: "cli_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_clientes",
                table: "tb_clientes");

            migrationBuilder.DropColumn(
                name: "Observacao",
                table: "tb_clientes");

            migrationBuilder.RenameTable(
                name: "tb_clientes",
                newName: "Clientes");

            migrationBuilder.RenameColumn(
                name: "cli_telefone",
                table: "Clientes",
                newName: "Telefone");

            migrationBuilder.RenameColumn(
                name: "cli_nome",
                table: "Clientes",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "cli_id",
                table: "Clientes",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "Telefone",
                table: "Clientes",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20,
                oldComment: "Este é o número de telefone do cliente.")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clientes",
                table: "Clientes",
                column: "Id");
        }
    }
}
