using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleTextMenu.Model
{
    class MenuItemObject : IMenuItem
    {
        public string title;
        public string getDisplayText()
        {
            return title;
        }
    }
}