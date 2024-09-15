using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentCarApi.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PromoCode_CompanyDatas_CompanyDataId",
                table: "PromoCode");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PromoCode",
                table: "PromoCode");

            migrationBuilder.RenameTable(
                name: "PromoCode",
                newName: "PromoCodes");

            migrationBuilder.RenameIndex(
                name: "IX_PromoCode_CompanyDataId",
                table: "PromoCodes",
                newName: "IX_PromoCodes_CompanyDataId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PromoCodes",
                table: "PromoCodes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PromoCodes_CompanyDatas_CompanyDataId",
                table: "PromoCodes",
                column: "CompanyDataId",
                principalTable: "CompanyDatas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PromoCodes_CompanyDatas_CompanyDataId",
                table: "PromoCodes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PromoCodes",
                table: "PromoCodes");

            migrationBuilder.RenameTable(
                name: "PromoCodes",
                newName: "PromoCode");

            migrationBuilder.RenameIndex(
                name: "IX_PromoCodes_CompanyDataId",
                table: "PromoCode",
                newName: "IX_PromoCode_CompanyDataId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PromoCode",
                table: "PromoCode",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PromoCode_CompanyDatas_CompanyDataId",
                table: "PromoCode",
                column: "CompanyDataId",
                principalTable: "CompanyDatas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
