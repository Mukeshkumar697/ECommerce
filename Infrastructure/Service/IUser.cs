using Project.Model;

namespace Project.Infrastructure.Service
{
    public interface IUser
    {
        List<User> GetAll();
        User AddUser(User user);
        string Login(string email, string password);
        User UpdateUserDetail(int id, User user);
        
    }
}
