// Copyright (c) Forged WoW LLC <https://github.com/ForgedWoW/ForgedCore>
// Licensed under GPL-3.0 license. See <https://github.com/ForgedWoW/ForgedCore/blob/master/LICENSE> for full information.

using Google.Apis.Auth.OAuth2;
using Google.Apis.Download;
using Google.Apis.Drive.v3;
using Google.Apis.Services;

namespace Forged.Tools.Shared.Utils
{
    public class DownloadGoogleDriveFile
    {
        /// <summary>
        /// Download a Document file in PDF format.
        /// </summary>
        /// <param name="fileId">file ID of any workspace document format file.</param>
        /// <returns>byte array stream if successful, null otherwise.</returns>
        public static void DriveDownloadFile(string fileId, string saveLocation)
        {
            // Create Drive API service.
            var service = new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = GoogleCredential.FromFile("readonly_cred.json").CreateScoped(DriveService.Scope.DriveReadonly),
                ApplicationName = "Forged.Tools"
                   
            });

              
            var files = service.Files;
            var request = service.Files.Get(fileId);  
            //request.SupportsAllDrives = true;

            using (var fs = new FileStream(saveLocation, FileMode.Create))
            {
                var status = request.DownloadWithStatus(fs);

                if (status.Status != DownloadStatus.Completed)
                    throw new Exception("Error downloading from drive for " + saveLocation, status.Exception);
            }
        }
    }
}

