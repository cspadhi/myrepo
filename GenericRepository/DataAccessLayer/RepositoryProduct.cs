using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class RepositoryProduct : IRepository
    {
        Database db = new Database();

        public IEnumerable GetAll()
        {
            return db.Products.ToList();
        }

        public dynamic GetById(int id)
        {
            return db.Products.Find(id);
        }
    }
}
