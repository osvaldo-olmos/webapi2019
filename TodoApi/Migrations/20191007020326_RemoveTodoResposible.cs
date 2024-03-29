﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoApi.Migrations
{
    public partial class RemoveTodoResposible : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoItems_AspNetUsers_ResponsibleId",
                table: "TodoItems");

            migrationBuilder.DropIndex(
                name: "IX_TodoItems_ResponsibleId",
                table: "TodoItems");

            migrationBuilder.DropColumn(
                name: "ResponsibleId",
                table: "TodoItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ResponsibleId",
                table: "TodoItems",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TodoItems_ResponsibleId",
                table: "TodoItems",
                column: "ResponsibleId");

            migrationBuilder.AddForeignKey(
                name: "FK_TodoItems_AspNetUsers_ResponsibleId",
                table: "TodoItems",
                column: "ResponsibleId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
