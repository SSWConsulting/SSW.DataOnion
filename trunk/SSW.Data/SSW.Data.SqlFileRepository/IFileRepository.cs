using System;

namespace SSW.Data.SqlFileRepository
{
    public interface IFileRepository
    {
        int PostFile(
            String Name,
            String Path,
            String ContentType,
            System.IO.Stream InputStream
            );

        int PostFile(FileModel file);

        FileModel GetFile(int id);

        FileModel GetFileByPath(String path);

        void Delete(int id);
    }
}