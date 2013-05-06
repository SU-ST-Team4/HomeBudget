using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;

namespace HomeBudget.Infrastructure.Principal
{
    /// <summary>
    /// Represents a principal that must be used by ASP.NET Authentication Module
    /// </summary>
    public interface ICustomPrincipal : IPrincipal
    {
        string FirstName { get; }
        string Lastname { get; }
    }
}
