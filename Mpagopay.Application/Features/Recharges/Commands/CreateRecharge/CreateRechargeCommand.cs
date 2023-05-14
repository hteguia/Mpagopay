using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Mpagopay.Domain.Entities;
using Mpagopay.Domain.Tools;

namespace Mpagopay.Application.Features.Recharges.Commands.CreateRecharge
{
    public class CreateRechargeCommand : IRequest<Recharge>
    {
        public decimal Amount { get; set; }
        public PaymentServiceProvider PaymentServiceProvider { get; set; }
        public long UserId { get; set; }
        public string Reference { get; set; }
    }
}
