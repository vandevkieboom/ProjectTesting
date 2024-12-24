using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTesting
{
    public interface IUserService
    {
        string UserUrl { get; set; }
        User GetUser(int id);
    }
}
