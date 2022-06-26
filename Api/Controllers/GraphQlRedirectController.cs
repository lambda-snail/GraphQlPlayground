using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;
[ApiController]
[Route("")]
[Route("[controller]")]
public class GraphQlRedirectController : ControllerBase
{
    public GraphQlRedirectController()
    {
        
    }

    [HttpGet]
    public RedirectResult Get()
    {
        return Redirect("/graphql");
    }
}
