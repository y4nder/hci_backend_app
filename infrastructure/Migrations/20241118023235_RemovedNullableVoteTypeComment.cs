﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemovedNullableVoteTypeComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "vote_type",
                table: "UserCommentVote",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0,
                oldClrType: typeof(short),
                oldType: "smallint",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "vote_type",
                table: "UserCommentVote",
                type: "smallint",
                nullable: true,
                oldClrType: typeof(short),
                oldType: "smallint");
        }
    }
}
