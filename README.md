This program code presents a simple file uploader in WPF C#, requirements list:
 - The user specifies several files to upload and assigns each file an upload priority. Files with the same upload priority are uploaded simultaneously. 
 - Files with higher priority are downloaded before files with lower priority.
 - If the downloaded file is a zip archive, it must be unzipped when the download is complete.
 - When downloading, it is required to show the download progress of each file and the download speed in any way.
 - File downloading and unzipping operations should not block the main thread of the application.
