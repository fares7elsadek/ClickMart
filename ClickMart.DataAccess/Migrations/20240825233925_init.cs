using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ClickMart.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
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
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValueSql: "newid()"),
                    Name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
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
                    { "015834bd-ff72-4216-b549-04a2a7125894", "BI", "Burundi" },
                    { "01bd63f1-faf9-4f13-afc1-88016e086cc6", "PA", "Panama" },
                    { "095e8e16-d1a2-48d1-a425-7e8038916bfc", "KY", "Cayman Islands" },
                    { "09c49704-ddd1-4b3a-ac7e-fd6f26e1331d", "LR", "Liberia" },
                    { "0a1988d2-c55a-4821-9abf-17dd60d52bf3", "HM", "Heard Island and McDonald Islands" },
                    { "0c8b7068-dbe8-4de9-a8f6-71e51596ff7c", "AL", "Albania" },
                    { "0cdb2145-b8d7-4d1a-b3fb-84a1c306fd74", "GM", "Gambia" },
                    { "0ce49185-8aca-4f63-9c9d-c7b17465d3e1", "EE", "Estonia" },
                    { "0d5b4a95-40f6-4406-95ba-22d60ee19707", "RW", "Rwanda" },
                    { "12d2d2f7-20ea-4802-acc6-983b435eaca9", "BJ", "Benin" },
                    { "135f0dbc-34cb-410e-b34b-0c93feb1d735", "AO", "Angola" },
                    { "15765f5e-28e1-4dff-b36f-f4bc55346a71", "BN", "Brunei Darussalam" },
                    { "159324ac-7292-4396-929a-2db97f3a182c", "MN", "Mongolia" },
                    { "1672c625-65a9-45cb-9e31-c7b9b0521fa2", "UY", "Uruguay" },
                    { "16e9fda8-499d-47d4-8165-c474b38a6039", "FJ", "Fiji" },
                    { "19c447a2-929d-4ef3-b3a2-0562df8866d1", "KG", "Kyrgyzstan" },
                    { "19d43b6d-1b6f-493e-a78c-aa4e2e2a16f4", "LS", "Lesotho" },
                    { "1a36b0e5-712f-4583-a3d7-d28042990118", "TW", "Taiwan" },
                    { "1a76e150-1c68-4e09-96a0-452745fd6ef4", "RE", "Reunion" },
                    { "1b97f403-ec01-41e9-85af-4ba13ffa1216", "MX", "Mexico" },
                    { "1c56a4a3-f0dc-4a25-9fc8-16c72a116d69", "CL", "Chile" },
                    { "1d4ea2d2-6509-460b-8bba-c21fc7cc3337", "CG", "Congo" },
                    { "1da11fa4-59a3-4e96-bb81-b1ce92f10571", "MK", "Macedonia" },
                    { "1f713fec-2ccd-4a65-8b12-55211c4b402c", "BG", "Bulgaria" },
                    { "2269697f-df76-46e1-9810-6a9657352009", "ET", "Ethiopia" },
                    { "2354d156-c12e-46df-9104-96054cf40fc2", "HU", "Hungary" },
                    { "26397781-63ed-4628-9fea-8a3514a05f94", "PG", "Papua New Guinea" },
                    { "267099f4-698a-4f69-a1f2-157ea4d0609c", "GN", "Guinea" },
                    { "267b649a-94b7-4733-bcaf-6e682b88a635", "SS", "South Sudan" },
                    { "27663b3a-122f-4520-ba54-459d2eb2694f", "TC", "Turks and Caicos Islands" },
                    { "285e87a0-2c20-46fb-be66-c9891e022dd0", "ST", "Sao Tome and Principe" },
                    { "292ac760-91ee-46b0-8575-1d0818aeb9fe", "TL", "Timor-Leste" },
                    { "29421b4d-dc90-4879-b657-b5acc7d8dd9a", "FR", "France" },
                    { "29895060-96b0-4942-9157-24b1ad011050", "TH", "Thailand" },
                    { "2af4a299-b355-4be1-bbdb-1dbeaab4bba7", "NF", "Norfolk Island" },
                    { "2b6cb908-d93b-4650-9a43-cda27410f1b5", "KH", "Cambodia" },
                    { "2c6b3982-6f22-45a2-baac-35e351402b4d", "KN", "Saint Kitts and Nevis" },
                    { "2eb26d30-d27a-41f7-8c15-08bd94c33be3", "SD", "Sudan" },
                    { "31963399-e5e8-4fb5-b30b-32ccb6392d04", "PN", "Pitcairn" },
                    { "367cc4b6-0c82-40e1-ae8c-568a2b5c0fb5", "GS", "South Georgia" },
                    { "36df9083-9620-4f2e-a8bb-c69261dee5ed", "SR", "Suriname" },
                    { "38759d5f-c323-4fff-8b80-b3b02c868d67", "ZM", "Zambia" },
                    { "38edb94c-77cf-4278-804a-acc75c10a5da", "NO", "Norway" },
                    { "3c36b7f7-47b3-4b72-8b5c-29f45534593b", "SM", "San Marino" },
                    { "3e8a8559-6d93-4ed5-8301-bba815368d6a", "AS", "American Samoa" },
                    { "3ec118bd-44cf-445d-b6bc-44bff5950e41", "GD", "Grenada" },
                    { "3f37733b-0408-4b8e-af30-3bfa4eafde39", "BM", "Bermuda" },
                    { "3fdaccc6-a1a8-4bf0-93e9-f85ad6e7d55b", "MF", "Saint Martin" },
                    { "417358dd-3968-47c8-b2b9-d1f96580a65b", "IT", "Italy" },
                    { "4219566d-fee9-45d1-9dc5-b3fb1ce45e0e", "IN", "India" },
                    { "42eb6060-35de-4539-aee7-18b7709a4653", "MT", "Malta" },
                    { "430e6a32-1bca-4fc2-b5ce-555d73ab0ff6", "PE", "Peru" },
                    { "4368a856-5924-4837-aed3-39a3c4a02c20", "MY", "Malaysia" },
                    { "44b9965f-d94f-4150-bbcc-97d1c3136a47", "EG", "Egypt" },
                    { "45a6720a-695e-4794-bea0-831c911c4882", "MU", "Mauritius" },
                    { "45b6f8b5-b552-4d66-bb67-a011beb8eb43", "LI", "Liechtenstein" },
                    { "45c61519-1c73-4955-a5c8-a3f5e1dd708d", "AT", "Austria" },
                    { "46fccf1d-954a-4663-9b2a-cc233097e71d", "ES", "Spain" },
                    { "472536fe-e6ff-4782-a5e8-56291f63e63c", "GF", "French Guiana" },
                    { "48caab47-5ef8-4229-be62-3cdaeab95dbb", "VA", "Holy See (Vatican City State)" },
                    { "4c04f16d-8413-40c6-9280-f55d20bdb2c2", "NU", "Niue" },
                    { "4c1ca0b6-b34d-4462-b9c9-f1bbb5573fb1", "RU", "Russian Federation" },
                    { "4d755224-3fcd-4ca9-9c45-d1d05a22481c", "MA", "Morocco" },
                    { "4e748e7a-7193-4d9d-b568-d06f02153067", "GQ", "Equatorial Guinea" },
                    { "4e8fe716-3e34-4420-9105-de0e168cf324", "TT", "Trinidad and Tobago" },
                    { "4eb621ee-a53e-40a0-a821-f68c867c5121", "SV", "El Salvador" },
                    { "4ee0b523-f3fe-4260-9fa6-8870484a0c76", "BR", "Brazil" },
                    { "51f968a4-08d3-4b41-9c8f-9f0b9a2acc7a", "PH", "Philippines" },
                    { "5201ce02-91d5-4388-9d59-e07c83c5f7e0", "CV", "Cape Verde" },
                    { "531615eb-257c-4412-b920-546139bc5823", "TV", "Tuvalu" },
                    { "541b18db-ba22-4d5a-8bf7-8c75bb241bd0", "YE", "Yemen" },
                    { "55fe7c83-21d1-4aa7-bf75-22346b4d8cfd", "TJ", "Tajikistan" },
                    { "563e47fc-6d91-41fb-8334-098e7dbf8f7a", "DO", "Dominican Republic" },
                    { "58d95d96-db2f-4b2c-b705-38924d49c9ec", "LA", "Lao Peoples Democratic Republic" },
                    { "591220e9-bbca-47d9-880e-51b9228c6a3c", "SC", "Seychelles" },
                    { "59154c91-4d7d-44ed-bb15-21c277706175", "CH", "Switzerland" },
                    { "59ec33be-383a-4cd2-bed4-ad3d038e2d2d", "IS", "Iceland" },
                    { "5a11282b-ec6f-4381-971c-86afb979f920", "GG", "Guernsey" },
                    { "5b9061ba-44a4-4d7d-92ab-45c0545ad884", "NE", "Niger" },
                    { "5c97c35a-8320-49e4-b3e1-58f1b6b49aca", "IM", "Isle of Man" },
                    { "5ce8721c-7d15-441e-b6a6-d452cf1672fe", "SB", "Solomon Islands" },
                    { "5fd04ac2-986e-43a7-8704-16ccabeb6676", "NI", "Nicaragua" },
                    { "5fec103d-b740-4927-a26f-1ea754944be2", "AI", "Anguilla" },
                    { "61ba700f-74e8-4ad4-b485-9bf6d8397656", "FI", "Finland" },
                    { "62cc441c-004e-4c4f-b01e-9c4389a3f4f3", "WS", "Samoa" },
                    { "638f59bd-175d-4929-8b09-482a16a8669f", "ME", "Montenegro" },
                    { "6413c21a-aaac-4581-9ec4-183a30b9c09e", "MP", "Northern Mariana Islands" },
                    { "64f134ba-1eff-42ac-8424-e7f31762a009", "HR", "Croatia" },
                    { "6550e20c-139f-40aa-b1bb-c2efe910b75b", "ID", "Indonesia" },
                    { "6563eee5-cc85-4160-a82e-04ddb4f9d35c", "LC", "Saint Lucia" },
                    { "65a3561b-d9c1-4768-bf12-4dcce8421d01", "TM", "Turkmenistan" },
                    { "66af952d-b6d1-49f9-a1c3-6bd18c0cf9c0", "MR", "Mauritania" },
                    { "6ce43a60-8ffb-4406-a789-27499ebeaa68", "AG", "Antigua and Barbuda" },
                    { "70c06809-92c5-4c62-bb16-f1f3b6447130", "SA", "Saudi Arabia" },
                    { "711d71df-0704-4eec-8840-fb6927f75aa7", "NL", "Netherlands" },
                    { "721f3a65-0a13-48d9-86f5-1967ceddb868", "BB", "Barbados" },
                    { "72c7ddb1-4c55-49bb-9037-0bda3e9600eb", "SY", "Syrian Arab Republic" },
                    { "733a2c51-b2ad-40bf-a4f1-f1a670d8380f", "AW", "Aruba" },
                    { "73bf0f97-acad-46af-8426-7fb37e24fc0e", "PM", "Saint Pierre and Miquelon" },
                    { "74210d80-304a-456e-9505-f068f1d72c6b", "MC", "Monaco" },
                    { "7599fbfd-ba38-436d-9b0d-49121141ad48", "BD", "Bangladesh" },
                    { "7680eec5-20ca-4579-98f4-78c597741001", "PF", "French Polynesia" },
                    { "76a7c83e-6309-41af-ac43-9f55f3e210fd", "NA", "Namibia" },
                    { "7738efea-b887-455b-a43a-f05ee3d51718", "SJ", "Svalbard and Jan Mayen" },
                    { "7843c215-c9dc-4ac8-a0eb-e5be28c4bd6c", "US", "United States" },
                    { "789dc390-a33a-46ee-bbe2-7f9dc92272ae", "GR", "Greece" },
                    { "7a1fcb5a-4258-4c35-8a47-d9fc2f2462e4", "KE", "Kenya" },
                    { "7a77d689-3a37-46d7-b8d4-25a75b070a18", "VU", "Vanuatu" },
                    { "7c155b08-ab2e-4416-aceb-baf94b978d7a", "JO", "Jordan" },
                    { "7c74c5ed-c331-4e61-8036-0bc3dc760856", "YT", "Mayotte" },
                    { "7cc2b561-f86a-47a4-b84d-005d961ea055", "BE", "Belgium" },
                    { "7d010471-95f5-4823-888d-e341fe826c7d", "LK", "Sri Lanka" },
                    { "80041922-95a3-44e9-8bdc-370a4b7ff2c1", "SK", "Slovakia" },
                    { "8194b145-8d22-4163-928a-31fb29fc5b1f", "PL", "Poland" },
                    { "81ebec3d-12e5-4b52-b22e-279e50d2ceea", "ZA", "South Africa" },
                    { "82a1b8d5-9606-45db-91b2-0c38207c426c", "TK", "Tokelau" },
                    { "82b1438f-83b3-4105-aab3-9f11fc53d5b1", "ML", "Mali" },
                    { "83728bf1-79a8-493e-84be-4d9a22c56767", "HT", "Haiti" },
                    { "84039580-415b-4d00-b702-b568312dad55", "NP", "Nepal" },
                    { "86ccef23-42df-4d6a-921a-317980a64004", "SO", "Somalia" },
                    { "89d92936-5d40-469f-9ff5-5ec6a22c5bf6", "LV", "Latvia" },
                    { "89d93bb3-0a50-4388-94b9-0e2bda23e880", "QA", "Qatar" },
                    { "89dde9c5-a7d7-4ce9-bfb0-e59862467c80", "BV", "Bouvet Island" },
                    { "8bb9fe12-5c4b-40ab-9712-6ea0d9dc981c", "TZ", "Tanzania" },
                    { "8c4f57e9-3f57-4166-a26f-ff73bbcbbe73", "DK", "Denmark" },
                    { "8c57e853-00ef-4b43-8ed8-0a1bdb5a8065", "OM", "Oman" },
                    { "8d3438de-6d34-4040-83ed-301396e9e361", "AD", "Andorra" },
                    { "8e0625e6-1c9c-4618-b2f3-3d8d3ed49239", "TD", "Chad" },
                    { "8f93f051-8ba3-40a4-99f1-264501313467", "BF", "Burkina Faso" },
                    { "90148e33-bd0e-4bc9-825e-77ae1f2ab024", "UG", "Uganda" },
                    { "902c7897-77e5-40ad-8586-cf5b15686474", "GU", "Guam" },
                    { "9097e526-3049-4028-88c8-7358cb5f3e59", "GW", "Guinea-Bissau" },
                    { "925d938e-cfe3-4916-8d7f-0ca3afada634", "MZ", "Mozambique" },
                    { "92710c4b-aca5-4c6d-a752-38adbf6734c2", "LY", "Libya" },
                    { "93b11371-a1c7-44ec-8bb5-855c200c37fd", "PY", "Paraguay" },
                    { "9448ff53-ee3f-48b4-bcf1-03ad081a67f0", "PK", "Pakistan" },
                    { "94be01f7-63da-4c9e-9577-2d8a43e1b670", "FK", "Falkland Islands (Malvinas)" },
                    { "95557024-1d31-4762-b40d-f554d9ce35cf", "BW", "Botswana" },
                    { "96212742-f6af-4559-92de-c18aaf8d75be", "KW", "Kuwait" },
                    { "96e3d7b9-bfe7-4111-9c36-c3d8e154cbc4", "NZ", "New Zealand" },
                    { "99af4181-302b-4e38-9833-72a71ebc7385", "GP", "Guadeloupe" },
                    { "9aafd837-0419-4815-b3f7-047f189a43b8", "KM", "Comoros" },
                    { "9b4cdce4-e0f9-4d5c-ae86-427b967bca88", "TF", "French Southern Territories" },
                    { "9c154140-2073-4e11-b551-a120daa8a5d3", "BS", "Bahamas" },
                    { "9c59167e-cc1d-43ff-99d5-1b8742d7c757", "MO", "Macao" },
                    { "9d27929d-32c7-4eda-a3ad-263a13a3d771", "SH", "Saint Helena" },
                    { "9dfe7913-06b7-4fb0-918a-cbe6ff4dc589", "JM", "Jamaica" },
                    { "9fb39243-008b-4aec-8102-ec2164beb9b2", "TN", "Tunisia" },
                    { "a118fa35-8f57-4548-9c84-4df9e6cadd54", "GE", "Georgia" },
                    { "a191de86-b237-41e7-b8ed-6a8204245e19", "PW", "Palau" },
                    { "a1be0baf-e293-4ada-88b4-0543c544c394", "MV", "Maldives" },
                    { "a2350eb6-2437-4b67-b17c-6007dab02660", "IO", "British Indian Ocean Territory" },
                    { "a2bbc1a8-04fc-407c-b692-8bf33f3a329c", "LT", "Lithuania" },
                    { "a6fc340c-e47a-4441-9941-4afc002e2289", "CM", "Cameroon" },
                    { "a7474319-f6b9-4f42-9a0e-41f9a44705b4", "CZ", "Czech Republic" },
                    { "a7853e93-7033-4207-b884-9fe9faf43750", "UA", "Ukraine" },
                    { "ab2e231b-976c-4917-875d-e9a78352a337", "MQ", "Martinique" },
                    { "ab48449e-75f8-4640-a35b-e960be83b014", "CK", "Cook Islands" },
                    { "abb9d107-04af-4f93-ae0c-030235dfbc5b", "PS", "Palestinian Territory" },
                    { "ac3633e1-df1b-4af1-b4ac-b91f73289986", "VE", "Venezuela" },
                    { "ad0d8d40-e333-4999-b8ad-eb4d60712fd6", "SG", "Singapore" },
                    { "adb4bf0a-e054-426b-a540-1a693c47ffa2", "GA", "Gabon" },
                    { "ae36c87a-52ce-42c9-90be-6178b7ae2f8e", "SE", "Sweden" },
                    { "aef9c479-5e8f-4046-9b14-2a54f93352a8", "KI", "Kiribati" },
                    { "b0246d67-9047-4076-a5c1-8638a7820a36", "AU", "Australia" },
                    { "b0a7927e-9847-4d28-b5df-46b2a5545ed2", "BZ", "Belize" },
                    { "b556a849-8965-4b8a-b6fe-dad526f26524", "JP", "Japan" },
                    { "b5e623e5-50d8-4009-bb52-5ad9ef036dc4", "VN", "Viet Nam" },
                    { "b67d7d3c-b22f-458a-b0b7-2d5ac6c4d9cc", "SZ", "Swaziland" },
                    { "b73e31fc-ae07-4795-895c-ea1dbee1faff", "NR", "Nauru" },
                    { "b81c5cdd-b56d-48a9-bc80-ffcb16e053b3", "CR", "Costa Rica" },
                    { "b8d727b7-e3fb-4f39-857d-737bd2863d66", "AZ", "Azerbaijan" },
                    { "b91b4e39-8e2c-4752-b664-706505134642", "IR", "Iran" },
                    { "b933a352-ca6b-4ee6-9b06-2765395569b9", "CA", "Canada" },
                    { "ba035b15-fc44-4002-9f6e-1cb3037d2b80", "KZ", "Kazakhstan" },
                    { "ba43da21-809f-427a-a1c8-472010ad376d", "MS", "Montserrat" },
                    { "ba9983cf-f67c-4004-85cb-c2011d773724", "DE", "Germany" },
                    { "bac6283b-51bf-428e-8b9e-7a876fc15d4e", "AM", "Armenia" },
                    { "bb041341-281f-4ae2-8b49-97ba9b3821fe", "NG", "Nigeria" },
                    { "bc678375-83b3-4569-9cf2-13036560f3bc", "MW", "Malawi" },
                    { "bd092dd3-baec-4924-9e66-454df6267137", "CC", "Cocos (Keeling) Islands" },
                    { "be171361-13cc-4dbb-a34c-4726294aa9da", "HK", "Hong Kong" },
                    { "be7cbccf-5046-4c1c-967f-f55da29e7a28", "JE", "Jersey" },
                    { "bfebdf75-04e3-49e0-8dbd-6ffb08e04752", "TG", "Togo" },
                    { "c078b577-c7f0-4cd1-b353-96fef2a9fb1b", "IQ", "Iraq" },
                    { "c12ea6a7-d372-4b3a-b7d9-fb1990dbd7e6", "HN", "Honduras" },
                    { "c1a9a881-5770-431b-b88e-d7256c72fe47", "AR", "Argentina" },
                    { "c1d41a3f-7925-4d30-acfb-40d944881384", "VC", "Saint Vincent and the Grenadines" },
                    { "c420bba8-dad3-431b-aa08-ee01f01553ed", "NC", "New Caledonia" },
                    { "c4f51155-ca86-4e5b-b205-e192b7b180d5", "MM", "Myanmar" },
                    { "c4f597d9-7ca4-45c5-8c0a-4c79f1b67852", "MG", "Madagascar" },
                    { "c6bcecee-0e46-4b08-92e0-926bb557764b", "TO", "Tonga" },
                    { "c832763b-5bc2-4d94-ba75-c79cc2da9baf", "CO", "Colombia" },
                    { "c98a047b-5f77-4f39-9fed-73206aaa1228", "FO", "Faroe Islands" },
                    { "ca5c9b8e-be6a-43e8-87fa-4b5426fe355c", "GH", "Ghana" },
                    { "ca841d97-b21c-4634-9333-aa6cb3fd69ca", "GB", "United Kingdom" },
                    { "cbad6787-d0e5-4bd6-96b1-4a89d0f3a75d", "DZ", "Algeria" },
                    { "ce063718-69b2-457d-90e7-a4f1bd999cdf", "SI", "Slovenia" },
                    { "ce7d90ae-006f-423f-9221-11870a882901", "GY", "Guyana" },
                    { "ce858fcf-60c8-4a66-8fc1-962fdb6e3ee3", "MH", "Marshall Islands" },
                    { "ced329b6-3869-4929-9869-c64cb9130451", "DM", "Dominica" },
                    { "cee7a750-d7b6-41b4-8e6a-969b7d230c02", "FM", "Micronesia" },
                    { "cf3e852b-013a-47e9-97eb-1016c4830e81", "CF", "Central African Republic" },
                    { "d01b6553-2e3c-436c-bed2-fd66d7f626ce", "CN", "China" },
                    { "d1b5ad94-a3c8-4fa9-8e6a-1578b2803b06", "AF", "Afghanistan" },
                    { "d483a943-38ca-40d7-adbe-8a7b242bdca0", "RS", "Serbia" },
                    { "d5a16b91-5cca-4f78-8cfb-761d705497de", "PR", "Puerto Rico" },
                    { "d615491b-9848-41e3-857a-18e14b8d1395", "CX", "Christmas Island" },
                    { "d70ce9d6-90e4-4a58-916c-5d752013749d", "LB", "Lebanon" },
                    { "d7b3a57c-a90b-4ab1-afae-448dc99495e9", "EC", "Ecuador" },
                    { "d89c7070-c690-4758-aff2-00be680c01c2", "GL", "Greenland" },
                    { "dbbf7921-beca-44ca-8e40-e4c7cc8eca63", "DJ", "Djibouti" },
                    { "dc594866-d2b1-4b66-9d32-62e0e23f08d0", "ER", "Eritrea" },
                    { "dca7ea9d-52de-4b79-909f-3a8c35bd33d8", "AE", "United Arab Emirates" },
                    { "dd0ebcb1-ab40-4a14-9a3a-040e72a899c0", "PT", "Portugal" },
                    { "dd6b9641-a290-4782-8b31-e29239abd50a", "BH", "Bahrain" },
                    { "dd922d1c-4199-4bf7-93da-09bbf8caa184", "LU", "Luxembourg" },
                    { "e0108940-c7f7-4885-8960-131cc4c4091e", "BL", "Saint Barthelemy" },
                    { "e01c801a-357b-4188-802c-7d51169faf76", "IE", "Ireland" },
                    { "e05bb8ed-0dbc-4a09-acec-e567c524bfda", "CU", "Cuba" },
                    { "e10840ed-1423-4baf-aaa2-f24adeec1998", "BT", "Bhutan" },
                    { "e18dc153-c7d3-4696-9072-6fea4061f270", "GI", "Gibraltar" },
                    { "e2853259-5066-4748-b364-b01b37b3b6dc", "BY", "Belarus" },
                    { "e67f679f-9ba4-4e40-8491-018a0011b43e", "CY", "Cyprus" },
                    { "e81045e9-a48c-4d18-81f3-3bf8c64679d9", "EH", "Western Sahara" },
                    { "eade17b6-c109-4ae8-a380-ac78df6fefa1", "SL", "Sierra Leone" },
                    { "eaf902fd-f524-4b65-bde2-78e2a90f4b71", "ZW", "Zimbabwe" },
                    { "ebab2e5e-86d1-4910-b8fa-92113e13522d", "SN", "Senegal" },
                    { "eff45a0f-5534-4823-9a79-add5e9a43d8e", "MD", "Moldova" },
                    { "f3fb7a0e-28af-4cab-8659-117d453dd9a1", "GT", "Guatemala" },
                    { "f41295e7-d57f-450d-92ad-a6ec647f5aa6", "RO", "Romania" },
                    { "fc2b508a-1c0c-4170-8be1-4f9e4be884cc", "AQ", "Antarctica" },
                    { "fd240989-ec0a-463d-a80e-2b1f06deacb8", "UZ", "Uzbekistan" },
                    { "fd8f5281-4862-4326-a522-afbd0bf8d794", "TR", "Turkey" },
                    { "ff0f184a-f927-435e-b6e2-8ece5bed772e", "BA", "Bosnia and Herzegovina" }
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
                name: "Categories");

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
