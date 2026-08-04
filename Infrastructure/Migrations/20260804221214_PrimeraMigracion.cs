using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PrimeraMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TeamMembers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    AuditRecord_CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AuditRecord_LastModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AuditRecord_CreatedBy = table.Column<string>(type: "text", nullable: true),
                    AuditRecord_LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamMembers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Incidents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Severity = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    ReportedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExpectedResolutionAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ResolvedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AssignedToId = table.Column<Guid>(type: "uuid", nullable: true),
                    AuditRecord_CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AuditRecord_LastModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AuditRecord_CreatedBy = table.Column<string>(type: "text", nullable: true),
                    AuditRecord_LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incidents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Incidents_TeamMembers_AssignedToId",
                        column: x => x.AssignedToId,
                        principalTable: "TeamMembers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PostMortems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RootCause = table.Column<string>(type: "text", nullable: false),
                    LessonsLearned = table.Column<string>(type: "text", nullable: false),
                    IncidentId = table.Column<Guid>(type: "uuid", nullable: false),
                    AuditRecord_CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AuditRecord_LastModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AuditRecord_CreatedBy = table.Column<string>(type: "text", nullable: true),
                    AuditRecord_LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostMortems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostMortems_Incidents_IncidentId",
                        column: x => x.IncidentId,
                        principalTable: "Incidents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_AssignedToId",
                table: "Incidents",
                column: "AssignedToId");

            migrationBuilder.CreateIndex(
                name: "IX_PostMortems_IncidentId",
                table: "PostMortems",
                column: "IncidentId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostMortems");

            migrationBuilder.DropTable(
                name: "Incidents");

            migrationBuilder.DropTable(
                name: "TeamMembers");
        }
    }
}
