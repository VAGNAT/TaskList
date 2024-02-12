using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskList.Infrastructure.PostgreSql.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaskList",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    Owner = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskList", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Task",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    AddDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TaskListId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Task_TaskList_TaskListId",
                        column: x => x.TaskListId,
                        principalTable: "TaskList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    TaskID = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comment_Task_TaskID",
                        column: x => x.TaskID,
                        principalTable: "Task",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StatusTaskHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<string>(type: "varchar", nullable: false),
                    AddDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TaskId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusTaskHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StatusTaskHistory_Task_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Task",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TaskList",
                columns: new[] { "Id", "Description", "Name", "Owner" },
                values: new object[,]
                {
                    { new Guid("00a4b751-07f5-437a-b6da-8f5fcec4bdc9"), "Описание первого списка задач", "Первый список задач", "user@example.com" },
                    { new Guid("04d38219-3cf3-4bf4-9604-f54a7f244833"), "Описание второго списка задач", "Второй список задач", "user@example.com" },
                    { new Guid("f395367c-ddb7-40e2-ba9c-844c3965adf6"), "Описание третьего списка задач", "Третий список задач", "user@example.com" }
                });

            migrationBuilder.InsertData(
                table: "Task",
                columns: new[] { "Id", "AddDate", "Description", "Name", "TaskListId" },
                values: new object[,]
                {
                    { new Guid("17591683-782c-4440-878a-7e286081a81e"), new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Описание задачи 3 листа Второй список задач", "Задача 3 листа Второй список задач", new Guid("04d38219-3cf3-4bf4-9604-f54a7f244833") },
                    { new Guid("24ce9f1c-0f6e-4c26-bf40-624535ec8730"), new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Описание задачи 10 листа Третий список задач", "Задача 10 листа Третий список задач", new Guid("f395367c-ddb7-40e2-ba9c-844c3965adf6") },
                    { new Guid("4ef740ed-732e-4ca1-b404-80678ba6cbc1"), new DateTime(2024, 2, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Описание задачи 9 листа Второй список задач", "Задача 9 листа Второй список задач", new Guid("04d38219-3cf3-4bf4-9604-f54a7f244833") },
                    { new Guid("5561d361-33ba-4bf0-8c9d-f5f035bf9807"), new DateTime(2024, 2, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Описание задачи 5 листа Первый список задач", "Задача 5 листа Первый список задач", new Guid("00a4b751-07f5-437a-b6da-8f5fcec4bdc9") },
                    { new Guid("599bab9c-6cb8-4196-9ceb-9afc74254247"), new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Описание задачи 5 листа Второй список задач", "Задача 5 листа Второй список задач", new Guid("04d38219-3cf3-4bf4-9604-f54a7f244833") },
                    { new Guid("6457f588-6bf7-413f-9685-70ffd90e6ba6"), new DateTime(2024, 2, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Описание задачи 7 листа Третий список задач", "Задача 7 листа Третий список задач", new Guid("f395367c-ddb7-40e2-ba9c-844c3965adf6") },
                    { new Guid("6b4bbae9-2200-49b7-8f2f-d1f240050cec"), new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Описание задачи 7 листа Первый список задач", "Задача 7 листа Первый список задач", new Guid("00a4b751-07f5-437a-b6da-8f5fcec4bdc9") },
                    { new Guid("6e17e8f2-620e-4bab-b4b2-971d1acc9be2"), new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Описание задачи 8 листа Первый список задач", "Задача 8 листа Первый список задач", new Guid("00a4b751-07f5-437a-b6da-8f5fcec4bdc9") },
                    { new Guid("76e0e6b2-932e-450e-b944-86ccb7ff0bff"), new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Описание задачи 8 листа Второй список задач", "Задача 8 листа Второй список задач", new Guid("04d38219-3cf3-4bf4-9604-f54a7f244833") },
                    { new Guid("7d9fdaf5-e7c4-4d91-aa1f-31bf51571c3a"), new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Описание задачи 3 листа Третий список задач", "Задача 3 листа Третий список задач", new Guid("f395367c-ddb7-40e2-ba9c-844c3965adf6") },
                    { new Guid("7de0edfc-5e64-4d14-b38b-f5c0ba5ea34c"), new DateTime(2024, 2, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Описание задачи 1 листа Третий список задач", "Задача 1 листа Третий список задач", new Guid("f395367c-ddb7-40e2-ba9c-844c3965adf6") },
                    { new Guid("8541f399-22c4-4f04-a928-612b4a37edab"), new DateTime(2024, 2, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Описание задачи 9 листа Третий список задач", "Задача 9 листа Третий список задач", new Guid("f395367c-ddb7-40e2-ba9c-844c3965adf6") },
                    { new Guid("865430dd-32fe-4673-93db-f18a29e9a1b8"), new DateTime(2024, 2, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Описание задачи 9 листа Первый список задач", "Задача 9 листа Первый список задач", new Guid("00a4b751-07f5-437a-b6da-8f5fcec4bdc9") },
                    { new Guid("86bab430-5e0a-4f62-9163-aeeb7aa9bc2a"), new DateTime(2024, 2, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Описание задачи 2 листа Второй список задач", "Задача 2 листа Второй список задач", new Guid("04d38219-3cf3-4bf4-9604-f54a7f244833") },
                    { new Guid("88f9453d-301b-4f3f-a1f7-50087e98c3c5"), new DateTime(2024, 2, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Описание задачи 10 листа Первый список задач", "Задача 10 листа Первый список задач", new Guid("00a4b751-07f5-437a-b6da-8f5fcec4bdc9") },
                    { new Guid("939eb2d4-439c-4491-956d-82b639aa42bc"), new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Описание задачи 7 листа Второй список задач", "Задача 7 листа Второй список задач", new Guid("04d38219-3cf3-4bf4-9604-f54a7f244833") },
                    { new Guid("a182168e-d522-4a08-b2d4-96080f7a80b8"), new DateTime(2024, 2, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Описание задачи 5 листа Третий список задач", "Задача 5 листа Третий список задач", new Guid("f395367c-ddb7-40e2-ba9c-844c3965adf6") },
                    { new Guid("a19cff3c-921f-4b73-b482-27aca75eaae3"), new DateTime(2024, 2, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Описание задачи 6 листа Первый список задач", "Задача 6 листа Первый список задач", new Guid("00a4b751-07f5-437a-b6da-8f5fcec4bdc9") },
                    { new Guid("a458d036-0f19-49cd-91ff-5ce9b7a19bc8"), new DateTime(2024, 2, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Описание задачи 4 листа Второй список задач", "Задача 4 листа Второй список задач", new Guid("04d38219-3cf3-4bf4-9604-f54a7f244833") },
                    { new Guid("a47b01b1-c7dc-4766-8f3c-ea0451b91741"), new DateTime(2024, 2, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Описание задачи 8 листа Третий список задач", "Задача 8 листа Третий список задач", new Guid("f395367c-ddb7-40e2-ba9c-844c3965adf6") },
                    { new Guid("a68c09fe-11e3-4a52-b76b-b56151ea4a35"), new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Описание задачи 2 листа Третий список задач", "Задача 2 листа Третий список задач", new Guid("f395367c-ddb7-40e2-ba9c-844c3965adf6") },
                    { new Guid("bd3cf78d-82c4-4bd3-b575-76ae7a4b9197"), new DateTime(2024, 2, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Описание задачи 3 листа Первый список задач", "Задача 3 листа Первый список задач", new Guid("00a4b751-07f5-437a-b6da-8f5fcec4bdc9") },
                    { new Guid("c5607256-205a-48c7-8b4f-c7fa4bd684cc"), new DateTime(2024, 2, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Описание задачи 4 листа Первый список задач", "Задача 4 листа Первый список задач", new Guid("00a4b751-07f5-437a-b6da-8f5fcec4bdc9") },
                    { new Guid("d4e7ff20-43dd-430d-9fcb-ad8523f62b0c"), new DateTime(2024, 2, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Описание задачи 1 листа Второй список задач", "Задача 1 листа Второй список задач", new Guid("04d38219-3cf3-4bf4-9604-f54a7f244833") },
                    { new Guid("d8093abf-dcd5-449a-8754-f90c34fb3982"), new DateTime(2024, 2, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Описание задачи 6 листа Третий список задач", "Задача 6 листа Третий список задач", new Guid("f395367c-ddb7-40e2-ba9c-844c3965adf6") },
                    { new Guid("de889a60-0f47-4341-acce-2414514d91a8"), new DateTime(2024, 2, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Описание задачи 2 листа Первый список задач", "Задача 2 листа Первый список задач", new Guid("00a4b751-07f5-437a-b6da-8f5fcec4bdc9") },
                    { new Guid("ea83e531-7985-4341-afdf-be9d43e79bfc"), new DateTime(2024, 2, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Описание задачи 4 листа Третий список задач", "Задача 4 листа Третий список задач", new Guid("f395367c-ddb7-40e2-ba9c-844c3965adf6") },
                    { new Guid("eceec90e-5144-4f28-808d-c7f2c1bef010"), new DateTime(2024, 2, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Описание задачи 1 листа Первый список задач", "Задача 1 листа Первый список задач", new Guid("00a4b751-07f5-437a-b6da-8f5fcec4bdc9") },
                    { new Guid("ed207dfa-d8ed-40f9-be96-24d5be74c9a9"), new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Описание задачи 6 листа Второй список задач", "Задача 6 листа Второй список задач", new Guid("04d38219-3cf3-4bf4-9604-f54a7f244833") },
                    { new Guid("f5a697d1-b2bf-40c9-863b-6bd43d27595d"), new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Описание задачи 10 листа Второй список задач", "Задача 10 листа Второй список задач", new Guid("04d38219-3cf3-4bf4-9604-f54a7f244833") }
                });

            migrationBuilder.InsertData(
                table: "Comment",
                columns: new[] { "Id", "Content", "TaskID" },
                values: new object[,]
                {
                    { new Guid("0ccc3c22-4205-4b93-9185-44bef009118d"), "Комментарий 1 к задаче Задача 6 листа Третий список задач", new Guid("d8093abf-dcd5-449a-8754-f90c34fb3982") },
                    { new Guid("0e23c4e2-c65c-4018-947d-8f732863584c"), "Комментарий 1 к задаче Задача 8 листа Третий список задач", new Guid("a47b01b1-c7dc-4766-8f3c-ea0451b91741") },
                    { new Guid("22e9a69a-f940-4532-a14b-134111bd2f98"), "Комментарий 1 к задаче Задача 7 листа Второй список задач", new Guid("939eb2d4-439c-4491-956d-82b639aa42bc") },
                    { new Guid("291d7479-1524-47dc-a08e-4294b7b6db76"), "Комментарий 1 к задаче Задача 5 листа Второй список задач", new Guid("599bab9c-6cb8-4196-9ceb-9afc74254247") },
                    { new Guid("2990667d-0a6b-466e-875a-29a3cd3a6f06"), "Комментарий 2 к задаче Задача 5 листа Второй список задач", new Guid("599bab9c-6cb8-4196-9ceb-9afc74254247") },
                    { new Guid("2a0ce373-bbe6-48be-ab58-90262943f065"), "Комментарий 2 к задаче Задача 4 листа Третий список задач", new Guid("ea83e531-7985-4341-afdf-be9d43e79bfc") },
                    { new Guid("2a43a44d-2089-4e2f-bec7-60ab2d765676"), "Комментарий 1 к задаче Задача 7 листа Третий список задач", new Guid("6457f588-6bf7-413f-9685-70ffd90e6ba6") },
                    { new Guid("34a4ddce-02b6-45db-85ff-f92254ad3a61"), "Комментарий 1 к задаче Задача 5 листа Первый список задач", new Guid("5561d361-33ba-4bf0-8c9d-f5f035bf9807") },
                    { new Guid("3cd62217-e70e-4721-bfc6-cc16bbec3698"), "Комментарий 1 к задаче Задача 4 листа Третий список задач", new Guid("ea83e531-7985-4341-afdf-be9d43e79bfc") },
                    { new Guid("404b4055-1684-4402-84f5-a1e90f75189f"), "Комментарий 2 к задаче Задача 2 листа Второй список задач", new Guid("86bab430-5e0a-4f62-9163-aeeb7aa9bc2a") },
                    { new Guid("442a5cf3-d04d-472d-bf4f-57c1cacfbafa"), "Комментарий 2 к задаче Задача 10 листа Третий список задач", new Guid("24ce9f1c-0f6e-4c26-bf40-624535ec8730") },
                    { new Guid("46c0dbc8-26ab-40f0-b08c-90632efce36e"), "Комментарий 2 к задаче Задача 1 листа Второй список задач", new Guid("d4e7ff20-43dd-430d-9fcb-ad8523f62b0c") },
                    { new Guid("4e90dfaf-45d0-434d-9b10-1968f323d8e6"), "Комментарий 1 к задаче Задача 1 листа Первый список задач", new Guid("eceec90e-5144-4f28-808d-c7f2c1bef010") },
                    { new Guid("53f4be9d-bb46-4c17-b9db-66855e8ab1d7"), "Комментарий 2 к задаче Задача 5 листа Третий список задач", new Guid("a182168e-d522-4a08-b2d4-96080f7a80b8") },
                    { new Guid("563c54a2-06fd-446e-a9aa-81c0097b2e4c"), "Комментарий 2 к задаче Задача 3 листа Второй список задач", new Guid("17591683-782c-4440-878a-7e286081a81e") },
                    { new Guid("56adc2bf-a70d-4bd3-9741-f05d9b1f64dd"), "Комментарий 1 к задаче Задача 8 листа Первый список задач", new Guid("6e17e8f2-620e-4bab-b4b2-971d1acc9be2") },
                    { new Guid("583606dd-edbc-4419-a985-4e0ebb74972c"), "Комментарий 1 к задаче Задача 10 листа Третий список задач", new Guid("24ce9f1c-0f6e-4c26-bf40-624535ec8730") },
                    { new Guid("5f457cb2-de0d-4184-b760-9ea8d3d1bf58"), "Комментарий 1 к задаче Задача 3 листа Второй список задач", new Guid("17591683-782c-4440-878a-7e286081a81e") },
                    { new Guid("61030ea7-17ca-4dab-a394-0da6b353339b"), "Комментарий 1 к задаче Задача 7 листа Первый список задач", new Guid("6b4bbae9-2200-49b7-8f2f-d1f240050cec") },
                    { new Guid("65e2c31a-42f4-476c-8994-5ea0d8c5a454"), "Комментарий 1 к задаче Задача 4 листа Первый список задач", new Guid("c5607256-205a-48c7-8b4f-c7fa4bd684cc") },
                    { new Guid("6a1114ad-32ac-4dfa-bb13-fd0e983a127b"), "Комментарий 1 к задаче Задача 10 листа Второй список задач", new Guid("f5a697d1-b2bf-40c9-863b-6bd43d27595d") },
                    { new Guid("6ae6b928-6288-4170-bc04-737f153c3eb1"), "Комментарий 1 к задаче Задача 2 листа Первый список задач", new Guid("de889a60-0f47-4341-acce-2414514d91a8") },
                    { new Guid("6b32ee15-ae69-4a99-b52e-45b2f81b59c4"), "Комментарий 1 к задаче Задача 9 листа Второй список задач", new Guid("4ef740ed-732e-4ca1-b404-80678ba6cbc1") },
                    { new Guid("6bed42f4-7c2d-4ff5-8a7c-e0a61f2c0b6d"), "Комментарий 1 к задаче Задача 9 листа Первый список задач", new Guid("865430dd-32fe-4673-93db-f18a29e9a1b8") },
                    { new Guid("6c9f0f72-962d-4c76-9c19-90c9d5037754"), "Комментарий 1 к задаче Задача 1 листа Второй список задач", new Guid("d4e7ff20-43dd-430d-9fcb-ad8523f62b0c") },
                    { new Guid("6d242961-7a08-4c30-be08-e3b8fe9c69ef"), "Комментарий 2 к задаче Задача 10 листа Второй список задач", new Guid("f5a697d1-b2bf-40c9-863b-6bd43d27595d") },
                    { new Guid("836be0e7-0aab-4e2e-91d5-69ef0175061c"), "Комментарий 2 к задаче Задача 3 листа Третий список задач", new Guid("7d9fdaf5-e7c4-4d91-aa1f-31bf51571c3a") },
                    { new Guid("8f2385ab-1127-40c6-a498-30a28a4522cf"), "Комментарий 1 к задаче Задача 6 листа Второй список задач", new Guid("ed207dfa-d8ed-40f9-be96-24d5be74c9a9") },
                    { new Guid("90fb58e8-f731-4deb-ad3a-f79e49232a5d"), "Комментарий 2 к задаче Задача 6 листа Третий список задач", new Guid("d8093abf-dcd5-449a-8754-f90c34fb3982") },
                    { new Guid("91706fb3-5df6-4da2-a23a-ce088bf943b3"), "Комментарий 2 к задаче Задача 7 листа Первый список задач", new Guid("6b4bbae9-2200-49b7-8f2f-d1f240050cec") },
                    { new Guid("923b6f75-9162-46c0-84cd-5acc2156abb0"), "Комментарий 1 к задаче Задача 2 листа Второй список задач", new Guid("86bab430-5e0a-4f62-9163-aeeb7aa9bc2a") },
                    { new Guid("940453a3-5a90-4cfa-ac51-6141fce7d1fe"), "Комментарий 2 к задаче Задача 1 листа Третий список задач", new Guid("7de0edfc-5e64-4d14-b38b-f5c0ba5ea34c") },
                    { new Guid("9911d738-039f-4ec7-81b9-0c428ea6f2cd"), "Комментарий 1 к задаче Задача 6 листа Первый список задач", new Guid("a19cff3c-921f-4b73-b482-27aca75eaae3") },
                    { new Guid("a266369e-6f54-445f-a160-b62ce8f2c9e9"), "Комментарий 1 к задаче Задача 3 листа Третий список задач", new Guid("7d9fdaf5-e7c4-4d91-aa1f-31bf51571c3a") },
                    { new Guid("a64f055e-bd3f-4d28-acc9-501f0dbf6fdb"), "Комментарий 1 к задаче Задача 10 листа Первый список задач", new Guid("88f9453d-301b-4f3f-a1f7-50087e98c3c5") },
                    { new Guid("a6e7c697-9ffe-4f31-a936-99a0c6ac6940"), "Комментарий 1 к задаче Задача 3 листа Первый список задач", new Guid("bd3cf78d-82c4-4bd3-b575-76ae7a4b9197") },
                    { new Guid("ac9be2c7-c982-456b-8a56-74374c8f30f2"), "Комментарий 2 к задаче Задача 3 листа Первый список задач", new Guid("bd3cf78d-82c4-4bd3-b575-76ae7a4b9197") },
                    { new Guid("ad530a93-867d-4415-a9d0-989085a201ce"), "Комментарий 2 к задаче Задача 6 листа Первый список задач", new Guid("a19cff3c-921f-4b73-b482-27aca75eaae3") },
                    { new Guid("ae0e52ca-ea4a-4a98-a1c2-1392d8c77969"), "Комментарий 2 к задаче Задача 6 листа Второй список задач", new Guid("ed207dfa-d8ed-40f9-be96-24d5be74c9a9") },
                    { new Guid("b78b4347-b82e-4f95-aa3f-4878f96b9e42"), "Комментарий 2 к задаче Задача 8 листа Третий список задач", new Guid("a47b01b1-c7dc-4766-8f3c-ea0451b91741") },
                    { new Guid("c3539a00-2355-445b-b73d-deafb9b75bdf"), "Комментарий 2 к задаче Задача 4 листа Второй список задач", new Guid("a458d036-0f19-49cd-91ff-5ce9b7a19bc8") },
                    { new Guid("c4c162fb-f3fd-4d27-be62-ec85cb790888"), "Комментарий 2 к задаче Задача 2 листа Первый список задач", new Guid("de889a60-0f47-4341-acce-2414514d91a8") },
                    { new Guid("cbfdc8d5-e382-4a28-b7d8-50b0500b4058"), "Комментарий 1 к задаче Задача 2 листа Третий список задач", new Guid("a68c09fe-11e3-4a52-b76b-b56151ea4a35") },
                    { new Guid("d61ed73c-6dc1-4f0a-9e8e-00c9075337d8"), "Комментарий 1 к задаче Задача 9 листа Третий список задач", new Guid("8541f399-22c4-4f04-a928-612b4a37edab") },
                    { new Guid("d7bc58da-8178-4442-958e-0bd0d06ce5d9"), "Комментарий 2 к задаче Задача 7 листа Второй список задач", new Guid("939eb2d4-439c-4491-956d-82b639aa42bc") },
                    { new Guid("d86910e8-b5c0-47f4-97f5-5e4e5eb26990"), "Комментарий 1 к задаче Задача 8 листа Второй список задач", new Guid("76e0e6b2-932e-450e-b944-86ccb7ff0bff") },
                    { new Guid("ddb31317-5116-4e1f-ae68-dc1f1037f9ed"), "Комментарий 1 к задаче Задача 4 листа Второй список задач", new Guid("a458d036-0f19-49cd-91ff-5ce9b7a19bc8") },
                    { new Guid("e38e502b-331f-470e-b6a0-172a373e991d"), "Комментарий 2 к задаче Задача 9 листа Второй список задач", new Guid("4ef740ed-732e-4ca1-b404-80678ba6cbc1") },
                    { new Guid("ebaf3dd8-7f4a-4423-b745-c942e462bde2"), "Комментарий 1 к задаче Задача 1 листа Третий список задач", new Guid("7de0edfc-5e64-4d14-b38b-f5c0ba5ea34c") },
                    { new Guid("faa74b26-c261-4749-9c1e-6fb4253e9bdb"), "Комментарий 2 к задаче Задача 4 листа Первый список задач", new Guid("c5607256-205a-48c7-8b4f-c7fa4bd684cc") },
                    { new Guid("fd5df444-1465-4322-8003-74b6a5c7b4c8"), "Комментарий 1 к задаче Задача 5 листа Третий список задач", new Guid("a182168e-d522-4a08-b2d4-96080f7a80b8") }
                });

            migrationBuilder.InsertData(
                table: "StatusTaskHistory",
                columns: new[] { "Id", "AddDate", "Status", "TaskId" },
                values: new object[,]
                {
                    { new Guid("01c1fa73-fda7-4cfa-9731-2d5a802e8da1"), new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Waiting", new Guid("a182168e-d522-4a08-b2d4-96080f7a80b8") },
                    { new Guid("1779c213-bdbc-4d06-9e50-7a4e210886b5"), new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Waiting", new Guid("c5607256-205a-48c7-8b4f-c7fa4bd684cc") },
                    { new Guid("20c09be9-e996-46c1-aaeb-b06dc59dd2a5"), new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Waiting", new Guid("a19cff3c-921f-4b73-b482-27aca75eaae3") },
                    { new Guid("31629bf3-e0d8-4f5b-a894-972f02c34c82"), new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Waiting", new Guid("939eb2d4-439c-4491-956d-82b639aa42bc") },
                    { new Guid("345d2c72-1e1b-43ba-b404-6779edbe2fc0"), new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Waiting", new Guid("5561d361-33ba-4bf0-8c9d-f5f035bf9807") },
                    { new Guid("3bf4f338-62a3-4be8-a05c-96f491c222bb"), new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Waiting", new Guid("d4e7ff20-43dd-430d-9fcb-ad8523f62b0c") },
                    { new Guid("45470a25-94df-4e5a-a115-8ecbef97854e"), new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Waiting", new Guid("ea83e531-7985-4341-afdf-be9d43e79bfc") },
                    { new Guid("463ddd43-1e9f-477a-801a-7810ac9c73bd"), new DateTime(2024, 2, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Waiting", new Guid("d8093abf-dcd5-449a-8754-f90c34fb3982") },
                    { new Guid("49141ec8-3013-4bca-a5ac-e7997a51c9ab"), new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Waiting", new Guid("a68c09fe-11e3-4a52-b76b-b56151ea4a35") },
                    { new Guid("55f0ab14-f76e-49d2-99b8-facc5b44efce"), new DateTime(2024, 2, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Waiting", new Guid("de889a60-0f47-4341-acce-2414514d91a8") },
                    { new Guid("634a0607-68ec-443f-b022-99843974c434"), new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Waiting", new Guid("bd3cf78d-82c4-4bd3-b575-76ae7a4b9197") },
                    { new Guid("6ecbed1b-e95a-4bf9-a39f-b9551bbb6fb8"), new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Waiting", new Guid("a47b01b1-c7dc-4766-8f3c-ea0451b91741") },
                    { new Guid("809fdebf-0c95-4c76-bad2-ce3fecdf37ef"), new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Waiting", new Guid("8541f399-22c4-4f04-a928-612b4a37edab") },
                    { new Guid("88aa703e-9dcc-48e0-ad49-52e8b1fc752c"), new DateTime(2024, 2, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Waiting", new Guid("eceec90e-5144-4f28-808d-c7f2c1bef010") },
                    { new Guid("88f3f476-1d47-4574-a1ff-7f5d8422c11b"), new DateTime(2024, 2, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Waiting", new Guid("6b4bbae9-2200-49b7-8f2f-d1f240050cec") },
                    { new Guid("8d584f03-9b01-424c-bc58-c0eb15fe7b63"), new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Waiting", new Guid("6457f588-6bf7-413f-9685-70ffd90e6ba6") },
                    { new Guid("a2eab4f9-062f-4a8c-98bb-a9f339617004"), new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Waiting", new Guid("86bab430-5e0a-4f62-9163-aeeb7aa9bc2a") },
                    { new Guid("a7a574f3-8e29-4d57-9fa7-efb23ff0ca1f"), new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Waiting", new Guid("6e17e8f2-620e-4bab-b4b2-971d1acc9be2") },
                    { new Guid("acdd34ab-9d95-4c6a-965c-d091a38912cd"), new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Waiting", new Guid("ed207dfa-d8ed-40f9-be96-24d5be74c9a9") },
                    { new Guid("ad527ef9-d46c-4412-b1da-bd61267251b5"), new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Waiting", new Guid("f5a697d1-b2bf-40c9-863b-6bd43d27595d") },
                    { new Guid("b951b26f-33ad-4bc9-85d0-720ec6dc75d6"), new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Waiting", new Guid("599bab9c-6cb8-4196-9ceb-9afc74254247") },
                    { new Guid("c6bdcb5d-7ed1-4e1f-836e-67466a439c81"), new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Waiting", new Guid("4ef740ed-732e-4ca1-b404-80678ba6cbc1") },
                    { new Guid("ca3f0081-1bd8-40dc-b203-22570b4204ba"), new DateTime(2024, 2, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Waiting", new Guid("7d9fdaf5-e7c4-4d91-aa1f-31bf51571c3a") },
                    { new Guid("caaed764-45d1-4d30-871c-8add3daa9cdb"), new DateTime(2024, 2, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Waiting", new Guid("7de0edfc-5e64-4d14-b38b-f5c0ba5ea34c") },
                    { new Guid("cd6548c8-055d-4298-917a-1f3f7cb23986"), new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Waiting", new Guid("865430dd-32fe-4673-93db-f18a29e9a1b8") },
                    { new Guid("e896ebeb-c913-49df-9792-4787b2d1de2b"), new DateTime(2024, 2, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Waiting", new Guid("24ce9f1c-0f6e-4c26-bf40-624535ec8730") },
                    { new Guid("e8f029a3-0d2b-4403-a44d-501753b02230"), new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Waiting", new Guid("88f9453d-301b-4f3f-a1f7-50087e98c3c5") },
                    { new Guid("ed34af26-5ddb-4ca7-9dfd-6ef31468223e"), new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Waiting", new Guid("76e0e6b2-932e-450e-b944-86ccb7ff0bff") },
                    { new Guid("fc72d630-32ed-466b-b656-ae12c162b858"), new DateTime(2024, 2, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Waiting", new Guid("a458d036-0f19-49cd-91ff-5ce9b7a19bc8") },
                    { new Guid("fceb1793-00ea-45fd-a382-96ddff800f80"), new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Waiting", new Guid("17591683-782c-4440-878a-7e286081a81e") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_TaskID",
                table: "Comment",
                column: "TaskID");

            migrationBuilder.CreateIndex(
                name: "IX_StatusTaskHistory_TaskId",
                table: "StatusTaskHistory",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Task_TaskListId",
                table: "Task",
                column: "TaskListId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "StatusTaskHistory");

            migrationBuilder.DropTable(
                name: "Task");

            migrationBuilder.DropTable(
                name: "TaskList");
        }
    }
}
