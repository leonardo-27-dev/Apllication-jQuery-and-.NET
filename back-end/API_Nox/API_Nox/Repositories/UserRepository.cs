using API_Nox.Data;
using API_Nox.Model;
using API_Nox.Repositories.Interfaces;
using API_Nox.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace API_Nox.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly NoxDBContext _dbContext;

        public UserRepository(NoxDBContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        public async Task<List<UserViewModel>> GetAllUsersAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<UserViewModel?> GetUserByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("O ID é inválido ou inexistente", nameof(id));
            }

            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<UserViewModel?> AddUserAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "O usuário não pode ser nulo");
            }

            UserViewModel usuario = new UserViewModel();
            usuario.Id = Guid.NewGuid();
            usuario.Name = user.Name;
            usuario.Email = user.Email;
            usuario.Password = user.Password;

            await _dbContext.Users.AddAsync(usuario);
            await _dbContext.SaveChangesAsync();

            return usuario;
        }


        public async Task<UserViewModel?> UpdateUserAsync(UserViewModel user, Guid id)
        {
            var existingUser = await GetUserByIdAsync(id);

            if (existingUser != null)
            {
                existingUser.Name = user.Name;
                existingUser.Email = user.Email;

                _dbContext.Users.Update(existingUser);
                await _dbContext.SaveChangesAsync();
            }

            return existingUser;
        }

        public async Task<bool?> DeleteUserAsync(Guid id)
        {
            var user = await GetUserByIdAsync(id);

            if (user != null)
            {
                _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<UserViewModel> AuthenticateUser(string email, string password)
        {
            UserViewModel user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

            return user;
        }
    }
}
