using System;
using System.Collections.Generic;
using System.Numerics;

namespace Shamir
{
    class Program
    {
        // Meant to work with Int and NOT BigIntegers
        static void Main(string[] args)
        {
            Console.WriteLine("Enter Secret: ");
            var secret = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();
            Console.WriteLine("Enter # of shares (must be > 3) : ");
            var numOfSharesN = Convert.ToInt32(Console.ReadLine());
            numOfSharesN = InputValidation.ValidateInput(numOfSharesN, 3, "The number of Shares must be at least 4!", "Enter new # of shares: ");

            Console.WriteLine();
            Console.WriteLine("Enter threshold (must be > 2) : ");
            var thresholdK = Convert.ToInt32(Console.ReadLine());
            thresholdK = InputValidation.ValidateInput(thresholdK, 2, "Threshold must be at least 3!", "Enter new threshold: ");

            Console.WriteLine();
            Console.WriteLine("Enter Finite Field (must be > than the Secret): ");
            var fField = Convert.ToInt32(Console.ReadLine());
            fField = InputValidation.ValidateInput(fField, secret, "Threshold must be greater than the Secret value!",
                "Enter new finite Field value: ");

            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("~~~ Original secret: " + secret + " ~~~");
            Console.WriteLine("~~~ Number of Shares N: " + numOfSharesN + " ~~~");
            Console.WriteLine("~~~ Threshold to Reconstruct K: " + thresholdK + " ~~~");
            Console.WriteLine("~~~ Finite Field: " + fField + " ~~~");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("~~~ 1. Generate Shares (Points) for Reconstruction ~~~");
            List<Point> points = ShamirSecretSharing.SplitSecret(secret, numOfSharesN, thresholdK, fField);
            Console.WriteLine();

            Console.WriteLine("~~~ 2. Reconstruct based on Random " + thresholdK + " Shares ~~~");
            List<Point> pointsToReconstruct = ShamirSecretSharing.GetRandomPoints(points, thresholdK);
            BigInteger reconstructedSecret = ShamirSecretSharing.ReconstructSecret(pointsToReconstruct, fField);

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("The reconstructed secret: " + reconstructedSecret);

            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
