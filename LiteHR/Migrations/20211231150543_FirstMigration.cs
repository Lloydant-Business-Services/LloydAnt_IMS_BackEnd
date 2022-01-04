using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LiteHR.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "APPLICATION_SECTION_HEADER",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APPLICATION_SECTION_HEADER", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "APPOINTMENT_TYPE",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APPOINTMENT_TYPE", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AREA_OF_SPECIALIZATION",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AREA_OF_SPECIALIZATION", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ASSET_TYPE",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ASSET_TYPE", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "COUNTRY",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COUNTRY", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DOCUMENT_TYPE",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DOCUMENT_TYPE", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EDUCATIONAL_QUALIFICATION",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EDUCATIONAL_QUALIFICATION", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EVENT",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Venue = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EVENT", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FACULTY",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FACULTY", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FAILED_STAFF_UPLOADS",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Surname = table.Column<string>(nullable: true),
                    Firstname = table.Column<string>(nullable: true),
                    Othername = table.Column<string>(nullable: true),
                    StaffNumber = table.Column<string>(nullable: true),
                    DOB = table.Column<string>(nullable: true),
                    DateOfConfirmation = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    InstitutionDepartmentId = table.Column<long>(nullable: false),
                    DateOfLastPromotion = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    AppointmentType = table.Column<string>(nullable: true),
                    SalaryGradeCategory = table.Column<string>(nullable: true),
                    SalaryLevelId = table.Column<long>(nullable: false),
                    SalaryStepId = table.Column<long>(nullable: false),
                    InstitutionRankId = table.Column<long>(nullable: false),
                    DOE = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FAILED_STAFF_UPLOADS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GENDER",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GENDER", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HOLIDAYS",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HOLIDAYS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "INSTITUTION_APPOINTMENT",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INSTITUTION_APPOINTMENT", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "INSTITUTION_STAFF_CATEGORY",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INSTITUTION_STAFF_CATEGORY", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "INSTITUTION_STAFF_TYPE",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INSTITUTION_STAFF_TYPE", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "INSTITUTION_UNIT",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INSTITUTION_UNIT", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JOB_TYPE",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JOB_TYPE", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LEAVE_TYPE",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LEAVE_TYPE", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MailArchiveFileType",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    FileNumber = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailArchiveFileType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MARITAL_STATUS",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MARITAL_STATUS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PARENT_MENU",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Icon = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PARENT_MENU", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PFA_NAME",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PFA_NAME", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PFA_STATUS",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PFA_STATUS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RELIGION",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RELIGION", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ROLE",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROLE", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SALARY_EXTRA_TYPE",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SALARY_EXTRA_TYPE", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SALARY_GRADE_CATEGORY",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Representation = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SALARY_GRADE_CATEGORY", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SALARY_LEVEL",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SALARY_LEVEL", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SALARY_STEP",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SALARY_STEP", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SALARY_TYPE",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SALARY_TYPE", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SETTINGS",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    LogoUrl = table.Column<string>(nullable: true),
                    BaseColour = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SETTINGS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "STAFF_DB_COMMIT_DETAILS",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastSerialNumber = table.Column<long>(nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STAFF_DB_COMMIT_DETAILS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "STATE",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STATE", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VISITATION_TYPE",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(maxLength: 100, nullable: true),
                    Description = table.Column<string>(maxLength: 100, nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VISITATION_TYPE", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ASSET",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssetTypeId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ASSET", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ASSET_ASSET_TYPE_AssetTypeId",
                        column: x => x.AssetTypeId,
                        principalTable: "ASSET_TYPE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "INSTITUTION_DEPARTMENT",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    FacultyId = table.Column<long>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INSTITUTION_DEPARTMENT", x => x.Id);
                    table.ForeignKey(
                        name: "FK_INSTITUTION_DEPARTMENT_FACULTY_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "FACULTY",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LEAVE",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    StaffTypeId = table.Column<long>(nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LEAVE", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LEAVE_INSTITUTION_STAFF_TYPE_StaffTypeId",
                        column: x => x.StaffTypeId,
                        principalTable: "INSTITUTION_STAFF_TYPE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TRAINING_TYPE",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    StaffTypeId = table.Column<long>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TRAINING_TYPE", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TRAINING_TYPE_INSTITUTION_STAFF_TYPE_StaffTypeId",
                        column: x => x.StaffTypeId,
                        principalTable: "INSTITUTION_STAFF_TYPE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "INSTITUTION_CHART",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitId = table.Column<long>(nullable: true),
                    Position = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INSTITUTION_CHART", x => x.Id);
                    table.ForeignKey(
                        name: "FK_INSTITUTION_CHART_INSTITUTION_UNIT_UnitId",
                        column: x => x.UnitId,
                        principalTable: "INSTITUTION_UNIT",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "INSTITUTION_RANK",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    InstitutionUnitId = table.Column<long>(nullable: false),
                    GradeLevel = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INSTITUTION_RANK", x => x.Id);
                    table.ForeignKey(
                        name: "FK_INSTITUTION_RANK_INSTITUTION_UNIT_InstitutionUnitId",
                        column: x => x.InstitutionUnitId,
                        principalTable: "INSTITUTION_UNIT",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "STAFF_ADDITIONAL_INFO",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    staffId = table.Column<long>(nullable: false),
                    PfaNameId = table.Column<long>(nullable: true),
                    PfaStatusId = table.Column<long>(nullable: true),
                    RsaNumber = table.Column<string>(nullable: true),
                    AreaOfSpecializationId = table.Column<long>(nullable: true),
                    SignatureUrl = table.Column<string>(nullable: true),
                    SignaturePin = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STAFF_ADDITIONAL_INFO", x => x.Id);
                    table.ForeignKey(
                        name: "FK_STAFF_ADDITIONAL_INFO_AREA_OF_SPECIALIZATION_AreaOfSpecializationId",
                        column: x => x.AreaOfSpecializationId,
                        principalTable: "AREA_OF_SPECIALIZATION",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_STAFF_ADDITIONAL_INFO_PFA_NAME_PfaNameId",
                        column: x => x.PfaNameId,
                        principalTable: "PFA_NAME",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_STAFF_ADDITIONAL_INFO_PFA_STATUS_PfaStatusId",
                        column: x => x.PfaStatusId,
                        principalTable: "PFA_STATUS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MENU",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Route = table.Column<string>(nullable: true),
                    RoleId = table.Column<long>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ParentMenuId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MENU", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MENU_PARENT_MENU_ParentMenuId",
                        column: x => x.ParentMenuId,
                        principalTable: "PARENT_MENU",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MENU_ROLE_RoleId",
                        column: x => x.RoleId,
                        principalTable: "ROLE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SALARY_GRADE",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalaryGradeCategoryId = table.Column<long>(nullable: false),
                    SalaryStepId = table.Column<long>(nullable: false),
                    SalaryLevelId = table.Column<long>(nullable: false),
                    Amount = table.Column<decimal>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SALARY_GRADE", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SALARY_GRADE_SALARY_GRADE_CATEGORY_SalaryGradeCategoryId",
                        column: x => x.SalaryGradeCategoryId,
                        principalTable: "SALARY_GRADE_CATEGORY",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SALARY_GRADE_SALARY_LEVEL_SalaryLevelId",
                        column: x => x.SalaryLevelId,
                        principalTable: "SALARY_LEVEL",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SALARY_GRADE_SALARY_STEP_SalaryStepId",
                        column: x => x.SalaryStepId,
                        principalTable: "SALARY_STEP",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LGA",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StateId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LGA", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LGA_STATE_StateId",
                        column: x => x.StateId,
                        principalTable: "STATE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BROADCAST",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subject = table.Column<string>(nullable: true),
                    Details = table.Column<string>(nullable: true),
                    AttachmentUrl = table.Column<string>(nullable: true),
                    UnitId = table.Column<long>(nullable: true),
                    InstitutionUnitId = table.Column<long>(nullable: true),
                    RankId = table.Column<long>(nullable: true),
                    InstitutionRankId = table.Column<long>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BROADCAST", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BROADCAST_INSTITUTION_RANK_InstitutionRankId",
                        column: x => x.InstitutionRankId,
                        principalTable: "INSTITUTION_RANK",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BROADCAST_INSTITUTION_UNIT_InstitutionUnitId",
                        column: x => x.InstitutionUnitId,
                        principalTable: "INSTITUTION_UNIT",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "INSTITUTION_CHART_DETAIL",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitId = table.Column<long>(nullable: true),
                    RankId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INSTITUTION_CHART_DETAIL", x => x.Id);
                    table.ForeignKey(
                        name: "FK_INSTITUTION_CHART_DETAIL_INSTITUTION_RANK_RankId",
                        column: x => x.RankId,
                        principalTable: "INSTITUTION_RANK",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_INSTITUTION_CHART_DETAIL_INSTITUTION_CHART_UnitId",
                        column: x => x.UnitId,
                        principalTable: "INSTITUTION_CHART",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LEAVE_ASSIGNMENT",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LeaveId = table.Column<long>(nullable: false),
                    RankId = table.Column<long>(nullable: false),
                    Duration = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LEAVE_ASSIGNMENT", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LEAVE_ASSIGNMENT_LEAVE_LeaveId",
                        column: x => x.LeaveId,
                        principalTable: "LEAVE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LEAVE_ASSIGNMENT_INSTITUTION_RANK_RankId",
                        column: x => x.RankId,
                        principalTable: "INSTITUTION_RANK",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LEAVE_TYPE_RANK",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RankId = table.Column<long>(nullable: false),
                    LeaveTypeId = table.Column<long>(nullable: false),
                    Duration = table.Column<int>(nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LEAVE_TYPE_RANK", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LEAVE_TYPE_RANK_LEAVE_TYPE_LeaveTypeId",
                        column: x => x.LeaveTypeId,
                        principalTable: "LEAVE_TYPE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LEAVE_TYPE_RANK_INSTITUTION_RANK_RankId",
                        column: x => x.RankId,
                        principalTable: "INSTITUTION_RANK",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TRAINING_TYPE_ASSIGNMENT",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainingId = table.Column<long>(nullable: true),
                    LeaveId = table.Column<long>(nullable: false),
                    RankId = table.Column<long>(nullable: false),
                    Duration = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TRAINING_TYPE_ASSIGNMENT", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TRAINING_TYPE_ASSIGNMENT_INSTITUTION_RANK_RankId",
                        column: x => x.RankId,
                        principalTable: "INSTITUTION_RANK",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TRAINING_TYPE_ASSIGNMENT_TRAINING_TYPE_TrainingId",
                        column: x => x.TrainingId,
                        principalTable: "TRAINING_TYPE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GRADE_BENEFIT",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalaryGradeId = table.Column<long>(nullable: false),
                    SalaryTypeId = table.Column<long>(nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GRADE_BENEFIT", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GRADE_BENEFIT_SALARY_GRADE_SalaryGradeId",
                        column: x => x.SalaryGradeId,
                        principalTable: "SALARY_GRADE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GRADE_BENEFIT_SALARY_TYPE_SalaryTypeId",
                        column: x => x.SalaryTypeId,
                        principalTable: "SALARY_TYPE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PERSON",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    Firstname = table.Column<string>(nullable: true),
                    Othername = table.Column<string>(nullable: true),
                    BirthDay = table.Column<DateTime>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    StateId = table.Column<long>(nullable: true),
                    LGAId = table.Column<long>(nullable: true),
                    MaritalStatusId = table.Column<long>(nullable: true),
                    ReligionId = table.Column<long>(nullable: true),
                    GenderId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PERSON", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PERSON_GENDER_GenderId",
                        column: x => x.GenderId,
                        principalTable: "GENDER",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PERSON_LGA_LGAId",
                        column: x => x.LGAId,
                        principalTable: "LGA",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PERSON_MARITAL_STATUS_MaritalStatusId",
                        column: x => x.MaritalStatusId,
                        principalTable: "MARITAL_STATUS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PERSON_RELIGION_ReligionId",
                        column: x => x.ReligionId,
                        principalTable: "RELIGION",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PERSON_STATE_StateId",
                        column: x => x.StateId,
                        principalTable: "STATE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FOREIGN_VISITATION",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<long>(nullable: false),
                    AddressSecond = table.Column<string>(maxLength: 200, nullable: true),
                    PassportNumber = table.Column<string>(maxLength: 100, nullable: true),
                    ProgrammeType = table.Column<string>(maxLength: 100, nullable: true),
                    AcademicYear = table.Column<string>(maxLength: 20, nullable: true),
                    FacultyId = table.Column<long>(nullable: true),
                    InstitutionDepartmentId = table.Column<long>(nullable: true),
                    DurationOfStay = table.Column<int>(nullable: true),
                    DegreeAwardDate = table.Column<DateTime>(nullable: true),
                    AwardingInstitution = table.Column<string>(maxLength: 100, nullable: true),
                    CurrentInstitutionDepartmentId = table.Column<long>(nullable: true),
                    CurrentInstitutionFacultyId = table.Column<long>(nullable: true),
                    VisitationTypeId = table.Column<long>(nullable: false),
                    CurrentYearOfStudy = table.Column<int>(nullable: true),
                    CurrentExpectedQualificationYear = table.Column<string>(maxLength: 50, nullable: true),
                    Reason = table.Column<string>(nullable: true),
                    SponsorshipType = table.Column<string>(maxLength: 50, nullable: true),
                    SponsorshipOrganization = table.Column<string>(maxLength: 100, nullable: true),
                    ApplicationNumber = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    isApproved = table.Column<bool>(nullable: true),
                    Active = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FOREIGN_VISITATION", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FOREIGN_VISITATION_INSTITUTION_DEPARTMENT_CurrentInstitutionDepartmentId",
                        column: x => x.CurrentInstitutionDepartmentId,
                        principalTable: "INSTITUTION_DEPARTMENT",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FOREIGN_VISITATION_FACULTY_CurrentInstitutionFacultyId",
                        column: x => x.CurrentInstitutionFacultyId,
                        principalTable: "FACULTY",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FOREIGN_VISITATION_FACULTY_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "FACULTY",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FOREIGN_VISITATION_INSTITUTION_DEPARTMENT_InstitutionDepartmentId",
                        column: x => x.InstitutionDepartmentId,
                        principalTable: "INSTITUTION_DEPARTMENT",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FOREIGN_VISITATION_PERSON_PersonId",
                        column: x => x.PersonId,
                        principalTable: "PERSON",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FOREIGN_VISITATION_VISITATION_TYPE_VisitationTypeId",
                        column: x => x.VisitationTypeId,
                        principalTable: "VISITATION_TYPE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PERSON_CERTIFICATION",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Issuer = table.Column<string>(nullable: true),
                    Year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PERSON_CERTIFICATION", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PERSON_CERTIFICATION_PERSON_PersonId",
                        column: x => x.PersonId,
                        principalTable: "PERSON",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PERSON_EDUCATION",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<long>(nullable: false),
                    Institution = table.Column<string>(nullable: true),
                    Course = table.Column<string>(nullable: true),
                    Year = table.Column<int>(nullable: false),
                    EducationalQualificationId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PERSON_EDUCATION", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PERSON_EDUCATION_EDUCATIONAL_QUALIFICATION_EducationalQualificationId",
                        column: x => x.EducationalQualificationId,
                        principalTable: "EDUCATIONAL_QUALIFICATION",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PERSON_EDUCATION_PERSON_PersonId",
                        column: x => x.PersonId,
                        principalTable: "PERSON",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PERSON_EXPERIENCE",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<long>(nullable: false),
                    Organisation = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PERSON_EXPERIENCE", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PERSON_EXPERIENCE_PERSON_PersonId",
                        column: x => x.PersonId,
                        principalTable: "PERSON",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PERSON_JOURNAL",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Publisher = table.Column<string>(nullable: true),
                    Year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PERSON_JOURNAL", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PERSON_JOURNAL_PERSON_PersonId",
                        column: x => x.PersonId,
                        principalTable: "PERSON",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PERSON_NEXT_OF_KIN",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<long>(nullable: false),
                    Fullname = table.Column<string>(maxLength: 100, nullable: true),
                    Email = table.Column<string>(maxLength: 30, nullable: true),
                    Phone = table.Column<string>(maxLength: 30, nullable: true),
                    Address = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PERSON_NEXT_OF_KIN", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PERSON_NEXT_OF_KIN_PERSON_PersonId",
                        column: x => x.PersonId,
                        principalTable: "PERSON",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PERSON_PROFESSIONAL_BODY",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Comments = table.Column<string>(nullable: true),
                    Year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PERSON_PROFESSIONAL_BODY", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PERSON_PROFESSIONAL_BODY_PERSON_PersonId",
                        column: x => x.PersonId,
                        principalTable: "PERSON",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PERSON_REFEREE",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Organisation = table.Column<string>(nullable: true),
                    Designation = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PERSON_REFEREE", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PERSON_REFEREE_PERSON_PersonId",
                        column: x => x.PersonId,
                        principalTable: "PERSON",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PERSON_RESEARCH_GRANT",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Topic = table.Column<string>(nullable: true),
                    Year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PERSON_RESEARCH_GRANT", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PERSON_RESEARCH_GRANT_PERSON_PersonId",
                        column: x => x.PersonId,
                        principalTable: "PERSON",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "STAFF",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<long>(nullable: false),
                    StaffNumber = table.Column<string>(nullable: true),
                    GeneratedStaffNumber = table.Column<string>(nullable: true),
                    RankId = table.Column<long>(nullable: true),
                    DepartmentId = table.Column<long>(nullable: true),
                    AppointmentId = table.Column<long>(nullable: true),
                    StaffTypeId = table.Column<long>(nullable: true),
                    CategoryId = table.Column<long>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    RetirementReason = table.Column<string>(nullable: true),
                    AppointmentIsConfirmed = table.Column<bool>(nullable: false),
                    AppointmentDate = table.Column<DateTime>(nullable: true),
                    DateOfRegularization = table.Column<DateTime>(nullable: true),
                    DateOfConfirmation = table.Column<DateTime>(nullable: true),
                    AppointmentTypeId = table.Column<long>(nullable: true),
                    BiometricId = table.Column<string>(nullable: true),
                    IsDisengaged = table.Column<bool>(nullable: false),
                    IsRetired = table.Column<bool>(nullable: false),
                    DateOfLastPromotion = table.Column<DateTime>(nullable: true),
                    DateOfEmployment = table.Column<DateTime>(nullable: true),
                    DateOfAssumption = table.Column<DateTime>(nullable: true),
                    Guid = table.Column<string>(nullable: true),
                    DateOfLastReset = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STAFF", x => x.Id);
                    table.ForeignKey(
                        name: "FK_STAFF_INSTITUTION_APPOINTMENT_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "INSTITUTION_APPOINTMENT",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_STAFF_APPOINTMENT_TYPE_AppointmentTypeId",
                        column: x => x.AppointmentTypeId,
                        principalTable: "APPOINTMENT_TYPE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_STAFF_INSTITUTION_STAFF_CATEGORY_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "INSTITUTION_STAFF_CATEGORY",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_STAFF_INSTITUTION_DEPARTMENT_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "INSTITUTION_DEPARTMENT",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_STAFF_PERSON_PersonId",
                        column: x => x.PersonId,
                        principalTable: "PERSON",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_STAFF_INSTITUTION_RANK_RankId",
                        column: x => x.RankId,
                        principalTable: "INSTITUTION_RANK",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_STAFF_INSTITUTION_STAFF_TYPE_StaffTypeId",
                        column: x => x.StaffTypeId,
                        principalTable: "INSTITUTION_STAFF_TYPE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "STAFF_DOCUMENT",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<long>(nullable: false),
                    DocumentTypeId = table.Column<long>(nullable: false),
                    Url = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    DateEntered = table.Column<DateTime>(nullable: false),
                    isVerified = table.Column<bool>(nullable: false),
                    verifiedBy = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STAFF_DOCUMENT", x => x.Id);
                    table.ForeignKey(
                        name: "FK_STAFF_DOCUMENT_DOCUMENT_TYPE_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalTable: "DOCUMENT_TYPE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_STAFF_DOCUMENT_PERSON_PersonId",
                        column: x => x.PersonId,
                        principalTable: "PERSON",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CHANGE_OF_NAME",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestedSurname = table.Column<string>(nullable: true),
                    RequestedFirstname = table.Column<string>(nullable: true),
                    RequestedOthername = table.Column<string>(nullable: true),
                    Attachment = table.Column<string>(nullable: true),
                    DateOfRequest = table.Column<DateTime>(nullable: false),
                    Comments = table.Column<string>(nullable: true),
                    StaffId = table.Column<long>(nullable: false),
                    IsApproved = table.Column<bool>(nullable: false),
                    IsClosed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CHANGE_OF_NAME", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CHANGE_OF_NAME_STAFF_StaffId",
                        column: x => x.StaffId,
                        principalTable: "STAFF",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DEPARTMENT_HEAD",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffId = table.Column<long>(nullable: false),
                    InstitutionDepartmentId = table.Column<long>(nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DEPARTMENT_HEAD", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DEPARTMENT_HEAD_INSTITUTION_DEPARTMENT_InstitutionDepartmentId",
                        column: x => x.InstitutionDepartmentId,
                        principalTable: "INSTITUTION_DEPARTMENT",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DEPARTMENT_HEAD_STAFF_StaffId",
                        column: x => x.StaffId,
                        principalTable: "STAFF",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DEPARTMENT_TRANSFER",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeptFrom = table.Column<string>(nullable: true),
                    DeptTo = table.Column<string>(nullable: true),
                    DateOfRequest = table.Column<DateTime>(nullable: false),
                    Reasons = table.Column<string>(nullable: true),
                    StaffId = table.Column<long>(nullable: false),
                    NewDepartmentId = table.Column<long>(nullable: false),
                    IsApproved = table.Column<bool>(nullable: false),
                    IsClosed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DEPARTMENT_TRANSFER", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DEPARTMENT_TRANSFER_STAFF_StaffId",
                        column: x => x.StaffId,
                        principalTable: "STAFF",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GENERATED_STAFFNUMBER_RECORDS",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffId = table.Column<long>(nullable: false),
                    DefaultDepartmentId = table.Column<long>(nullable: true),
                    DateGenerated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GENERATED_STAFFNUMBER_RECORDS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GENERATED_STAFFNUMBER_RECORDS_INSTITUTION_DEPARTMENT_DefaultDepartmentId",
                        column: x => x.DefaultDepartmentId,
                        principalTable: "INSTITUTION_DEPARTMENT",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GENERATED_STAFFNUMBER_RECORDS_STAFF_StaffId",
                        column: x => x.StaffId,
                        principalTable: "STAFF",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "INSTITUTION_CALENDAR",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    Detail = table.Column<string>(nullable: true),
                    AddedById = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INSTITUTION_CALENDAR", x => x.Id);
                    table.ForeignKey(
                        name: "FK_INSTITUTION_CALENDAR_STAFF_AddedById",
                        column: x => x.AddedById,
                        principalTable: "STAFF",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LEAVE_REQUEST",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Remarks = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    Start = table.Column<DateTime>(nullable: false),
                    End = table.Column<DateTime>(nullable: false),
                    EnteredDate = table.Column<DateTime>(nullable: false),
                    SupportDocumentUrl = table.Column<string>(nullable: true),
                    StaffId = table.Column<long>(nullable: false),
                    LeaveTypeRankId = table.Column<long>(nullable: false),
                    LeaveDaysApplied = table.Column<long>(nullable: false),
                    RemainingLeaveDays = table.Column<long>(nullable: false),
                    IsCompleted = table.Column<bool>(nullable: false),
                    Staus = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LEAVE_REQUEST", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LEAVE_REQUEST_LEAVE_TYPE_RANK_LeaveTypeRankId",
                        column: x => x.LeaveTypeRankId,
                        principalTable: "LEAVE_TYPE_RANK",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LEAVE_REQUEST_STAFF_StaffId",
                        column: x => x.StaffId,
                        principalTable: "STAFF",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SALARY_EXTRA_EARNING",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffId = table.Column<long>(nullable: false),
                    SalaryExtraTypeId = table.Column<long>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    IsDeductible = table.Column<bool>(nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SALARY_EXTRA_EARNING", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SALARY_EXTRA_EARNING_SALARY_EXTRA_TYPE_SalaryExtraTypeId",
                        column: x => x.SalaryExtraTypeId,
                        principalTable: "SALARY_EXTRA_TYPE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SALARY_EXTRA_EARNING_STAFF_StaffId",
                        column: x => x.StaffId,
                        principalTable: "STAFF",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "STAFF_ASSET",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffId = table.Column<long>(nullable: false),
                    AssetId = table.Column<long>(nullable: false),
                    SerialNumber = table.Column<string>(nullable: true),
                    AssetNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STAFF_ASSET", x => x.Id);
                    table.ForeignKey(
                        name: "FK_STAFF_ASSET_ASSET_AssetId",
                        column: x => x.AssetId,
                        principalTable: "ASSET",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_STAFF_ASSET_STAFF_StaffId",
                        column: x => x.StaffId,
                        principalTable: "STAFF",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "STAFF_ATTENDANCE",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffId = table.Column<long>(nullable: false),
                    ClockIn = table.Column<TimeSpan>(nullable: true),
                    ClockOut = table.Column<TimeSpan>(nullable: true),
                    Date = table.Column<DateTime>(nullable: true),
                    Absent = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STAFF_ATTENDANCE", x => x.Id);
                    table.ForeignKey(
                        name: "FK_STAFF_ATTENDANCE_STAFF_StaffId",
                        column: x => x.StaffId,
                        principalTable: "STAFF",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "STAFF_GRADE",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalaryGradeId = table.Column<long>(nullable: false),
                    StaffId = table.Column<long>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    DatePromoted = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STAFF_GRADE", x => x.Id);
                    table.ForeignKey(
                        name: "FK_STAFF_GRADE_SALARY_GRADE_SalaryGradeId",
                        column: x => x.SalaryGradeId,
                        principalTable: "SALARY_GRADE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_STAFF_GRADE_STAFF_StaffId",
                        column: x => x.StaffId,
                        principalTable: "STAFF",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "STAFF_NOMINAL_ROLL",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffId = table.Column<long>(nullable: false),
                    Month = table.Column<long>(nullable: false),
                    Year = table.Column<long>(nullable: false),
                    IsCleared = table.Column<bool>(nullable: false),
                    Comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STAFF_NOMINAL_ROLL", x => x.Id);
                    table.ForeignKey(
                        name: "FK_STAFF_NOMINAL_ROLL_STAFF_StaffId",
                        column: x => x.StaffId,
                        principalTable: "STAFF",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "STAFF_RETIREMENT",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffId = table.Column<long>(nullable: false),
                    IsRetired = table.Column<bool>(nullable: false),
                    DateOfRetirement = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STAFF_RETIREMENT", x => x.Id);
                    table.ForeignKey(
                        name: "FK_STAFF_RETIREMENT_STAFF_StaffId",
                        column: x => x.StaffId,
                        principalTable: "STAFF",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "STAFF_SALARY_CATEGORY_REFERENCE",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffId = table.Column<long>(nullable: false),
                    SalaryGradeCategoryId = table.Column<long>(nullable: true),
                    SalaryLevelId = table.Column<long>(nullable: true),
                    SalaryStepId = table.Column<long>(nullable: true),
                    DatePromoted = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STAFF_SALARY_CATEGORY_REFERENCE", x => x.Id);
                    table.ForeignKey(
                        name: "FK_STAFF_SALARY_CATEGORY_REFERENCE_SALARY_GRADE_CATEGORY_SalaryGradeCategoryId",
                        column: x => x.SalaryGradeCategoryId,
                        principalTable: "SALARY_GRADE_CATEGORY",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_STAFF_SALARY_CATEGORY_REFERENCE_SALARY_LEVEL_SalaryLevelId",
                        column: x => x.SalaryLevelId,
                        principalTable: "SALARY_LEVEL",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_STAFF_SALARY_CATEGORY_REFERENCE_SALARY_STEP_SalaryStepId",
                        column: x => x.SalaryStepId,
                        principalTable: "SALARY_STEP",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_STAFF_SALARY_CATEGORY_REFERENCE_STAFF_StaffId",
                        column: x => x.StaffId,
                        principalTable: "STAFF",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "STAFF_STATUS",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusName = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    StaffId = table.Column<long>(nullable: false),
                    DateLogged = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STAFF_STATUS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_STAFF_STATUS_STAFF_StaffId",
                        column: x => x.StaffId,
                        principalTable: "STAFF",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "STAFF_TRAINING_REQUEST",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainingTypeId = table.Column<long>(nullable: false),
                    StaffId = table.Column<long>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Reason = table.Column<string>(nullable: true),
                    AttachmentUrl = table.Column<string>(nullable: true),
                    Approved = table.Column<bool>(nullable: false),
                    ApprovedDate = table.Column<DateTime>(nullable: true),
                    ApprovedById = table.Column<long>(nullable: true),
                    Remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STAFF_TRAINING_REQUEST", x => x.Id);
                    table.ForeignKey(
                        name: "FK_STAFF_TRAINING_REQUEST_STAFF_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "STAFF",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_STAFF_TRAINING_REQUEST_STAFF_StaffId",
                        column: x => x.StaffId,
                        principalTable: "STAFF",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_STAFF_TRAINING_REQUEST_TRAINING_TYPE_TrainingTypeId",
                        column: x => x.TrainingTypeId,
                        principalTable: "TRAINING_TYPE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "USER",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true),
                    StaffId = table.Column<long>(nullable: false),
                    RoleId = table.Column<long>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Reason = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER", x => x.Id);
                    table.ForeignKey(
                        name: "FK_USER_ROLE_RoleId",
                        column: x => x.RoleId,
                        principalTable: "ROLE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_USER_STAFF_StaffId",
                        column: x => x.StaffId,
                        principalTable: "STAFF",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "INSTITUTION_CALENDAR_DETAIL",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CalendarId = table.Column<long>(nullable: true),
                    RankId = table.Column<long>(nullable: false),
                    DepartmentId = table.Column<long>(nullable: false),
                    AppointmentId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INSTITUTION_CALENDAR_DETAIL", x => x.Id);
                    table.ForeignKey(
                        name: "FK_INSTITUTION_CALENDAR_DETAIL_INSTITUTION_APPOINTMENT_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "INSTITUTION_APPOINTMENT",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_INSTITUTION_CALENDAR_DETAIL_INSTITUTION_CALENDAR_CalendarId",
                        column: x => x.CalendarId,
                        principalTable: "INSTITUTION_CALENDAR",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_INSTITUTION_CALENDAR_DETAIL_INSTITUTION_DEPARTMENT_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "INSTITUTION_DEPARTMENT",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_INSTITUTION_CALENDAR_DETAIL_INSTITUTION_RANK_RankId",
                        column: x => x.RankId,
                        principalTable: "INSTITUTION_RANK",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LeaveResponse",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LeaveRequestId = table.Column<long>(nullable: false),
                    Remarks = table.Column<string>(nullable: true),
                    DateActed = table.Column<DateTime>(nullable: true),
                    IsApproved = table.Column<bool>(nullable: false),
                    IsActed = table.Column<bool>(nullable: false),
                    IsClosed = table.Column<bool>(nullable: false),
                    ActiveDesk = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveResponse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaveResponse_LEAVE_REQUEST_LeaveRequestId",
                        column: x => x.LeaveRequestId,
                        principalTable: "LEAVE_REQUEST",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JOB_VACANCY",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    JobTypeId = table.Column<long>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JOB_VACANCY", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JOB_VACANCY_JOB_TYPE_JobTypeId",
                        column: x => x.JobTypeId,
                        principalTable: "JOB_TYPE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JOB_VACANCY_USER_UserId",
                        column: x => x.UserId,
                        principalTable: "USER",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MAILING",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 500, nullable: true),
                    Body = table.Column<string>(nullable: true),
                    AttachmentUrl = table.Column<string>(nullable: true),
                    ReferenceNuber = table.Column<string>(nullable: true),
                    DateEntered = table.Column<DateTime>(nullable: false),
                    IsAcknowledged = table.Column<bool>(nullable: false),
                    IsExternal = table.Column<bool>(nullable: false),
                    IsClosed = table.Column<bool>(nullable: false),
                    IsRejected = table.Column<bool>(nullable: false),
                    OriginatorId = table.Column<long>(nullable: true),
                    FileTypeId = table.Column<long>(nullable: true),
                    AcknowledgedUserId = table.Column<long>(nullable: true),
                    OriginatorSignatureUrl = table.Column<string>(nullable: true),
                    OriginatorRoleDepartment = table.Column<string>(maxLength: 200, nullable: true),
                    AcknowledgedRoleOffice = table.Column<string>(nullable: true),
                    AcknowledgedUserRoleDepartment = table.Column<string>(nullable: true),
                    OriginatorEmail = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    IsConfidential = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MAILING", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MAILING_USER_AcknowledgedUserId",
                        column: x => x.AcknowledgedUserId,
                        principalTable: "USER",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MAILING_MailArchiveFileType_FileTypeId",
                        column: x => x.FileTypeId,
                        principalTable: "MailArchiveFileType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MAILING_USER_OriginatorId",
                        column: x => x.OriginatorId,
                        principalTable: "USER",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "STAFF_LEAVE_RECORD",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffId = table.Column<long>(nullable: false),
                    LeaveResponseId = table.Column<long>(nullable: false),
                    Progress = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STAFF_LEAVE_RECORD", x => x.Id);
                    table.ForeignKey(
                        name: "FK_STAFF_LEAVE_RECORD_LeaveResponse_LeaveResponseId",
                        column: x => x.LeaveResponseId,
                        principalTable: "LeaveResponse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "APPLICATION_SECTION_WEIGHT",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobVacancyId = table.Column<long>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ApplicationSectionHeaderId = table.Column<long>(nullable: false),
                    Weight = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APPLICATION_SECTION_WEIGHT", x => x.Id);
                    table.ForeignKey(
                        name: "FK_APPLICATION_SECTION_WEIGHT_APPLICATION_SECTION_HEADER_ApplicationSectionHeaderId",
                        column: x => x.ApplicationSectionHeaderId,
                        principalTable: "APPLICATION_SECTION_HEADER",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_APPLICATION_SECTION_WEIGHT_JOB_VACANCY_JobVacancyId",
                        column: x => x.JobVacancyId,
                        principalTable: "JOB_VACANCY",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JOB_RECIPIENTS",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Firstname = table.Column<string>(nullable: true),
                    Lastname = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Guid = table.Column<string>(nullable: true),
                    JobVacancyId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JOB_RECIPIENTS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JOB_RECIPIENTS_JOB_VACANCY_JobVacancyId",
                        column: x => x.JobVacancyId,
                        principalTable: "JOB_VACANCY",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_APPLICATION_SECTION_WEIGHT_ApplicationSectionHeaderId",
                table: "APPLICATION_SECTION_WEIGHT",
                column: "ApplicationSectionHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_APPLICATION_SECTION_WEIGHT_JobVacancyId",
                table: "APPLICATION_SECTION_WEIGHT",
                column: "JobVacancyId");

            migrationBuilder.CreateIndex(
                name: "IX_ASSET_AssetTypeId",
                table: "ASSET",
                column: "AssetTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_BROADCAST_InstitutionRankId",
                table: "BROADCAST",
                column: "InstitutionRankId");

            migrationBuilder.CreateIndex(
                name: "IX_BROADCAST_InstitutionUnitId",
                table: "BROADCAST",
                column: "InstitutionUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_CHANGE_OF_NAME_StaffId",
                table: "CHANGE_OF_NAME",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_DEPARTMENT_HEAD_InstitutionDepartmentId",
                table: "DEPARTMENT_HEAD",
                column: "InstitutionDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_DEPARTMENT_HEAD_StaffId",
                table: "DEPARTMENT_HEAD",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_DEPARTMENT_TRANSFER_StaffId",
                table: "DEPARTMENT_TRANSFER",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_FOREIGN_VISITATION_CurrentInstitutionDepartmentId",
                table: "FOREIGN_VISITATION",
                column: "CurrentInstitutionDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_FOREIGN_VISITATION_CurrentInstitutionFacultyId",
                table: "FOREIGN_VISITATION",
                column: "CurrentInstitutionFacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_FOREIGN_VISITATION_FacultyId",
                table: "FOREIGN_VISITATION",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_FOREIGN_VISITATION_InstitutionDepartmentId",
                table: "FOREIGN_VISITATION",
                column: "InstitutionDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_FOREIGN_VISITATION_PersonId",
                table: "FOREIGN_VISITATION",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_FOREIGN_VISITATION_VisitationTypeId",
                table: "FOREIGN_VISITATION",
                column: "VisitationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_GENERATED_STAFFNUMBER_RECORDS_DefaultDepartmentId",
                table: "GENERATED_STAFFNUMBER_RECORDS",
                column: "DefaultDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_GENERATED_STAFFNUMBER_RECORDS_StaffId",
                table: "GENERATED_STAFFNUMBER_RECORDS",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_GRADE_BENEFIT_SalaryGradeId",
                table: "GRADE_BENEFIT",
                column: "SalaryGradeId");

            migrationBuilder.CreateIndex(
                name: "IX_GRADE_BENEFIT_SalaryTypeId",
                table: "GRADE_BENEFIT",
                column: "SalaryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_INSTITUTION_CALENDAR_AddedById",
                table: "INSTITUTION_CALENDAR",
                column: "AddedById");

            migrationBuilder.CreateIndex(
                name: "IX_INSTITUTION_CALENDAR_DETAIL_AppointmentId",
                table: "INSTITUTION_CALENDAR_DETAIL",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_INSTITUTION_CALENDAR_DETAIL_CalendarId",
                table: "INSTITUTION_CALENDAR_DETAIL",
                column: "CalendarId");

            migrationBuilder.CreateIndex(
                name: "IX_INSTITUTION_CALENDAR_DETAIL_DepartmentId",
                table: "INSTITUTION_CALENDAR_DETAIL",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_INSTITUTION_CALENDAR_DETAIL_RankId",
                table: "INSTITUTION_CALENDAR_DETAIL",
                column: "RankId");

            migrationBuilder.CreateIndex(
                name: "IX_INSTITUTION_CHART_UnitId",
                table: "INSTITUTION_CHART",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_INSTITUTION_CHART_DETAIL_RankId",
                table: "INSTITUTION_CHART_DETAIL",
                column: "RankId");

            migrationBuilder.CreateIndex(
                name: "IX_INSTITUTION_CHART_DETAIL_UnitId",
                table: "INSTITUTION_CHART_DETAIL",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_INSTITUTION_DEPARTMENT_FacultyId",
                table: "INSTITUTION_DEPARTMENT",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_INSTITUTION_RANK_InstitutionUnitId",
                table: "INSTITUTION_RANK",
                column: "InstitutionUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_JOB_RECIPIENTS_JobVacancyId",
                table: "JOB_RECIPIENTS",
                column: "JobVacancyId");

            migrationBuilder.CreateIndex(
                name: "IX_JOB_VACANCY_JobTypeId",
                table: "JOB_VACANCY",
                column: "JobTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_JOB_VACANCY_UserId",
                table: "JOB_VACANCY",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LEAVE_StaffTypeId",
                table: "LEAVE",
                column: "StaffTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LEAVE_ASSIGNMENT_LeaveId",
                table: "LEAVE_ASSIGNMENT",
                column: "LeaveId");

            migrationBuilder.CreateIndex(
                name: "IX_LEAVE_ASSIGNMENT_RankId",
                table: "LEAVE_ASSIGNMENT",
                column: "RankId");

            migrationBuilder.CreateIndex(
                name: "IX_LEAVE_REQUEST_LeaveTypeRankId",
                table: "LEAVE_REQUEST",
                column: "LeaveTypeRankId");

            migrationBuilder.CreateIndex(
                name: "IX_LEAVE_REQUEST_StaffId",
                table: "LEAVE_REQUEST",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_LEAVE_TYPE_RANK_LeaveTypeId",
                table: "LEAVE_TYPE_RANK",
                column: "LeaveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LEAVE_TYPE_RANK_RankId",
                table: "LEAVE_TYPE_RANK",
                column: "RankId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveResponse_LeaveRequestId",
                table: "LeaveResponse",
                column: "LeaveRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_LGA_StateId",
                table: "LGA",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_MAILING_AcknowledgedUserId",
                table: "MAILING",
                column: "AcknowledgedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MAILING_FileTypeId",
                table: "MAILING",
                column: "FileTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MAILING_OriginatorId",
                table: "MAILING",
                column: "OriginatorId");

            migrationBuilder.CreateIndex(
                name: "IX_MENU_ParentMenuId",
                table: "MENU",
                column: "ParentMenuId");

            migrationBuilder.CreateIndex(
                name: "IX_MENU_RoleId",
                table: "MENU",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_PERSON_GenderId",
                table: "PERSON",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_PERSON_LGAId",
                table: "PERSON",
                column: "LGAId");

            migrationBuilder.CreateIndex(
                name: "IX_PERSON_MaritalStatusId",
                table: "PERSON",
                column: "MaritalStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PERSON_ReligionId",
                table: "PERSON",
                column: "ReligionId");

            migrationBuilder.CreateIndex(
                name: "IX_PERSON_StateId",
                table: "PERSON",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_PERSON_CERTIFICATION_PersonId",
                table: "PERSON_CERTIFICATION",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PERSON_EDUCATION_EducationalQualificationId",
                table: "PERSON_EDUCATION",
                column: "EducationalQualificationId");

            migrationBuilder.CreateIndex(
                name: "IX_PERSON_EDUCATION_PersonId",
                table: "PERSON_EDUCATION",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PERSON_EXPERIENCE_PersonId",
                table: "PERSON_EXPERIENCE",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PERSON_JOURNAL_PersonId",
                table: "PERSON_JOURNAL",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PERSON_NEXT_OF_KIN_PersonId",
                table: "PERSON_NEXT_OF_KIN",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PERSON_PROFESSIONAL_BODY_PersonId",
                table: "PERSON_PROFESSIONAL_BODY",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PERSON_REFEREE_PersonId",
                table: "PERSON_REFEREE",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PERSON_RESEARCH_GRANT_PersonId",
                table: "PERSON_RESEARCH_GRANT",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_SALARY_EXTRA_EARNING_SalaryExtraTypeId",
                table: "SALARY_EXTRA_EARNING",
                column: "SalaryExtraTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SALARY_EXTRA_EARNING_StaffId",
                table: "SALARY_EXTRA_EARNING",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_SALARY_GRADE_SalaryGradeCategoryId",
                table: "SALARY_GRADE",
                column: "SalaryGradeCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SALARY_GRADE_SalaryLevelId",
                table: "SALARY_GRADE",
                column: "SalaryLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_SALARY_GRADE_SalaryStepId",
                table: "SALARY_GRADE",
                column: "SalaryStepId");

            migrationBuilder.CreateIndex(
                name: "IX_STAFF_AppointmentId",
                table: "STAFF",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_STAFF_AppointmentTypeId",
                table: "STAFF",
                column: "AppointmentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_STAFF_CategoryId",
                table: "STAFF",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_STAFF_DepartmentId",
                table: "STAFF",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_STAFF_PersonId",
                table: "STAFF",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_STAFF_RankId",
                table: "STAFF",
                column: "RankId");

            migrationBuilder.CreateIndex(
                name: "IX_STAFF_StaffTypeId",
                table: "STAFF",
                column: "StaffTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_STAFF_ADDITIONAL_INFO_AreaOfSpecializationId",
                table: "STAFF_ADDITIONAL_INFO",
                column: "AreaOfSpecializationId");

            migrationBuilder.CreateIndex(
                name: "IX_STAFF_ADDITIONAL_INFO_PfaNameId",
                table: "STAFF_ADDITIONAL_INFO",
                column: "PfaNameId");

            migrationBuilder.CreateIndex(
                name: "IX_STAFF_ADDITIONAL_INFO_PfaStatusId",
                table: "STAFF_ADDITIONAL_INFO",
                column: "PfaStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_STAFF_ASSET_AssetId",
                table: "STAFF_ASSET",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_STAFF_ASSET_StaffId",
                table: "STAFF_ASSET",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_STAFF_ATTENDANCE_StaffId",
                table: "STAFF_ATTENDANCE",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_STAFF_DOCUMENT_DocumentTypeId",
                table: "STAFF_DOCUMENT",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_STAFF_DOCUMENT_PersonId",
                table: "STAFF_DOCUMENT",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_STAFF_GRADE_SalaryGradeId",
                table: "STAFF_GRADE",
                column: "SalaryGradeId");

            migrationBuilder.CreateIndex(
                name: "IX_STAFF_GRADE_StaffId",
                table: "STAFF_GRADE",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_STAFF_LEAVE_RECORD_LeaveResponseId",
                table: "STAFF_LEAVE_RECORD",
                column: "LeaveResponseId");

            migrationBuilder.CreateIndex(
                name: "IX_STAFF_NOMINAL_ROLL_StaffId",
                table: "STAFF_NOMINAL_ROLL",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_STAFF_RETIREMENT_StaffId",
                table: "STAFF_RETIREMENT",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_STAFF_SALARY_CATEGORY_REFERENCE_SalaryGradeCategoryId",
                table: "STAFF_SALARY_CATEGORY_REFERENCE",
                column: "SalaryGradeCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_STAFF_SALARY_CATEGORY_REFERENCE_SalaryLevelId",
                table: "STAFF_SALARY_CATEGORY_REFERENCE",
                column: "SalaryLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_STAFF_SALARY_CATEGORY_REFERENCE_SalaryStepId",
                table: "STAFF_SALARY_CATEGORY_REFERENCE",
                column: "SalaryStepId");

            migrationBuilder.CreateIndex(
                name: "IX_STAFF_SALARY_CATEGORY_REFERENCE_StaffId",
                table: "STAFF_SALARY_CATEGORY_REFERENCE",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_STAFF_STATUS_StaffId",
                table: "STAFF_STATUS",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_STAFF_TRAINING_REQUEST_ApprovedById",
                table: "STAFF_TRAINING_REQUEST",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_STAFF_TRAINING_REQUEST_StaffId",
                table: "STAFF_TRAINING_REQUEST",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_STAFF_TRAINING_REQUEST_TrainingTypeId",
                table: "STAFF_TRAINING_REQUEST",
                column: "TrainingTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TRAINING_TYPE_StaffTypeId",
                table: "TRAINING_TYPE",
                column: "StaffTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TRAINING_TYPE_ASSIGNMENT_RankId",
                table: "TRAINING_TYPE_ASSIGNMENT",
                column: "RankId");

            migrationBuilder.CreateIndex(
                name: "IX_TRAINING_TYPE_ASSIGNMENT_TrainingId",
                table: "TRAINING_TYPE_ASSIGNMENT",
                column: "TrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_USER_RoleId",
                table: "USER",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_USER_StaffId",
                table: "USER",
                column: "StaffId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "APPLICATION_SECTION_WEIGHT");

            migrationBuilder.DropTable(
                name: "BROADCAST");

            migrationBuilder.DropTable(
                name: "CHANGE_OF_NAME");

            migrationBuilder.DropTable(
                name: "COUNTRY");

            migrationBuilder.DropTable(
                name: "DEPARTMENT_HEAD");

            migrationBuilder.DropTable(
                name: "DEPARTMENT_TRANSFER");

            migrationBuilder.DropTable(
                name: "EVENT");

            migrationBuilder.DropTable(
                name: "FAILED_STAFF_UPLOADS");

            migrationBuilder.DropTable(
                name: "FOREIGN_VISITATION");

            migrationBuilder.DropTable(
                name: "GENERATED_STAFFNUMBER_RECORDS");

            migrationBuilder.DropTable(
                name: "GRADE_BENEFIT");

            migrationBuilder.DropTable(
                name: "HOLIDAYS");

            migrationBuilder.DropTable(
                name: "INSTITUTION_CALENDAR_DETAIL");

            migrationBuilder.DropTable(
                name: "INSTITUTION_CHART_DETAIL");

            migrationBuilder.DropTable(
                name: "JOB_RECIPIENTS");

            migrationBuilder.DropTable(
                name: "LEAVE_ASSIGNMENT");

            migrationBuilder.DropTable(
                name: "MAILING");

            migrationBuilder.DropTable(
                name: "MENU");

            migrationBuilder.DropTable(
                name: "PERSON_CERTIFICATION");

            migrationBuilder.DropTable(
                name: "PERSON_EDUCATION");

            migrationBuilder.DropTable(
                name: "PERSON_EXPERIENCE");

            migrationBuilder.DropTable(
                name: "PERSON_JOURNAL");

            migrationBuilder.DropTable(
                name: "PERSON_NEXT_OF_KIN");

            migrationBuilder.DropTable(
                name: "PERSON_PROFESSIONAL_BODY");

            migrationBuilder.DropTable(
                name: "PERSON_REFEREE");

            migrationBuilder.DropTable(
                name: "PERSON_RESEARCH_GRANT");

            migrationBuilder.DropTable(
                name: "SALARY_EXTRA_EARNING");

            migrationBuilder.DropTable(
                name: "SETTINGS");

            migrationBuilder.DropTable(
                name: "STAFF_ADDITIONAL_INFO");

            migrationBuilder.DropTable(
                name: "STAFF_ASSET");

            migrationBuilder.DropTable(
                name: "STAFF_ATTENDANCE");

            migrationBuilder.DropTable(
                name: "STAFF_DB_COMMIT_DETAILS");

            migrationBuilder.DropTable(
                name: "STAFF_DOCUMENT");

            migrationBuilder.DropTable(
                name: "STAFF_GRADE");

            migrationBuilder.DropTable(
                name: "STAFF_LEAVE_RECORD");

            migrationBuilder.DropTable(
                name: "STAFF_NOMINAL_ROLL");

            migrationBuilder.DropTable(
                name: "STAFF_RETIREMENT");

            migrationBuilder.DropTable(
                name: "STAFF_SALARY_CATEGORY_REFERENCE");

            migrationBuilder.DropTable(
                name: "STAFF_STATUS");

            migrationBuilder.DropTable(
                name: "STAFF_TRAINING_REQUEST");

            migrationBuilder.DropTable(
                name: "TRAINING_TYPE_ASSIGNMENT");

            migrationBuilder.DropTable(
                name: "APPLICATION_SECTION_HEADER");

            migrationBuilder.DropTable(
                name: "VISITATION_TYPE");

            migrationBuilder.DropTable(
                name: "SALARY_TYPE");

            migrationBuilder.DropTable(
                name: "INSTITUTION_CALENDAR");

            migrationBuilder.DropTable(
                name: "INSTITUTION_CHART");

            migrationBuilder.DropTable(
                name: "JOB_VACANCY");

            migrationBuilder.DropTable(
                name: "LEAVE");

            migrationBuilder.DropTable(
                name: "MailArchiveFileType");

            migrationBuilder.DropTable(
                name: "PARENT_MENU");

            migrationBuilder.DropTable(
                name: "EDUCATIONAL_QUALIFICATION");

            migrationBuilder.DropTable(
                name: "SALARY_EXTRA_TYPE");

            migrationBuilder.DropTable(
                name: "AREA_OF_SPECIALIZATION");

            migrationBuilder.DropTable(
                name: "PFA_NAME");

            migrationBuilder.DropTable(
                name: "PFA_STATUS");

            migrationBuilder.DropTable(
                name: "ASSET");

            migrationBuilder.DropTable(
                name: "DOCUMENT_TYPE");

            migrationBuilder.DropTable(
                name: "SALARY_GRADE");

            migrationBuilder.DropTable(
                name: "LeaveResponse");

            migrationBuilder.DropTable(
                name: "TRAINING_TYPE");

            migrationBuilder.DropTable(
                name: "JOB_TYPE");

            migrationBuilder.DropTable(
                name: "USER");

            migrationBuilder.DropTable(
                name: "ASSET_TYPE");

            migrationBuilder.DropTable(
                name: "SALARY_GRADE_CATEGORY");

            migrationBuilder.DropTable(
                name: "SALARY_LEVEL");

            migrationBuilder.DropTable(
                name: "SALARY_STEP");

            migrationBuilder.DropTable(
                name: "LEAVE_REQUEST");

            migrationBuilder.DropTable(
                name: "ROLE");

            migrationBuilder.DropTable(
                name: "LEAVE_TYPE_RANK");

            migrationBuilder.DropTable(
                name: "STAFF");

            migrationBuilder.DropTable(
                name: "LEAVE_TYPE");

            migrationBuilder.DropTable(
                name: "INSTITUTION_APPOINTMENT");

            migrationBuilder.DropTable(
                name: "APPOINTMENT_TYPE");

            migrationBuilder.DropTable(
                name: "INSTITUTION_STAFF_CATEGORY");

            migrationBuilder.DropTable(
                name: "INSTITUTION_DEPARTMENT");

            migrationBuilder.DropTable(
                name: "PERSON");

            migrationBuilder.DropTable(
                name: "INSTITUTION_RANK");

            migrationBuilder.DropTable(
                name: "INSTITUTION_STAFF_TYPE");

            migrationBuilder.DropTable(
                name: "FACULTY");

            migrationBuilder.DropTable(
                name: "GENDER");

            migrationBuilder.DropTable(
                name: "LGA");

            migrationBuilder.DropTable(
                name: "MARITAL_STATUS");

            migrationBuilder.DropTable(
                name: "RELIGION");

            migrationBuilder.DropTable(
                name: "INSTITUTION_UNIT");

            migrationBuilder.DropTable(
                name: "STATE");
        }
    }
}
