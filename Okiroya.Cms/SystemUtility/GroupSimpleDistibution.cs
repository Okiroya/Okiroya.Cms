using System;

namespace Okiroya.Cms.SystemUtility
{
    public class GroupSimpleDistibution : IGroupRandomDistribution
    {
        private Random _rnd;

        public GroupSimpleDistibution()
        {
            _rnd = new Random();
        }

        public byte GetNextGroup()
        {
            return checked((byte)Math.Round(_rnd.NextDouble() * 100));
        }
    }
}
