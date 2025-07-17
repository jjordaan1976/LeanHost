using Microsoft.AspNetCore.Mvc;

[Route("nuget")]
[ApiController]
public class PackageController : ControllerBase
{
    private readonly PackageStorageService _storage;

    public PackageController(PackageStorageService storage)
    {
        _storage = storage;
    }

    [HttpPut("upload")]
    public async Task<IActionResult> UploadPackage([FromForm] IFormFile package)
    {
        if (package == null || !package.FileName.EndsWith(".nupkg"))
            return BadRequest("Invalid package.");

        await _storage.SavePackageAsync(package);
        return Ok("Package uploaded.");
    }

    [HttpGet("packages")]
    public IActionResult ListPackages()
    {
        var packages = _storage.GetPackageFiles();
        return Ok(packages);
    }

    [HttpGet("download/{id}/{version}")]
    public IActionResult DownloadPackage(string id, string version)
    {
        var stream = _storage.GetPackageStream(id, version);
        if (stream == null)
            return NotFound();

        return File(stream, "application/octet-stream", $"{id}.{version}.nupkg");
    }
}
