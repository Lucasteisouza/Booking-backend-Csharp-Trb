using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public class UserRepository : IUserRepository
    {
        protected readonly ITrybeHotelContext _context;
        public UserRepository(ITrybeHotelContext context)
        {
            _context = context;
        }
        public UserDto GetUserById(int userId)
        {
            throw new NotImplementedException();
        }

        public UserDto Login(LoginDto login)
        {
           var user = from u in _context.Users
                      where u.Email == login.Email && u.Password == login.Password
                      select new UserDto
                      {
                          UserId = u.UserId,
                          Name = u.Name,
                          Email = u.Email,
                          userType = u.UserType
                      };
            if (user.Count() == 0)
            {
                return null;
            }
            return user.Last();
        }
        public UserDto Add(UserDtoInsert user)
        {
            User userToInsert = new User
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                UserType = "client"
            };
            _context.Users.Add(userToInsert);
            _context.SaveChanges();
            var newUser = from u in _context.Users
                          where u.Email == userToInsert.Email
                          select new UserDto
                          {
                              UserId = u.UserId,
                              Name = u.Name,
                              Email = u.Email,
                              userType = u.UserType
                          };
            return newUser.Last();
        }

        public UserDto GetUserByEmail(string userEmail)
        {
            var user = from u in _context.Users
                       where u.Email == userEmail
                       select new UserDto
                       {
                           UserId = u.UserId,
                           Name = u.Name,
                           Email = u.Email,
                           userType = u.UserType
                       };
            if (user.Count() == 0)
            {
                return null;
            }
            return user.Last();
        }

        public IEnumerable<UserDto> GetUsers()
        {
            var users = from u in _context.Users
                        select new UserDto
                        {
                            UserId = u.UserId,
                            Name = u.Name,
                            Email = u.Email,
                            userType = u.UserType
                        };
            return users;
        }

    }
}