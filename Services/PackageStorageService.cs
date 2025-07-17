public class PackageStorageService
{
    private readonly string _storagePath = Path.Combine(Directory.GetCurrentDirectory(), "packages");

    public PackageStorageService()
    {
        Directory.CreateDirectory(_storagePath);
    }

    public async Task SavePackageAsync(IFormFile file)
    {
        var packagePath = Path.Combine(_storagePath, file.FileName);

        using var stream = new FileStream(packagePath, FileMode.Create);
        await file.CopyToAsync(stream);
    }

    public IEnumerable<string> GetPackageFiles() =>
        Directory.GetFiles(_storagePath, "*.nupkg").Select(Path.GetFileName);

    public FileStream? GetPackageStream(string id, string version)
    {
        var file = Directory.GetFiles(_storagePath, $"{id}.{version}.nupkg", SearchOption.TopDirectoryOnly)
                            .FirstOrDefault();

        return file != null ? new FileStream(file, FileMode.Open, FileAccess.Read) : null;
    }
}
