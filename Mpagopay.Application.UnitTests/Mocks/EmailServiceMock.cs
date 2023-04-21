using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Mpagopay.Application.Contrats.Infrastructure;
using Mpagopay.Application.Contrats.Persistence;
using Mpagopay.Application.Models.Mail;
using Mpagopay.Domain.Entities;

namespace Mpagopay.Application.UnitTests.Mocks
{
    public static class EmailServiceMock
    {
        public static Mock<IEmailService> GetEmailService()
        {
            var mockEmailService = new Mock<IEmailService>();

            mockEmailService.Setup(repo => repo.SendEmail(It.IsAny<Email>())).ReturnsAsync(true);

            return mockEmailService;
        }
    }
}
