using System;

namespace NetCoreTraining.Services
{public interface IMailService
    {
        void Send(string subject, string message);
    }
}
