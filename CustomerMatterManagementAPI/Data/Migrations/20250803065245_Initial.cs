using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CustomerMatterManagementAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "cmschema");

            migrationBuilder.CreateTable(
                name: "customers",
                schema: "cmschema",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    PhoneNum = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("customers_pkey", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "cmschema",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    EmailAddr = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    FirstName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    LastName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Password = table.Column<byte[]>(type: "bytea", nullable: false),
                    FirmName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("users_pkey", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "matters",
                schema: "cmschema",
                columns: table => new
                {
                    MatterId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    CustomerId = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("matters_pkey", x => x.MatterId);
                    table.ForeignKey(
                        name: "matters_CustomerId_fkey",
                        column: x => x.CustomerId,
                        principalSchema: "cmschema",
                        principalTable: "customers",
                        principalColumn: "CustomerId");
                });

            migrationBuilder.CreateIndex(
                name: "fki_customerId_ref_customers_fk",
                schema: "cmschema",
                table: "customers",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_matters_CustomerId",
                schema: "cmschema",
                table: "matters",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "users_EmailAddr_key",
                schema: "cmschema",
                table: "users",
                column: "EmailAddr",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "matters",
                schema: "cmschema");

            migrationBuilder.DropTable(
                name: "users",
                schema: "cmschema");

            migrationBuilder.DropTable(
                name: "customers",
                schema: "cmschema");
        }
    }
}
