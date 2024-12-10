namespace conferencePlannerCore.Models
{
  public class ImageResponse
  {
    public byte[] Content { get; set; } = Array.Empty<byte>();
    public string ContentType { get; set; } = string.Empty;
  }

  public class ImageServiceException : Exception
  {
    public ImageServiceException(string message, Exception? innerException)
        : base(message, innerException)
    {
    }
  }
}