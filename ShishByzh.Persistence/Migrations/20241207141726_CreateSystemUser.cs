using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations;
using ShishByzh.Domain.Users;

#nullable disable

namespace ShishByzh.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreateSystemUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Добавление начальных данных в таблицу пользователей
            var user = new User("System User")
            {
                Id = Guid.NewGuid(),
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@example.com",
                NormalizedEmail = "ADMIN@EXAMPLE.COM",
                EmailConfirmed = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                PhoneNumber = string.Empty,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = false,
                AccessFailedCount = 0
            };

            //user.PasswordHash = new PasswordHasher<User>().HashPassword(user, "pass");
            user.PasswordHash = "AQAAAAIAAYagAAAAEJZ1OV6Rt7V+8xU1LXvE4s8YYupdegWG/GcV89iP3qTAhWIlXEsUwBd5PmYrNif3QQ==";
            
            migrationBuilder.InsertData(
                table: "AspNetUsers", // Имя таблицы пользователей в Identity
                columns: new[] { "Id", "Fio", "UserName", "NormalizedUserName", "Email", "NormalizedEmail", "EmailConfirmed", 
                    "PasswordHash", "SecurityStamp", "ConcurrencyStamp", "PhoneNumber", "PhoneNumberConfirmed",
                    "TwoFactorEnabled", "LockoutEnd", "LockoutEnabled", "AccessFailedCount" },
                values: new object[] {
                    user.Id,
                    user.Fio,
                    user.UserName,
                    user.NormalizedUserName,
                    user.Email,
                    user.NormalizedEmail,
                    user.EmailConfirmed,
                    user.PasswordHash,
                    user.SecurityStamp,
                    user.ConcurrencyStamp,
                    user.PhoneNumber,
                    user.PhoneNumberConfirmed,
                    user.TwoFactorEnabled,
                    user.LockoutEnd,
                    user.LockoutEnabled,
                    user.AccessFailedCount
                });

            // Добавление роли архитектора
            var architectRoleId = Guid.NewGuid();
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[] { architectRoleId, "Architect", "ARCHITECT", Guid.NewGuid().ToString() });

            // Привязка пользователя к роли
            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { user.Id, architectRoleId });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Удаление данных при откате миграции
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "UserName",
                keyValue: "admin");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Name",
                keyValue: "Architect");
        }
    }
}
