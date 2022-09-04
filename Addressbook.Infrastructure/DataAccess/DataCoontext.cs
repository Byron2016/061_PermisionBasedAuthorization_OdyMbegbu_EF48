using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Addressbook.Infrastructure.DataAccess
{
    public class DataCoontext : DbContext
    {
        public DataCoontext() : base("DataEntities")
        {

        }
    }
}
