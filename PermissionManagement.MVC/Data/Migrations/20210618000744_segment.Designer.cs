﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PermissionManagement.MVC.Data;

namespace PermissionManagement.MVC.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210618000744_segment")]
    partial class segment
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("PermissionManagement.MVC.Models.Activite", b =>
                {
                    b.Property<int>("ActiviteID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AllocationID")
                        .HasColumnType("int");

                    b.Property<int>("ContactID")
                        .HasColumnType("int");

                    b.Property<DateTime>("De")
                        .HasColumnType("datetime2");

                    b.Property<int?>("PlanMarketingID")
                        .HasColumnType("int");

                    b.Property<int?>("PlanMedicalID")
                        .HasColumnType("int");

                    b.Property<int?>("Ratings")
                        .HasColumnType("int");

                    b.Property<int?>("RessourceID")
                        .HasColumnType("int");

                    b.Property<int?>("Statut")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("À")
                        .HasColumnType("datetime2");

                    b.HasKey("ActiviteID");

                    b.HasIndex("AllocationID");

                    b.HasIndex("ContactID");

                    b.HasIndex("PlanMarketingID");

                    b.HasIndex("PlanMedicalID");

                    b.HasIndex("RessourceID");

                    b.ToTable("Activites");
                });

            modelBuilder.Entity("PermissionManagement.MVC.Models.Affectation", b =>
                {
                    b.Property<int>("AffectationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CompteID")
                        .HasColumnType("int");

                    b.Property<int>("ContactID")
                        .HasColumnType("int");

                    b.Property<DateTime>("debut")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("fin")
                        .HasColumnType("datetime2");

                    b.HasKey("AffectationID");

                    b.HasIndex("CompteID");

                    b.HasIndex("ContactID");

                    b.ToTable("Affectation");
                });

            modelBuilder.Entity("PermissionManagement.MVC.Models.Allocation", b =>
                {
                    b.Property<int>("AllocationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("A_CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("A_Statut")
                        .HasColumnType("int");

                    b.Property<DateTime>("A_To")
                        .HasColumnType("datetime2");

                    b.Property<string>("A_Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CompteID")
                        .HasColumnType("int");

                    b.Property<int?>("ContactID")
                        .HasColumnType("int");

                    b.Property<int>("ProduitID")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("AllocationID");

                    b.HasIndex("CompteID");

                    b.HasIndex("ContactID");

                    b.HasIndex("ProduitID");

                    b.ToTable("Allocation");
                });

            modelBuilder.Entity("PermissionManagement.MVC.Models.Brand", b =>
                {
                    b.Property<int>("BrandID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BrandNom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BrandID");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("PermissionManagement.MVC.Models.Change_Log", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Log")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Change_Log");
                });

            modelBuilder.Entity("PermissionManagement.MVC.Models.Compte", b =>
                {
                    b.Property<int>("CompteID")
                        .HasColumnType("int");

                    b.Property<string>("AccountName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Adress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CType")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SegmentID")
                        .HasColumnType("int");

                    b.HasKey("CompteID");

                    b.HasIndex("SegmentID");

                    b.ToTable("Comptes");
                });

            modelBuilder.Entity("PermissionManagement.MVC.Models.Consent", b =>
                {
                    b.Property<int>("ConsentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BrandID")
                        .HasColumnType("int");

                    b.Property<string>("C_CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("C_CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("ContactID")
                        .HasColumnType("int");

                    b.Property<string>("Statut")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ConsentID");

                    b.HasIndex("BrandID");

                    b.HasIndex("ContactID");

                    b.ToTable("Consents");
                });

            modelBuilder.Entity("PermissionManagement.MVC.Models.Contact", b =>
                {
                    b.Property<int>("ContactID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SegmentID")
                        .HasColumnType("int");

                    b.Property<string>("Statut")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Tel")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ville")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ContactID");

                    b.HasIndex("SegmentID");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("PermissionManagement.MVC.Models.NoteFrais", b =>
                {
                    b.Property<int>("NoteFraisID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("montant")
                        .HasColumnType("float");

                    b.Property<string>("statut")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("NoteFraisID");

                    b.ToTable("NoteFrais");
                });

            modelBuilder.Entity("PermissionManagement.MVC.Models.PlanMarketing", b =>
                {
                    b.Property<int>("PlanMarketingID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Activities_Generated")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Date_debut")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Date_fin")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Frequence")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PlanMedicalID")
                        .HasColumnType("int");

                    b.HasKey("PlanMarketingID");

                    b.HasIndex("PlanMedicalID");

                    b.ToTable("PlanMarketings");
                });

            modelBuilder.Entity("PermissionManagement.MVC.Models.PlanMedical", b =>
                {
                    b.Property<int>("PlanMedicalID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Activities_Generated")
                        .HasColumnType("bit");

                    b.Property<int?>("BrandID")
                        .HasColumnType("int");

                    b.Property<int>("E_Proposed")
                        .HasColumnType("int");

                    b.Property<int>("Frequence")
                        .HasColumnType("int");

                    b.Property<string>("PName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProduitID")
                        .HasColumnType("int");

                    b.Property<int?>("SegmentID")
                        .HasColumnType("int");

                    b.Property<bool>("Targets_Generated")
                        .HasColumnType("bit");

                    b.Property<DateTime>("date_debut")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("date_fin")
                        .HasColumnType("datetime2");

                    b.HasKey("PlanMedicalID");

                    b.HasIndex("BrandID");

                    b.HasIndex("ProduitID");

                    b.HasIndex("SegmentID");

                    b.ToTable("PlanMedicals");
                });

            modelBuilder.Entity("PermissionManagement.MVC.Models.PlanTarget", b =>
                {
                    b.Property<int>("PlanTargetID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CompteID")
                        .HasColumnType("int");

                    b.Property<int?>("ContactID")
                        .HasColumnType("int");

                    b.Property<int>("PlanMarketingID")
                        .HasColumnType("int");

                    b.Property<int>("PlanMedicalID")
                        .HasColumnType("int");

                    b.HasKey("PlanTargetID");

                    b.HasIndex("CompteID");

                    b.HasIndex("ContactID");

                    b.HasIndex("PlanMarketingID");

                    b.HasIndex("PlanMedicalID");

                    b.ToTable("PlanTargets");
                });

            modelBuilder.Entity("PermissionManagement.MVC.Models.Price", b =>
                {
                    b.Property<int>("PriceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Context")
                        .HasColumnType("int");

                    b.Property<int>("Currency")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateDebut")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateFin")
                        .HasColumnType("datetime2");

                    b.Property<string>("PriceNom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProduitID")
                        .HasColumnType("int");

                    b.Property<decimal>("Unit")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("PriceID");

                    b.HasIndex("ProduitID");

                    b.ToTable("Prices");
                });

            modelBuilder.Entity("PermissionManagement.MVC.Models.Produit", b =>
                {
                    b.Property<int>("ProduitID")
                        .HasColumnType("int");

                    b.Property<int>("BrandID")
                        .HasColumnType("int");

                    b.Property<int?>("Competitor")
                        .HasColumnType("int");

                    b.Property<string>("PDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PriceID")
                        .HasColumnType("int");

                    b.Property<string>("ProduitNom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Stock_Physique")
                        .HasColumnType("int");

                    b.Property<int>("Stock_Theorique")
                        .HasColumnType("int");

                    b.Property<int?>("Type")
                        .HasColumnType("int");

                    b.Property<int?>("Unite")
                        .HasColumnType("int");

                    b.HasKey("ProduitID");

                    b.HasIndex("BrandID");

                    b.HasIndex("PriceID");

                    b.ToTable("Produits");
                });

            modelBuilder.Entity("PermissionManagement.MVC.Models.Ressource", b =>
                {
                    b.Property<int>("RessourceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Entree")
                        .HasColumnType("datetime2");

                    b.Property<string>("RName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RPrenom")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SRegion")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Sortie")
                        .HasColumnType("datetime2");

                    b.Property<string>("Ville")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RessourceID");

                    b.ToTable("Ressources");
                });

            modelBuilder.Entity("PermissionManagement.MVC.Models.Segment", b =>
                {
                    b.Property<int>("SegmentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Axe")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Raison")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SRang")
                        .HasColumnType("int");

                    b.Property<int>("SType")
                        .HasColumnType("int");

                    b.HasKey("SegmentID");

                    b.ToTable("Segments");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PermissionManagement.MVC.Models.Activite", b =>
                {
                    b.HasOne("PermissionManagement.MVC.Models.Allocation", "Allocation")
                        .WithMany()
                        .HasForeignKey("AllocationID");

                    b.HasOne("PermissionManagement.MVC.Models.Contact", "Contact")
                        .WithMany()
                        .HasForeignKey("ContactID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PermissionManagement.MVC.Models.PlanMarketing", "PlanMarketing")
                        .WithMany()
                        .HasForeignKey("PlanMarketingID");

                    b.HasOne("PermissionManagement.MVC.Models.PlanMedical", "PlanMedical")
                        .WithMany()
                        .HasForeignKey("PlanMedicalID");

                    b.HasOne("PermissionManagement.MVC.Models.Ressource", "Ressource")
                        .WithMany()
                        .HasForeignKey("RessourceID");

                    b.Navigation("Allocation");

                    b.Navigation("Contact");

                    b.Navigation("PlanMarketing");

                    b.Navigation("PlanMedical");

                    b.Navigation("Ressource");
                });

            modelBuilder.Entity("PermissionManagement.MVC.Models.Affectation", b =>
                {
                    b.HasOne("PermissionManagement.MVC.Models.Compte", "Compte")
                        .WithMany()
                        .HasForeignKey("CompteID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PermissionManagement.MVC.Models.Contact", "Contact")
                        .WithMany()
                        .HasForeignKey("ContactID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Compte");

                    b.Navigation("Contact");
                });

            modelBuilder.Entity("PermissionManagement.MVC.Models.Allocation", b =>
                {
                    b.HasOne("PermissionManagement.MVC.Models.Compte", "Compte")
                        .WithMany()
                        .HasForeignKey("CompteID");

                    b.HasOne("PermissionManagement.MVC.Models.Contact", "Contact")
                        .WithMany("Allocations")
                        .HasForeignKey("ContactID");

                    b.HasOne("PermissionManagement.MVC.Models.Produit", "Produit")
                        .WithMany("Allocations")
                        .HasForeignKey("ProduitID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Compte");

                    b.Navigation("Contact");

                    b.Navigation("Produit");
                });

            modelBuilder.Entity("PermissionManagement.MVC.Models.Compte", b =>
                {
                    b.HasOne("PermissionManagement.MVC.Models.Segment", "Segment")
                        .WithMany("Comptes")
                        .HasForeignKey("SegmentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Segment");
                });

            modelBuilder.Entity("PermissionManagement.MVC.Models.Consent", b =>
                {
                    b.HasOne("PermissionManagement.MVC.Models.Brand", "Brand")
                        .WithMany("Consents")
                        .HasForeignKey("BrandID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PermissionManagement.MVC.Models.Contact", "Contact")
                        .WithMany("Consents")
                        .HasForeignKey("ContactID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");

                    b.Navigation("Contact");
                });

            modelBuilder.Entity("PermissionManagement.MVC.Models.Contact", b =>
                {
                    b.HasOne("PermissionManagement.MVC.Models.Segment", "Segment")
                        .WithMany("Contacts")
                        .HasForeignKey("SegmentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Segment");
                });

            modelBuilder.Entity("PermissionManagement.MVC.Models.PlanMarketing", b =>
                {
                    b.HasOne("PermissionManagement.MVC.Models.PlanMedical", "PlanMedical")
                        .WithMany()
                        .HasForeignKey("PlanMedicalID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PlanMedical");
                });

            modelBuilder.Entity("PermissionManagement.MVC.Models.PlanMedical", b =>
                {
                    b.HasOne("PermissionManagement.MVC.Models.Brand", "Brand")
                        .WithMany()
                        .HasForeignKey("BrandID");

                    b.HasOne("PermissionManagement.MVC.Models.Produit", "Produit")
                        .WithMany()
                        .HasForeignKey("ProduitID");

                    b.HasOne("PermissionManagement.MVC.Models.Segment", "Segment")
                        .WithMany()
                        .HasForeignKey("SegmentID");

                    b.Navigation("Brand");

                    b.Navigation("Produit");

                    b.Navigation("Segment");
                });

            modelBuilder.Entity("PermissionManagement.MVC.Models.PlanTarget", b =>
                {
                    b.HasOne("PermissionManagement.MVC.Models.Compte", "Compte")
                        .WithMany()
                        .HasForeignKey("CompteID");

                    b.HasOne("PermissionManagement.MVC.Models.Contact", "Contact")
                        .WithMany()
                        .HasForeignKey("ContactID");

                    b.HasOne("PermissionManagement.MVC.Models.PlanMarketing", "PlanMarketing")
                        .WithMany()
                        .HasForeignKey("PlanMarketingID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PermissionManagement.MVC.Models.PlanMedical", "PlanMedical")
                        .WithMany()
                        .HasForeignKey("PlanMedicalID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Compte");

                    b.Navigation("Contact");

                    b.Navigation("PlanMarketing");

                    b.Navigation("PlanMedical");
                });

            modelBuilder.Entity("PermissionManagement.MVC.Models.Price", b =>
                {
                    b.HasOne("PermissionManagement.MVC.Models.Produit", null)
                        .WithMany("Prices")
                        .HasForeignKey("ProduitID");
                });

            modelBuilder.Entity("PermissionManagement.MVC.Models.Produit", b =>
                {
                    b.HasOne("PermissionManagement.MVC.Models.Brand", "Brand")
                        .WithMany("Produits")
                        .HasForeignKey("BrandID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PermissionManagement.MVC.Models.Price", "Price")
                        .WithMany()
                        .HasForeignKey("PriceID");

                    b.Navigation("Brand");

                    b.Navigation("Price");
                });

            modelBuilder.Entity("PermissionManagement.MVC.Models.Brand", b =>
                {
                    b.Navigation("Consents");

                    b.Navigation("Produits");
                });

            modelBuilder.Entity("PermissionManagement.MVC.Models.Contact", b =>
                {
                    b.Navigation("Allocations");

                    b.Navigation("Consents");
                });

            modelBuilder.Entity("PermissionManagement.MVC.Models.Produit", b =>
                {
                    b.Navigation("Allocations");

                    b.Navigation("Prices");
                });

            modelBuilder.Entity("PermissionManagement.MVC.Models.Segment", b =>
                {
                    b.Navigation("Comptes");

                    b.Navigation("Contacts");
                });
#pragma warning restore 612, 618
        }
    }
}
