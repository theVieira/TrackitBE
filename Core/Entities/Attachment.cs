namespace Trackit.Core.Entities;

public class Attachment : BaseEntity
{
  public string Filename { get; private set; } = string.Empty;
  public string Path { get; private set; } = string.Empty;
  public float Size { get; private set; }
  public Tech Author { get; private set; } = new();

  public Attachment() {}

  public Attachment(string filename, string path, float size, Tech author)
  {
    if(size < 0)
      throw new ArgumentException("size cannot be less than 0");

    Filename = filename;
    Path = path;
    Size = size;
    Author = author;
  }
}