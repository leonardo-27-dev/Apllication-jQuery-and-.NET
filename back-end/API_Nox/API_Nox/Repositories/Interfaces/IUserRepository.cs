using API_Nox.Model;
using API_Nox.ViewModel;

namespace API_Nox.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<List<UserViewModel>> GetAllUsersAsync();
        Task<UserViewModel?> GetUserByIdAsync(Guid id);
        Task<UserViewModel?> AddUserAsync(User user);
        Task<UserViewModel?> UpdateUserAsync(UserViewModel user, Guid id);
        Task<bool?> DeleteUserAsync(Guid id);
        Task<UserViewModel> AuthenticateUser(string email, string password);
    }
}
