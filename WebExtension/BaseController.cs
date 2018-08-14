using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebExtension
{
    [Authorize]
    public class BaseController : Controller
    {
    }

    [Authorize]
    public class BaseApiController:ControllerBase
    {

    }
}
