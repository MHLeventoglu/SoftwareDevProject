using System.Net;
using System.Net.Mail;
using Business.Abstract.Users;
using Core.Utilities.Results;
using DataAccess.Abstract.Users;
using Core.Entities.Concrete;

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

    public void SendVerificationEmail(string email)
    {
        var user = _userDal.Get(u => u.Email == email);
        if (user == null)
            throw new Exception("User not found.");

        var token = Guid.NewGuid().ToString();
        user.VerificationToken = token;
        user.IsEmailVerified = false;
        _userDal.Update(user);

        var verificationLink = $"https://yourdomain.com/verify?userId={user.Id}&token={token}";
        var body = $"Hello,\n\nPlease verify your email by clicking the following link:\n{verificationLink}";

        // SMTP ayarları (gerçek değerlerle değiştirin)
        var smtpClient = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new NetworkCredential("your_email@gmail.com", "your_email_password_or_app_password"),
            EnableSsl = true,
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress("your_email@gmail.com"),
            Subject = "Email Verification",
            Body = body,
            IsBodyHtml = false,
        };

        mailMessage.To.Add(email);

        smtpClient.Send(mailMessage);

        Console.WriteLine("Verification email sent.");
    }

    public void VerifyEmail(string userId, string token)
    {
        int id = int.Parse(userId);
        var user = _userDal.Get(u => u.Id == id);

        if (user == null)
            throw new Exception("User not found.");

        if (user.VerificationToken != token)
            throw new Exception("Invalid verification token.");

        user.IsEmailVerified = true;
        user.VerificationToken = null;

        _userDal.Update(user);

        Console.WriteLine("Email verified successfully.");
    }
}
