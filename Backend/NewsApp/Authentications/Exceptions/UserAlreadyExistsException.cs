using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentications.Exceptions
{
    public class UserAlreadyExistsException:ApplicationException
    {
        public UserAlreadyExistsException() : base(){ }
        public UserAlreadyExistsException(string message) : base(message) { }
    }
}
