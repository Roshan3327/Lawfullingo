using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationContract.Lawfullingo.IApplicationService.Services
{
    public interface IEmailAppService
    {
        Task SendEmailAsync(string toEmail, string subject, string bodyHtml);
    }
}
