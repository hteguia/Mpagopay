using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mpagopay.Application.Features.Users.Commands.CreateUser;
using Mpagopay.Application.Models.Authentication;

namespace Mpagopay.Application.Contrats.Business
{
    public interface IUserBusiness
    {
        Task<bool> Create(CreateUserCommand createUserCommand, string password);
    }
}
