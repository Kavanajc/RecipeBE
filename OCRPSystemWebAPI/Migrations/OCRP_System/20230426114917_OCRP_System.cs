using Microsoft.EntityFrameworkCore.Migrations;

namespace OCRPSystemWebAPI.Migrations.OCRP_System
{
    public partial class OCRP_System : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Category_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category_Name = table.Column<string>(unicode: false, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Category_ID);
                });

            migrationBuilder.CreateTable(
                name: "State",
                columns: table => new
                {
                    state_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    State_Name = table.Column<string>(unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_State", x => x.state_ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    User_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_Name = table.Column<string>(unicode: false, maxLength: 255, nullable: false),
                    Password = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    User_Type = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
                    Email_Id = table.Column<string>(unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__206D9190ED116D69", x => x.User_ID);
                });

            migrationBuilder.CreateTable(
                name: "Recipe",
                columns: table => new
                {
                    Recipe_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    Ingredients = table.Column<string>(unicode: false, nullable: true),
                    Recipe_steps = table.Column<string>(unicode: false, nullable: true),
                    Serves = table.Column<int>(nullable: true),
                    Recipe_status = table.Column<string>(unicode: false, maxLength: 10, nullable: false, defaultValueSql: "('Pending')"),
                    Image_Url = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    Category_ID = table.Column<int>(nullable: false),
                    User_ID = table.Column<int>(nullable: false),
                    State_ID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipe", x => x.Recipe_Id);
                    table.ForeignKey(
                        name: "FK__Recipe__Category__46E78A0C",
                        column: x => x.Category_ID,
                        principalTable: "Category",
                        principalColumn: "Category_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Recipe__State_ID__48CFD27E",
                        column: x => x.State_ID,
                        principalTable: "State",
                        principalColumn: "state_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Recipe__User_ID__47DBAE45",
                        column: x => x.User_ID,
                        principalTable: "Users",
                        principalColumn: "User_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_Category_ID",
                table: "Recipe",
                column: "Category_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_State_ID",
                table: "Recipe",
                column: "State_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_User_ID",
                table: "Recipe",
                column: "User_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Recipe");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "State");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
