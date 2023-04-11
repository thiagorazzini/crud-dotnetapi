using taskSystem.Models;

namespace taskSystem.Repositories.Interfaces
{
    public interface IUserRepositorie
    {
        Task<List<UserModel>> SearchAllUsers();
        Task<UserModel> SearchById(int id);
        Task<UserModel> Add(UserModel user);
        Task<UserModel> Update(UserModel user, int id);
        Task<bool> Delete(int id);
    }
}
