using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FootballLeaguesSimulation.Data.Migrations
{
    public partial class initialsetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "League",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_League", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Season",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Season", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Competition",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SeasonId = table.Column<int>(type: "int", nullable: false),
                    LeagueId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Competition_League_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "League",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Competition_Season_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Season",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompetitionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Group_Competition_CompetitionId",
                        column: x => x.CompetitionId,
                        principalTable: "Competition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Round",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompetitionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Round", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Round_Competition_CompetitionId",
                        column: x => x.CompetitionId,
                        principalTable: "Competition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompetitionId = table.Column<int>(type: "int", nullable: true),
                    GroupId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Team_Competition_CompetitionId",
                        column: x => x.CompetitionId,
                        principalTable: "Competition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Team_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Match",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Score1 = table.Column<int>(type: "int", nullable: false),
                    Score2 = table.Column<int>(type: "int", nullable: false),
                    Score1ET = table.Column<int>(type: "int", nullable: true),
                    Score2ET = table.Column<int>(type: "int", nullable: true),
                    Score1P = table.Column<int>(type: "int", nullable: true),
                    Score2P = table.Column<int>(type: "int", nullable: true),
                    Winner = table.Column<int>(type: "int", nullable: false),
                    HomeTeamId = table.Column<int>(type: "int", nullable: true),
                    GuestTeamId = table.Column<int>(type: "int", nullable: true),
                    CompetitionId = table.Column<int>(type: "int", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: true),
                    RoundId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Match", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Match_Competition_CompetitionId",
                        column: x => x.CompetitionId,
                        principalTable: "Competition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Match_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Match_Round_RoundId",
                        column: x => x.RoundId,
                        principalTable: "Round",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Match_Team_GuestTeamId",
                        column: x => x.GuestTeamId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Match_Team_HomeTeamId",
                        column: x => x.HomeTeamId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "League",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "EUFA" });

            migrationBuilder.InsertData(
                table: "Season",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "2017/2018" });

            migrationBuilder.InsertData(
                table: "Competition",
                columns: new[] { "Id", "EndAt", "LeagueId", "Logo", "Name", "SeasonId", "StartAt" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "https://e7.pngegg.com/pngimages/115/195/png-clipart-graphics-uefa-europa-league-logo-football-football-uefa-europa-league-logo.png", "UEFA CHAMPIONS LEAGUE", 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Group",
                columns: new[] { "Id", "CompetitionId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Group A" },
                    { 2, 1, "Group B" },
                    { 3, 1, "Group C" },
                    { 4, 1, "Group D" },
                    { 5, 1, "Group E" },
                    { 6, 1, "Group F" },
                    { 7, 1, "Group G" },
                    { 8, 1, "Group H" }
                });

            migrationBuilder.InsertData(
                table: "Round",
                columns: new[] { "Id", "CompetitionId", "EndAt", "Name", "StartAt" },
                values: new object[,]
                {
                    { 1, 1, null, "Groups", null },
                    { 2, 1, null, "Round of 16", null },
                    { 3, 1, null, "Round of 8", null },
                    { 4, 1, null, "Round of 4", null },
                    { 5, 1, null, "Final", null }
                });

            migrationBuilder.InsertData(
                table: "Team",
                columns: new[] { "Id", "Code", "CompetitionId", "GroupId", "Logo", "Name" },
                values: new object[,]
                {
                    { 1, "", 1, 1, "https://kassiesa.net/uefa/clubs/images/Benfica.png", "Benfica" },
                    { 30, "BVB", 1, 8, "https://kassiesa.net/uefa/clubs/images/Borussia-Dortmund.png", "Borussia Dortmund" },
                    { 29, "", 1, 8, "https://kassiesa.net/uefa/clubs/images/APOEL-Nicosia.png", "APOEL" },
                    { 28, "ASM", 1, 7, "https://kassiesa.net/uefa/clubs/images/AS-Monaco.png", "AS Monaco" },
                    { 27, "RBL", 1, 7, "https://kassiesa.net/uefa/clubs/images/RB-Leipzig.png", "RB Leipzig" },
                    { 26, "", 1, 7, "https://kassiesa.net/uefa/clubs/images/Besiktas.png", "Beşiktaş JK" },
                    { 25, "", 1, 7, "https://kassiesa.net/uefa/clubs/images/FC-Porto.png", "Porto" },
                    { 24, "NAP", 1, 6, "https://kassiesa.net/uefa/clubs/images/Napoli.png", "Napoli" },
                    { 23, "MCI", 1, 6, "https://kassiesa.net/uefa/clubs/images/Manchester-City.png", "Manchester City" },
                    { 22, "", 1, 6, "https://kassiesa.net/uefa/clubs/images/Shakhtar-Donetsk.png", "Shakhtar Donetsk" },
                    { 21, "", 1, 6, "https://kassiesa.net/uefa/clubs/images/Feyenoord.png", "Feyenoord" },
                    { 20, "SEV", 1, 5, "https://kassiesa.net/uefa/clubs/images/Sevilla.png", "Sevilla" },
                    { 19, "", 1, 5, "https://kassiesa.net/uefa/clubs/images/Liverpool.png", "Liverpool" },
                    { 18, "", 1, 5, "https://kassiesa.net/uefa/clubs/images/NK-Maribor.png", "Maribor" },
                    { 17, "", 1, 5, "https://kassiesa.net/uefa/clubs/images/Spartak-Moscow.png", "Spartak Moskva" },
                    { 16, "JUV", 1, 4, "https://kassiesa.net/uefa/clubs/images/Juventus.png", "Juventus" },
                    { 15, "", 1, 4, "https://kassiesa.net/uefa/clubs/images/FC-Barcelona.png", "Barcelona" },
                    { 14, "", 1, 4, "https://kassiesa.net/uefa/clubs/images/Olympiakos-Piraeus.png", "Olympiakos Piräus" },
                    { 13, "", 1, 4, "https://kassiesa.net/uefa/clubs/images/Sporting-CP-Lisbon.png", "Sporting Lisboa" },
                    { 12, "ROM", 1, 3, "https://kassiesa.net/uefa/clubs/images/AS-Roma.png", "AS Roma" },
                    { 11, "ATL", 1, 3, "https://kassiesa.net/uefa/clubs/images/Atl%E9tico-Madrid.png", "Atlético Madrid" },
                    { 10, "CHE", 1, 3, "https://kassiesa.net/uefa/clubs/images/Chelsea.png", "Chelsea" },
                    { 9, "", 1, 3, "https://www.logofootball.net/wp-content/uploads/Qarabag-FK-Logo.png", "Qarabağ FK" },
                    { 8, "PSG", 1, 2, "https://kassiesa.net/uefa/clubs/images/Paris-Saint-Germain.png", "Paris SG" },
                    { 7, "FCB", 1, 2, "https://kassiesa.net/uefa/clubs/images/Bayern-M%FCnchen.png", "FC Bayern München" },
                    { 6, "CEL", 1, 2, "https://kassiesa.net/uefa/clubs/images/Celtic.png", "Celtic" },
                    { 5, "", 1, 2, "https://kassiesa.net/uefa/clubs/images/Anderlecht.png", "Anderlecht" },
                    { 4, "BAS", 1, 1, "https://kassiesa.net/uefa/clubs/images/FC-Basel.png", "FC Basel 1893" },
                    { 3, "MUN", 1, 1, "https://kassiesa.net/uefa/clubs/images/Manchester-United.png", "Manchester United" },
                    { 2, "", 1, 1, "https://kassiesa.net/uefa/clubs/images/CSKA-Moscow.png", "CSKA Moskva" },
                    { 31, "TOT", 1, 8, "https://kassiesa.net/uefa/clubs/images/Tottenham-Hotspur.png", "Tottenham Hotspur" },
                    { 32, "RMD", 1, 8, "https://kassiesa.net/uefa/clubs/images/Real-Madrid.png", "R. Madrid" }
                });

            migrationBuilder.InsertData(
                table: "Match",
                columns: new[] { "Id", "CompetitionId", "GroupId", "GuestTeamId", "HomeTeamId", "PlayedAt", "RoundId", "Score1", "Score1ET", "Score1P", "Score2", "Score2ET", "Score2P", "Winner" },
                values: new object[,]
                {
                    { 1, 1, 1, 2, 1, new DateTime(2017, 9, 12, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 1, null, null, 2, null, null, 2 },
                    { 67, 1, 6, 23, 24, new DateTime(2017, 11, 1, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 2, null, null, 4, null, null, 23 },
                    { 65, 1, 6, 24, 23, new DateTime(2017, 10, 17, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 2, null, null, 1, null, null, 23 },
                    { 63, 1, 6, 21, 24, new DateTime(2017, 9, 26, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 3, null, null, 1, null, null, 24 },
                    { 62, 1, 6, 24, 22, new DateTime(2017, 9, 13, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 2, null, null, 1, null, null, 22 },
                    { 72, 1, 6, 23, 22, new DateTime(2017, 12, 6, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 2, null, null, 1, null, null, 22 },
                    { 69, 1, 6, 21, 23, new DateTime(2017, 11, 21, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 1, null, null, 0, null, null, 23 },
                    { 64, 1, 6, 22, 23, new DateTime(2017, 9, 26, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 2, null, null, 0, null, null, 23 },
                    { 61, 1, 6, 23, 21, new DateTime(2017, 9, 13, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 0, null, null, 4, null, null, 23 },
                    { 68, 1, 6, 21, 22, new DateTime(2017, 11, 1, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 3, null, null, 1, null, null, 22 },
                    { 70, 1, 6, 22, 24, new DateTime(2017, 11, 21, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 3, null, null, 0, null, null, 24 },
                    { 66, 1, 6, 22, 21, new DateTime(2017, 10, 17, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 1, null, null, 2, null, null, 22 },
                    { 58, 1, 5, 19, 20, new DateTime(2017, 11, 21, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 3, null, null, 3, null, null, 0 },
                    { 55, 1, 5, 17, 20, new DateTime(2017, 11, 1, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 2, null, null, 1, null, null, 20 },
                    { 53, 1, 5, 20, 17, new DateTime(2017, 10, 17, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 5, null, null, 1, null, null, 17 },
                    { 51, 1, 5, 18, 20, new DateTime(2017, 9, 26, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 3, null, null, 0, null, null, 20 },
                    { 50, 1, 5, 20, 19, new DateTime(2017, 9, 13, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 2, null, null, 2, null, null, 0 },
                    { 60, 1, 5, 17, 19, new DateTime(2017, 12, 6, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 7, null, null, 0, null, null, 19 },
                    { 56, 1, 5, 18, 19, new DateTime(2017, 11, 1, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 3, null, null, 0, null, null, 19 },
                    { 54, 1, 5, 19, 18, new DateTime(2017, 10, 17, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 0, null, null, 7, null, null, 19 },
                    { 52, 1, 5, 19, 17, new DateTime(2017, 9, 26, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 1, null, null, 1, null, null, 0 },
                    { 59, 1, 5, 20, 18, new DateTime(2017, 12, 6, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 1, null, null, 1, null, null, 0 },
                    { 57, 1, 5, 18, 17, new DateTime(2017, 11, 21, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 1, null, null, 1, null, null, 0 },
                    { 71, 1, 6, 24, 21, new DateTime(2017, 12, 6, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 2, null, null, 1, null, null, 21 },
                    { 81, 1, 7, 25, 26, new DateTime(2017, 11, 21, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 1, null, null, 1, null, null, 0 },
                    { 92, 1, 8, 32, 31, new DateTime(2017, 11, 1, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 3, null, null, 1, null, null, 31 },
                    { 90, 1, 8, 31, 32, new DateTime(2017, 10, 17, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 1, null, null, 1, null, null, 0 },
                    { 87, 1, 8, 32, 30, new DateTime(2017, 9, 26, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 1, null, null, 3, null, null, 32 },
                    { 85, 1, 8, 29, 32, new DateTime(2017, 9, 13, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 3, null, null, 0, null, null, 32 },
                    { 96, 1, 8, 29, 31, new DateTime(2017, 12, 6, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 3, null, null, 0, null, null, 31 },
                    { 94, 1, 8, 31, 30, new DateTime(2017, 11, 21, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 1, null, null, 2, null, null, 31 },
                    { 88, 1, 8, 31, 29, new DateTime(2017, 9, 26, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 0, null, null, 3, null, null, 31 },
                    { 86, 1, 8, 30, 31, new DateTime(2017, 9, 13, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 3, null, null, 1, null, null, 31 },
                    { 91, 1, 8, 29, 30, new DateTime(2017, 11, 1, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 1, null, null, 1, null, null, 0 },
                    { 74, 1, 7, 26, 25, new DateTime(2017, 9, 13, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 1, null, null, 3, null, null, 26 },
                    { 89, 1, 8, 30, 29, new DateTime(2017, 10, 17, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 1, null, null, 1, null, null, 0 },
                    { 82, 1, 7, 27, 28, new DateTime(2017, 11, 21, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 1, null, null, 4, null, null, 27 },
                    { 79, 1, 7, 28, 26, new DateTime(2017, 11, 1, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 1, null, null, 1, null, null, 0 },
                    { 77, 1, 7, 26, 28, new DateTime(2017, 10, 17, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 1, null, null, 2, null, null, 26 },
                    { 76, 1, 7, 25, 28, new DateTime(2017, 9, 26, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 0, null, null, 3, null, null, 25 },
                    { 73, 1, 7, 28, 27, new DateTime(2017, 9, 13, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 1, null, null, 1, null, null, 0 },
                    { 83, 1, 7, 26, 27, new DateTime(2017, 12, 6, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 1, null, null, 2, null, null, 26 }
                });

            migrationBuilder.InsertData(
                table: "Match",
                columns: new[] { "Id", "CompetitionId", "GroupId", "GuestTeamId", "HomeTeamId", "PlayedAt", "RoundId", "Score1", "Score1ET", "Score1P", "Score2", "Score2ET", "Score2P", "Winner" },
                values: new object[,]
                {
                    { 80, 1, 7, 27, 25, new DateTime(2017, 11, 1, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 3, null, null, 1, null, null, 25 },
                    { 78, 1, 7, 25, 27, new DateTime(2017, 10, 17, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 3, null, null, 2, null, null, 27 },
                    { 75, 1, 7, 27, 26, new DateTime(2017, 9, 26, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 2, null, null, 0, null, null, 26 },
                    { 84, 1, 7, 28, 25, new DateTime(2017, 12, 6, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 5, null, null, 2, null, null, 25 },
                    { 49, 1, 5, 17, 18, new DateTime(2017, 9, 13, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 1, null, null, 1, null, null, 0 },
                    { 48, 1, 4, 16, 14, new DateTime(2017, 12, 5, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 0, null, null, 2, null, null, 16 },
                    { 45, 1, 4, 15, 16, new DateTime(2017, 11, 22, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 0, null, null, 0, null, null, 0 },
                    { 17, 1, 2, 8, 5, new DateTime(2017, 10, 18, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 0, null, null, 4, null, null, 8 },
                    { 15, 1, 2, 7, 8, new DateTime(2017, 9, 27, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 3, null, null, 0, null, null, 8 },
                    { 14, 1, 2, 8, 6, new DateTime(2017, 9, 12, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 0, null, null, 5, null, null, 8 },
                    { 21, 1, 2, 7, 5, new DateTime(2017, 11, 22, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 1, null, null, 2, null, null, 7 },
                    { 20, 1, 2, 7, 6, new DateTime(2017, 10, 31, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 1, null, null, 2, null, null, 7 },
                    { 18, 1, 2, 6, 7, new DateTime(2017, 10, 18, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 3, null, null, 0, null, null, 7 },
                    { 13, 1, 2, 5, 7, new DateTime(2017, 9, 12, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 3, null, null, 0, null, null, 7 },
                    { 24, 1, 2, 5, 6, new DateTime(2017, 12, 5, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 0, null, null, 1, null, null, 5 },
                    { 16, 1, 2, 6, 5, new DateTime(2017, 9, 27, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 0, null, null, 3, null, null, 6 },
                    { 19, 1, 2, 5, 8, new DateTime(2017, 10, 31, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 5, null, null, 0, null, null, 8 },
                    { 11, 1, 1, 4, 1, new DateTime(2017, 12, 5, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 0, null, null, 2, null, null, 4 },
                    { 7, 1, 1, 2, 4, new DateTime(2017, 10, 31, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 1, null, null, 2, null, null, 2 },
                    { 5, 1, 1, 4, 2, new DateTime(2017, 10, 18, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 0, null, null, 2, null, null, 4 },
                    { 3, 1, 1, 1, 4, new DateTime(2017, 9, 27, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 5, null, null, 0, null, null, 4 },
                    { 2, 1, 1, 4, 3, new DateTime(2017, 9, 12, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 3, null, null, 0, null, null, 3 },
                    { 12, 1, 1, 2, 3, new DateTime(2017, 12, 5, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 2, null, null, 1, null, null, 3 },
                    { 8, 1, 1, 1, 3, new DateTime(2017, 10, 31, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 2, null, null, 0, null, null, 3 },
                    { 6, 1, 1, 3, 1, new DateTime(2017, 10, 18, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 0, null, null, 1, null, null, 3 },
                    { 4, 1, 1, 3, 2, new DateTime(2017, 9, 27, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 1, null, null, 4, null, null, 3 },
                    { 9, 1, 1, 1, 2, new DateTime(2017, 11, 22, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 2, null, null, 0, null, null, 2 },
                    { 10, 1, 1, 3, 4, new DateTime(2017, 11, 22, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 1, null, null, 0, null, null, 4 },
                    { 22, 1, 2, 6, 8, new DateTime(2017, 11, 22, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 7, null, null, 1, null, null, 8 },
                    { 23, 1, 2, 8, 7, new DateTime(2017, 12, 5, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 3, null, null, 1, null, null, 7 },
                    { 25, 1, 3, 9, 10, new DateTime(2017, 9, 12, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 6, null, null, 0, null, null, 10 },
                    { 43, 1, 4, 16, 13, new DateTime(2017, 10, 31, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 1, null, null, 1, null, null, 0 },
                    { 41, 1, 4, 13, 16, new DateTime(2017, 10, 18, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 2, null, null, 1, null, null, 16 },
                    { 40, 1, 4, 14, 16, new DateTime(2017, 9, 27, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 2, null, null, 0, null, null, 16 },
                    { 37, 1, 4, 16, 15, new DateTime(2017, 9, 12, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 3, null, null, 0, null, null, 15 },
                    { 47, 1, 4, 13, 15, new DateTime(2017, 12, 5, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 2, null, null, 0, null, null, 15 },
                    { 44, 1, 4, 15, 14, new DateTime(2017, 10, 31, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 0, null, null, 0, null, null, 0 },
                    { 42, 1, 4, 14, 15, new DateTime(2017, 10, 18, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 3, null, null, 1, null, null, 15 },
                    { 39, 1, 4, 15, 13, new DateTime(2017, 9, 27, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 0, null, null, 1, null, null, 15 },
                    { 46, 1, 4, 14, 13, new DateTime(2017, 11, 22, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 3, null, null, 1, null, null, 13 },
                    { 38, 1, 4, 13, 14, new DateTime(2017, 9, 12, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 2, null, null, 3, null, null, 13 },
                    { 36, 1, 3, 9, 12, new DateTime(2017, 12, 5, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 1, null, null, 0, null, null, 12 }
                });

            migrationBuilder.InsertData(
                table: "Match",
                columns: new[] { "Id", "CompetitionId", "GroupId", "GuestTeamId", "HomeTeamId", "PlayedAt", "RoundId", "Score1", "Score1ET", "Score1P", "Score2", "Score2ET", "Score2P", "Winner" },
                values: new object[,]
                {
                    { 34, 1, 3, 12, 11, new DateTime(2017, 11, 22, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 2, null, null, 0, null, null, 11 },
                    { 32, 1, 3, 10, 12, new DateTime(2017, 10, 31, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 3, null, null, 0, null, null, 12 },
                    { 30, 1, 3, 12, 10, new DateTime(2017, 10, 18, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 3, null, null, 3, null, null, 0 },
                    { 27, 1, 3, 12, 9, new DateTime(2017, 9, 27, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 1, null, null, 2, null, null, 12 },
                    { 26, 1, 3, 11, 12, new DateTime(2017, 9, 12, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 0, null, null, 0, null, null, 0 },
                    { 35, 1, 3, 11, 10, new DateTime(2017, 12, 5, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 1, null, null, 1, null, null, 0 },
                    { 31, 1, 3, 9, 11, new DateTime(2017, 10, 31, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 1, null, null, 1, null, null, 0 },
                    { 29, 1, 3, 11, 9, new DateTime(2017, 10, 18, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 0, null, null, 0, null, null, 0 },
                    { 28, 1, 3, 10, 11, new DateTime(2017, 9, 27, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 1, null, null, 2, null, null, 10 },
                    { 33, 1, 3, 10, 9, new DateTime(2017, 11, 22, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 0, null, null, 4, null, null, 10 },
                    { 93, 1, 8, 32, 29, new DateTime(2017, 11, 21, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 0, null, null, 6, null, null, 32 },
                    { 95, 1, 8, 30, 32, new DateTime(2017, 12, 6, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 3, null, null, 2, null, null, 32 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Competition_LeagueId",
                table: "Competition",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_Competition_SeasonId",
                table: "Competition",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_Group_CompetitionId",
                table: "Group",
                column: "CompetitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Match_CompetitionId",
                table: "Match",
                column: "CompetitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Match_GroupId",
                table: "Match",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Match_GuestTeamId",
                table: "Match",
                column: "GuestTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Match_HomeTeamId",
                table: "Match",
                column: "HomeTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Match_RoundId",
                table: "Match",
                column: "RoundId");

            migrationBuilder.CreateIndex(
                name: "IX_Round_CompetitionId",
                table: "Round",
                column: "CompetitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Team_CompetitionId",
                table: "Team",
                column: "CompetitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Team_GroupId",
                table: "Team",
                column: "GroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Match");

            migrationBuilder.DropTable(
                name: "Round");

            migrationBuilder.DropTable(
                name: "Team");

            migrationBuilder.DropTable(
                name: "Group");

            migrationBuilder.DropTable(
                name: "Competition");

            migrationBuilder.DropTable(
                name: "League");

            migrationBuilder.DropTable(
                name: "Season");
        }
    }
}
