using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Uni.Controllers.api;

[Route("/api/book")]
[ApiController]
public class Book : Controller
{
    public record BookModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;
        [Required]
        [MaxLength(16)]
        public string Phone { get; set; } = null!;
        [Required]
        public DateTime Date { get; set; }
        public string? Message { get; set; }
        public bool IsMessage => Message == "on";
    }

    [HttpPost]
    public IActionResult Post([FromForm] BookModel bookInfo)
    {
        return Ok();
    }
}
