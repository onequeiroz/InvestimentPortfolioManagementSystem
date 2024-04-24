using Quartz;
using SendGrid.Helpers.Mail;
using SendGrid;
using InvestimentPortfolioManagementSystem.Application.Repository.Interfaces;
using InvestimentPortfolioManagementSystem.Application.Models;
using InvestimentPortfolioManagementSystem.Application.Models.Enums;

namespace InvestimentPortfolioManagementSystem.API.Services
{
    public class SendMailService : IJob
    {
        private readonly ISendGridClient _sendGridClient;
        private readonly ILogger _logger;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;

        public SendMailService(ISendGridClient sendGridClient, ILogger<SendMailService> logger, IProductRepository productRepository, IUserRepository userRepository)
        {
            _sendGridClient = sendGridClient;
            _logger = logger;
            _productRepository = productRepository;
            _userRepository = userRepository;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            // Configure the Expiration Date to notify managers
            int DaysToExpire = 7;
            DateTime notifyIfAboutDaysToExpire = DateTime.Now.AddDays(DaysToExpire);

            IEnumerable<Product> productsAboutToExpire = await _productRepository.SearchAsync(x => x.ExpirationDate <= notifyIfAboutDaysToExpire);
            
            if (productsAboutToExpire.Any())
            {
                string content = "Here's the list of products about to expire with their respective Expiration date: \n";
                foreach(Product product in productsAboutToExpire)
                {
                    content += $"{product.Name} expires on {product.ExpirationDate.ToLocalTime()}\n";
                }

                var msg = new SendGridMessage()
                {
                    From = new EmailAddress("gustavo.g.queiroz2015@gmail.com", "Gustavo Queiroz"),
                    Subject = "Investment products about to expire!",
                    PlainTextContent = content
                };

                IEnumerable<User> activeManagers = await _userRepository.SearchAsync(x => x.IsActive == true && x.UserType == UserTypeEnum.Manager);

                if (activeManagers.Any())
                {
                    foreach(User manager in activeManagers)
                    {
                        msg.AddTo(new EmailAddress(manager.EmailAddress, manager.Name));
                    }

                    var response = await _sendGridClient.SendEmailAsync(msg);

                    // A success status code means SendGrid received the email request and will process it.
                    // Errors can still occur when SendGrid tries to send the email. 
                    // If email is not received, use this URL to debug: https://app.sendgrid.com/email_activity 
                    _logger.LogInformation(response.IsSuccessStatusCode ? "Email queued successfully!" : "Something went wrong!");
                }
            }
        }
    }
}
