using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Mpagopay.Persistence.UnitTests.Accounts
{
    public class ChangeOrResetPasswordTests
    {
        [Test]
        public async Task ResetPasswordRequest_WithExistEmail_ThenReturnToken()
        {

        }

        [Test]
        public async Task ResetPasswordRequest_WithNoExistEmail_ThenReturnException()
        {

        }

        [Test]
        public async Task ChangePassword_WithValidInput_ThenChangePasswordInRepo()
        {

        }

        [Test]
        public async Task ChangePassword_WithInValid_ThenReturnException()
        {

        }

        [Test]
        public async Task ResetPassword_WithValidInput_ThenResetPasswordInRepo()
        {

        }

        [Test]
        public async Task ResetPassword_WithInValid_ThenReturnException()
        {

        }

        [Test]
        public async Task GenerateEmailConfirmation_WithUserExist_ThenReturnException()
        {

        }

        [Test]
        public async Task GenerateEmailConfirmation_WithUserNoExist_ThenReturnException()
        {

        }

        [Test]
        public async Task ConfirmEmail_WithValidInput_ThenReturnSuccess()
        {

        }

        [Test]
        public async Task ConfirmEmail_WithInValidInput_ThenReturnException()
        {

        }
    }
}
