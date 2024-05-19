﻿#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace QuantumCore.Game.Persistence.Migrations.Sqlite
{
    /// <inheritdoc />
    public partial class AddGuild : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<uint>(
                name: "GuildId",
                table: "Players",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Guilds",
                columns: table => new
                {
                    Id = table.Column<uint>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 12, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false,
                        defaultValueSql: "current_timestamp"),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false,
                        defaultValueSql: "current_timestamp"),
                    OwnerId = table.Column<uint>(type: "INTEGER", nullable: false),
                    Level = table.Column<byte>(type: "INTEGER", nullable: false),
                    Experience = table.Column<uint>(type: "INTEGER", nullable: false),
                    MaxMemberCount = table.Column<ushort>(type: "INTEGER", nullable: false),
                    Gold = table.Column<uint>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guilds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Guilds_Players_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GuildRanks",
                columns: table => new
                {
                    GuildId = table.Column<uint>(type: "INTEGER", nullable: false),
                    Rank = table.Column<byte>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 8, nullable: false),
                    Permissions = table.Column<byte>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuildRanks", x => new {x.GuildId, x.Rank});
                    table.ForeignKey(
                        name: "FK_GuildRanks_Guilds_GuildId",
                        column: x => x.GuildId,
                        principalTable: "Guilds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GuildMembers",
                columns: table => new
                {
                    GuildId = table.Column<uint>(type: "INTEGER", nullable: false),
                    PlayerId = table.Column<uint>(type: "INTEGER", nullable: false),
                    RankId = table.Column<byte>(type: "INTEGER", nullable: false),
                    IsLeader = table.Column<bool>(type: "INTEGER", nullable: false),
                    SpentExperience = table.Column<uint>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuildMembers", x => new {x.GuildId, x.PlayerId});
                    table.ForeignKey(
                        name: "FK_GuildMembers_GuildRanks_GuildId_RankId",
                        columns: x => new {x.GuildId, x.RankId},
                        principalTable: "GuildRanks",
                        principalColumns: new[] {"GuildId", "Rank"},
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GuildMembers_Guilds_GuildId",
                        column: x => x.GuildId,
                        principalTable: "Guilds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GuildMembers_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1u,
                column: "GuildId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Players_GuildId",
                table: "Players",
                column: "GuildId");

            migrationBuilder.CreateIndex(
                name: "IX_GuildMembers_GuildId_RankId",
                table: "GuildMembers",
                columns: new[] {"GuildId", "RankId"});

            migrationBuilder.CreateIndex(
                name: "IX_GuildMembers_PlayerId",
                table: "GuildMembers",
                column: "PlayerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Guilds_OwnerId",
                table: "Guilds",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Guilds_GuildId",
                table: "Players",
                column: "GuildId",
                principalTable: "Guilds",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Guilds_GuildId",
                table: "Players");

            migrationBuilder.DropTable(
                name: "GuildMembers");

            migrationBuilder.DropTable(
                name: "GuildRanks");

            migrationBuilder.DropTable(
                name: "Guilds");

            migrationBuilder.DropIndex(
                name: "IX_Players_GuildId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "GuildId",
                table: "Players");
        }
    }
}