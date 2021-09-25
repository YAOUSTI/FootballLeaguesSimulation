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
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    HomeTeamId = table.Column<int>(type: "int", nullable: false),
                    GuestTeamId = table.Column<int>(type: "int", nullable: false),
                    CompetitionId = table.Column<int>(type: "int", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: true),
                    RoundId = table.Column<int>(type: "int", nullable: false)
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
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Match_Team_GuestTeamId",
                        column: x => x.GuestTeamId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Match_Team_HomeTeamId",
                        column: x => x.HomeTeamId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "TeamStanding",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchPlayed = table.Column<int>(type: "int", nullable: false),
                    Wins = table.Column<int>(type: "int", nullable: false),
                    Loses = table.Column<int>(type: "int", nullable: false),
                    Draws = table.Column<int>(type: "int", nullable: false),
                    GoalsFor = table.Column<int>(type: "int", nullable: false),
                    GoalsAgaints = table.Column<int>(type: "int", nullable: false),
                    GoalsDifference = table.Column<int>(type: "int", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false),
                    Leg = table.Column<int>(type: "int", nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: false),
                    SeasonId = table.Column<int>(type: "int", nullable: false),
                    CompetitionId = table.Column<int>(type: "int", nullable: false),
                    RoundId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamStanding", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamStanding_Competition_CompetitionId",
                        column: x => x.CompetitionId,
                        principalTable: "Competition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamStanding_Round_RoundId",
                        column: x => x.RoundId,
                        principalTable: "Round",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_TeamStanding_Season_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Season",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_TeamStanding_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "League",
                columns: new[] { "Id", "Logo", "Name" },
                values: new object[] { 1, "https://w7.pngwing.com/pngs/145/829/png-transparent-uefa-champions-league-uefa-europa-league-uefa-super-cup-uefa-euro-2016-uefa-euro-2020-croatia-football-federation-blue-emblem-text-thumbnail.png", "EUFA" });

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
                    { 8, 1, "Group H" },
                    { 9, 1, "Null" }
                });

            migrationBuilder.InsertData(
                table: "Round",
                columns: new[] { "Id", "CompetitionId", "EndAt", "Name", "StartAt" },
                values: new object[,]
                {
                    { 1, 1, null, "Groups", null },
                    { 2, 1, null, "Round of 16", null },
                    { 3, 1, null, "Quarter Finals", null },
                    { 4, 1, null, "Semi Final", null },
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

            migrationBuilder.CreateIndex(
                name: "IX_TeamStanding_CompetitionId",
                table: "TeamStanding",
                column: "CompetitionId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamStanding_RoundId",
                table: "TeamStanding",
                column: "RoundId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamStanding_SeasonId",
                table: "TeamStanding",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamStanding_TeamId",
                table: "TeamStanding",
                column: "TeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Match");

            migrationBuilder.DropTable(
                name: "TeamStanding");

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
