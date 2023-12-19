using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace authorization
{
    public class Items
    {
        public long ID { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }
    }

    public class Repository
    {
        public long ItemID {  get; set; }
        public int Count { get; set; }
    }

    public class Cash
    {
        public long ID;
        public int Money { get; set; }

        public Cash(long iD, int money)
        {
            ID = iD;
            Money = money;
        }
    }
}
