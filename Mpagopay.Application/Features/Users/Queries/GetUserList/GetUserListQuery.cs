using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Mpagopay.Application.Features.Pricings.Queries.GetPricingList;

namespace Mpagopay.Application.Features.Users.Queries.GetUserList
{
    public class GetUserListQuery : IRequest<List<UserListVm>>
    {
    }
}
