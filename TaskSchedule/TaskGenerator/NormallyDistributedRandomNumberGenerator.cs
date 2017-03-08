using System;
using System.Xml.Serialization;

namespace TaskGenerator
{
    public class NormallyDistributedRandomNumberGenerator
    {
        private Random random;
        private double previous = Double.NaN;

        public NormallyDistributedRandomNumberGenerator(Random random)
        {
            this.random = random;
        }

        public double GetNextDouble()
        {
            if (!double.IsNaN(previous))
            {
                var res = previous;
                previous = Double.NaN;
                return res;
            }

            double x, y, s;
            do
            {
                x = 2 * random.NextDouble() - 1;
                y = 2 * random.NextDouble() - 1;

                s = x * x + y * y;
            } while (x <= -1 || y <= -1 || s >= 1 || s <= double.Epsilon);

            var r = Math.Sqrt(-2 * Math.Log(s) / s);
            previous = r * x;
            return r * y;
        }

        public double GetNextDouble(double expectation, double dispersion)
        {
            return (GetNextDouble() * dispersion) + expectation;
        }
    }
}