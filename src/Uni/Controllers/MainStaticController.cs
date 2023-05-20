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
        if (!PathRegex().IsMatch(path))
            return NotFound();

        string fullPath = Path.GetFullPath(Path.Combine(_webHostEnvironment.WebRootPath, path));

        // Directory index and absolute path check
        if (Directory.Exists(fullPath) &&
            System.IO.File.Exists(Path.Combine(fullPath, "index.html")) &&
            fullPath.StartsWith(_webHostEnvironment.WebRootPath, StringComparison.OrdinalIgnoreCase))
            return PhysicalFile(Path.Combine(fullPath, "index.html"), "text/html");

        string fileToSearch = fullPath + ".html";

        // Absolute path check
        if (System.IO.File.Exists(fileToSearch) &&
            fullPath.StartsWith(_webHostEnvironment.WebRootPath, StringComparison.OrdinalIgnoreCase))
            return PhysicalFile(fileToSearch, "text/html");

        return NotFound();
    }

    [GeneratedRegex(@"([a-zA-Z0-9_\.\-/])*")]
    private static partial Regex PathRegex();
}
