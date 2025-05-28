namespace Trackit.Application.Services;

public class FileService
{
    private readonly string[] _allowedExtensions = [".pdf", ".jpg", ".jpeg", ".png", ".webp", ".xls", ".xlsx"];
    
    public async Task AttachFile(
        IFormFile file,
        string path
    )
    {
        var directory = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", path);
        if(!Directory.Exists(directory)) Directory.CreateDirectory(directory);
        
        var fileName = Path.GetFileName(file.FileName);

        var extension = Path.GetExtension(fileName).ToLowerInvariant();
        
        if(!_allowedExtensions.Contains(extension)) throw new NotSupportedException("mimetype not supported");
        
        var filePath = Path.Combine(directory, fileName);
        
        await using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);
    }
}