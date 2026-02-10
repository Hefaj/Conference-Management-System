using CMS.Shared.DDD;

namespace CMS.Modules.Submission.Domain.Models;

internal class Attachment : ValueObject
{
    public string Url { get; init; }
    public string FileHash { get; init; }
    public DateTime UploadDate { get; init; }

    public Attachment(string url, string fileHash, DateTime uploadDate)
    {
        Url = url;
        FileHash = fileHash;
        UploadDate = uploadDate;
    }

    public static Result<Attachment> Create(string url, string fileHash, DateTime uploadDate)
    {
        var errors = new List<string>();
        if (string.IsNullOrWhiteSpace(url))
        {
            errors.Add("URL cannot be empty.");
        }
        if (string.IsNullOrWhiteSpace(fileHash))
        {
            errors.Add("File hash cannot be empty.");
        }
        if (uploadDate > DateTime.UtcNow)
        {
            errors.Add("Upload date cannot be in the future.");
        }
        if (errors.Count != 0)
        {
            return Result<Attachment>.Failure(errors);
        }
        return Result<Attachment>.Success(new Attachment(url, fileHash, uploadDate));
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Url;
        yield return FileHash;
        yield return UploadDate;
    }
}
