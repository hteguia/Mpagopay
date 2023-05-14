using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Mpagopay.Application.Features.Users.Commands.CreateUser;
using Mpagopay.Application.Models.Authentication;
using Mpagopay.Persistence;
using System.Transactions;
using Mpagopay.Application.Contrats.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Mpagopay.Identity;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;
using Mpagopay.Application.Models.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Mpagopay.Identity.Models;
using static System.Formats.Asn1.AsnWriter;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Mpagopay.Business.Users
{
    public class User : BaseBusiness
    {

        protected IAuthenticationService _authenticationService;
        private readonly IMediator _mediator;
        public User(IOptions<ConnectionStringsSettings> connectionStringsSettings, IAuthenticationService authenticationService, IMediator mediator) : base(connectionStringsSettings)
        {
            _authenticationService = authenticationService;
            _mediator = mediator;
        }

        public async Task<bool> Create(CreateUserCommand createUserCommand, string password)
        {
            using var connection = new SqlConnection(_connectionStringsSettings.MpagopayIdentityConnectioString);
            var options = new DbContextOptionsBuilder<MpagopayIdentityDbContext>().UseSqlServer(connection).Options;
            using var mpagopayIdentityDbContext = new MpagopayIdentityDbContext(options);
            using (var t1 = mpagopayIdentityDbContext.Database.BeginTransaction())
            {
                try
                {
                   
                    RegistrationRequest registrationRequest = new()
                    {
                        FirstName = createUserCommand.FirstName,
                        LastName = createUserCommand.LastName,
                        Email = createUserCommand.Email,
                        UserName = createUserCommand.Email,
                        Password = password
                    };
                    RegistrationResponse registrationResponse = await _authenticationService.RegisterAsync(registrationRequest);

                    using var connectiondb = new SqlConnection(_connectionStringsSettings.MpagopayConnectionString);
                    var optionsdb = new DbContextOptionsBuilder<MpagopayDbContext>().UseSqlServer(connectiondb).Options;

                    using var mpagopayDbContext = new MpagopayDbContext(optionsdb);
                    using (var t2 = mpagopayDbContext.Database.BeginTransaction())
                    {
                        createUserCommand.IdentityId = registrationResponse.UserId;
                        await _mediator.Send(createUserCommand);
                        t2.Commit();
                    }

                    t1.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    t1.Rollback();
                    return false;
                }
            }
           
            //using var transaction = mpagopayIdentityDbContext.Database.BeginTransaction();

            ////using var connectiondb = new SqlConnection(_connectionStringsSettings.MpagopayConnectionString);
            ////var optionsdb = new DbContextOptionsBuilder<MpagopayDbContext>().UseSqlServer(connectiondb).Options;

            ////using var mpagopayDbContext = new MpagopayDbContext(optionsdb);

            //using var connectiondb = new SqlConnection(_connectionStringsSettings.MpagopayIdentityConnectioString);
            //var optionsdb = new DbContextOptionsBuilder<MpagopayIdentityDbContext>().UseSqlServer(connectiondb).Options;

            //using var mpagopayDbContext = new MpagopayIdentityDbContext(optionsdb);


            //mpagopayDbContext.Database.UseTransaction(transaction.GetDbTransaction());
            //try
            //{
            //    RegistrationRequest registrationRequest = new()
            //    {
            //        FirstName = createUserCommand.FirstName,
            //        LastName = createUserCommand.LastName,
            //        Email = createUserCommand.Email,
            //        UserName = createUserCommand.Email,
            //        Password = password
            //    };

            //    //RegistrationResponse registrationResponse = await _authenticationService.RegisterAsync(registrationRequest);
            //    //using(var cont = new MpagopayDbContext())
            //    //{

            //    //}

            //    //transaction.Commit();
            //}
            //catch(Exception ex)
            //{

            //}

            ////using (TransactionScope scope = new TransactionScope())
            ////{
            ////    //using var connection = new SqlConnection("Data Source=localhost;Initial Catalog=mpagopay;Integrated Security=true;");
            ////    //var options = new DbContextOptionsBuilder<MpagopayDbContext>().UseSqlServer(connection).Options;


            ////    var context = new MpagopayDbContext();
                

                

                

            ////    RegistrationResponse registrationResponse = await _authenticationService.RegisterAsync(registrationRequest);

            ////    createUserCommand.IdentityId = registrationResponse.UserId;
            ////    await _mediator.Send(createUserCommand);

            ////    scope.Complete();
            ////}
            

            return true;
        }
    }
}
