namespace ManageUsers.Api.Model
{
    public class FileModel
    {
        public string FileName { get; set; } = string.Empty;
        public IFormFile File { get; set; } = default!;
    }
}
