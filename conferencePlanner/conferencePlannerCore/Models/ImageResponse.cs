namespace conferencePlannerCore.Models
{
  public class ImageResponse
  {
    public byte[] Data { get; set; } = Array.Empty<byte>();
    public string ContentType { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
  }
}