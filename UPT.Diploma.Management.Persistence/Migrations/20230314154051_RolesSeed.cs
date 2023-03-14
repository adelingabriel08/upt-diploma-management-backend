using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UPT.Diploma.Management.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RolesSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO AspNetRoles(Id, Name, NormalizedName, ConcurrencyStamp) VALUES
                    (uuid(), 'Company', 'COMPANY', ''),
                    (uuid(), 'Professor', 'PROFESSOR', ''),
                    (uuid(), 'Student', 'STUDENT', '')
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DELETE FROM AspNetRoles
                WHERE NormalizedName in ('COMPANY', 'PROFESSOR', 'STUDENT')
            ");
        }
    }
}
