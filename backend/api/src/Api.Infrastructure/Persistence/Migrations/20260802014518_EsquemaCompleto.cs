using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Api.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class EsquemaCompleto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EstadosProyecto",
                columns: table => new
                {
                    Codigo = table.Column<string>(type: "text", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    EstaActivo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadosProyecto", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "Prioridades",
                columns: table => new
                {
                    Secuencial = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Color = table.Column<string>(type: "text", nullable: false),
                    EstaActivo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prioridades", x => x.Secuencial);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Secuencial = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Correo = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Secuencial);
                });

            migrationBuilder.CreateTable(
                name: "Proyectos",
                columns: table => new
                {
                    Secuencial = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CodigoEstadoProyecto = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proyectos", x => x.Secuencial);
                    table.ForeignKey(
                        name: "FK_Proyectos_EstadosProyecto_CodigoEstadoProyecto",
                        column: x => x.CodigoEstadoProyecto,
                        principalTable: "EstadosProyecto",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Columnas",
                columns: table => new
                {
                    Secuencial = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Orden = table.Column<int>(type: "integer", nullable: false),
                    SecuencialProyecto = table.Column<int>(type: "integer", nullable: false),
                    EstaActivo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Columnas", x => x.Secuencial);
                    table.ForeignKey(
                        name: "FK_Columnas_Proyectos_SecuencialProyecto",
                        column: x => x.SecuencialProyecto,
                        principalTable: "Proyectos",
                        principalColumn: "Secuencial",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tareas",
                columns: table => new
                {
                    Secuencial = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Titulo = table.Column<string>(type: "text", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    SecuencialColumna = table.Column<int>(type: "integer", nullable: false),
                    SecuencialPrioridad = table.Column<int>(type: "integer", nullable: false),
                    SecuencialUsuarioAsignado = table.Column<int>(type: "integer", nullable: false),
                    Orden = table.Column<int>(type: "integer", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EstaActivo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tareas", x => x.Secuencial);
                    table.ForeignKey(
                        name: "FK_Tareas_Columnas_SecuencialColumna",
                        column: x => x.SecuencialColumna,
                        principalTable: "Columnas",
                        principalColumn: "Secuencial",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tareas_Prioridades_SecuencialPrioridad",
                        column: x => x.SecuencialPrioridad,
                        principalTable: "Prioridades",
                        principalColumn: "Secuencial",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tareas_Usuarios_SecuencialUsuarioAsignado",
                        column: x => x.SecuencialUsuarioAsignado,
                        principalTable: "Usuarios",
                        principalColumn: "Secuencial",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Columnas_SecuencialProyecto",
                table: "Columnas",
                column: "SecuencialProyecto");

            migrationBuilder.CreateIndex(
                name: "IX_Proyectos_CodigoEstadoProyecto",
                table: "Proyectos",
                column: "CodigoEstadoProyecto");

            migrationBuilder.CreateIndex(
                name: "IX_Tareas_SecuencialColumna",
                table: "Tareas",
                column: "SecuencialColumna");

            migrationBuilder.CreateIndex(
                name: "IX_Tareas_SecuencialPrioridad",
                table: "Tareas",
                column: "SecuencialPrioridad");

            migrationBuilder.CreateIndex(
                name: "IX_Tareas_SecuencialUsuarioAsignado",
                table: "Tareas",
                column: "SecuencialUsuarioAsignado");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tareas");

            migrationBuilder.DropTable(
                name: "Columnas");

            migrationBuilder.DropTable(
                name: "Prioridades");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Proyectos");

            migrationBuilder.DropTable(
                name: "EstadosProyecto");
        }
    }
}
