using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Project_3___Press_Project
{
    public partial class Newspaper
    {
        public bool IsEAN13Registered(string ean13)
        {
            try
            {
                using (var context = new PressContext())
                {
                    var newspaper = (from c in context.Newspapers
                                     select c);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
