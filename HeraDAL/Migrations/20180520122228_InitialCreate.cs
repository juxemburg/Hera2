using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HeraDAL.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    SecurityStamp = table.Column<string>(nullable: true),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    UsuarioId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.UniqueConstraint("AK_AspNetUsers_UsuarioId", x => x.UsuarioId);
                });

            migrationBuilder.CreateTable(
                name: "Estudiantes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ActividadesPc = table.Column<int>(nullable: false),
                    Apellidos = table.Column<string>(nullable: true),
                    ConocimientoComputador = table.Column<int>(nullable: false),
                    Edad = table.Column<int>(nullable: false),
                    FrecuenciaPc = table.Column<int>(nullable: false),
                    Genero = table.Column<int>(nullable: false),
                    Grado = table.Column<int>(nullable: false),
                    ManejoComputador = table.Column<int>(nullable: false),
                    MateriaFavorita = table.Column<int>(nullable: false),
                    Nombres = table.Column<string>(nullable: true),
                    UsuarioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estudiantes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Profesores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Activo = table.Column<bool>(nullable: false),
                    Apellidos = table.Column<string>(nullable: true),
                    Nombres = table.Column<string>(nullable: true),
                    UsuarioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profesores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cursos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Activo = table.Column<bool>(nullable: false),
                    Color = table.Column<int>(nullable: false),
                    Descripcion = table.Column<string>(nullable: true),
                    Nombre = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    ProfesorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cursos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cursos_Profesores_ProfesorId",
                        column: x => x.ProfesorId,
                        principalTable: "Profesores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Desafios",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(nullable: true),
                    DirDesafioInicial = table.Column<string>(nullable: true),
                    DirSolucion = table.Column<string>(nullable: true),
                    Nombre = table.Column<string>(nullable: true),
                    ProfesorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Desafios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Desafios_Profesores_ProfesorId",
                        column: x => x.ProfesorId,
                        principalTable: "Profesores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rel_Cursos_Estudiantes",
                columns: table => new
                {
                    CursoId = table.Column<int>(nullable: false),
                    EstudianteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rel_Cursos_Estudiantes", x => new { x.CursoId, x.EstudianteId });
                    table.ForeignKey(
                        name: "FK_Rel_Cursos_Estudiantes_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rel_Cursos_Estudiantes_Estudiantes_EstudianteId",
                        column: x => x.EstudianteId,
                        principalTable: "Estudiantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InfoDesafios",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AdvancedEventUse = table.Column<bool>(nullable: false),
                    BasicInputUse = table.Column<bool>(nullable: false),
                    BasicOperators = table.Column<bool>(nullable: false),
                    CloneUse = table.Column<bool>(nullable: false),
                    DesafioId = table.Column<int>(nullable: false),
                    ListUse = table.Column<bool>(nullable: false),
                    MediumOperators = table.Column<bool>(nullable: false),
                    MessageUse = table.Column<bool>(nullable: false),
                    MultipleSpriteEvents = table.Column<bool>(nullable: false),
                    MultipleThreads = table.Column<bool>(nullable: false),
                    NestedOperators = table.Column<bool>(nullable: false),
                    NonCreatedVariableUse = table.Column<bool>(nullable: false),
                    NonUnusedBlocks = table.Column<bool>(nullable: false),
                    SecuenceUse = table.Column<bool>(nullable: false),
                    SpriteSensisng = table.Column<bool>(nullable: false),
                    TwoGreenFlagThread = table.Column<bool>(nullable: false),
                    UseMediumBlocks = table.Column<bool>(nullable: false),
                    UseNestedControl = table.Column<bool>(nullable: false),
                    UseSimpleBlocks = table.Column<bool>(nullable: false),
                    UserDefinedBlocks = table.Column<bool>(nullable: false),
                    VariableUse = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfoDesafios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InfoDesafios_Desafios_DesafioId",
                        column: x => x.DesafioId,
                        principalTable: "Desafios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    ProfesorId = table.Column<int>(nullable: false),
                    DesafioId = table.Column<int>(nullable: false),
                    Rating = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => new { x.ProfesorId, x.DesafioId });
                    table.ForeignKey(
                        name: "FK_Ratings_Desafios_DesafioId",
                        column: x => x.DesafioId,
                        principalTable: "Desafios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ratings_Profesores_ProfesorId",
                        column: x => x.ProfesorId,
                        principalTable: "Profesores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rel_Cursos_Desafios",
                columns: table => new
                {
                    DesafioId = table.Column<int>(nullable: false),
                    CursoId = table.Column<int>(nullable: false),
                    Initial = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rel_Cursos_Desafios", x => new { x.DesafioId, x.CursoId });
                    table.ForeignKey(
                        name: "FK_Rel_Cursos_Desafios_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Rel_Cursos_Desafios_Desafios_DesafioId",
                        column: x => x.DesafioId,
                        principalTable: "Desafios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "RegistroCalificaiones",
                columns: table => new
                {
                    CursoId = table.Column<int>(nullable: false),
                    EstudianteId = table.Column<int>(nullable: false),
                    DesafioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistroCalificaiones", x => new { x.CursoId, x.EstudianteId, x.DesafioId });
                    table.ForeignKey(
                        name: "FK_RegistroCalificaiones_Desafios_DesafioId",
                        column: x => x.DesafioId,
                        principalTable: "Desafios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_RegistroCalificaiones_Rel_Cursos_Estudiantes_CursoId_EstudianteId",
                        columns: x => new { x.CursoId, x.EstudianteId },
                        principalTable: "Rel_Cursos_Estudiantes",
                        principalColumns: new[] { "CursoId", "EstudianteId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Calificaciones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CalificacionCualitativaId = table.Column<int>(nullable: true),
                    CursoId = table.Column<int>(nullable: false),
                    DesafioId = table.Column<int>(nullable: false),
                    DirArchivo = table.Column<string>(nullable: true),
                    EstudianteId = table.Column<int>(nullable: false),
                    TiempoFinal = table.Column<DateTime>(nullable: true),
                    Tiempoinicio = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calificaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Calificaciones_RegistroCalificaiones_CursoId_EstudianteId_DesafioId",
                        columns: x => new { x.CursoId, x.EstudianteId, x.DesafioId },
                        principalTable: "RegistroCalificaiones",
                        principalColumns: new[] { "CursoId", "EstudianteId", "DesafioId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CalificacionesCualitativas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CalificacionId = table.Column<int>(nullable: false),
                    Completada = table.Column<bool>(nullable: false),
                    Descripcion = table.Column<string>(nullable: true),
                    FechaRegistro = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalificacionesCualitativas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CalificacionesCualitativas_Calificaciones_CalificacionId",
                        column: x => x.CalificacionId,
                        principalTable: "Calificaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResultadosScratch",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CalificacionId = table.Column<int>(nullable: false),
                    DeadCodeCount = table.Column<int>(nullable: false),
                    DuplicateScriptsCount = table.Column<int>(nullable: false),
                    General = table.Column<bool>(nullable: false),
                    IInfoScratch_GeneralId = table.Column<int>(nullable: true),
                    IInfoScratch_SpriteId = table.Column<int>(nullable: true),
                    Nombre = table.Column<string>(nullable: true),
                    NumBloques = table.Column<int>(nullable: false),
                    NumScripts = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResultadosScratch", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResultadosScratch_Calificaciones_CalificacionId",
                        column: x => x.CalificacionId,
                        principalTable: "Calificaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BloquesScratch",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Frecuencia = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    ResultadoScratchId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloquesScratch", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BloquesScratch_ResultadosScratch_ResultadoScratchId",
                        column: x => x.ResultadoScratchId,
                        principalTable: "ResultadosScratch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InfoGenerales",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AdvancedEventUse = table.Column<int>(nullable: false),
                    AdvancedOperators = table.Column<int>(nullable: false),
                    BasicInputUse = table.Column<int>(nullable: false),
                    BasicOperators = table.Column<int>(nullable: false),
                    CloneUse = table.Column<int>(nullable: false),
                    EventsUse = table.Column<bool>(nullable: false),
                    ListUse = table.Column<bool>(nullable: false),
                    MediumOperators = table.Column<int>(nullable: false),
                    MessageUse = table.Column<bool>(nullable: false),
                    MultipleThreads = table.Column<int>(nullable: false),
                    NonUnusedBlocks = table.Column<int>(nullable: false),
                    ResultadoScratchId = table.Column<int>(nullable: false),
                    SecuenceUse = table.Column<int>(nullable: false),
                    SharedVariables = table.Column<bool>(nullable: false),
                    SpriteCount = table.Column<int>(nullable: false),
                    SpriteSensing = table.Column<int>(nullable: false),
                    TwoGreenFlagTrhead = table.Column<int>(nullable: false),
                    UseMediumBlocks = table.Column<int>(nullable: false),
                    UseNestedControl = table.Column<int>(nullable: false),
                    UseSimpleBlocks = table.Column<int>(nullable: false),
                    UserDefinedBlocks = table.Column<int>(nullable: false),
                    VariableCreation = table.Column<int>(nullable: false),
                    VariableUse = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfoGenerales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InfoGenerales_ResultadosScratch_ResultadoScratchId",
                        column: x => x.ResultadoScratchId,
                        principalTable: "ResultadosScratch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InfoSprites",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AdvancedEventUse = table.Column<bool>(nullable: false),
                    AdvancedOperators = table.Column<bool>(nullable: false),
                    BasicInputUse = table.Column<bool>(nullable: false),
                    BasicOperators = table.Column<bool>(nullable: false),
                    CloneUse = table.Column<bool>(nullable: false),
                    MediumOperators = table.Column<bool>(nullable: false),
                    MultipleThreads = table.Column<bool>(nullable: false),
                    NonUnusedBlocks = table.Column<bool>(nullable: false),
                    ResultadoScratchId = table.Column<int>(nullable: false),
                    SecuenceUse = table.Column<bool>(nullable: false),
                    SpriteSensing = table.Column<bool>(nullable: false),
                    TwoGreenFlagTrhead = table.Column<bool>(nullable: false),
                    UseMediumBlocks = table.Column<bool>(nullable: false),
                    UseNestedControl = table.Column<bool>(nullable: false),
                    UseSimpleBlocks = table.Column<bool>(nullable: false),
                    UserDefinedBlocks = table.Column<bool>(nullable: false),
                    VariableCreation = table.Column<bool>(nullable: false),
                    VariableUse = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfoSprites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InfoSprites_ResultadosScratch_ResultadoScratchId",
                        column: x => x.ResultadoScratchId,
                        principalTable: "ResultadosScratch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BloquesScratch_ResultadoScratchId",
                table: "BloquesScratch",
                column: "ResultadoScratchId");

            migrationBuilder.CreateIndex(
                name: "IX_Calificaciones_CursoId_EstudianteId_DesafioId",
                table: "Calificaciones",
                columns: new[] { "CursoId", "EstudianteId", "DesafioId" });

            migrationBuilder.CreateIndex(
                name: "IX_CalificacionesCualitativas_CalificacionId",
                table: "CalificacionesCualitativas",
                column: "CalificacionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cursos_ProfesorId",
                table: "Cursos",
                column: "ProfesorId");

            migrationBuilder.CreateIndex(
                name: "IX_Desafios_ProfesorId",
                table: "Desafios",
                column: "ProfesorId");

            migrationBuilder.CreateIndex(
                name: "IX_InfoDesafios_DesafioId",
                table: "InfoDesafios",
                column: "DesafioId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InfoGenerales_ResultadoScratchId",
                table: "InfoGenerales",
                column: "ResultadoScratchId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InfoSprites_ResultadoScratchId",
                table: "InfoSprites",
                column: "ResultadoScratchId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_DesafioId",
                table: "Ratings",
                column: "DesafioId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistroCalificaiones_DesafioId",
                table: "RegistroCalificaiones",
                column: "DesafioId");

            migrationBuilder.CreateIndex(
                name: "IX_Rel_Cursos_Desafios_CursoId",
                table: "Rel_Cursos_Desafios",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_Rel_Cursos_Estudiantes_EstudianteId",
                table: "Rel_Cursos_Estudiantes",
                column: "EstudianteId");

            migrationBuilder.CreateIndex(
                name: "IX_ResultadosScratch_CalificacionId",
                table: "ResultadosScratch",
                column: "CalificacionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BloquesScratch");

            migrationBuilder.DropTable(
                name: "CalificacionesCualitativas");

            migrationBuilder.DropTable(
                name: "InfoDesafios");

            migrationBuilder.DropTable(
                name: "InfoGenerales");

            migrationBuilder.DropTable(
                name: "InfoSprites");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "Rel_Cursos_Desafios");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ResultadosScratch");

            migrationBuilder.DropTable(
                name: "Calificaciones");

            migrationBuilder.DropTable(
                name: "RegistroCalificaiones");

            migrationBuilder.DropTable(
                name: "Desafios");

            migrationBuilder.DropTable(
                name: "Rel_Cursos_Estudiantes");

            migrationBuilder.DropTable(
                name: "Cursos");

            migrationBuilder.DropTable(
                name: "Estudiantes");

            migrationBuilder.DropTable(
                name: "Profesores");
        }
    }
}
