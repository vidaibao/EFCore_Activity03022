﻿using EFCore_DBLibrary.Migrations.Scripts;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCore_DBLibrary.Migrations
{
    public partial class CreateProc_GetItemsForListing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SqlResource("EFCore_DBLibrary.Migrations.Scripts.Procedures.GetItemsForListing.GetItemsForListing.v1.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("EFCore_DBLibrary.Migrations.Scripts.Procedures.GetItemsForListing.GetItemsForListing.v0.sql");
        }
    }
}
