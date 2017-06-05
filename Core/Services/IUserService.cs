using Core.Domain;
using System.Collections.Generic;

namespace Core.Services
{
    public interface IUserService
    {
        User GetById(int id);
        IList<User> GetAll();
        void Save(User user);
    }
}
