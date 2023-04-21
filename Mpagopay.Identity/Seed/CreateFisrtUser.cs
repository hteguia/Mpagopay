using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Mpagopay.Identity.Models;

namespace Mpagopay.Identity.Seed
{
	public static class CreateFisrtUser
	{
		public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
		{
			var application = new ApplicationUser
			{
				FirstName = "John",
				LastName = "Smith",
				UserName = "johnsmith",
				Email = "test@test.com",
				EmailConfirmed = true,
			};
			var user = await userManager.FindByEmailAsync(application.Email);
			if(user == null)
			{
				
				var result = await userManager.CreateAsync(application, "User@tegus05");
			}
		}
	}
}
