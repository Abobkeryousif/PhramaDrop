using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaDrop.Application.Contract.Services
{
    public interface ISendEmailServices
    {
        void SendEmail(string email,string subject , string message);
    }
}
