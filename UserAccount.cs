using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StickyBot.Core.UserAccounts
{
    public class UserAccount
    {
        public ulong ID { get; set; }

        public uint Level { get; set; }

        public uint Skill { get; set; }

        public uint LevelNumber
        {
            get
            {
                return (uint)Math.Sqrt(Skill / 50);
            }
        }

        public bool IsTimeout { get; set; }

        public uint NumberOfWarnings { get; set; }


    }
}
