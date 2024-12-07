namespace conferencePlannerCore.Models
{
  public class PictureModel
  {
    private byte[]? _picture;

    [System.Text.Json.Serialization.JsonIgnore]
    public byte[] PictureByteArray
    {
      get => _picture!;
      set => _picture = value;
    }

    public string? PictureBase64
    {
      get => _picture != null ? Convert.ToBase64String(_picture) : null;
      set => _picture = value != null ? Convert.FromBase64String(value) : null;
    }
  }
}