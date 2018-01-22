using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VHD_Director
{
    public class SourceCredits
    {
        public List<SourceCredit> list = new List<SourceCredit>();
        public void Add(SourceCredit credit)
        {
            list.Add(credit);
        }

        internal void Add(string p, string p_2, string p_3)
        {
            SourceCredit credit = new SourceCredit();
            credit.Author = p;
            credit.Url = p_3;
            credit.Product = p_2;
            this.Add(credit);
        }
    }
}
