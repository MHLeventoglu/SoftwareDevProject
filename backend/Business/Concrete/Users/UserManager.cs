using System.Net;
using System.Net.Mail;
using Business.Abstract.Users;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract.Users;
using Entities.DTOs.UserDtos;

namespace Business.Concrete.Users;

public class UserManager : IUserService
{
    private readonly IUserDal _userDal;

    public UserManager(IUserDal userDal)
    {
        _userDal = userDal;
    }

    public IResult Add(User entity)
    {
        _userDal.Add(entity);
        return new SuccessResult("User added successfully.");
    }

    public IResult Delete(User entity)
    {
        if (entity == null)
            return new ErrorResult("User not found.");

        _userDal.Delete(entity);
        return new SuccessResult("User deleted successfully.");
    }

    public IResult Update(User entity)
    {
        if (entity == null)
            return new ErrorResult("User not found.");

        _userDal.Update(entity);
        return new SuccessResult("User updated successfully.");
    }

    public IDataResult<List<User>> GetAll()
    {
        var users = _userDal.GetAll();
        return new SuccessDataResult<List<User>>(users);
    }

    public IDataResult<User> GetById(int id)
    {
        var user = _userDal.Get(u => u.Id == id);
        if (user == null)
            return new ErrorDataResult<User>("User not found.");

        return new SuccessDataResult<User>(user);
    }

    public IDataResult<User> GetByEmail(string email)
    {
        return new SuccessDataResult<User>(_userDal.Get(u => u.Email == email), "User found by email.");
    }

    public IDataResult<List<OperationClaim>> GetClaims(User user)
    {
        var claims = _userDal.GetClaims(user);
        if (claims == null || claims.Count == 0)
            return new ErrorDataResult<List<OperationClaim>>("No claims found for this user.");

        return new SuccessDataResult<List<OperationClaim>>(claims, "Claims retrieved successfully.");
    }

    public IResult Register(UserForRegisterDto dto)
    {
        var user = new User
        {
            FirstName = dto.FirstName,
            Surname = dto.Surname,
            Email = dto.Email,
            Status = true
            // Şifreleme işlemi burada yapılmalı (örn. hashing)
        };

        _userDal.Add(user);
        return new SuccessResult("Kullanıcı başarıyla kaydedildi.");
    }

    public IResult SendVerificationEmail(string email)
    {
        var message = new MailMessage();
        message.To.Add(email);
        message.Subject = "Doğrulama Kodu";
        message.Body = "Doğrulama kodunuz: 123456";

        using var smtp = new SmtpClient("smtp.example.com", 587)
        {
            Credentials = new NetworkCredential("no-reply@example.com", "password"),
            EnableSsl = true
        };

        try
        {
            smtp.Send(message);
            return new SuccessResult("Doğrulama e-postası gönderildi.");
        }
        catch (Exception ex)
        {
            return new ErrorResult($"E-posta gönderilemedi: {ex.Message}");
        }
    }

    public IResult VerifyEmail(string email, string code)
    {
        if (code == "123456")
            return new SuccessResult("E-posta doğrulaması başarılı.");

        return new ErrorResult("Doğrulama kodu geçersiz.");
    }
}
