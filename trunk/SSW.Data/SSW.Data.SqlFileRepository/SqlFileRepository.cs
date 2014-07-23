using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace SSW.Data.SqlFileRepository
{
    /// <summary>
    /// File repository that read and writes files from/to SQLServer using streams.
    /// based on http://rusanu.com/2010/12/28/download-and-upload-images-from-sql-server-with-asp-net-mvc/
    /// </summary>
    public class SqlFileRepository : IFileRepository
    {

        private String _connectionString;

        public SqlFileRepository(string connectionString)
        {
            _connectionString = connectionString;
            if (!IsInitialised)Initialize();
        }


        public SqlFileRepository(string connectionString, string schemaName, string tableName) 
        {
            _connectionString = connectionString;
            _tableName = tableName;
            _schemaName = schemaName;
            if (!IsInitialised) Initialize();
        }


        public int PostFile(string Name, string Path, string ContentType, System.IO.Stream InputStream)
        {
            return PostFile(new FileModel() {
                Name = Name,
                Path = Path,
                ContentType = ContentType,
                InputStream = InputStream,
            });
        }

        public int PostFile(FileModel fileModel)
        {
            int result = 0;
            using (SqlConnection conn = GetConnection())
            {
                using (SqlTransaction trn = conn.BeginTransaction())
                {
                    SqlCommand cmdInsert = new SqlCommand(
                        @"INSERT INTO "+TableName+@" (
                            Name,
                            Path,
                            ContentType)
                        values (
                            @name,
                            @path,
                            @contentType);Select Scope_Identity();", conn, trn);
                    cmdInsert.Parameters.Add("@name", SqlDbType.VarChar, 256);
                    cmdInsert.Parameters["@name"].Value = fileModel.Name;
                    cmdInsert.Parameters.Add("@path", SqlDbType.VarChar, 256);
                    cmdInsert.Parameters["@path"].Value = fileModel.Path;
                    cmdInsert.Parameters.Add("@contentType", SqlDbType.VarChar, 256);
                    cmdInsert.Parameters["@contentType"].Value = fileModel.ContentType;

                    result = Convert.ToInt32(cmdInsert.ExecuteScalar());


                    SqlCommand cmdFirstData = new SqlCommand(
                        @"UPDATE "+TableName+@"
                            SET content = @data
                            WHERE id = @id", conn, trn);
                    cmdFirstData.Parameters.Add("@data", SqlDbType.VarBinary, -1);
                    cmdFirstData.Parameters.Add("@id", SqlDbType.Int);
                    cmdFirstData.Parameters["@id"].Value = result;



                    SqlCommand cmdUpdate = new SqlCommand(
                            @"UPDATE "+TableName+@"
                            SET content.write (@data, NULL, NULL)
                            WHERE id = @id", conn, trn);
                    cmdUpdate.Parameters.Add("@data", SqlDbType.VarBinary, -1);
                    cmdUpdate.Parameters.Add("@id", SqlDbType.Int);
                    cmdUpdate.Parameters["@id"].Value = result;


                    using (System.IO.Stream uploadStream = new BufferedStream(
                        new SqlStreamUpload
                        {
                            InsertCommand = cmdFirstData,
                            InsertDataParam = cmdFirstData.Parameters["@data"],
                            InsertIdParam = cmdFirstData.Parameters["@id"],
                            UpdateCommand = cmdUpdate,
                            UpdateDataParam = cmdUpdate.Parameters["@data"],
                            UpdateIdParam = cmdUpdate.Parameters["@id"]
                        }, 8040))
                    {
                        fileModel.InputStream.CopyTo(uploadStream);
                    }
                    
                    trn.Commit();
                }
            }
            return result;
        }

        public FileModel GetFile(int id)
        {
            SqlConnection conn = GetConnection();
            FileModel result = null;
            try
            {
                SqlCommand cmd = new SqlCommand(
                        
                    @"SELECT name,
                        path,
	                    contentType,
	                    content
                    FROM "+TableName+@"
                    WHERE id = @id;", conn);
                //content_coding, DATALENGTH(content) as content_length,

                cmd.Parameters.Add("@id", SqlDbType.Int);
                cmd.Parameters["@id"].Value = id;

                SqlDataReader reader = cmd.ExecuteReader(
                    CommandBehavior.SequentialAccess |
                    CommandBehavior.SingleResult |
                    CommandBehavior.SingleRow |
                    CommandBehavior.CloseConnection);
                if (false == reader.Read())
                {
                    reader.Dispose();
                    conn = null;
                    return null;
                }

                string name = reader["name"].ToString();
                string path = reader["path"].ToString();
                string contentType = reader["ContentType"].ToString();
                System.IO.Stream contentStream = new SqlReaderStream(reader, 3);

                result = new FileModel
                {                    
                    Name = name,
                    Path = path,
                    ContentType = contentType,
                    OutputStream = contentStream,                    
                };
                conn = null; // ownership transfered to the reader/stream
                return result;
            }
            finally
            {
                if (null != conn)
                {
                    conn.Dispose();
                }
            }
        }



        public void Delete(int id)
        {
            using (SqlConnection conn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand(
                    @"Delete  FROM " + TableName + @"
                    WHERE id = @id;", conn);
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
            }
        }


        public FileModel GetFileByPath(string path)
        {
            throw new NotImplementedException();
        }



        private SqlConnection GetConnection()
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();
            return conn;
        }


        #region configuration Properties

        private string _schemaName = "dbo";
        public String SchemaName
        {
            get { return _schemaName; }
            set { _schemaName = value; }
        }


        private string _tableName = "FileBlob";
        public String TableName
        {
            get { return _tableName; }
            set { _tableName = value; }
        }


        #endregion


        #region initialiszation

        private static bool IsInitialised = false;
        private void Initialize()
        {
            if (!CheckDB())
            {
                SetupDB();
            }
            IsInitialised = true;
        }



        private bool CheckDB()
        {
            try
            {

                using (SqlConnection conn = GetConnection())
                {
                    SqlCommand checkCommand = conn.CreateCommand();
                    checkCommand.CommandText = "select count(TABLE_NAME) from INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = @schema AND  TABLE_NAME = @table";
                    checkCommand.Parameters.AddWithValue("schema", SchemaName);
                    checkCommand.Parameters.AddWithValue("table", TableName);

                    return ((int)checkCommand.ExecuteScalar()) > 0;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("SqlFileRepository: CheckDB Error: ", ex);
            }
        }

      

        private void SetupDB()
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    SqlCommand setupCommand = conn.CreateCommand();

                    setupCommand.CommandText = String.Format(@"Create table {0}.{1} (
    Id int identity primary key,
    [Content] varbinary(max),
    [Name] varchar(255),
    [Path] varchar(255),
    [ContentType] varchar(255)
)
", SchemaName, TableName);

                    setupCommand.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                throw new ApplicationException("SqlFileRepository: Failed to Setup database:", ex);
            }
        }


        #endregion

    }
}
