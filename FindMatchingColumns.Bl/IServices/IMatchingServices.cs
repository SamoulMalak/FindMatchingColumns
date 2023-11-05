using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindMatchingColumns.BL.IServices
{
    public interface IMatchingServices
    {
        List<string> GetColumnNamesOfPropertyInSpecificTable<T>(T propertyValue, string tableName);
        void GetPropertyValueInTable<T>(T dto, List<string> Tables)where T :class;
        void WriteResultInFile(List<string> result, string parameterName,object value, string pathFile);
        void WriteHeader(string tableName, string FilePath);
        void WriteFooter(string tableName, string FilePath);
        public string? FilePath { get; set; }

    }
}
