using System.Net;
using System.Net.Mail;
using Business.Abstract.Users;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract.Users;
using Entities.DTOs.UserDtos;
using System.Linq;

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
        // Şifre hashleme AuthManager'da, burada temel kullanıcı oluşturuluyor
        var user = new User
        {
            Email = dto.Email,
            FirstName = dto.FirstName,
            Surname = dto.Surname,
            // PasswordHash, PasswordSalt AuthManager'da atanıyor
            Status = false, // Pasif olarak kaydediliyor
            EmailConfirmed = false,
            DateAdded = DateTime.Now
        };
        _userDal.Add(user);
        SendVerificationEmail(user.Email!);
        return new SuccessResult("Register işlemi tamamlandı. Lütfen e-posta adresinizi doğrulayın.");
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

        // Email ve token'ı URL encode et
        var encodedEmail = System.Net.WebUtility.UrlEncode(email);
        var encodedToken = System.Net.WebUtility.UrlEncode(token);
        var link = $"http://localhost:5070/api/User/verify-email?email={encodedEmail}&token={encodedToken}";
        var body = $@"<meta charset='UTF-8'><div style='font-family:sans-serif;'>
            <h2>E-posta Doğrulama</h2>
            <p>Lütfen e-posta adresinizi doğrulamak için bu linke tıklayın: <a href='{link}'>Doğrula</a></p>
            <p>Veya bu linki tarayıcınıza yapıştırın: {link}</p>
        </div>";

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
        user.Status = true; // Doğrulama sonrası aktif!
        user.EmailVerificationToken = null;
        user.EmailVerificationTokenExpiry = null;
        _userDal.Update(user);

        // Başarı durumunda HTML döndürmek için:
        return new SuccessResult(
            "<h2>E-posta doğrulandı!</h2><p>Hesabınız başarıyla doğrulandı.</p>"
        );
    }

    public IResult SendNotification(string email, string message)
    {
        var users = _userDal.GetAll(u => u.Email == email);
        if (users.Count == 0)
            return new ErrorResult("User not found.");
        if (users.Count > 1)
            return new ErrorResult("Birden fazla kullanıcı var, veritabanı hatası!");
        var user = users.First();

        // Mesajı HTML şablonuna sar, başa meta charset ekle
        var htmlBody = $@"<meta charset='UTF-8'><div style='font-family:sans-serif;'>
            <h2>Yeni Bildirim</h2>
            <p>{System.Net.WebUtility.HtmlEncode(message).Replace("\n", "<br>")}</p>
        </div>";

        var sent = Core.Utilities.Helpers.EmailHelper.SendEmail(email, "Bildirim", htmlBody);
        if (!sent)
            return new ErrorResult("Bildirim maili gönderilemedi.");

        return new SuccessResult("Bildirim maili gönderildi.");
    }

    public IDataResult<List<OperationClaim>> GetClaims(User user)
    {
        var claims = _userDal.GetClaims(user);
        return new SuccessDataResult<List<OperationClaim>>(claims ?? new List<OperationClaim>(), "Claims retrieved successfully.");
    }
}