using EcommerceApi.Core.Utilities.MailHelper;
using EcommerceApi.Entities.SharedModels;
using MassTransit;

namespace EcommerceApi.Business.Consumer
{
    public class ReceiveEmailConsumer : IConsumer<SendEmailCommand>
    {
        private readonly IEmailHelper _emailHelper;

        public ReceiveEmailConsumer(IEmailHelper emailHelper)
        {
            _emailHelper = emailHelper;
        }

        public async Task Consume(ConsumeContext<SendEmailCommand> context)
        {
            _emailHelper.SendEmail(context.Message.Email, context.Message.Token, true);
        }
    }
}
