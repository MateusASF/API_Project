using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIEvents.Core.CustomAtributtes
{
    internal class CustomAtributteDate : RangeAttribute
    {
        internal CustomAtributteDate() : base(typeof(DateTime), DateTime.Now.ToString(), DateTime.MaxValue.ToString())
        {
        }
    }
}
