using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Attributes
{
    public class DbColumnAttribute : Attribute
    {
        public string Name { get; private set; }

        public DbColumnAttribute(string name)
        {
            this.Name = name;
        }
    }
}
