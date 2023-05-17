using Portal.Domain.ViewModel.Outh2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Interfaces.Outh2
{
    public interface IOuth2Service
    {
        Task<string> Authenication(AuthenticationRequest request);
        Task<bool> Register(RegisterRequest request);
        
    }
}
