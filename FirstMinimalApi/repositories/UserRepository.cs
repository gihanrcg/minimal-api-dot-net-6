
using FirstMinimalApi.models;

namespace FirstMinimalApi.repositories
{
    public class UserRepository
    {
        private readonly Dictionary<Guid, User> _users = new();

        public void Create(User user)
        {
            if (user == null)
            {
                return;
            }
            _users[user.Id] = user;
        }

        public User GetById(Guid Id)
        {
            return _users[Id];
        }

        public List<User> GetAll()
        {
            return _users.Values.ToList();
        }

        public void Update(User user)
        {
            User existingUser = GetById(user.Id);
            if (existingUser is null)
            {
                return;
            }
            _users[existingUser.Id] = user; ;

        }

        public void Delete(Guid Id)
        {
            _users.Remove(Id);
        }
    }
}
