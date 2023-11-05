using FindMatchingColumns.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindMatchingColumns.Data.IRepository
{
    public interface IPolicyReopsitory
    {
       Policy GetPolicyById(int Id);
    }
}
