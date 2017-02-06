using System;

namespace Okiroya.Cms.SystemUtility
{
    public class GroupNormalDistribution : IGroupRandomDistribution
    {
        private double _mean;
        private double _sigma;
        private Random _rnd;

        public GroupNormalDistribution(double mean = 0, double sigma = 1)
        {
            _mean = mean;
            _sigma = sigma;

            _rnd = new Random();
        }

        public byte GetNextGroup()
        {
            double p1 = _rnd.NextDouble(), p2 = _rnd.NextDouble();

            var random = _mean + _sigma * (Math.Sqrt(-2.0 * Math.Log(p1)) * Math.Sin(2.0 * Math.PI * p2));

            return checked((byte)Math.Round(random * 100));
        }
    }
}
