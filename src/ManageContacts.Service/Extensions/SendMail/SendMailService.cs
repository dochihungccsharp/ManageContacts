using MailKit.Security;
using ManageContacts.Shared.Configurations;
using ManageContacts.Shared.Extensions;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace ManageContacts.Service.Extensions.SendMail;

public class SendMailService : IEmailSender
{
    private readonly MailSetting _mailSetting;
    private readonly ILogger<SendMailService> _logger;
    
    public SendMailService(IConfiguration configuration, ILogger<SendMailService> logger)
    {
        _mailSetting = configuration.GetOptions<MailSetting>() ??
                       throw new ArgumentNullException(nameof(configuration));
        _logger = logger;
    }
    
    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var message = new MimeMessage();
        message.Sender = new MailboxAddress(_mailSetting.DisplayName, _mailSetting.Mail);
        message.From.Add(new MailboxAddress(_mailSetting.DisplayName, _mailSetting.Mail));
        message.To.Add(MailboxAddress.Parse(email));
        message.Subject = subject;

        var builder = new BodyBuilder();
        builder.HtmlBody = htmlMessage;
        message.Body = builder.ToMessageBody();
        
        using var smtp = new MailKit.Net.Smtp.SmtpClient();

        try
        {
            await smtp.ConnectAsync(_mailSetting.Host, _mailSetting.Port, SecureSocketOptions.StartTls).ConfigureAwait(false);
            await smtp.AuthenticateAsync(_mailSetting.Mail, _mailSetting.Password).ConfigureAwait(false);
            await smtp.SendAsync(message).ConfigureAwait(false);
            
            Directory.CreateDirectory("MailSave");
            var emailSaveFile = $"MailSave/{Guid.NewGuid()}.eml";
            await message.WriteToAsync(emailSaveFile).ConfigureAwait(false);
        }
        catch(Exception ex)
        {
            Directory.CreateDirectory("MailSave");
            var emailSaveFile = $"MailSave/{Guid.NewGuid()}.eml";
            await message.WriteToAsync(emailSaveFile).ConfigureAwait(false);
            
            _logger.LogError(ex ,ex.Message);
        }
        
        await smtp.DisconnectAsync(true).ConfigureAwait(false);

        _logger.LogInformation("Send mail to: " + email);
    }
}
