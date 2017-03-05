using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class RepositoryEmployee : IRepository
    {
        Database db = new Database();

        public IEnumerable GetAll()
        {
            return db.Employees.ToList();
        }

        public dynamic GetById(int id)
        {
            return db.Employees.Find(id);
        }
    }
}
