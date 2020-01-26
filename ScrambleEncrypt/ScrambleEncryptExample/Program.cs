using ScrambleEncrypt.IO;
using ScrambleEncrypt.Keys;
using System;
using System.Text;

namespace ScrambleEncryptExample
{
    class Program
    {
        static void Main(string[] args)
        {
            StringEncryptDecrypt();

            Console.ReadLine();
        }

        static void StringEncryptDecrypt()
        {
            KeyBlock encKey = new KeyBlock(64, "SeedPassword123*");

            string Original1 = "Some string shorter than 64";
            string Original2 = "Some string longer than 64 which is the same as the shorter one but with extra words to make it longer.";
            string Original3 = "Some string longer than 64 which is the same as the shorter one but with extra words to make it longer but I want to go all the way past 128.";

            Console.WriteLine();
            Console.WriteLine(Original1);
            Console.WriteLine(Original2);
            Console.WriteLine(Original3);

            byte[] scrambleBytes1 = EncryptorDecryptor.ScrambleEncrypt(Encoding.UTF8.GetBytes(Original1), encKey);
            byte[] scrambleBytes2 = EncryptorDecryptor.ScrambleEncrypt(Encoding.UTF8.GetBytes(Original2), encKey);
            byte[] scrambleBytes3 = EncryptorDecryptor.ScrambleEncrypt(Encoding.UTF8.GetBytes(Original3), encKey);

            string scrambleString1 = Encoding.UTF8.GetString(scrambleBytes1);
            string scrambleString2 = Encoding.UTF8.GetString(scrambleBytes2);
            string scrambleString3 = Encoding.UTF8.GetString(scrambleBytes3);

            Console.WriteLine();
            Console.WriteLine(scrambleString1);
            Console.WriteLine(scrambleString2);
            Console.WriteLine(scrambleString3);

            KeyBlock decKey = new KeyBlock(64, "SeedPassword123*");

            byte[] deScrambled1 = EncryptorDecryptor.ScrambleDecrypt(scrambleBytes1, decKey);
            byte[] deScrambled2 = EncryptorDecryptor.ScrambleDecrypt(scrambleBytes2, decKey);
            byte[] deScrambled3 = EncryptorDecryptor.ScrambleDecrypt(scrambleBytes3, decKey);

            string deScrambledString1 = Encoding.UTF8.GetString(deScrambled1);
            string deScrambledString2 = Encoding.UTF8.GetString(deScrambled2);
            string deScrambledString3 = Encoding.UTF8.GetString(deScrambled3);

            Console.WriteLine();
            Console.WriteLine(deScrambledString1);
            Console.WriteLine(deScrambledString2);
            Console.WriteLine(deScrambledString3);
        }
    }
}
