using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace P1_AP1_SAMILDELACRUZ.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntradasHuacalesDetalle_TiposHuacales_TipoId",
                table: "EntradasHuacalesDetalle");

            migrationBuilder.AddForeignKey(
                name: "FK_EntradasHuacalesDetalle_TiposHuacales_TipoId",
                table: "EntradasHuacalesDetalle",
                column: "TipoId",
                principalTable: "TiposHuacales",
                principalColumn: "TipoId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntradasHuacalesDetalle_TiposHuacales_TipoId",
                table: "EntradasHuacalesDetalle");

            migrationBuilder.AddForeignKey(
                name: "FK_EntradasHuacalesDetalle_TiposHuacales_TipoId",
                table: "EntradasHuacalesDetalle",
                column: "TipoId",
                principalTable: "TiposHuacales",
                principalColumn: "TipoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
