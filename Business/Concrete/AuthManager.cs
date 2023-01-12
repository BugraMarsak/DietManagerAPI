using Business.Abstract;
using Core.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using DataAccess.Concrete.Context;
using Entities.Concrete;
using Entities.DTO;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;


        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        
        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByMail(userForLoginDto.Email);
            if (userToCheck == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }

            return new SuccessDataResult<User>(userToCheck, Messages.SuccessfulLogin);
        }

        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var user = new User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                PhoneNumber = userForRegisterDto.PhoneNumber,
                Status = true,
                Photo = userForRegisterDto.Photo,
                Address = userForRegisterDto.Address
            };
            _userService.Add(user);
            using (DietManagerContext context = new DietManagerContext())
            {
                Random rnd = new Random();
                string[] x = { "Male", "Female" };
                ClientDefaultData temp = new ClientDefaultData { Age = 20, Height = 1.78,Gender = x[rnd.Next(x.Length)] , ClientId = user.Id };

                context.ClientDefaultData.Add(temp);
                ClientAllergies allergies = new ClientAllergies { ClientId=user.Id, AllergiesList=""};
                context.ClientAllergies.Add(allergies);
                var dietians = context.UserOperationClaims.Where(u=>u.OperationClaimId == 1).ToList();
                var dietianId = dietians.ElementAt(rnd.Next(dietians.Count)).Id;
                DietianClientLists dietianClientList = new DietianClientLists
                {
                    DietianId = dietianId,
                    ClientId = user.Id,
                };
                context.DietianClients.Add(dietianClientList);
                context.SaveChanges();
            }


            return new SuccessDataResult<User>(user, Messages.UserRegistered);
        }

        public IResult UserExist(string email)
        {
            if (_userService.GetByMail(email) != null)
            {
                return new ErrorResult(Messages.UserAlreadyExists);
            }
            return new SuccessResult();
        }
    }
}
