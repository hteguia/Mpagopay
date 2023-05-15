using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Mpagopay.Application.Contrats.Business;
using Mpagopay.Application.Models.Contexts;
using Mpagopay.Application.Models.Sms;
using Mpagopay.Identity;
using Mpagopay.Persistence;

namespace Mpagopay.Business.Users
{
    public class BaseBusiness : IBusinessService
    {
        protected ConnectionStringsSettings _connectionStringsSettings { get; }
        public BaseBusiness(IOptions<ConnectionStringsSettings> connectionStringsSettings)
        {
            _connectionStringsSettings = connectionStringsSettings.Value;
        }
    }
}
