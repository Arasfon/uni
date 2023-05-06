using Microsoft.AspNetCore.Mvc;

using System.Text.RegularExpressions;

namespace Uni.Controllers;

[Route("/")]
public partial class MainStaticController : Controller
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public MainStaticController(IWebHostEnvironment webHostEnvironment) =>
        _webHostEnvironment = webHostEnvironment;

    [Route("{**path}")]
    public IActionResult Index(string? path)
    {
        // Trim query
        path = path?.Trim('/') ?? "index";
        
        // Validate path
        if (PathRegex().IsMatch(path))
        {
            string fileToSearch = Path.Combine(_webHostEnvironment.WebRootPath, path + ".html");

            string fullPath = Path.GetFullPath(fileToSearch);

            // Absolute path check
            if (System.IO.File.Exists(fileToSearch) && fullPath.StartsWith(_webHostEnvironment.WebRootPath, StringComparison.OrdinalIgnoreCase))
                return PhysicalFile(fullPath, "text/html");
        }

        return NotFound();
    }

    [GeneratedRegex(@"([a-zA-Z0-9_\.\-/])*")]
    private static partial Regex PathRegex();
}
