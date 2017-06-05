using Core.Domain;
using System.Collections.Generic;

namespace Core.Repositories
{
    public interface IUserRepository
    {
        User GetById(int id);
        IList<User> GetAll();
        void Save(User user);
    }
}
