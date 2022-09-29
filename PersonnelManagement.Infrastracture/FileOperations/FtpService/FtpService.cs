using FluentFTP;
using PersonnelManagement.Application.FileOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Infrastracture.FileOperations
{
    public class FtpService : IFtpService
    {
        private readonly FtpClientSettings _ftpClientSettings;

        public FtpService(FtpClientSettings ftpClientSettings)
        {
            _ftpClientSettings = ftpClientSettings;
        }

        public async Task<bool> SaveFileToFtpAsync(string localPath, string remotePath)
        {
            using var ftp = new AsyncFtpClient(_ftpClientSettings.Host,
                 new NetworkCredential
                 {
                     UserName = _ftpClientSettings.UserName,
                     Password = _ftpClientSettings.Password
                 }, _ftpClientSettings.Port);

            await ftp.Connect();
            var status = await ftp.UploadFile(localPath, remotePath);
            // TODO: log connection error
            return status == FtpStatus.Success;
        }

        public async Task<bool> SaveFileToFtpAsync(byte[] bytes, string remotePath)
        {
            using var ftp = new AsyncFtpClient(_ftpClientSettings.Host,
                new NetworkCredential
                {
                    UserName = _ftpClientSettings.UserName,
                    Password = _ftpClientSettings.Password
                }, _ftpClientSettings.Port);

            await ftp.Connect();
            var status = await ftp.UploadBytes(bytes, remotePath);
            // TODO: log connection error
            return status == FtpStatus.Success;
        }

        public async Task DeleteFileFromFtpAsync(string filePath)
        {
            using var ftp = new AsyncFtpClient(_ftpClientSettings.Host,
                new NetworkCredential
                {
                    UserName = _ftpClientSettings.UserName,
                    Password = _ftpClientSettings.Password
                }, _ftpClientSettings.Port);

            await ftp.Connect();
            await ftp.DeleteFile(filePath);
        } 

        public async Task<byte[]> ReadAllBytesAsync(string remotePath, CancellationToken cancellationToken = default)
        {
            using var ftp = new AsyncFtpClient(_ftpClientSettings.Host,
                new NetworkCredential
                {
                    UserName = _ftpClientSettings.UserName,
                    Password = _ftpClientSettings.Password
                }, _ftpClientSettings.Port);
            // TODO: log connection error
            await ftp.Connect();
            var bytes = await ftp.DownloadBytes(remotePath, cancellationToken);
            return bytes;
        }
    }
}
