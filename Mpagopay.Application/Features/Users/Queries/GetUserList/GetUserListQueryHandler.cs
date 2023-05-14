using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Mpagopay.Application.Contrats.Persistence;
using Mpagopay.Application.Features.Pricings.Queries.GetPricingList;
using Mpagopay.Domain.Entities.Users;
using PhoneNumbers;

namespace Mpagopay.Application.Features.Users.Queries.GetUserList
{
    public class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, List<UserListVm>>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<User> _userRepository;
        

        public GetUserListQueryHandler(IMapper mapper, IAsyncRepository<User> userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<List<UserListVm>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            var allCard = (await _userRepository.ListAllAsync()).OrderBy(p => p.FirstName).ThenBy(p=>p.LastName).ToList();
            return _mapper.Map<List<UserListVm>>(allCard);
        }
    }
}
