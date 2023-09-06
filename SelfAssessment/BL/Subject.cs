using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfAssessment.BL
{
    public class Subject
    {
        public string code;
        public string type;
        public int creditHours;
        public double fees;
        public Subject(string code, string type, int creditHours, double fees)
        {
            this.code = code;
            this.type = type;
            this.creditHours = creditHours;
            this.fees = fees;
        }
        public Subject(string code)
        {
            this.code = code;
        }
    }
}
