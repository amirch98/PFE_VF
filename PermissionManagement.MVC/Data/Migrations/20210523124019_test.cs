using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PermissionManagement.MVC.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    BrandID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandNom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProduitID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.BrandID);
                });

            migrationBuilder.CreateTable(
                name: "NoteFrais",
                columns: table => new
                {
                    NoteFraisID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    montant = table.Column<double>(type: "float", nullable: false),
                    type = table.Column<int>(type: "int", nullable: false),
                    statut = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoteFrais", x => x.NoteFraisID);
                });

            migrationBuilder.CreateTable(
                name: "Ressources",
                columns: table => new
                {
                    RessourceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RPrenom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SRegion = table.Column<int>(type: "int", nullable: true),
                    Ville = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nville = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cville = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sville = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Entree = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sortie = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ressources", x => x.RessourceID);
                });

            migrationBuilder.CreateTable(
                name: "Segments",
                columns: table => new
                {
                    SegmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SRang = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SType = table.Column<int>(type: "int", nullable: false),
                    Axe = table.Column<int>(type: "int", nullable: true),
                    GRaison = table.Column<int>(type: "int", nullable: true),
                    DRaison = table.Column<int>(type: "int", nullable: true),
                    PRaison = table.Column<int>(type: "int", nullable: true),
                    CRaison = table.Column<int>(type: "int", nullable: true),
                    Raison = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Segments", x => x.SegmentID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "Comptes",
                columns: table => new
                {
                    CompteID = table.Column<int>(type: "int", nullable: false),
                    AccountName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Adress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CType = table.Column<int>(type: "int", nullable: true),
                    SegmentID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comptes", x => x.CompteID);
                    table.ForeignKey(
                        name: "FK_Comptes_Segments_SegmentID",
                        column: x => x.SegmentID,
                        principalTable: "Segments",
                        principalColumn: "SegmentID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    ContactID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tel = table.Column<int>(type: "int", nullable: false),
                    Ville = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Statut = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SegmentID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.ContactID);
                    table.ForeignKey(
                        name: "FK_Contacts_Segments_SegmentID",
                        column: x => x.SegmentID,
                        principalTable: "Segments",
                        principalColumn: "SegmentID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Affectation",
                columns: table => new
                {
                    AffectationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    debut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompteID = table.Column<int>(type: "int", nullable: false),
                    ContactID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Affectation", x => x.AffectationID);
                    table.ForeignKey(
                        name: "FK_Affectation_Comptes_CompteID",
                        column: x => x.CompteID,
                        principalTable: "Comptes",
                        principalColumn: "CompteID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Affectation_Contacts_ContactID",
                        column: x => x.ContactID,
                        principalTable: "Contacts",
                        principalColumn: "ContactID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Consents",
                columns: table => new
                {
                    ConsentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactID = table.Column<int>(type: "int", nullable: false),
                    BrandID = table.Column<int>(type: "int", nullable: false),
                    C_CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    C_CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Statut = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consents", x => x.ConsentID);
                    table.ForeignKey(
                        name: "FK_Consents_Brands_BrandID",
                        column: x => x.BrandID,
                        principalTable: "Brands",
                        principalColumn: "BrandID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Consents_Contacts_ContactID",
                        column: x => x.ContactID,
                        principalTable: "Contacts",
                        principalColumn: "ContactID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Activites",
                columns: table => new
                {
                    ActiviteID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanMedicalID = table.Column<int>(type: "int", nullable: true),
                    PlanMarketingID = table.Column<int>(type: "int", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactID = table.Column<int>(type: "int", nullable: false),
                    De = table.Column<DateTime>(type: "datetime2", nullable: false),
                    À = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AllocationID = table.Column<int>(type: "int", nullable: true),
                    Ratings = table.Column<int>(type: "int", nullable: true),
                    RessourceID = table.Column<int>(type: "int", nullable: true),
                    Statut = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activites", x => x.ActiviteID);
                    table.ForeignKey(
                        name: "FK_Activites_Contacts_ContactID",
                        column: x => x.ContactID,
                        principalTable: "Contacts",
                        principalColumn: "ContactID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Activites_Ressources_RessourceID",
                        column: x => x.RessourceID,
                        principalTable: "Ressources",
                        principalColumn: "RessourceID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlanMedicals",
                columns: table => new
                {
                    PlanMedicalID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    date_debut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    date_fin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BrandID = table.Column<int>(type: "int", nullable: true),
                    SegmentID = table.Column<int>(type: "int", nullable: true),
                    E_Proposed = table.Column<int>(type: "int", nullable: false),
                    Frequence = table.Column<int>(type: "int", nullable: false),
                    ProduitID = table.Column<int>(type: "int", nullable: true),
                    Targets_Generated = table.Column<bool>(type: "bit", nullable: false),
                    Activities_Generated = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanMedicals", x => x.PlanMedicalID);
                    table.ForeignKey(
                        name: "FK_PlanMedicals_Brands_BrandID",
                        column: x => x.BrandID,
                        principalTable: "Brands",
                        principalColumn: "BrandID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlanMedicals_Segments_SegmentID",
                        column: x => x.SegmentID,
                        principalTable: "Segments",
                        principalColumn: "SegmentID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlanMarketings",
                columns: table => new
                {
                    PlanMarketingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date_debut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Date_fin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlanMedicalID = table.Column<int>(type: "int", nullable: false),
                    Frequence = table.Column<int>(type: "int", nullable: true),
                    Activities_Generated = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanMarketings", x => x.PlanMarketingID);
                    table.ForeignKey(
                        name: "FK_PlanMarketings_PlanMedicals_PlanMedicalID",
                        column: x => x.PlanMedicalID,
                        principalTable: "PlanMedicals",
                        principalColumn: "PlanMedicalID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlanTargets",
                columns: table => new
                {
                    PlanTargetID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanMedicalID = table.Column<int>(type: "int", nullable: false),
                    PlanMarketingID = table.Column<int>(type: "int", nullable: false),
                    CompteID = table.Column<int>(type: "int", nullable: true),
                    ContactID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanTargets", x => x.PlanTargetID);
                    table.ForeignKey(
                        name: "FK_PlanTargets_Comptes_CompteID",
                        column: x => x.CompteID,
                        principalTable: "Comptes",
                        principalColumn: "CompteID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlanTargets_Contacts_ContactID",
                        column: x => x.ContactID,
                        principalTable: "Contacts",
                        principalColumn: "ContactID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlanTargets_PlanMarketings_PlanMarketingID",
                        column: x => x.PlanMarketingID,
                        principalTable: "PlanMarketings",
                        principalColumn: "PlanMarketingID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlanTargets_PlanMedicals_PlanMedicalID",
                        column: x => x.PlanMedicalID,
                        principalTable: "PlanMedicals",
                        principalColumn: "PlanMedicalID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Produits",
                columns: table => new
                {
                    ProduitID = table.Column<int>(type: "int", nullable: false),
                    ProduitNom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: true),
                    BrandID = table.Column<int>(type: "int", nullable: false),
                    Competitor = table.Column<int>(type: "int", nullable: true),
                    Unite = table.Column<int>(type: "int", nullable: true),
                    PriceID = table.Column<int>(type: "int", nullable: true),
                    PDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stock_Theorique = table.Column<int>(type: "int", nullable: false),
                    Stock_Physique = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produits", x => x.ProduitID);
                    table.ForeignKey(
                        name: "FK_Produits_Brands_BrandID",
                        column: x => x.BrandID,
                        principalTable: "Brands",
                        principalColumn: "BrandID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Allocation",
                columns: table => new
                {
                    AllocationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    A_Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompteID = table.Column<int>(type: "int", nullable: true),
                    ContactID = table.Column<int>(type: "int", nullable: true),
                    ProduitID = table.Column<int>(type: "int", nullable: false),
                    A_CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    A_To = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    A_Statut = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Allocation", x => x.AllocationID);
                    table.ForeignKey(
                        name: "FK_Allocation_Comptes_CompteID",
                        column: x => x.CompteID,
                        principalTable: "Comptes",
                        principalColumn: "CompteID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Allocation_Contacts_ContactID",
                        column: x => x.ContactID,
                        principalTable: "Contacts",
                        principalColumn: "ContactID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Allocation_Produits_ProduitID",
                        column: x => x.ProduitID,
                        principalTable: "Produits",
                        principalColumn: "ProduitID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Prices",
                columns: table => new
                {
                    PriceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PriceNom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Context = table.Column<int>(type: "int", nullable: false),
                    DateDebut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Currency = table.Column<int>(type: "int", nullable: false),
                    Unit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProduitID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prices", x => x.PriceID);
                    table.ForeignKey(
                        name: "FK_Prices_Produits_ProduitID",
                        column: x => x.ProduitID,
                        principalTable: "Produits",
                        principalColumn: "ProduitID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activites_AllocationID",
                table: "Activites",
                column: "AllocationID");

            migrationBuilder.CreateIndex(
                name: "IX_Activites_ContactID",
                table: "Activites",
                column: "ContactID");

            migrationBuilder.CreateIndex(
                name: "IX_Activites_PlanMarketingID",
                table: "Activites",
                column: "PlanMarketingID");

            migrationBuilder.CreateIndex(
                name: "IX_Activites_PlanMedicalID",
                table: "Activites",
                column: "PlanMedicalID");

            migrationBuilder.CreateIndex(
                name: "IX_Activites_RessourceID",
                table: "Activites",
                column: "RessourceID");

            migrationBuilder.CreateIndex(
                name: "IX_Affectation_CompteID",
                table: "Affectation",
                column: "CompteID");

            migrationBuilder.CreateIndex(
                name: "IX_Affectation_ContactID",
                table: "Affectation",
                column: "ContactID");

            migrationBuilder.CreateIndex(
                name: "IX_Allocation_CompteID",
                table: "Allocation",
                column: "CompteID");

            migrationBuilder.CreateIndex(
                name: "IX_Allocation_ContactID",
                table: "Allocation",
                column: "ContactID");

            migrationBuilder.CreateIndex(
                name: "IX_Allocation_ProduitID",
                table: "Allocation",
                column: "ProduitID");

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
                name: "IX_Comptes_SegmentID",
                table: "Comptes",
                column: "SegmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Consents_BrandID",
                table: "Consents",
                column: "BrandID");

            migrationBuilder.CreateIndex(
                name: "IX_Consents_ContactID",
                table: "Consents",
                column: "ContactID");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_SegmentID",
                table: "Contacts",
                column: "SegmentID");

            migrationBuilder.CreateIndex(
                name: "IX_PlanMarketings_PlanMedicalID",
                table: "PlanMarketings",
                column: "PlanMedicalID");

            migrationBuilder.CreateIndex(
                name: "IX_PlanMedicals_BrandID",
                table: "PlanMedicals",
                column: "BrandID");

            migrationBuilder.CreateIndex(
                name: "IX_PlanMedicals_ProduitID",
                table: "PlanMedicals",
                column: "ProduitID");

            migrationBuilder.CreateIndex(
                name: "IX_PlanMedicals_SegmentID",
                table: "PlanMedicals",
                column: "SegmentID");

            migrationBuilder.CreateIndex(
                name: "IX_PlanTargets_CompteID",
                table: "PlanTargets",
                column: "CompteID");

            migrationBuilder.CreateIndex(
                name: "IX_PlanTargets_ContactID",
                table: "PlanTargets",
                column: "ContactID");

            migrationBuilder.CreateIndex(
                name: "IX_PlanTargets_PlanMarketingID",
                table: "PlanTargets",
                column: "PlanMarketingID");

            migrationBuilder.CreateIndex(
                name: "IX_PlanTargets_PlanMedicalID",
                table: "PlanTargets",
                column: "PlanMedicalID");

            migrationBuilder.CreateIndex(
                name: "IX_Prices_ProduitID",
                table: "Prices",
                column: "ProduitID");

            migrationBuilder.CreateIndex(
                name: "IX_Produits_BrandID",
                table: "Produits",
                column: "BrandID");

            migrationBuilder.CreateIndex(
                name: "IX_Produits_PriceID",
                table: "Produits",
                column: "PriceID");

            migrationBuilder.AddForeignKey(
                name: "FK_Activites_Allocation_AllocationID",
                table: "Activites",
                column: "AllocationID",
                principalTable: "Allocation",
                principalColumn: "AllocationID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Activites_PlanMarketings_PlanMarketingID",
                table: "Activites",
                column: "PlanMarketingID",
                principalTable: "PlanMarketings",
                principalColumn: "PlanMarketingID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Activites_PlanMedicals_PlanMedicalID",
                table: "Activites",
                column: "PlanMedicalID",
                principalTable: "PlanMedicals",
                principalColumn: "PlanMedicalID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlanMedicals_Produits_ProduitID",
                table: "PlanMedicals",
                column: "ProduitID",
                principalTable: "Produits",
                principalColumn: "ProduitID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Produits_Prices_PriceID",
                table: "Produits",
                column: "PriceID",
                principalTable: "Prices",
                principalColumn: "PriceID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prices_Produits_ProduitID",
                table: "Prices");

            migrationBuilder.DropTable(
                name: "Activites");

            migrationBuilder.DropTable(
                name: "Affectation");

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
                name: "Consents");

            migrationBuilder.DropTable(
                name: "NoteFrais");

            migrationBuilder.DropTable(
                name: "PlanTargets");

            migrationBuilder.DropTable(
                name: "Allocation");

            migrationBuilder.DropTable(
                name: "Ressources");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "PlanMarketings");

            migrationBuilder.DropTable(
                name: "Comptes");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "PlanMedicals");

            migrationBuilder.DropTable(
                name: "Segments");

            migrationBuilder.DropTable(
                name: "Produits");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Prices");
        }
    }
}
