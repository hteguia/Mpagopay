using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Moq;
using Mpagopay.Application.Contrats.Infrastructure;

namespace Mpagopay.Application.UnitTests.Mocks
{
    public static class LoggerMock
    {
        public static Mock<ILogger<T>> GetLoggerService<T>()
        {
            var _logger = new Mock<ILogger<T>>();

            return _logger;
        }
    }
}
