// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Mpagopay.Persistence;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Mpagopay.Identity;

Console.WriteLine("Hello, World!");

//public class ContextFactory : IDesignTimeDbContextFactory<MpagopayDbContext>
//{
//	//dotnet ef migrations add One --project ..\Mpagopay.Persistence\Mpagopay.Persistence.csproj -- "DataSource=Mpagopay.db"
//	public MpagopayDbContext CreateDbContext(string[] args)
//		=> new(new DbContextOptionsBuilder<MpagopayDbContext>().UseSqlServer(args[0]).Options);
//}

public class ContextFactory : IDesignTimeDbContextFactory<MpagopayIdentityDbContext>
{
	//dotnet ef migrations add One --project ..\Mpagopay.Identity\Mpagopay.Identity.csproj -- "DataSource=MpagopayIdentity.db"
	public MpagopayIdentityDbContext CreateDbContext(string[] args)
		=> new(new DbContextOptionsBuilder<MpagopayIdentityDbContext>().UseSqlServer(args[0]).Options);
}