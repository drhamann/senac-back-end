
using Authentication.Domain.Entities;
using Authentication.Domain.Repositories;

namespace Authentication.Application.UserModule
{
    public interface IUserService
    {
        Task<IEnumerable<UserModel>> GetAll();
        Task<string> Create(UserModel user);
        Task<bool> Check(string email);
        Task<string> CheckIfIdExist(Guid id);
        Task<string> Delete(Guid id);
        Task<string> Update(UserModel user);
        Task<string> ValidateUserData(UserModel userUpdateRequest);
    }
    public class UserService : IUserService
    {
        public IUserRepository _userRepository { get; }
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        private async Task<string> ValidateUserData(UserModel userCreateRequest)
        {
            if (userCreateRequest == null)
            {
                return "Dados invalidos";
            }
            if (string.IsNullOrEmpty(userCreateRequest.Id.ToString()) == true && await Check(userCreateRequest.Email))
            {
                return "Usuario já existe";
            }
            if (userCreateRequest.Password.Equals(userCreateRequest.ConfirmPassword) is false)
            {
                return "Senha não confere";
            }
            return string.Empty;
        }

        public async Task<IEnumerable<UserModel>> GetAll()
        {
           var users = await _userRepository.GetAll();
           var usersModel = new List<UserModel>();
            foreach (var user in users)
            {
                usersModel.Add(new UserModel()
                {
                    Email = user.Email,
                    Id = user.Id,
                    Role = user.Role,
                    UserName = user.UserName,
                });
            }
            return usersModel;                
        }

        public async Task<string> Create(UserModel user)
        {
            string error = await ValidateUserData(user);
            if (String.IsNullOrEmpty(error))
            {
                var userVo = UserMap(user);
                error = await _userRepository.Create(userVo);
                if (String.IsNullOrEmpty(error))
                {
                    return string.Empty;
                }
            }
            return error;
        }

        public Task<bool> Check(string email)
        {
            throw new NotImplementedException();
        }

        public Task<string> CheckIfIdExist(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<string> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<string> Update(UserModel user)
        {
            throw new NotImplementedException();
        }

        Task<string> IUserService.ValidateUserData(UserModel userUpdateRequest)
        {
            throw new NotImplementedException();
        }

        private User UserMap(UserModel userUpdateRequest)
        {
            var user = new User()
            {
                Email = userUpdateRequest.Email,
                Password = userUpdateRequest.Password,
                Role = userUpdateRequest.Role,
                UserName = userUpdateRequest.UserName
            };
            user.Id = string.IsNullOrEmpty(userUpdateRequest.Id!.ToString()) ? userUpdateRequest.Id : user.Id;
            return user;            
        }
    }
}
