using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HRAcuity.Persistence.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NotableQuotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Author = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    Quote = table.Column<string>(type: "character varying(1000000)", maxLength: 1000000, nullable: false),
                    QuoteLength = table.Column<int>(type: "integer", nullable: false),
                    QuoteHash = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotableQuotes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotableQuotes_QuoteLength",
                table: "NotableQuotes",
                column: "QuoteLength");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotableQuotes");
        }
    }
}
