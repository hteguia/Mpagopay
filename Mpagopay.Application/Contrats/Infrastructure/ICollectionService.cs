using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mpagopay.Domain.Tools;

namespace Mpagopay.Application.Contrats.Infrastructure
{
    public interface ICollectionService
    {
        Task<bool> Collection(string accountNumber, decimal amount, PaymentServiceProvider pServiceProvider);
    }
}
