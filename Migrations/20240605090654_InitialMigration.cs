using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFDatabase.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("user_pk", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "messages",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    message_text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    message_date = table.Column<int>(type: "int", nullable: false),
                    is_sent = table.Column<bool>(type: "bit", nullable: false),
                    FromId = table.Column<int>(type: "int", nullable: false),
                    ToId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("message_pk", x => x.id);
                    table.ForeignKey(
                        name: "message_from_user_fk",
                        column: x => x.FromId,
                        principalTable: "users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "message_to_user_fk",
                        column: x => x.ToId,
                        principalTable: "users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_messages_FromId",
                table: "messages",
                column: "FromId");

            migrationBuilder.CreateIndex(
                name: "IX_messages_ToId",
                table: "messages",
                column: "ToId");

            migrationBuilder.CreateIndex(
                name: "IX_users_FullName",
                table: "users",
                column: "FullName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "messages");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
