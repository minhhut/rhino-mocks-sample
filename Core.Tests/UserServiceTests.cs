using Core.Domain;
using Core.Repositories;
using Core.Services;
using NUnit.Framework;
using Rhino.Mocks;
using System;

namespace Core.Tests
{
    [TestFixture]
    public class UserServiceTests
    {

        [Test]
        public void Save_ValidUser_Success()
        {
            var user = new User()
            {
                Name = "John",
                Email = "john@domain.com"
            };

            var emailValidation = MockRepository.GenerateStub<IEmailValidation>();
            var userRepository = MockRepository.GenerateMock<IUserRepository>();

            emailValidation.Stub(p => p.isValid(Arg<string>.Is.Anything)).Return(true);
            userRepository.Expect(p => p.Save(Arg<User>.Is.Equal(user)));

            var userService = new UserService(userRepository, emailValidation);

            userService.Save(user);

            userRepository.VerifyAllExpectations();
        }

        [Test]
        public void Save_ThrowsExceptionOnSave_SavesLog()
        {
            var user = new User()
            {
                Name = "John",
                Email = "john@domain.com"
            };

            var exception = new Exception("Fail to connect to database");

            var emailValidation = MockRepository.GenerateStub<IEmailValidation>();
            var userRepository = MockRepository.GenerateStub<IUserRepository>();
            var logService = MockRepository.GenerateMock<ILogService>();

            emailValidation.Stub(p => p.isValid(Arg<string>.Is.Anything)).Return(true);
            userRepository.Stub(p => p.Save(Arg<User>.Is.Equal(user))).Throw(exception);
            logService.Expect(p => p.LogError(Arg<Exception>.Is.Equal(exception)));

            var userService = new UserService(userRepository, emailValidation);
            userService.LogService = logService;

            userService.Save(user);

            userRepository.VerifyAllExpectations();
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "Email is invalid",  MatchType = MessageMatch.Contains)]
        public void Save_InvalidEmail_ThrowsException()
        {
            var user = new User()
            {
                Name = "John",
                Email = "email"
            };

            var emailValidation = MockRepository.GenerateStub<IEmailValidation>();
            emailValidation.Stub(p => p.isValid(Arg<string>.Is.Anything)).Return(false);

            var userService = new UserService(null, emailValidation);

            userService.Save(user);
        }

        [Test]
        public void GetById_ValidId_ReturnsUser()
        {
            int id = 1;

            var user = new User()
            {
                Id = id
            };

            var userRepository = MockRepository.GenerateMock<IUserRepository>();
            userRepository.Expect(p => p.GetById(id)).Return(user);

            var userService = new UserService(userRepository, null);

            var r = userService.GetById(id);
            Assert.AreEqual(user, r);
            userRepository.VerifyAllExpectations();
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Save_NullUser_ThrowsException()
        {
            var userService = new UserService(null, null);
            userService.Save(null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "Name is required", MatchType = MessageMatch.Contains)]
        public void Save_NullName_ThrowsException()
        {
            var user = new User();

            var userService = new UserService(null, null);
            userService.Save(user);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "Email is required", MatchType = MessageMatch.Contains)]
        public void Save_NullEmail_ThrowsException()
        {
            var user = new User()
            {
                Name = "John"
            };

            var userService = new UserService(null, null);
            userService.Save(user);
        }
    }
}
