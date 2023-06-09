﻿using Microsoft.AspNetCore.Mvc;

namespace ManageContacts.WebApi.Controllers;

[ApiController]
public class BaseController : ControllerBase
{
    protected readonly ILogger logger;

    public BaseController(ILogger logger)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
}