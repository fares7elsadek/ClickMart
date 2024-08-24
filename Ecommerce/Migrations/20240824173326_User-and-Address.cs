using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class UserandAddress : Migration
    {
        /// <inheritdoc />
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
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValueSql: "newid()"),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    ISO = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
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
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValueSql: "newid()"),
                    UnitNumber = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    StreetNumber = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    AddressLine1 = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    AddressLine2 = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    City = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    Region = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    PostalCode = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: false),
                    CountryId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAddresses",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AddressId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAddresses", x => new { x.AddressId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserAddresses_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAddresses_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "ISO", "Name" },
                values: new object[,]
                {
                    { "005ee659-34e8-4a67-83ab-c4fb4d0487cf", "MK", "Macedonia" },
                    { "01e42e46-3f43-47f6-bab5-888b8466c93d", "MG", "Madagascar" },
                    { "01ed1ef4-8283-47a1-8702-27da06072a8e", "MH", "Marshall Islands" },
                    { "02f7221c-f694-48df-b3ec-76385d26cd4e", "NE", "Niger" },
                    { "03a8b0f4-1287-4ce4-93a6-ff57a76f5a52", "JM", "Jamaica" },
                    { "03ab0610-fa7e-48d5-be2f-7dc5bf7ae31e", "LC", "Saint Lucia" },
                    { "05e0be1d-91da-43e9-80a5-f402e28b0c99", "KY", "Cayman Islands" },
                    { "05f02002-f6e5-4107-ad18-4a9521bda667", "GI", "Gibraltar" },
                    { "079d9f8a-575a-426f-9013-1657941d5f19", "NF", "Norfolk Island" },
                    { "07d96518-3fe5-4783-a86e-32ffd790c2eb", "MO", "Macao" },
                    { "096640ae-8eaf-42f9-a54e-6649e02cd80f", "GL", "Greenland" },
                    { "0a2e4377-7172-43da-8362-14e66bf9a537", "SD", "Sudan" },
                    { "0af2d290-fe9f-40c4-8848-c671a476be05", "UA", "Ukraine" },
                    { "0d0b5da5-8f02-4e45-8934-0b122c591d41", "IT", "Italy" },
                    { "0e846ecb-4963-4440-ad5d-51fddcfc1b77", "GS", "South Georgia" },
                    { "10fce04c-0bf5-491d-b9ac-39b5a37fe49d", "PW", "Palau" },
                    { "119287be-d27e-4cd7-848e-0847914106c2", "LY", "Libya" },
                    { "13d0598d-e8a6-428f-82a6-075fd9517be7", "CK", "Cook Islands" },
                    { "1472f6e9-3219-4538-9baf-9983ceca9f1f", "AQ", "Antarctica" },
                    { "150f52be-888c-4fa6-986f-b9854c333103", "PS", "Palestinian Territory" },
                    { "172df277-c897-48a3-a97c-e3721da7760b", "DK", "Denmark" },
                    { "18994a71-20a1-41bf-82a0-b9cc36bf1a91", "PY", "Paraguay" },
                    { "189c57c3-86c6-4772-b6be-b8e11c1e7f49", "TH", "Thailand" },
                    { "1aff024c-42a7-419b-9d8b-cf274f1776b8", "BE", "Belgium" },
                    { "1b4dd757-552a-413c-90d6-08dbcdda7076", "US", "United States" },
                    { "1c5fd123-b7a5-4f7c-9176-fd4022f8d469", "TK", "Tokelau" },
                    { "1d6bebad-3e30-4528-b8a3-d80be68fc3b1", "ST", "Sao Tome and Principe" },
                    { "1da2394c-9f21-4572-8200-445bdc785997", "TT", "Trinidad and Tobago" },
                    { "1df7da3b-718e-47e0-85f3-073abc1d13c4", "RE", "Reunion" },
                    { "20d3059b-c608-4963-8a4a-147853603416", "DO", "Dominican Republic" },
                    { "21a8ea71-53e9-4d7d-84f5-6d08472f28c6", "CF", "Central African Republic" },
                    { "21ad3813-045f-42bf-b099-98ee175101ad", "PH", "Philippines" },
                    { "234f5461-1dd8-4ba2-8df3-676af40ef8b7", "QA", "Qatar" },
                    { "2370403d-0122-4998-b1c4-5a69747c3455", "MY", "Malaysia" },
                    { "24668804-652b-4747-9dfa-a7e44bb30023", "SG", "Singapore" },
                    { "256ae7cd-3d73-4897-934b-49391b3cf8d2", "CN", "China" },
                    { "270e05db-207d-4236-8432-ea85712376f8", "MT", "Malta" },
                    { "297e1f7d-5170-4145-826c-fa9b2dddec64", "MU", "Mauritius" },
                    { "2a7811e9-0ba9-4426-9267-a31b13650fe5", "MS", "Montserrat" },
                    { "2bfc96e4-d130-4245-99a1-77def4a6e1cc", "MR", "Mauritania" },
                    { "2f13c7dc-01a1-44da-8f3e-0010f834ee3a", "KW", "Kuwait" },
                    { "2f2a0c0e-2ca3-40d9-90f5-04e306820589", "TL", "Timor-Leste" },
                    { "30a8b8d5-4018-4f25-a109-57a8c689d4b1", "SO", "Somalia" },
                    { "30afc184-e8c3-4bbb-b5af-0a5bdc04581b", "MA", "Morocco" },
                    { "30c91c9c-02a8-4877-a0ae-c6777479e3c3", "ME", "Montenegro" },
                    { "32185d4b-adbc-410e-9259-c0923dbc0bad", "AT", "Austria" },
                    { "32ce6e83-6809-4a8f-bd3c-09ca2d61cf74", "TD", "Chad" },
                    { "32e7c6af-2967-4806-b652-8024f964c367", "PL", "Poland" },
                    { "35014b62-c9c0-435e-8224-63e8e4461cff", "LV", "Latvia" },
                    { "351849ee-0531-4d02-a1ec-d8240662ea09", "TN", "Tunisia" },
                    { "3565baf5-a612-49d9-9706-470f4dbc2a51", "VA", "Holy See (Vatican City State)" },
                    { "35c6c6cb-3275-4a0f-ae3a-d362b945380f", "BG", "Bulgaria" },
                    { "35ecccce-7292-48ea-b828-d0c97563c64e", "IS", "Iceland" },
                    { "362fe2da-d2ac-4f93-8762-d3b32c6e7416", "MM", "Myanmar" },
                    { "36810e74-0f22-4cb9-b124-0d389054498c", "BW", "Botswana" },
                    { "36c15669-25ed-4ef3-9bb3-e076197bc5e1", "MD", "Moldova" },
                    { "371197d7-aab2-4cce-a709-bea1bbe591b8", "SS", "South Sudan" },
                    { "37be5078-ace7-43c1-9513-1a650c3e213c", "NP", "Nepal" },
                    { "39d6c7f1-1e08-4464-b68d-d44eb390c236", "NC", "New Caledonia" },
                    { "3b52fe1f-003f-440a-ac4d-7fbff9f7d384", "MC", "Monaco" },
                    { "3b83bdfc-996c-42e8-ac3f-f9a73d45d829", "TO", "Tonga" },
                    { "3d424576-20ec-498c-b403-4d18d9570f3e", "UY", "Uruguay" },
                    { "3de1faeb-8980-45c3-ae8f-ca9e3beb95b3", "LK", "Sri Lanka" },
                    { "3e513a9e-5eb2-4541-abdb-e3ae3f3be779", "BS", "Bahamas" },
                    { "3f0741be-3524-4578-88a1-db617d487627", "OM", "Oman" },
                    { "3f09b082-c57c-4611-966f-020af57feb4d", "GG", "Guernsey" },
                    { "4110ffe1-1167-4d45-b796-d26fd58cc479", "SY", "Syrian Arab Republic" },
                    { "4125f65a-2d75-4be9-9d22-930f2f408eb0", "BJ", "Benin" },
                    { "420d750c-2b9a-43a7-9824-067d4ea04095", "SB", "Solomon Islands" },
                    { "43c30f81-850f-4299-97f9-2faa02e25b16", "BD", "Bangladesh" },
                    { "43ec8874-0eed-4ab6-84c0-9cb4b6ab43b6", "TW", "Taiwan" },
                    { "43f8a031-0f01-4e96-8c6b-b3c539dc3dfe", "BF", "Burkina Faso" },
                    { "480f47cd-80f8-4065-8cfd-6ccbe3bfed41", "WS", "Samoa" },
                    { "48b8962a-1c6e-437d-a56e-7747bb4b3009", "AO", "Angola" },
                    { "4961e9d4-401f-40cd-a6a6-b69f42346417", "BH", "Bahrain" },
                    { "49799be7-6852-4e43-92ae-4678315370c8", "IQ", "Iraq" },
                    { "4afbb250-f42b-45ec-b873-99d9248bf620", "SV", "El Salvador" },
                    { "4ba370fb-6149-4f17-87a6-55c29e5f7d70", "TR", "Turkey" },
                    { "4d95061b-cfb7-46b1-9100-bc6077ed3800", "UZ", "Uzbekistan" },
                    { "4dc3933d-dcb6-4a46-89af-250da95feefa", "IM", "Isle of Man" },
                    { "4e5849c5-67a8-4f2c-8567-3f946dcba5ba", "AI", "Anguilla" },
                    { "518b7e6c-285b-42ba-a3a3-204173db6ecb", "RS", "Serbia" },
                    { "51e2b608-c392-4d70-af66-035286787358", "KE", "Kenya" },
                    { "53310dbe-4582-40cd-8940-0dd257239daf", "FM", "Micronesia" },
                    { "5498fcf3-9372-42b3-acb9-23dd134b0b62", "LS", "Lesotho" },
                    { "5762ddfe-cac2-46f7-bce5-5e6c59b21191", "ZW", "Zimbabwe" },
                    { "5a6b94db-e823-4025-93ad-69bdaee04cdb", "RO", "Romania" },
                    { "5b72882f-6c78-495e-8240-939e7494d889", "CM", "Cameroon" },
                    { "5b8af243-c0f4-47f4-89ed-4135dc32d820", "FO", "Faroe Islands" },
                    { "5e08b762-2e43-4ff9-952a-c729db4754f9", "GW", "Guinea-Bissau" },
                    { "61210860-fff6-4508-89e4-5fae72d17a55", "CX", "Christmas Island" },
                    { "6319bf4e-883b-4dc3-8217-ece4099c0ec5", "GM", "Gambia" },
                    { "636ea299-2435-4407-89d5-dd5526ee8b0c", "HU", "Hungary" },
                    { "66d282e8-2dfa-4701-80ce-4ff39349792c", "SH", "Saint Helena" },
                    { "68451cd3-3ff3-492e-92df-2a20d4e36749", "FI", "Finland" },
                    { "6923d723-8301-401e-827e-5ed843578320", "TC", "Turks and Caicos Islands" },
                    { "69cd5932-d7e2-4b51-8f3d-4f8b8f78d73a", "AD", "Andorra" },
                    { "69ebb30e-1fe1-4a66-9137-67a46e642a74", "SM", "San Marino" },
                    { "6a971cd4-5ba9-4fe5-8d99-dc5e5a40bc1e", "GQ", "Equatorial Guinea" },
                    { "6d478ee6-a21d-4804-ad82-dcbc13d5367d", "BI", "Burundi" },
                    { "6dff2f22-c846-4629-a713-bfe3a6f1895e", "NR", "Nauru" },
                    { "6fe1fe38-522b-4b15-8219-b7d55b40dafb", "EG", "Egypt" },
                    { "70602fa9-73c0-4d33-89c9-d392a6484950", "KH", "Cambodia" },
                    { "70aad067-d99a-4468-a482-9ae9d1f9f73e", "SE", "Sweden" },
                    { "70f70990-973a-4459-8b99-3385c6190aa2", "EC", "Ecuador" },
                    { "7379a55d-1342-4d4d-a56f-49aa04800d14", "VE", "Venezuela" },
                    { "738e8070-c05c-414e-807a-8a0eb14bacde", "MP", "Northern Mariana Islands" },
                    { "740e0346-f35f-47f1-9a09-170654197210", "MV", "Maldives" },
                    { "74b4813a-8dbc-4913-b2b4-a816e3a27360", "HK", "Hong Kong" },
                    { "753d83c8-1839-421a-bae5-c94b2e6c9c36", "GP", "Guadeloupe" },
                    { "75412820-c581-4b5c-abf2-dc7715d25491", "PM", "Saint Pierre and Miquelon" },
                    { "76c32d34-75cf-4a27-a9d0-550c9f0ad71f", "GA", "Gabon" },
                    { "778b93f4-f3a4-4414-a776-dd38939da03b", "KI", "Kiribati" },
                    { "77a53a77-3101-4380-aa98-cf87c2b4f023", "DJ", "Djibouti" },
                    { "77bfe271-e4a0-4801-ac63-524dc4ceab05", "NA", "Namibia" },
                    { "7822c7be-a0b4-4ba3-81cf-a34b60531e49", "ID", "Indonesia" },
                    { "789b2ba4-1700-4e11-9e77-58feb9ba51cd", "PG", "Papua New Guinea" },
                    { "78c38aaf-6be9-4af1-be16-817e7f9cc89a", "CH", "Switzerland" },
                    { "7b5727ab-c754-4a03-afb1-9d6479dce066", "BZ", "Belize" },
                    { "814b9d27-a369-4477-8145-af1aeef458eb", "EE", "Estonia" },
                    { "815b22fe-37e0-426c-a50c-a6691cc1be4a", "CR", "Costa Rica" },
                    { "81ec20b9-c797-46c0-b649-37cd655aa221", "IO", "British Indian Ocean Territory" },
                    { "85669ac7-05f6-4195-a172-60d9b947f1ca", "GR", "Greece" },
                    { "87a2c4f8-6ad9-4fe3-aa14-7a76c5287e15", "LA", "Lao Peoples Democratic Republic" },
                    { "885a4656-ae70-4521-9f49-655f3ed371a8", "AS", "American Samoa" },
                    { "89b07653-06b2-473a-bcbe-c8bd0ccfd6a3", "LI", "Liechtenstein" },
                    { "8a6b8cb2-2977-4847-859f-d9b36a79366d", "LR", "Liberia" },
                    { "8aafc1b6-3781-4a1b-87e7-3c1dbbab4b93", "PN", "Pitcairn" },
                    { "8b2b5487-d0c1-43e1-a71c-54b7f0ef07fa", "NL", "Netherlands" },
                    { "8b6d728f-e3b6-4648-8e62-6a4db34559b2", "AE", "United Arab Emirates" },
                    { "8be3d17c-497b-4b62-b344-7267f0485f88", "SC", "Seychelles" },
                    { "8ce3d4ab-da02-487a-8b7a-a5ce1aa4cd8a", "IE", "Ireland" },
                    { "8d42b85a-3856-41d3-bf20-e157ef21e716", "ET", "Ethiopia" },
                    { "8d7e2fe5-4fb9-4c57-affe-20cf9329dfd3", "GD", "Grenada" },
                    { "905dd96a-b17c-4582-85ee-e3b0ecd1e9f8", "KZ", "Kazakhstan" },
                    { "914595c5-d005-4b1c-9f7a-efe8fd47e291", "NU", "Niue" },
                    { "917be3e8-dbc0-4b42-894d-f11f38e4ac3d", "RW", "Rwanda" },
                    { "92b004b6-06a1-427c-85ce-7c3d86f4ca09", "UG", "Uganda" },
                    { "95614f66-58e7-4414-b192-e71225e2779c", "SA", "Saudi Arabia" },
                    { "96a86019-c2c2-4d79-9405-e96019a8be72", "HN", "Honduras" },
                    { "96da97d2-6de2-4aac-ab76-e6f515d02648", "VN", "Viet Nam" },
                    { "971ba193-1920-4fdb-a06a-ae5f903f0727", "RU", "Russian Federation" },
                    { "98fa5224-6508-4752-9f1a-46f0b52191b3", "GF", "French Guiana" },
                    { "9a61d597-d978-4c60-8c74-a0d03c9b4e12", "KM", "Comoros" },
                    { "9ac08f1f-714a-4088-9603-d2ada7a00077", "FJ", "Fiji" },
                    { "9b3188b2-93a4-4339-a9ee-c522c5cbcf8b", "MF", "Saint Martin" },
                    { "9d38405b-11a4-4a13-a92c-5aae2bddca5c", "PK", "Pakistan" },
                    { "9e29aae5-dfee-48ad-8504-c762a70acea7", "ZA", "South Africa" },
                    { "9e381224-b8b7-4034-9005-2172ced3989c", "BV", "Bouvet Island" },
                    { "9fc21a14-4a20-43bc-92de-4463881f4359", "AW", "Aruba" },
                    { "a39f1af9-689a-4393-82b8-31e6981a366c", "AZ", "Azerbaijan" },
                    { "a3eb189e-9e9d-44da-999d-edd21094436d", "BT", "Bhutan" },
                    { "a4234729-48aa-4c13-a74a-efb0b06a5230", "SI", "Slovenia" },
                    { "a42f91e0-9c1f-453a-becf-3d0f02356632", "JE", "Jersey" },
                    { "a4d4c988-77f9-409d-9a2a-de71f6b134dc", "HM", "Heard Island and McDonald Islands" },
                    { "a5124dd1-5b4d-45ae-aca0-0b628f63eace", "NG", "Nigeria" },
                    { "a604bad5-eff8-41b8-bb0d-d05e0f130449", "PE", "Peru" },
                    { "a6549f3a-75dd-4dd4-9478-04ab8045eea7", "PF", "French Polynesia" },
                    { "a6e5130b-bb16-428a-bedd-56e3107474eb", "CL", "Chile" },
                    { "a71762a4-e586-4d0c-a46e-4f574e7f4122", "AF", "Afghanistan" },
                    { "aaeb4ebc-5fbc-4bec-9abf-77968b892bca", "GB", "United Kingdom" },
                    { "ab2e0f91-3999-45c4-9e4b-5c7d8052d5b6", "SK", "Slovakia" },
                    { "ac9032cd-8d0a-4702-848d-5b877e08adb1", "BY", "Belarus" },
                    { "ac9ce39f-94ea-4d14-ae19-289bd764333d", "HR", "Croatia" },
                    { "af75ef58-22e2-43ea-924b-363a06a17498", "BN", "Brunei Darussalam" },
                    { "afa3af7e-b4ba-4d6d-b9d6-ccb844ba8da2", "CY", "Cyprus" },
                    { "b05e6f1b-ff5f-424e-9cd7-6cbea624b4f9", "KG", "Kyrgyzstan" },
                    { "b05e965a-27e1-4b7e-acf3-5d64607a69fd", "FR", "France" },
                    { "b084e33d-3c15-4e2c-8a15-35f5ab26d786", "HT", "Haiti" },
                    { "b0b4171a-da4b-4d02-904f-3a339395f53f", "VU", "Vanuatu" },
                    { "b13ae1e1-99cd-4373-b468-b7a6f243f356", "NO", "Norway" },
                    { "b2bfc083-5e9c-4a7c-bd42-7968ca302595", "TJ", "Tajikistan" },
                    { "b3110d1f-9bc6-4175-acc1-b3826c3f98be", "SR", "Suriname" },
                    { "b4234864-ab1d-45b6-91bb-213f35bc7950", "PT", "Portugal" },
                    { "b79a3be6-6a24-4c47-814e-799c89e13af6", "DZ", "Algeria" },
                    { "b96b71ff-63d8-47b4-9b70-0467456d8ed0", "CA", "Canada" },
                    { "bd12b415-4113-4d55-a5e3-45299ba0ab93", "IN", "India" },
                    { "be5e9e81-f78b-49fd-be1d-bad0b82ce572", "TM", "Turkmenistan" },
                    { "bec28109-949b-4eef-b699-20ae6d4aaae4", "TZ", "Tanzania" },
                    { "c0e493aa-095f-4153-9e97-4feb1972e21b", "TF", "French Southern Territories" },
                    { "c2583e33-c2b2-493d-a5e4-fb876732145a", "CZ", "Czech Republic" },
                    { "c264cb9e-ab8e-4c25-ac00-c1cec8355e74", "SL", "Sierra Leone" },
                    { "c28b561b-4ede-4723-816c-5abd47836722", "ZM", "Zambia" },
                    { "c31d9229-681d-44c4-9370-c8f3f02c9779", "IR", "Iran" },
                    { "c33c7ce1-271f-4976-af76-95eb2353be78", "AU", "Australia" },
                    { "c54433e9-67aa-495a-b264-ab491b3d2e2d", "SN", "Senegal" },
                    { "ca299447-7671-4da5-95cb-e98b095c3e3a", "AL", "Albania" },
                    { "ca97822c-4aa3-41fe-9ddc-32e2e43efc81", "BR", "Brazil" },
                    { "cc316bfb-2706-494f-973a-0dfe4dc420ea", "CO", "Colombia" },
                    { "cc4a3de3-9a97-4897-8cab-83c6a4909e91", "MQ", "Martinique" },
                    { "cf1c8846-b17d-48b6-8bfa-30a43e0e65e4", "AG", "Antigua and Barbuda" },
                    { "cfe48571-8714-418c-80fb-61bc99e252ec", "TV", "Tuvalu" },
                    { "cfe5c1c1-af97-4334-a566-0256ab263dc6", "GY", "Guyana" },
                    { "d05f9ae9-75de-4cdc-9522-4feada5e7546", "CU", "Cuba" },
                    { "d154d9d0-62fa-45d6-ad79-33b1ea8a2785", "JO", "Jordan" },
                    { "d1fca792-0105-4504-a0e1-fa166d183302", "ES", "Spain" },
                    { "d39fb202-9fa9-48e8-8284-c175626f1bfb", "ER", "Eritrea" },
                    { "d3a313f4-60dc-4adf-b88f-eaa455518baf", "BB", "Barbados" },
                    { "d46d68e2-0f77-4fa8-bd1a-83ca4287eeab", "DM", "Dominica" },
                    { "d49e7626-e11e-46b0-bafb-9fd7c41f4732", "NI", "Nicaragua" },
                    { "d53479ce-b9b4-4e97-a49f-bcf4f1145234", "NZ", "New Zealand" },
                    { "d5f7db19-21d4-4aee-a910-28f6ba68c9e7", "ML", "Mali" },
                    { "d6e23976-53a3-4884-abdf-f0d99d59c1f3", "PR", "Puerto Rico" },
                    { "d8338115-dc01-4505-ae7a-8c6bf5e02b92", "AM", "Armenia" },
                    { "dcce4d00-5397-4734-8e92-8026749c3954", "FK", "Falkland Islands (Malvinas)" },
                    { "dde2bf75-e2f9-404b-843e-2910944997d1", "DE", "Germany" },
                    { "ded24cdd-0088-4719-ad12-6b3c424524c5", "AR", "Argentina" },
                    { "e1fef3b2-9702-4183-9707-caf5b34578e0", "MX", "Mexico" },
                    { "e2712547-14a6-463e-9ba8-4276f5b0f0c7", "BM", "Bermuda" },
                    { "e3555c94-048d-478e-a08b-8df6a38d2c63", "CC", "Cocos (Keeling) Islands" },
                    { "e64817c1-9c2f-4f8f-af15-2561d4cffefd", "LB", "Lebanon" },
                    { "e6b0b0a1-2b66-4127-a0dc-2e573cbb507e", "YT", "Mayotte" },
                    { "e8e9443b-766d-4125-b319-76c1d7cfaf3b", "MN", "Mongolia" },
                    { "e900579a-9f12-409e-810b-1bb9a51f7be7", "SZ", "Swaziland" },
                    { "e998a23b-d7cd-4652-b48d-4039444ee3ba", "GU", "Guam" },
                    { "e9c0b0e4-f2a5-4d75-8ab6-1b5a059302a3", "VC", "Saint Vincent and the Grenadines" },
                    { "e9c41f5c-1dc8-4139-b5e9-5aadb51159da", "JP", "Japan" },
                    { "ea6ff77a-b5f0-44f7-ba83-f66da437a988", "GE", "Georgia" },
                    { "f0a460aa-f681-4868-bad5-3794836c1f36", "BA", "Bosnia and Herzegovina" },
                    { "f0bbe063-79a3-4b9c-9d79-0c1e732c1833", "CV", "Cape Verde" },
                    { "f0bfe808-f84e-4c4e-9bbf-2633711b3a35", "YE", "Yemen" },
                    { "f1e8077b-2116-49d4-a9eb-86bd8bb810cb", "GT", "Guatemala" },
                    { "f695d665-1ee7-4405-b858-12d9632ec4d4", "TG", "Togo" },
                    { "f6b11df9-c3e1-43c9-aaee-5556c4ba449d", "LU", "Luxembourg" },
                    { "f798244e-fd42-4c11-aa62-a1da94e99884", "CG", "Congo" },
                    { "f83e3617-83d1-4ad6-ac0a-d06720fff5eb", "SJ", "Svalbard and Jan Mayen" },
                    { "f88f5254-e3b0-4a89-af20-6ae4c1f849bf", "KN", "Saint Kitts and Nevis" },
                    { "f8c9bd12-2a96-438f-82f4-b4c0e17ae4b9", "MW", "Malawi" },
                    { "fa5bef28-9f75-4555-98ba-4296aca9479a", "PA", "Panama" },
                    { "fab9a026-73ed-47e8-ab16-c7cd78bfd4c3", "LT", "Lithuania" },
                    { "fb918fbe-5f39-4517-b562-fd708cf53686", "GH", "Ghana" },
                    { "fc2ac6ce-38db-4c9c-b79d-521857430f7d", "MZ", "Mozambique" },
                    { "fc2e4ac7-14a4-4af0-9a9f-307c66c3f307", "BL", "Saint Barthelemy" },
                    { "fd2af262-483e-407e-a3e2-30a5be849ed2", "EH", "Western Sahara" },
                    { "ffc21b4b-58b5-43fe-9cbd-133b1550bca4", "GN", "Guinea" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CountryId",
                table: "Addresses",
                column: "CountryId");

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
                name: "IX_UserAddresses_UserId",
                table: "UserAddresses",
                column: "UserId");
        }

        /// <inheritdoc />
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
                name: "UserAddresses");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
