﻿using Authentication.Domain.Entities;
using Authentication.Domain.Repositories;
using Microsoft.Extensions.Options;

namespace Authentication.Infra
{
    public class UserRepository : IUserRepository
    {
        private string ConnectionString { get; set; }
        public UserRepository(IOptions<Settings> settings)
        {
            ConnectionString = settings.Value.ConnectionString;
        }

        private static List<User> _users { get; set; }

        public List<User> Users
        {
            get
            {
                if (_users == null)
                {
                    _users = new();
                    _users.Add(new User { Id = Guid.NewGuid(), UserName = "batman", Password = "batman123456", Role = "simples", Email = "batman@test.com" });
                    _users.Add(new User { Id = Guid.NewGuid(), UserName = "robin", Password = "robin123456", Role = "simples", Email = "robin@test.com" });
                    _users.Add(new User { Id = Guid.NewGuid(), UserName = "admin", Password = "admin123456", Role = "manager", Email = "admin@test.com" });
                }
                return _users;
            }
        }
        public async Task<User> Get(string email, string password)
        {
            return Users.FirstOrDefault(x => x.Email.ToLower().Equals(email.ToLower(), StringComparison.Ordinal) && x.Password.Equals(password));
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return Users;
        }

        public async Task<bool> Check(string email)
        {
            return Users.Exists(x => x.Email.ToLower() == email.ToLower());
        }

        public async Task<string> Create(User user)
        {
            try
            {
                Users.Add(user);
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return string.Empty;
        }

        public async Task<string> Update(User user)
        {
            try
            {
                int indexToUpdate = Users.FindIndex(x => x.Id.Equals(user.Id));
                if (indexToUpdate > 0)
                {
                    Users[indexToUpdate-1] = user;
                    return string.Empty;
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return "Usuario não encontrado";
        }

        public async Task<string> CheckIfIdExist(Guid id)
        {
            int indexToUpdate = Users.FindIndex(x => x.Id.Equals(id));
            if (indexToUpdate < 0)
            {
                return "Usuario não encontrado";
            }
            Users.RemoveAt(indexToUpdate);
            return string.Empty;
        }

        public async Task<string> Delete(Guid id)
        {
            try
            {
                int indexToUpdate = Users.FindIndex(x => x.Id.Equals(x.Id));
                if (indexToUpdate > 0)
                {
                    Users.RemoveAt(indexToUpdate-1);
                    return string.Empty;
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return "Usuario não encontrado";
        }
    }
}
