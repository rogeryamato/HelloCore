using System;
using System.Collections.Generic;
using System.Text;

namespace WebExtension
{
    [AttributeUsage(AttributeTargets.Method)]
    public class UnitOfWorkAttribute : Attribute
    {
    }
}
