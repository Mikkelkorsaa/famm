namespace conferencePlanner.Models
{
  public class ImageServiceException : Exception
  {
    public ImageServiceException(string message, Exception? innerException)
        : base(message, innerException)
    {
    }
  }
}