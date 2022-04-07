using System;
using System.Collections.Generic;

namespace Shamir
{
    public static class ShamirSecretSharing
    {
        public static List<Point> SplitSecret(int secret, int n, int k, int fField)
        {
            Random rnd = new Random();
            List<int> kNumbers = new () {0};

            for (int i = 0; i < k - 1; i++)
            {
                int kShare = rnd.Next(1, fField);
                Console.WriteLine("K" + i + " = " + kShare);
                kNumbers.Add(kShare);
            }

            return GenerateShares(secret, n, kNumbers, fField);
        }

        private static List<Point> GenerateShares(int secret, int n, List<int> kNumbers, int fField)
        {
            List<Point> points = new();
            var equation = secret;
            for (int x = 1; x < n + 1; x++)
            {
                for (int i = 1; i < kNumbers.Count; i++)
                {
                    var kVal = kNumbers[i];
                    var xVal = Math.Pow(x, i);
                    equation += kVal * Convert.ToInt32(xVal);
                }

                Point point = new Point()
                {
                    X = x,
                    Y = mod(equation, fField)
                };

                points.Add(point);
                equation = secret;
                Console.WriteLine("Share (Point) " + x + " : " + " (X: " + point.X + ", Y: " + point.Y + ")");
            }

            return points;
        }

        public static List<Point> GetRandomPoints(List<Point> points, int numOfPoints)
        {
            List<Point> pointsToReconstruct = new();
            Random rnd = new Random();
            while (pointsToReconstruct.Count < numOfPoints)
            {
                int index = rnd.Next(points.Count);
                if (points[index].Y != 0 && !pointsToReconstruct.Contains(points[index]))
                {
                    pointsToReconstruct.Add(points[index]);
                    Console.WriteLine("Share (Point) Added: " + points[index].X + " : " + points[index].Y);
                }
            }

            return pointsToReconstruct;
        }

        public static int ReconstructSecret(List<Point> points, int fField)
        {
            Decimal secret = 0;
            foreach (var point in points)
            {
                Decimal lagronge = 1;
                foreach (var otherPoint in points)
                {
                    Decimal divident = 1;
                    Decimal divisor = 1;
                    Decimal divisonResult = 1;
                    if (otherPoint != point)
                    {
                        divident *= (0 - otherPoint.X);
                        divisor *= (point.X - otherPoint.X);
                        divisonResult = divident / divisor;
                    }
                    lagronge *= divisonResult;
                }
                var pointResult = point.Y * lagronge;
                secret += pointResult;
            }

            int secretToInt = (int)Math.Round(secret);
            return mod(secretToInt, fField);
        }

        private static int mod(int k, int n) { return ((k %= n) < 0) ? k + n : k; }

        //private static int IntPow(int x, uint pow)
        //{
        //    int ret = 1;
        //    while (pow != 0)
        //    {
        //        if ((pow & 1) == 1)
        //            ret *= x;
        //        x *= x;
        //        pow >>= 1;
        //    }
        //    return ret;
        //}

        //public static BigInteger RandomIntegerBelow(BigInteger N)
        //{
        //    byte[] bytes = N.ToByteArray();
        //    BigInteger R;

        //    do
        //    {
        //        random.NextBytes(bytes);
        //        bytes[bytes.Length - 1] &= (byte)0x7F; //force sign bit to positive
        //        R = new BigInteger(bytes);
        //    } while (R >= N);

        //    return R;
        //}
    }
}
