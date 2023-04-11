using Microsoft.EntityFrameworkCore;
using taskSystem.Context;
using taskSystem.Models;
using taskSystem.Repositories.Interfaces;

namespace taskSystem.Repositories
{
    public class UserRepositorie : IUserRepositorie
    {
        private readonly SystemTasksDBContext _dbcontext;

        public UserRepositorie(SystemTasksDBContext systemTasksDBContext)
        {
            _dbcontext = systemTasksDBContext;
        }
        public async Task<UserModel> SearchById(int id)
        {
            return await _dbcontext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<List<UserModel>> SearchAllUsers()
        {
            return await _dbcontext.Users.ToListAsync();

        }
        public async Task<UserModel> Add(UserModel user)
        {
            await _dbcontext.Users.AddAsync(user);
            await _dbcontext.SaveChangesAsync();

            return user;
        }
        public async Task<UserModel> Update(UserModel user, int id)
        {
            UserModel userById =  await SearchById(id);
            
            if (userById == null)
            {
                throw new Exception($"Users for ID: {id} Was not found ind Database");
            }

            userById.Name = user.Name;
            userById.Email = user.Email;

            _dbcontext.Users.Update(userById);
            await _dbcontext.SaveChangesAsync();

            return userById;
        }

        public async Task<bool> Delete(int id)
        {
            UserModel userById = await SearchById(id);

            if (userById == null)
            {
                throw new Exception($"Users for ID: {id} Was not found ind Database");
            }

            _dbcontext.Users.Remove(userById);
            await _dbcontext.SaveChangesAsync();

            return true;

        }



    }
}
