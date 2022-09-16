using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using app.Models;
using Oracle.ManagedDataAccess.Client;

namespace app.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [Route("[controller]/test")]
    public IActionResult test()
    {
        string connetString = "User Id=system;Password=oracle;Data Source=localhost:1521/xe";
        string x = string.Empty;
        using (OracleConnection con = new OracleConnection(connetString))
        {
            using (OracleCommand cmd = con.CreateCommand())
            {
                con.Open();
                cmd.BindByName = true;
                cmd.CommandText = "SELECT txt FROM demo";
                
                OracleDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    x = reader.GetString(0);
                }

            }
        }
            return this.Ok(new { name = "ZHE", age = 34, data = x });
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
}
