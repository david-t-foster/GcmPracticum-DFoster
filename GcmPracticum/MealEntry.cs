using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GcmPracticum
{
    public class MealEntry
    {
        public MealEntry(string[] dishes, int[] allowedMultiple)
        {
            this.Dishes = dishes;
            this.AllowedMultiple = allowedMultiple;
        }
        public string[] Dishes { get; private set; }
        public int[] AllowedMultiple { get; private set; }
    }
}
