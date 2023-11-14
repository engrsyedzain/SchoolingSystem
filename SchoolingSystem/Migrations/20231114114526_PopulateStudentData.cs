using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolingSystem.Migrations
{
    public partial class PopulateStudentData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT [dbo].[Students] ([Name], [GrNo], [FatherName], [Address]) VALUES ('Zain', '1001', 'Syed Zain', 'Karachi 123')" +
                "INSERT [dbo].[Students] ([Name], [GrNo], [FatherName], [Address]) VALUES ('Asghar', '1002', 'Zain', 'Karachi 234')" +
                "INSERT [dbo].[Students] ([Name], [GrNo], [FatherName], [Address]) VALUES ('Imran', '1003', 'Kamran', 'Karachi 345')" +
                "INSERT [dbo].[Students] ([Name], [GrNo], [FatherName], [Address]) VALUES ('Sara', '1004', 'Asif Mehmood', 'Karachi 456')" +
                "INSERT [dbo].[Students] ([Name], [GrNo], [FatherName], [Address]) VALUES ('Aisha', '1005', 'Zaman', 'Karachi 567')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from Students where id in (1,2,3,4,5)");
        }
    }
}
