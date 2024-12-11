namespace conferencePlannerCore.Models
{
  public record ImageUploadResult
  {
    public string FileName { get; set; } = string.Empty;
    public ImageMetadata Metadata { get; set; } = new();
  }

  public record ImageMetadata
    {
        public string OriginalName { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public long Size { get; set; }
        public DateTime UploadDate { get; set; }
    }
}