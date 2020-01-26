using System;
using System.Collections.Generic;

namespace ScrambleEncrypt.Keys
{
    public class KeyBlock
    {
        private int _blocksize;

        private int[] _targets;

        public int KeyLength
        {
            get
            {
                return _blocksize;
            }
        }

        public int this[int loc]
        {
            get
            {
                return _targets[loc];
            }
        }

        /// <summary>
        /// This is a simple example of how to generate a key but ultimately you can implement any method that 
        /// generates can consistently generate the same set of bytes from a given password and length.
        /// </summary>
        /// <param name="keyLength">Length of the scrambler key</param>
        /// <param name="password">Password that will be used as a seed to generate the key</param>
        public KeyBlock(int keyLength, string password)
        {
            _blocksize = keyLength;
            _targets = new int[_blocksize];
            Random rnd = new Random(password.GetHashCode());

            List<int> indexes = new List<int>();

            for(int i = 0; i < keyLength; ++i)
            {
                indexes.Add(i);
            }

            for(int i = indexes.Count - 1; i>= 0; --i)
            {
                int nextT = rnd.Next(0, indexes.Count);
                _targets[i] = indexes[nextT];
                indexes.RemoveAt(nextT);
            }
        }


    }
}
