using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizApi.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddGoogleAuthFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AttemptId1",
                table: "UserAnswers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "CheatingDetected",
                table: "QuizAttempts",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "CheatingWarningsCount",
                table: "QuizAttempts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ExpectedOutput",
                table: "Questions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InitialCodeTemplate",
                table: "Questions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCodeQuestion",
                table: "Questions",
                type: "boolean",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "CheatingDetected",
                table: "QuizAttempts");

            migrationBuilder.DropColumn(
                name: "CheatingWarningsCount",
                table: "QuizAttempts");

            migrationBuilder.DropColumn(
                name: "ExpectedOutput",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "InitialCodeTemplate",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "IsCodeQuestion",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "QuestionId1",
                table: "QuestionOptions");
        }
    }
}
