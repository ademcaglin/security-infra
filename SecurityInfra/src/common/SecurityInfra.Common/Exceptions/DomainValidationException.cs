using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityInfra.Common.Exceptions
{
    public class DomainValidationException : Exception
    {
        public DomainValidationException(): base()
        {

        }

        public DomainValidationException(string message) : base(message)
        {

        }
    }
}
