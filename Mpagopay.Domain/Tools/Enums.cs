using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mpagopay.Domain.Tools
{
    public enum PaymentServiceProvider
    {
        MTN_MOBILE_MONEY,
        ORANGE_MONEY
    }

    public enum Status
    {
        PENDING,
        SUCCESS,
        FAILED
    }
}
