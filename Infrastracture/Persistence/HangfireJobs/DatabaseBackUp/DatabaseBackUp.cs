using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace Persistence.HangfireJobs.DatabaseBackUp
{
    public class DatabaseBackUp
    {

        private static string connectionString = "";
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DatabaseBackUp(IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public void BackUpDB_LogTables()
        {
            string[] tableNames = GetAllTableNames().Where(x => x.StartsWith("Log") && x.EndsWith("_Temp") == false).ToArray();
            List<string> lstLogTablolar = new List<string>();
            foreach (string tableName in tableNames)

                BackupTables(tableName, true, true);
        }
        public void BackUpDB_AuthTables()
        {
            string[] tableNames = GetAllTableNames().Where(x => x.StartsWith("Auth") && x.EndsWith("_Temp") == false).ToArray();
            List<string> lstLogTablolar = new List<string>();
            foreach (string tableName in tableNames)
                BackupTables(tableName, false, false);
        }

        public void BackUpDB_OtherAllTables()
        {
            string[] tableNames = GetAllTableNames().Where(x => x.StartsWith("Log") == false && x.StartsWith("Auth") == false && x.EndsWith("_Temp") == false).ToArray();
            List<string> lstLogTablolar = new List<string>();
            foreach (string tableName in tableNames)
                BackupTables(tableName, false, false);
        }

        private void BackupTables(string tableName, bool doCreateTempTable, bool isTruncateTable)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                #region Tablo temp olarak yedeklenecek mi?

                if (doCreateTempTable)
                {
                    string query = $"SELECT * INTO {tableName}_{DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss")}_Temp FROM {tableName}";
                    SqlCommand command = new SqlCommand(query, connection);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();
                        reader.Close();
                    }
                }
                #endregion

                StringBuilder str = GenerateSqlFiles(tableName);
                DosyayaKaydet(tableName, str);

                #region Tabloyu Trancate Yap
                if (isTruncateTable)
                {
                    string queryTrancate = $"Truncate Table {tableName}";
                    SqlCommand command1 = new SqlCommand(queryTrancate, connection);

                    using (SqlDataReader reader1 = command1.ExecuteReader())
                    {
                        reader1.Read();
                        reader1.Close();
                    }


                }
                #endregion

            }
        }

        #region Gerekli Metodlar

        private static string[] GetAllTableNames()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Tüm tablo adlarını almak için bir sorgu
                string query = "SELECT name FROM sys.tables ORDER BY name; ";
                SqlCommand command = new SqlCommand(query, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // Tüm tablo adlarını bir diziye ekle
                    var tableNames = new List<string>();
                    while (reader.Read())
                    {
                        tableNames.Add(reader["name"].ToString());
                    }

                    return tableNames.ToArray();

                }


            }
        }

        private static bool CheckIfColumnIsIdentity(string columnName, string tableName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = $"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{tableName}' AND COLUMN_NAME = '{columnName}' AND COLUMNPROPERTY(OBJECT_ID('{tableName}'), '{columnName}', 'IsIdentity') = 1";
                SqlCommand command = new SqlCommand(query, connection);

                using (SqlDataReader identityReader = command.ExecuteReader())
                {
                    return identityReader.HasRows;
                }
            }
        }

        private static bool CheckIfColumnIsPrimaryKey(string columnName, string tableName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = $"SELECT * FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE TABLE_NAME = '{tableName}' AND COLUMN_NAME = '{columnName}'";
                SqlCommand command = new SqlCommand(query, connection);

                using (SqlDataReader primaryKeyReader = command.ExecuteReader())
                {
                    return primaryKeyReader.HasRows;
                }
            }
        }

        private static string PrintPrimaryKeyInfo(string tableName)
        {
            string primaryKeyScript = string.Empty;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = $@"
                SELECT
                    i.name AS IndexName,
                    ic.index_column_id AS ColumnOrder,
                    c.name AS ColumnName,
                    i.is_primary_key,
                    i.type_desc AS IndexType
                FROM
                    sys.tables t
                INNER JOIN
                    sys.indexes i ON t.object_id = i.object_id
                INNER JOIN
                    sys.index_columns ic ON i.object_id = ic.object_id AND i.index_id = ic.index_id
                INNER JOIN
                    sys.columns c ON ic.object_id = c.object_id AND ic.column_id = c.column_id
                WHERE
                    t.name = '{tableName}' AND
                    i.is_primary_key = 1
                ORDER BY
                    t.name, i.name, ic.index_column_id;
            ";
                SqlCommand command = new SqlCommand(query, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string indexName = reader["IndexName"].ToString();
                        int columnOrder = Convert.ToInt32(reader["ColumnOrder"]);
                        string columnName = reader["ColumnName"].ToString();
                        bool isPrimaryKey = Convert.ToBoolean(reader["is_primary_key"]);
                        string indexType = reader["IndexType"].ToString();

                        if (isPrimaryKey)
                        {
                            if (columnOrder == 1)
                            {
                                primaryKeyScript += $"CONSTRAINT [PK_{tableName}] PRIMARY KEY CLUSTERED (\n\t[{columnName}] ASC\n)";
                                primaryKeyScript += $"WITH ({GetIndexOptions(indexType)}) ON [PRIMARY]\n" + ") ON [PRIMARY]";

                            }
                        }
                    }
                }
            }

            return primaryKeyScript;
        }

        private static string GetForeignKeyScript(string tableName)
        {
            string foreignKeyScript = string.Empty;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = $@"
                SELECT
                    fk.name AS ForeignKeyName,
                    tp.name AS ParentTableName,
                    cp.name AS ParentColumnName,
                    tr.name AS ReferencedTableName,
                    cr.name AS ReferencedColumnName
                FROM
                    sys.foreign_keys fk
                INNER JOIN
                    sys.tables tp ON fk.parent_object_id = tp.object_id
                INNER JOIN
                    sys.tables tr ON fk.referenced_object_id = tr.object_id
                INNER JOIN
                    sys.foreign_key_columns fkc ON fk.object_id = fkc.constraint_object_id
                INNER JOIN
                    sys.columns cp ON fkc.parent_column_id = cp.column_id AND fkc.parent_object_id = cp.object_id
                INNER JOIN
                    sys.columns cr ON fkc.referenced_column_id = cr.column_id AND fkc.referenced_object_id = cr.object_id
                WHERE
                    tp.name = '{tableName}'
            ";
                SqlCommand command = new SqlCommand(query, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string foreignKeyName = reader["ForeignKeyName"].ToString();
                        string parentTableName = reader["ParentTableName"].ToString();
                        string parentColumnName = reader["ParentColumnName"].ToString();
                        string referencedTableName = reader["ReferencedTableName"].ToString();
                        string referencedColumnName = reader["ReferencedColumnName"].ToString();

                        //foreignKeyScript += $"ALTER TABLE [dbo].[{parentTableName}]";
                        //foreignKeyScript += $" WITH CHECK ADD FOREIGN KEY([{parentColumnName}])";
                        //foreignKeyScript += $" REFERENCES [dbo].[{referencedTableName}] ([{referencedColumnName}])";
                        //foreignKeyScript += ";\n";
                        foreignKeyScript += $"ALTER TABLE [dbo].[{parentTableName}]";
                        foreignKeyScript += $" WITH CHECK ADD CONSTRAINT [{foreignKeyName}] FOREIGN KEY([{parentColumnName}])";
                        foreignKeyScript += $" REFERENCES [dbo].[{referencedTableName}] ([{referencedColumnName}])";
                        foreignKeyScript += "\nGO\n";
                        foreignKeyScript += $"ALTER TABLE [dbo].[{parentTableName}]";
                        foreignKeyScript += $" CHECK CONSTRAINT [{foreignKeyName}];\n";
                        foreignKeyScript += "GO\n";
                    }
                }
            }

            return foreignKeyScript;
        }

        private static string GetIndexOptions(string indexType)
        {
            return "PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF";
        }

        private StringBuilder GenerateSqlFiles(string tableName)
        {
            StringBuilder str = new StringBuilder();

            try
            {
                #region Veritabanindaki Tablonun Scheme olustur
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $"SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{tableName}'";
                    SqlCommand command = new SqlCommand(query, connection);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Tablo yapısını scripte ekle
                        str.AppendLine($"-- Tablo yapısı oluşturuluyor: {tableName}");
                        str.AppendLine($"CREATE TABLE [dbo].[{tableName}](");
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                string columnName = reader["COLUMN_NAME"].ToString();
                                string dataType = reader["DATA_TYPE"].ToString();
                                string isNullable = reader["IS_NULLABLE"].ToString();
                                bool isIdentity = CheckIfColumnIsIdentity(columnName, tableName);
                                bool isPrimaryKey = CheckIfColumnIsPrimaryKey(columnName, tableName);

                                str.Append($"\t[{columnName}] [{dataType}]");

                                if (dataType == "varchar" || dataType == "nvarchar")
                                {
                                    int maxLength = Convert.ToInt32(reader["CHARACTER_MAXIMUM_LENGTH"]);
                                    str.Append($"({(maxLength == -1 ? "MAX" : maxLength.ToString())})");
                                }
                                if (dataType == "decimal")
                                {
                                    int numericPrecision = Convert.ToInt32(reader["NUMERIC_PRECISION"]);
                                    int numericScale = Convert.ToInt32(reader["NUMERIC_SCALE"]);
                                    str.Append("(" + numericPrecision + "," + numericScale + ")");

                                }
                                if (isIdentity)
                                {
                                    str.Append(" IDENTITY(1,1)");
                                }

                                if (isNullable == "NO")
                                {
                                    str.Append(" NOT NULL");
                                }
                                else if (isNullable == "YES")
                                {
                                    str.Append(" NULL");
                                }

                                str.AppendLine(",");
                            }
                        }

                        string primaryKey = PrintPrimaryKeyInfo(tableName);
                        str.AppendLine(primaryKey);
                        str.AppendLine("GO");
                        connection.Close();
                    }

                }

                #endregion

                #region Tablodaki Verileri Olustur

                str.AppendLine($"SET IDENTITY_INSERT [dbo].[{tableName}] ON ");
                str.AppendLine("GO");

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $"SELECT * FROM [dbo].[{tableName}]";
                    SqlCommand command = new SqlCommand(query, connection);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // string dataType = reader["DATA_TYPE"].ToString();
                            str.AppendLine($"INSERT [dbo].[{tableName}] (");

                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                str.Append($"[{reader.GetName(i)}], ");

                            }

                            str.Length -= 2; // Son iki karakteri (virgül ve boşluk) kaldır
                            str.AppendLine(")");

                            str.Append("VALUES (");

                            for (int i = 0; i < reader.FieldCount; i++)
                            {

                                if (reader.IsDBNull(i))
                                {
                                    str.Append("NULL, ");
                                }

                                else
                                {
                                    string dataType = reader.GetFieldType(i).ToString().ToLower();

                                    if (dataType == "system.string") // Varchar, nvarchar
                                    {
                                        string data = reader.GetString(i);
                                        if (data.Contains("'"))
                                        {
                                            data = data.Replace("'", "''");

                                        }
                                        str.Append($"N'{data}', ");

                                    }
                                    else if (dataType == "system.decimal") // Decimal
                                    {
                                        int numericPrecision = reader.GetSqlDecimal(i).Precision;
                                        int numericScale = reader.GetSqlDecimal(i).Scale;
                                        str.Append($"CAST({reader.GetValue(i)} AS Decimal({numericPrecision}, {numericScale})), ");
                                    }
                                    else if (dataType == "system.datetime") // DateTime
                                    {
                                        str.Append($"CAST(N'{((DateTime)reader.GetValue(i)).ToString("yyyy-MM-ddTHH:mm:ss.fff")}' AS DateTime), ");
                                    }
                                    else if (dataType == "system.boolean")
                                    {

                                        if ((bool)reader.GetValue(i) == true)
                                        {
                                            str.Append($"{1},");
                                        }
                                        else
                                        {
                                            str.Append($"{0},");
                                        }
                                    }
                                    else
                                    {
                                        str.Append($"{reader.GetValue(i)},");
                                    }

                                }
                            }

                            str.AppendLine(")");

                            string temp = str.ToString().Replace(", )", ")");
                            temp = temp.ToString().Replace(",)", ")");
                            str.Clear();

                            str.Append(temp);
                            str.AppendLine("GO");


                        }
                        connection.Close();
                    }
                }

                #endregion 

                str.AppendLine($"SET IDENTITY_INSERT [dbo].[{tableName}] OFF ");
                str.AppendLine("GO");

                #region Foreign Keyleri Olustur

                string foreignKey = GetForeignKeyScript(tableName);
                str.AppendLine(foreignKey);

                #endregion

                return str;

            }
            catch (Exception ex)
            {
                return new StringBuilder($"Hata oluştu: {ex.Message}");
            }
        }

        private void DosyayaKaydet(string tableName, StringBuilder scriptBuilder)
        {

            string directoryPath = Path.Combine(_webHostEnvironment.WebRootPath, $"Files/0BYedekler/{DateTime.Now.ToString("yyyy_MM_dd")}");
            string scriptFileName = Path.Combine(directoryPath, tableName + "_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm") + ".txt");

            try
            {
                if (!Directory.Exists(directoryPath))
                    Directory.CreateDirectory(directoryPath);

                using (StreamWriter sw = new StreamWriter(scriptFileName))
                {
                    sw.WriteLine(scriptBuilder.ToString());
                }

                //Mesaj Goster
            }
            catch (Exception ex)
            {
                //Hata Logla
                //Console.WriteLine($"Hata oluştu: {ex.Message}");
                //Console.WriteLine($"Hatanın olduğu tablo adı: {tableName}");
            }
        }

        #endregion

    }
}
