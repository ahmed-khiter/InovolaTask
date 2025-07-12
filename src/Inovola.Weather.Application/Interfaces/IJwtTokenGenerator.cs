using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inovola.Weather.Application.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(string username);
    }
}
