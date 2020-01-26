using ScrambleEncrypt.Keys;

namespace ScrambleEncrypt.IO
{
    /// <summary>
    /// A simple kind of shift encryption that where you can supply two ends with a password and a key length which will generate the same key block on each end.
    /// It will then reorder all the data based on the key making it unreadable and then reorder it back to the original data on the other end.
    /// "Hello world!" could for example become "oeolrdwHl! l" while in transit and then revert back using the correct key.
    /// What would make it annoying to crack is that you must for instance put the correct l in "Hello" in the correct position otherwise your next segment of data will be wrong again because your underlying key that you built to crack it will be wrong. And in large messages there can be hundreds or thousands+ of instances of the same bytes all over the message
    /// You could also take the algorithm a step further and move the individual bits of the data around at the cost of looping over 8x more positions.
    /// </summary>
    public static class EncryptorDecryptor
    {
        public static byte[] ScrambleEncrypt(byte[] data, KeyBlock key)
        {
            byte[] result = new byte[data.Length];

            int keyPos = 0;
            int keyShift = 0;

            for(int i = 0; i < data.Length; ++i)
            {
                while (key[keyPos] >= ((data.Length - keyShift))) 
                {
                    keyPos++;
                    if(keyPos >= key.KeyLength)
                    {
                        keyPos = 0;
                        keyShift = i+1;
                    }
                }

                result[i] = data[key[keyPos] + keyShift];
                keyPos++;

                if (keyPos >= key.KeyLength)
                {
                    keyPos = 0;
                    keyShift = i+1;
                }
            }

            return result;
        }

        public static byte[] ScrambleDecrypt(byte[] scrambledData, KeyBlock key)
        {
            byte[] result = new byte[scrambledData.Length];

            int keyPos = 0;
            int keyShift = 0;

            for (int i = 0; i < scrambledData.Length; ++i)
            {
                while (key[keyPos] >= ((scrambledData.Length - keyShift)))
                {
                    keyPos++;
                    if (keyPos >= key.KeyLength)
                    {
                        keyPos = 0;
                        keyShift = i+1;
                    }
                }

                result[key[keyPos] + keyShift] = scrambledData[i];
                keyPos++;

                if (keyPos >= key.KeyLength)
                {
                    keyPos = 0;
                    keyShift = i+1;
                }
            }

            return result;
        }
    }
}
