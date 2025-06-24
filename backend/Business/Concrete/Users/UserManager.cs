using System.Net;
using System.Net.Mail;
using Business.Abstract.Users;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract.Users;
using Entities.DTOs.UserDtos;
using System.Linq; // <-- Dikkat! First() için gerekebilir

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
        var users = _userDal.GetAll(u => u.Email == email);
        if (users.Count == 0)
            return new ErrorDataResult<User>("User not found.");
        if (users.Count > 1)
            return new ErrorDataResult<User>("Birden fazla kullanıcı var, veritabanı hatası!");
        return new SuccessDataResult<User>(users.First(), "User found by email.");
    }

    public IResult Register(UserForRegisterDto dto)
    {
        // Burada register işlemini kendi ihtiyacına göre geliştirebilirsin.
        return new SuccessResult("Register işlemi tamamlandı.");
    }

    public IResult SendVerificationEmail(string email)
    {
        var users = _userDal.GetAll(u => u.Email == email);
        if (users.Count == 0)
            return new ErrorResult("User not found.");
        if (users.Count > 1)
            return new ErrorResult("Birden fazla kullanıcı var, veritabanı hatası!");
        var user = users.First();

        var token = Guid.NewGuid().ToString();
        user.EmailVerificationToken = token;
        user.EmailVerificationTokenExpiry = DateTime.Now.AddHours(1);
        user.EmailConfirmed = false;
        _userDal.Update(user);

        var link = $"http://localhost:5070/api/User/verify-email?email={email}&token={token}";
        var body = $"Lütfen e-posta adresinizi doğrulamak için bu linke tıklayın: {link}";

        var sent = Core.Utilities.Helpers.EmailHelper.SendEmail(email, "E-posta Doğrulama", body);
        if (!sent)
            return new ErrorResult("Mail gönderilemedi.");

        return new SuccessResult("Doğrulama maili gönderildi.");
    }

    public IResult VerifyEmail(string email, string code)
    {
        var users = _userDal.GetAll(u => u.Email == email);
        if (users.Count == 0)
            return new ErrorResult("User not found.");
        if (users.Count > 1)
            return new ErrorResult("Birden fazla kullanıcı var, veritabanı hatası!");
        var user = users.First();

        if (user.EmailVerificationToken != code)
            return new ErrorResult("Kod hatalı.");

        if (user.EmailVerificationTokenExpiry < DateTime.Now)
            return new ErrorResult("Kodun süresi dolmuş.");

        user.EmailConfirmed = true;
        user.EmailVerificationToken = null;
        user.EmailVerificationTokenExpiry = null;
        _userDal.Update(user);

        return new SuccessResult("E-posta doğrulandı.");
    }

    public IResult SendNotification(string email, string message)
    {
        var users = _userDal.GetAll(u => u.Email == email);
        if (users.Count == 0)
            return new ErrorResult("User not found.");
        if (users.Count > 1)
            return new ErrorResult("Birden fazla kullanıcı var, veritabanı hatası!");
        var user = users.First();

        var sent = Core.Utilities.Helpers.EmailHelper.SendEmail(email, "Bildirim", message);
        if (!sent)
            return new ErrorResult("Bildirim maili gönderilemedi.");

        return new SuccessResult("Bildirim maili gönderildi.");
    }

    // --- Buradan itibaren eklenen kısım ---
    public IDataResult<List<OperationClaim>> GetClaims(User user)
    {
        var claims = _userDal.GetClaims(user);
        if (claims == null || claims.Count == 0)
            return new ErrorDataResult<List<OperationClaim>>("No claims found for this user.");

        return new SuccessDataResult<List<OperationClaim>>(claims, "Claims retrieved successfully.");
    }
}
