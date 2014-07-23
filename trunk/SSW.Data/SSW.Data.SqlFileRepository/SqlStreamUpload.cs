using System;
using System.Data;
using System.IO;

namespace SSW.Data.SqlFileRepository
{
    /// <summary>
    /// upload a file to SQLServer using streams
    /// based on http://rusanu.com/2010/12/28/download-and-upload-images-from-sql-server-with-asp-net-mvc/
    /// modified from original to support accessing db record by id
    /// </summary>
    public class SqlStreamUpload : Stream
    {
        public IDbCommand InsertCommand { get; set; }
        public IDbCommand UpdateCommand { get; set; }

        public IDataParameter InsertDataParam { get; set; }
        public IDataParameter InsertIdParam { get; set; }

        public IDataParameter UpdateDataParam { get; set; }
        public IDataParameter UpdateIdParam { get; set; }

        public int Id { get; set; }
        

        public override bool CanRead
        {
            get { return false; }
        }

        public override bool CanSeek
        {
            get { return false; }
        }

        public override bool CanWrite
        {
            get { return true; }
        }

        public override void Flush()
        {
        }

        public override long Length
        {
            get { throw new NotImplementedException(); }
        }

        public override long Position
        {
            get;
            set;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            byte[] data = buffer;
            if (offset != 0 ||
                count != buffer.Length)
            {
                data = new byte[count];
                Array.Copy(buffer, offset, data, 0, count);
            }
            if (0 == Position &&
                null != InsertCommand)
            {
                InsertDataParam.Value = data;
                InsertCommand.ExecuteNonQuery();
            }
            else
            {
                UpdateDataParam.Value = data;
                UpdateCommand.ExecuteNonQuery();
            }
            Position += count;
        }


    }
}
