using Core.Domain;
using Core.Repositories;
using System;
using System.Collections.Generic;

namespace Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailValidation _emailValidation;

        public ILogService LogService { get; set; }

        public UserService(IUserRepository userRepository, IEmailValidation emailValidation)
        {
            _userRepository = userRepository;
            _emailValidation = emailValidation;
        }

        public IList<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public User GetById(int id)
        {
            return _userRepository.GetById(id);
        }

        public void Save (User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            if (string.IsNullOrEmpty(user.Name))
                throw new ArgumentException("Name is required", "name");

            if (string.IsNullOrEmpty(user.Email))
                throw new ArgumentException("Email is required", "email");

            if (_emailValidation.isValid(user.Email) == false)
                    throw new ArgumentException("Email is invalid", "email");

            try
            {
                _userRepository.Save(user);
            }
            catch (Exception ex)
            {
                LogService.LogError(ex);
            }
        }
    }
}
