using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizApi.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixShadowPropertiesFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionOptions_Questions_QuestionId1",
                table: "QuestionOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswers_QuizAttempts_AttemptId1",
                table: "UserAnswers");

            migrationBuilder.DropIndex(
                name: "IX_UserAnswers_AttemptId1",
                table: "UserAnswers");

            migrationBuilder.DropIndex(
                name: "IX_QuestionOptions_QuestionId1",
                table: "QuestionOptions");

            migrationBuilder.DropColumn(
                name: "AttemptId1",
                table: "UserAnswers");

            migrationBuilder.DropColumn(
                name: "QuestionId1",
                table: "QuestionOptions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AttemptId1",
                table: "UserAnswers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "QuestionId1",
                table: "QuestionOptions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswers_AttemptId1",
                table: "UserAnswers",
                column: "AttemptId1");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionOptions_QuestionId1",
                table: "QuestionOptions",
                column: "QuestionId1");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionOptions_Questions_QuestionId1",
                table: "QuestionOptions",
                column: "QuestionId1",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswers_QuizAttempts_AttemptId1",
                table: "UserAnswers",
                column: "AttemptId1",
                principalTable: "QuizAttempts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
