using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

[ApiController]
[Route("api/[controller]")]
public class ImagesController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly string _imageStoragePath;
    
    public ImagesController(IConfiguration configuration)
    {
        _configuration = configuration;
        _imageStoragePath = _configuration["ImageStorage:Path"] ?? "Images";
        
        // Ensure storage directory exists
        if (!Directory.Exists(_imageStoragePath))
        {
            Directory.CreateDirectory(_imageStoragePath);
        }
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadImage(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded");

        // Validate file type
        var allowedTypes = new[] { "image/jpeg", "image/png", "image/gif" };
        if (!allowedTypes.Contains(file.ContentType.ToLower()))
            return BadRequest("Invalid file type");

        // Generate unique filename
        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var filePath = Path.Combine(_imageStoragePath, fileName);

        try
        {
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Store metadata in JSON file
            var metadata = new
            {
                OriginalName = file.FileName,
                ContentType = file.ContentType,
                Size = file.Length,
                UploadDate = DateTime.UtcNow
            };

            var metadataPath = Path.Combine(_imageStoragePath, $"{fileName}.json");
            await System.IO.File.WriteAllTextAsync(metadataPath, 
                JsonSerializer.Serialize(metadata));

            return Ok(new { fileName, metadata });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("{fileName}")]
    public IActionResult GetImage(string fileName)
    {
        var filePath = Path.Combine(_imageStoragePath, fileName);
        
        if (!System.IO.File.Exists(filePath))
            return NotFound();

        // Read metadata to get content type
        var metadataPath = Path.Combine(_imageStoragePath, $"{fileName}.json");
        if (System.IO.File.Exists(metadataPath))
        {
            var metadata = JsonSerializer.Deserialize<JsonElement>(
                System.IO.File.ReadAllText(metadataPath));
            var contentType = metadata.GetProperty("ContentType").GetString();
            return PhysicalFile(filePath, contentType ?? "application/octet-stream");
        }

        // Fallback content type based on extension
        var ext = Path.GetExtension(fileName).ToLower();
        var mimeType = ext switch
        {
            ".jpg" or ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".gif" => "image/gif",
            _ => "application/octet-stream"
        };

        return PhysicalFile(filePath, mimeType);
    }

    [HttpDelete("{fileName}")]
    public IActionResult DeleteImage(string fileName)
    {
        var filePath = Path.Combine(_imageStoragePath, fileName);
        var metadataPath = Path.Combine(_imageStoragePath, $"{fileName}.json");

        if (!System.IO.File.Exists(filePath))
            return NotFound();

        try
        {
            System.IO.File.Delete(filePath);
            if (System.IO.File.Exists(metadataPath))
                System.IO.File.Delete(metadataPath);
            
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}