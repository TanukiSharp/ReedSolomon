using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReedSolomon;

namespace ReedSolomonLibraryTester
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().Run();
        }

        private List<int> changePositions = new List<int>();

        private void Run()
        {
            byte[] originalMessage = Encoding.UTF8.GetBytes("Nervously I loaded the twin ducks aboard the revolving platform.");

            var ecc = ReedSolomonAlgorithm.Encode(originalMessage, 32);

            Console.WriteLine($"Original message: {Encoding.UTF8.GetString(originalMessage)}");
            Console.WriteLine($"Original ecc:     {ByteArrayToString(ecc)}");
            Console.WriteLine();

            ByteError(0x35, 3, originalMessage);
            ByteError(0x51, 8, ecc);

            ByteErasure(17, originalMessage);
            ByteErasure(21, originalMessage);
            ByteErasure(16, ecc);
            ByteErasure(18, ecc);
            Console.WriteLine();

            Console.WriteLine($"Damaged message:  {Encoding.UTF8.GetString(originalMessage)}");
            Console.WriteLine($"Damaged ecc:      {ByteArrayToString(ecc)}");
            Console.WriteLine();

            //var newEcc = new byte[32];
            //Array.Copy(ecc, newEcc, 32);

            byte[] decodedMessage = ReedSolomonAlgorithm.Decode(originalMessage, ecc);

            Console.WriteLine($"Decoded message:  {Encoding.UTF8.GetString(decodedMessage)}");
        }

        /* Introduce a byte error at LOC */
        private void ByteError(byte err, int loc, byte[] dst)
        {
            Console.WriteLine($"Adding Error at loc {loc}, data {dst[loc - 1]:X2}");
            dst[loc - 1] ^= err;
        }

        /* Pass in location of error (first byte position is
           labeled starting at 1, not 0), and the codeword.
        */
        private void ByteErasure(int loc, byte[] dst)
        {
            Console.WriteLine($"Erasure at loc {loc}, data {dst[loc - 1]:X2}");
            dst[loc - 1] = 0;
        }

        private string ByteArrayToString(byte[] array)
        {
            return string.Join(", ", array.Select(x => $"{x:x2}"));
        }
    }
}
