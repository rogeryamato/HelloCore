using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Linq;

namespace HelloCore.DomainModel.Models
{
    public class HelloCorePrincipal : ClaimsPrincipal
    {
        public HelloCorePrincipal(ClaimsIdentity claimsIdentity):base(claimsIdentity)
        { }

        public string UserName
        {
            get
            {
                var name = base.Claims.First(s => s.Type == ClaimTypes.Name);
                if (name != null && string.IsNullOrWhiteSpace(name.Value))
                    return name.Value;
                else
                    return "";
            }
        }
    }
}
