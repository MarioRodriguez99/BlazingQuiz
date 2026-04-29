using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazingQuiz.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "PasswordHash" },
                values: new object[] { "admin@gmail.com", "AQAAAAIAAYagAAAAEAK1a+U2c7bKy6IjzCVgaWYxi4JfZxNp+nqAqKh58qcfxIZUaR+lwt4Jh3o6QiGa2g==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "PasswordHash" },
                values: new object[] { "admin@gmal.com", "73l8gRjwLftklgfdXT+MdiMEjJwGPVMsyVxe16iYpk8=" });
        }
    }
}
