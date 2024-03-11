using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using demo_azure_openid_connect.Models;
using Microsoft.Identity.Web;
using IdentityModel.Client;
using System.Net.Http;

namespace demo_azure_openid_connect.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ITokenAcquisition tokenAcquisition;
    private readonly IHttpClientFactory httpClientFactory;


    public HomeController(ILogger<HomeController> logger, ITokenAcquisition tokenAcquisition, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        this.tokenAcquisition = tokenAcquisition;
        this.httpClientFactory = httpClientFactory;

    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [AuthorizeForScopes(Scopes = new[] { "user.read" })]
    public async Task<IActionResult> GetMyProfile()
    {
        string[] scopes = new[] { "user.read" };
        string token = await tokenAcquisition.GetAccessTokenForUserAsync(scopes);

        var http = httpClientFactory.CreateClient();
        http.SetBearerToken(token);
        var result = await http.GetStringAsync("https://graph.microsoft.com/v1.0/me");

        return Ok(result);
    }


    [AuthorizeForScopes(Scopes = new[] { "user.read" })]
    public async Task<IActionResult> GetMyPhoto()
    {
        string[] scopes = new[] { "user.read" };
        string token = await tokenAcquisition.GetAccessTokenForUserAsync(scopes);

        var http = httpClientFactory.CreateClient();
        http.SetBearerToken(token);
        var result = await http.GetStreamAsync("https://graph.microsoft.com/v1.0/me/photo/$value");

        return File(result, "image/jpeg");
    }
}
