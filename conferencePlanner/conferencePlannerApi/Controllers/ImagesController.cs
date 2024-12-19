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
        _imageStoragePath = Path.Combine(Directory.GetCurrentDirectory(), 
            configuration["ImageStorage:Path"] ?? "Images");
        
        if (!Directory.Exists(_imageStoragePath))
        {
            Directory.CreateDirectory(_imageStoragePath);
        }
    }

    /// <summary>
    /// Uploads an image file to the server and stores its metadata
    /// </summary>
    /// <param name="file">The image file to upload</param>
    /// <returns>
    /// 200 OK with the generated filename and metadata if successful
    /// 400 Bad Request if no file is provided
    /// 500 Internal Server Error if an error occurs during upload
    /// </returns>
    [HttpPost]
    [Route("upload")]
    public async Task<ActionResult<object>> UploadImage(IFormFile file)
    {
        try
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded");
            }

            // Generate unique filename to prevent collisions by combining GUID and original extension
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(_imageStoragePath, fileName);

            // Create new file and copy uploaded content to it asynchronously
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Create metadata object to track important file information
            // Stores original filename, type, size and upload timestamp
            var metadata = new
            {
                OriginalName = file.FileName,
                ContentType = file.ContentType,
                Size = file.Length,
                UploadDate = DateTime.UtcNow
            };

            // Save metadata to separate JSON file with same base name as image
            var metadataPath = Path.Combine(_imageStoragePath, $"{fileName}.json");
            await System.IO.File.WriteAllTextAsync(metadataPath,
                JsonSerializer.Serialize(metadata));

            // Return the generated filename and metadata to the caller
            return Ok(new { fileName, metadata });
        }
        catch
        {
            return StatusCode(500, "An error occurred while uploading the image");
        }
    }

    /// <summary>
    /// Retrieves an image file by its filename
    /// </summary>
    /// <param name="fileName">The name of the file to retrieve</param>
    /// <returns>
    /// 200 OK with the image file and appropriate content type
    /// 404 Not Found if the image doesn't exist
    /// 500 Internal Server Error if an error occurs during retrieval
    /// </returns>
    [HttpGet]
    [Route("get/{fileName}")]
    public ActionResult GetImage(string fileName)
    {
        try
        {
            // Construct full path to image file
            var filePath = Path.Combine(_imageStoragePath, fileName);

            // Check if image exists before attempting to return it
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound($"Image {fileName} not found");
            }

            // First attempt to get content type from metadata file if it exists
            var metadataPath = Path.Combine(_imageStoragePath, $"{fileName}.json");
            if (System.IO.File.Exists(metadataPath))
            {
                // Read and parse the metadata JSON file
                var metadata = JsonSerializer.Deserialize<JsonElement>(
                    System.IO.File.ReadAllText(metadataPath));
                var contentType = metadata.GetProperty("ContentType").GetString();
                // Return the file with the content type from metadata
                return PhysicalFile(filePath, contentType ?? "application/octet-stream");
            }

            // If no metadata exists, determine content type from file extension
            var ext = Path.GetExtension(fileName).ToLower();
            var mimeType = ext switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                _ => "application/octet-stream"  // Default to binary stream if type unknown
            };
            return PhysicalFile(filePath, mimeType);
        }
        catch
        {
            return StatusCode(500, "An error occurred while retrieving the image");
        }
    }

    /// <summary>
    /// Deletes an image file and its associated metadata
    /// </summary>
    /// <param name="fileName">The name of the file to delete</param>
    /// <returns>
    /// 204 No Content if deletion is successful
    /// 404 Not Found if the image doesn't exist
    /// 500 Internal Server Error if an error occurs during deletion
    /// </returns>
    [HttpDelete]
    [Route("delete/{fileName}")]
    public ActionResult DeleteImage(string fileName)
    {
        try
        {
            // Construct paths for both the image and its metadata file
            var filePath = Path.Combine(_imageStoragePath, fileName);
            var metadataPath = Path.Combine(_imageStoragePath, $"{fileName}.json");

            // Verify image exists before attempting deletion
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound($"Image {fileName} not found");
            }

            // Delete the image file
            System.IO.File.Delete(filePath);

            // Also delete the metadata file if it exists
            if (System.IO.File.Exists(metadataPath))
            {
                System.IO.File.Delete(metadataPath);
            }

            // Return success with no content
            return NoContent();
        }
        catch
        {
            return StatusCode(500, "An error occurred while deleting the image");
        }
    }
}