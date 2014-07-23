using System;
using System.Data.Common;
using System.IO;

namespace SSW.Data.SqlFileRepository
{

    /// <summary>
    /// Stream implementation for reading from DbDataReader.
    /// Allows working with BLOBS without copying the whole damn thing into a bytearray
    /// Based on:  http://rusanu.com/2010/12/28/download-and-upload-images-from-sql-server-with-asp-net-mvc/
    /// </summary>
    public class  SqlReaderStream : System.IO.Stream
    {
        private DbDataReader reader;
        private int columnIndex;
        private long position;

        public SqlReaderStream(
            DbDataReader reader,
            int columnIndex)
        {
            this.reader = reader;
            this.columnIndex = columnIndex;
        }

        public override long Position
        {
            get { return position; }
            set { throw new NotImplementedException(); }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            long bytesRead = reader.GetBytes(columnIndex, position, buffer, offset, count);
            position += bytesRead;
            return (int)bytesRead;
        }

        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanSeek
        {
            get { return false; }
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void Flush()
        {
            throw new NotImplementedException();
        }

        public override long Length
        {
            get { throw new NotImplementedException(); }
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
            throw new NotImplementedException();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && null != reader)
            {
                reader.Dispose();
                reader = null;
            }
            base.Dispose(disposing);
        }
    }
}
