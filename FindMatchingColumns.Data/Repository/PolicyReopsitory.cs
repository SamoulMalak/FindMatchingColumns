using Dapper;
using FindMatchingColumns.Data.Entities;
using FindMatchingColumns.Data.IRepository;
using Microsoft.Data.SqlClient;
using System.Data;

namespace FindMatchingColumns.Data.Repository
{
    public class PolicyReopsitory : IPolicyReopsitory
    {
        IDbConnection _db;
        public PolicyReopsitory(string ConnectionString)
        {
            _db =  new SqlConnection(ConnectionString);
        }
        public Policy GetPolicyById(int Id)
        {
          var result =  _db.Query<Policy>("select * from TR_PolicyDetails where PolicyID=@Id",new {Id}).FirstOrDefault();
            return result;
        }
    }
}
