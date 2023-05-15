 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Mpagopay.Application.Contrats.Persistence;
using Mpagopay.Domain.Entities;
using Mpagopay.Domain.Entities.Users;

namespace Mpagopay.Application.UnitTests.Mocks
{
    public class RepositoryMocks
    {
        public static Mock<IUserRepository> GetUserRepository()
        {
            var userOne = new User
            {
                FirstName = "Colt",
                LastName = "Blankenship",
                Email = "cotl@gmail.com",
                PhoneNumber= "679799607",
                CodeIso2 = "CM",
                PinCode = "1234"
            };

            var userTwo = new User
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john@gmail.com",
                PhoneNumber = "679799607",
                CodeIso2 = "CM",
                PinCode = "1234"
            };

            var userThree = new User
            {
                FirstName = "Timon",
                LastName = "Moran",
                Email = "timon@gmail.com",
                PhoneNumber = "679799607",
                CodeIso2 = "CM",
                PinCode = "1234"
            };

            var userFour = new User
            {
                FirstName = "Steven",
                LastName = "Clay",
                Email = "steven@gmail.com",
                PhoneNumber = "679799607",
                CodeIso2 = "CM",
                PinCode = "1234"
            };

            var userFive = new User
            {
                FirstName = "Omar",
                LastName = "Davis",
                Email = "omar@gmail.com",
                PhoneNumber = "679799607",
                CodeIso2 = "CM",
                PinCode = "1234"
            };

            var users = new List<User> { userOne, userTwo, userThree, userFour, userFive };

            var mockUserRepository = new Mock<IUserRepository>();

            mockUserRepository.Setup(repo => repo.ListAllAsync()).ReturnsAsync(users);

            mockUserRepository.Setup(repo => repo.FindByEmail("cotl@gmail.com")).ReturnsAsync(userOne);

            mockUserRepository.Setup(repo => repo.AddAsync(It.IsAny<User>())).ReturnsAsync(
            (User user) =>
            {
                users.Add(user);
                return user;
            });

            return mockUserRepository;
        } 

        public static Mock<ICardRepository> GetCardRepository()
        {
            var cardOne = new Card
            {
                Name = "Colt Blankenship",
                Number = "5549 8287 7683 5854",
                Cvv = "426",
                Expires = "12/2025"
            };

            var cardTwo = new Card
            {
                Name = "Timon Moran",
                Number = "4363 814 31 3633",
                Cvv = "506",
                Expires = "12/2025"
            };
            var cardThree = new Card
            {
                Name = "Timon Moran",
                Number = "4363 814 31 3633",
                Cvv = "506",
                Expires = "12/2025"
            };

            var cardFour = new Card
            {
                Name = "Steven Clay",
                Number = "4556 1738 7623 6269",
                Cvv = "151",
                Expires = "12/2025"
            };

            var cardFive = new Card
            {
                Name = "Omar Davis",
                Number = "4716 1648 7167 6184",
                Cvv = "202",
                Expires = "12/2026"
            };

            var cards = new List<Card>() { cardOne, cardTwo, cardThree, cardFour, cardFive };

            var mockCardRepository = new Mock<ICardRepository>();
            mockCardRepository.Setup(repo=>repo.ListAllAsync()).ReturnsAsync(cards);

            mockCardRepository.Setup(repo => repo.IsCardNumberUnique(cardOne.Number)).ReturnsAsync(true);

            mockCardRepository.Setup(repo => repo.AddAsync(It.IsAny<Card>())).ReturnsAsync(
            (Card Card) =>
            {
                cards.Add(Card);
                return Card;
            });

            return mockCardRepository;
        }
    }
}
