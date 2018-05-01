using System;

namespace NeuNet.Neural.Helpers
{
    public static class MathHelper
    {
        public static double Sigmoid(double value)
        {
            return 1.0 / (1.0 + Math.Exp(-value));
        }
    }
}
