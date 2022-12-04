using FluentFTP;
using Microsoft.Extensions.Logging;
using PersonnelManagement.Application.FileOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Reflection.Metadata.Ecma335;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Infrastracture.FileOperations
{
    public class FtpService : IFtpService
    {
        private readonly FtpClientSettings _ftpClientSettings;
        private readonly ILogger<FtpService> _logger;

        public FtpService(FtpClientSettings ftpClientSettings, ILogger<FtpService> logger)
        {
            _ftpClientSettings = ftpClientSettings;
            _logger = logger;
        }

        public async Task<bool> SaveFileToFtpAsync(string localPath, string remotePath)
        {
            using var ftp = new AsyncFtpClient(_ftpClientSettings.Host,
                 new NetworkCredential
                 {
                     UserName = _ftpClientSettings.UserName,
                     Password = _ftpClientSettings.Password
                 }, _ftpClientSettings.Port, logger: _logger);

            var ftpStatus = FtpStatus.Failed;
            try
            {
                await ftp.Connect();
                ftpStatus = await ftp.UploadFile(localPath, remotePath);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to connect to ftp when trying to save file on ftp.");
                ftpStatus = FtpStatus.Failed;
            }

            return ftpStatus == FtpStatus.Success;
        }

        public async Task<bool> SaveFileToFtpAsync(byte[] bytes, string remotePath)
        {
            using var ftp = new AsyncFtpClient(_ftpClientSettings.Host,
                new NetworkCredential
                {
                    UserName = _ftpClientSettings.UserName,
                    Password = _ftpClientSettings.Password
                }, _ftpClientSettings.Port);
            ftp.ValidateCertificate += new FtpSslValidation((client, e) => { e.Accept = true; });
            ftp.Config.EncryptionMode = FtpEncryptionMode.Auto;

            var ftpStatus = FtpStatus.Failed;
            try
            {
                await ftp.Connect();
                var status = await ftp.UploadBytes(bytes, remotePath);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to connect to ftp when trying to save file on ftp.");
                ftpStatus = FtpStatus.Failed;
            }

            return ftpStatus == FtpStatus.Success;
        }

        public async Task DeleteFileFromFtpAsync(string filePath)
        {
            using var ftp = new AsyncFtpClient(_ftpClientSettings.Host,
                new NetworkCredential
                {
                    UserName = _ftpClientSettings.UserName,
                    Password = _ftpClientSettings.Password
                }, _ftpClientSettings.Port);
            ftp.ValidateCertificate += new FtpSslValidation((client, e) => { e.Accept = true; });
            ftp.Config.EncryptionMode = FtpEncryptionMode.Auto;

            try
            {
                await ftp.Connect();
                await ftp.DeleteFile(filePath);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to connect to ftp when trying to save file on ftp.");
            }
        } 

        public async Task<byte[]> ReadAllBytesAsync(string remotePath, CancellationToken cancellationToken = default)
        {
            using var ftp = new AsyncFtpClient(_ftpClientSettings.Host,
                new NetworkCredential
                {
                    UserName = _ftpClientSettings.UserName,
                    Password = _ftpClientSettings.Password
                }, _ftpClientSettings.Port);
            ftp.ValidateCertificate += new FtpSslValidation((client, e) => { e.Accept = true; }); 
            ftp.Config.EncryptionMode = FtpEncryptionMode.Auto;

            byte[] bytes;
            try
            {
                await ftp.Connect();
                bytes = await ftp.DownloadBytes(remotePath, cancellationToken);
            }
            catch (Exception ex)
            {
                bytes = new byte[0];
                _logger.LogError(ex, "Failed to connect to ftp when trying to read file from ftp.");
            }

            return bytes;
        }
    }
}
