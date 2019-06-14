using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.Data.Migrations
{
    public partial class CobraKaiDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "CobraKai");

            migrationBuilder.CreateTable(
                name: "MusicLists",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PersonId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Songs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    Artist = table.Column<string>(nullable: true),
                    Genre = table.Column<string>(nullable: true),
                    Size = table.Column<decimal>(nullable: true),
                    Length = table.Column<string>(nullable: true),
                    ReleaseDate = table.Column<string>(nullable: true),
                    FilePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Songs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MusicListId = table.Column<int>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Firstname = table.Column<string>(nullable: true),
                    Lastname = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                    table.ForeignKey(
                        name: "FK_People_MusicLists_MusicListId",
                        column: x => x.MusicListId,
                        principalTable: "MusicLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Journals",
                schema: "CobraKai",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PersonId = table.Column<int>(nullable: true),
                    SongId = table.Column<int>(nullable: true),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Journals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Journals_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Journals_Songs_SongId",
                        column: x => x.SongId,
                        principalTable: "Songs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Playlists",
                schema: "CobraKai",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PersonId = table.Column<int>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Playlists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Playlists_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ListEntries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PlayListId = table.Column<int>(nullable: true),
                    MusicListId = table.Column<int>(nullable: true),
                    Favorite = table.Column<bool>(nullable: true),
                    TimeStamp = table.Column<string>(nullable: true),
                    JournalEntry = table.Column<string>(nullable: true),
                    SonglistId = table.Column<int>(nullable: true),
                    SongId = table.Column<int>(nullable: true),
                    JournalId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ListEntries_Journals_JournalId",
                        column: x => x.JournalId,
                        principalSchema: "CobraKai",
                        principalTable: "Journals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ListEntries_Playlists_MusicListId",
                        column: x => x.MusicListId,
                        principalSchema: "CobraKai",
                        principalTable: "Playlists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ListEntries_Songs_SongId",
                        column: x => x.SongId,
                        principalTable: "Songs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ListEntries_MusicLists_SonglistId",
                        column: x => x.SonglistId,
                        principalTable: "MusicLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ListEntries_JournalId",
                table: "ListEntries",
                column: "JournalId");

            migrationBuilder.CreateIndex(
                name: "IX_ListEntries_MusicListId",
                table: "ListEntries",
                column: "MusicListId");

            migrationBuilder.CreateIndex(
                name: "IX_ListEntries_SongId",
                table: "ListEntries",
                column: "SongId");

            migrationBuilder.CreateIndex(
                name: "IX_ListEntries_SonglistId",
                table: "ListEntries",
                column: "SonglistId");

            migrationBuilder.CreateIndex(
                name: "IX_People_MusicListId",
                table: "People",
                column: "MusicListId");

            migrationBuilder.CreateIndex(
                name: "IX_Journals_PersonId",
                schema: "CobraKai",
                table: "Journals",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Journals_SongId",
                schema: "CobraKai",
                table: "Journals",
                column: "SongId");

            migrationBuilder.CreateIndex(
                name: "IX_Playlists_PersonId",
                schema: "CobraKai",
                table: "Playlists",
                column: "PersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ListEntries");

            migrationBuilder.DropTable(
                name: "Journals",
                schema: "CobraKai");

            migrationBuilder.DropTable(
                name: "Playlists",
                schema: "CobraKai");

            migrationBuilder.DropTable(
                name: "Songs");

            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "MusicLists");
        }
    }
}
