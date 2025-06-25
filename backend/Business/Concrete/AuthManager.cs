using System;
using Business.Abstract;
using Business.Abstract.Users;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using Core.Utilities.Security.Hashing;
using Entities.DTOs.UserDtos;
using Business.Constants;
using Entities.Concrete.Users;


namespace Business.Concrete;

public class AuthManager:IAuthService
{
    private IUserService _userService;
    private ICustomerService _customerService;
    private IStaffService _staffService;
    private ITokenHelper _tokenHelper;

    public AuthManager(IUserService userService, ITokenHelper tokenHelper, ICustomerService customerService)
    {
        _userService = userService;
        _tokenHelper = tokenHelper;
        _customerService = customerService;
        }

        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password,out passwordHash,out passwordSalt);
            // var user = new User
            // {
            //     Email = userForRegisterDto.Email,
            //     FirstName = userForRegisterDto.FirstName,
            //     Surname = userForRegisterDto.Surname,
            //     PasswordHash = passwordHash,
            //     PasswordSalt = passwordSalt,
            //     Status = true
            // };
            var customer = new Customer
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                Surname = userForRegisterDto.Surname,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true,
                Balance = 0, // Default balance
                ActiveCartId = null // No active cart initially
            };
            
            // var addResult = _userService.Add(user);
            var customerAddResult = _customerService.Add(customer);
            // if (!addResult.Success)
            // return new ErrorDataResult<User>(addResult.Message);

            // Assign default customer role
            var roleResult = _userService.AssignRole((int)customer.Id, Roles.User);
            if (!roleResult.Success)
                return new ErrorDataResult<User>(roleResult.Message);

            return new SuccessDataResult<User>(customer, Messages.UserRegistered);
        }
        
        public IDataResult<Staff> RegisterAdmin(UserForRegisterDto userForRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password,out passwordHash,out passwordSalt);
            // var user = new User
            // {
            //     Email = userForRegisterDto.Email,
            //     FirstName = userForRegisterDto.FirstName,
            //     Surname = userForRegisterDto.Surname,
            //     PasswordHash = passwordHash,
            //     PasswordSalt = passwordSalt,
            //     Status = true
            // };
            var staff = new Staff
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                Surname = userForRegisterDto.Surname,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true,
                Role = Roles.Admin
                };
            
            // var addResult = _userService.Add(user);
            // if (!addResult.Success)
            // return new ErrorDataResult<User>(addResult.Message);
            var staffAddResult = _staffService.Add(staff);
            if (!staffAddResult.Success)
            return new ErrorDataResult<Staff>(staffAddResult.Message);

            // Assign default customer role
        var roleResult = _userService.AssignRole((int)staff.Id, Roles.Admin);
            if (!roleResult.Success)
                return new ErrorDataResult<Staff>(roleResult.Message);

            return new SuccessDataResult<Staff>(staff, Messages.UserRegistered);
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
    {
        var userToCheck = _userService.GetByEmail(userForLoginDto.Email).Data;
        if (userToCheck == null)
        {
            return new ErrorDataResult<User>(Messages.UserNotFound);
        }

        if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password!, userToCheck.PasswordHash!, userToCheck.PasswordSalt!))
        {
            return new ErrorDataResult<User>(Messages.PasswordError);
        }

        return new SuccessDataResult<User>(userToCheck, Messages.SuccessfulLogin);
    }

        public IResult UserExists(string email)
        {
            var user = _userService.GetByEmail(email).Data;
            if (user != null)
            {
                return new ErrorResult(Messages.UserExists);
            }
            return new SuccessResult();
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claimsResult = _userService.GetClaims(user);
            var claims = claimsResult.Data ?? new List<OperationClaim>();
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken,Messages.AccessTokenCreated);
        }
}
