namespace PersonnelManagement.Application.FileOperations
{
    public interface IFtpService
    {
        Task DeleteFileFromFtpAsync(string filePath);
        Task<byte[]> ReadAllBytesAsync(string remotePath, CancellationToken cancellationToken = default);
        Task<bool> SaveFileToFtpAsync(byte[] bytes, string remotePath);
        Task<bool> SaveFileToFtpAsync(string localPath, string remotePath);
    }
}