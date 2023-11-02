

using Dapper;
using FindMatchingColumns.BL.Helper;
using FindMatchingColumns.BL.IServices;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace FindMatchingColumns.BL.Services
{
    public class MatchingServices : IMatchingServices
    {
        IDbConnection _db;
        public MatchingServices(string Connection) 
        {
            _db = new SqlConnection(Connection);
        }
        public string? FilePath { get; set; }
        public List<string> GetColumnNamesOfPropertyInSpecificTable<T>(T propertyValue, string tableName)
        {
            string query = $"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{tableName}'";
            var columnNames = _db.Query<string>(query);
            var matchingColumns = new List<string>();
            int rowCount;
            string result = "";

            foreach (var columnName in columnNames)
            {
                query = $"SELECT COUNT(*) FROM {tableName} WHERE {columnName} = @PropertyValue";
                var param = new { PropertyValue = propertyValue };


                if (typeof(T) == typeof(string))
                {
                    param = new { PropertyValue = propertyValue };
                }
                else if (typeof(T) == typeof(int))
                {
                    query = $"SELECT COUNT(*) FROM {tableName} WHERE {columnName} = CAST(@PropertyValue AS INT)";
                }
                else if (typeof(T) == typeof(decimal))
                {
                    query = $"SELECT COUNT(*) FROM {tableName} WHERE {columnName} = CAST(@PropertyValue AS DECIMAL)";
                }
                try
                {
                    rowCount = _db.ExecuteScalar<int>(query, param);
                }
                catch (Exception ex)
                {
                    continue;
                }
                if (rowCount > 0)
                {
                    result = $" table ==> {tableName}--- Column ==>{columnName}";
                    matchingColumns.Add(result);
                }
            }

            return matchingColumns;
        }

        public void GetPropertyValueInTable<T>(T dto, List<string> Tables) where T : class 
        {
            List<string> result = new List<string>();
            // just create a file to append on it 
          var file =  FileHelper.CreateOrAppendFileStream(FilePath, FileCreatingOptions.Create);
            FileHelper.CloseFileStream(file);

            foreach (var table in Tables)
            {
                PropertyInfo[] properties = typeof(T).GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    #region write header
                    WriteHeader(table, FilePath);
                    #endregion


                    string parameterName = $"@{property.Name}";
                    object value = property.GetValue(dto);
                    if (value != null)
                    {
                        var propertyType = value.GetType();

                        if (propertyType == typeof(string))
                        {
                            result = GetColumnNamesOfPropertyInSpecificTable<string>(value.ToString(), table);

                            #region check for resutl and write in doc 
                            WriteResultInFile(result, parameterName,value, FilePath);
                            #endregion


                            result.Clear();
                        }
                        else if (propertyType == typeof(int))
                        {

                            result = GetColumnNamesOfPropertyInSpecificTable<int>((int)value, table);
                            #region check for resutl and write in doc 
                            WriteResultInFile(result, parameterName, value, FilePath);
                            #endregion
                            result.Clear();


                        }
                        else if (propertyType == typeof(decimal))
                        {
                            result = GetColumnNamesOfPropertyInSpecificTable<decimal>((decimal)value, table);

                            #region check for resutl and write in doc 
                            WriteResultInFile(result, parameterName, value, FilePath);
                            #endregion
                            result.Clear();
                        }
                        else if (propertyType == typeof(bool))
                        {
                            result = GetColumnNamesOfPropertyInSpecificTable<bool>((bool)value, table);

                            #region check for resutl and write in doc 
                            WriteResultInFile(result, parameterName, value, FilePath);
                            #endregion
                            result.Clear();
                        }
                        else if ((propertyType == typeof(byte)))
                        {
                            result = GetColumnNamesOfPropertyInSpecificTable<byte>((byte)value, table);

                            #region check for resutl and write in doc 
                            WriteResultInFile(result, parameterName, value, FilePath);
                            #endregion

                            result.Clear();
                        }

                        else if (((propertyType == typeof(DateTime))))
                        {
                            result = GetColumnNamesOfPropertyInSpecificTable<DateTime>((DateTime)value, table);


                            #region check for resutl and write in doc 
                            WriteResultInFile(result, parameterName, value, FilePath);
                            #endregion
                            result.Clear();
                        }
                        else
                        {

                        }
                    }
                    #region footer
                    WriteFooter(table, FilePath);
                    #endregion


                }
            }

        }

        public void WriteFooter(string tableName, string FilePath)
        {
            FileStream file = FileHelper.CreateOrAppendFileStream(FilePath, FileCreatingOptions.Append);
            StreamWriter writer = FileHelper.CreateStreamWriter(file);
            writer.WriteLine($"End of Searching in table -- {tableName} ");
            writer.WriteLine();
            FileHelper.CloseFileStreamAndStreamWriter(file, writer);
        }

        public void WriteHeader(string tableName, string FilePath)
        {
            FileStream file = FileHelper.CreateOrAppendFileStream(FilePath, FileCreatingOptions.Append);
           StreamWriter writer= FileHelper.CreateStreamWriter(file);
            writer.WriteLine();
            writer.WriteLine("##########");
            writer.WriteLine($"Searching In Table --  {tableName}  ---  :");
            writer.WriteLine();
            FileHelper.CloseFileStreamAndStreamWriter(file, writer);
        }

        public void WriteResultInFile(List<string> result, string parameterName,object value,string pathFile)
        {
           FileStream file = FileHelper.CreateOrAppendFileStream(pathFile, FileCreatingOptions.Append);
           StreamWriter writer= FileHelper.CreateStreamWriter(file);
            if (result != null)
            {
                writer.WriteLine($"Searching for column ({parameterName}) and its value ={value} ");
                writer.WriteLine($"Matching  Columns  : ");
                foreach (string item in result)
                {
                    writer.WriteLine(item);
                }
                writer.WriteLine("-----");
                writer.WriteLine();
            }
            FileHelper.CloseFileStreamAndStreamWriter(file, writer);
        }

    }
}
