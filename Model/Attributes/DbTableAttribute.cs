using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Attributes
{
    public class DbTableAttribute : Attribute
    {
        public string Name { get; private set; }

        public DbTableAttribute(string name)
        {
            this.Name = name;
        }

    }
}
