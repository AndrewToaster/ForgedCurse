using System;
using System.Collections.Generic;
using System.Text;

namespace ForgedCurse.Utility
{
	/// <summary>
	/// MurmurHash2 by jibit
	/// </summary>
	/// <remarks>
	/// https://github.com/jitbit/MurmurHash.net/
	/// </remarks>
	public static class MurmurHash2
	{
		public static uint Hash(string data)
		{
			byte[] dataByte = System.Text.Encoding.UTF8.GetBytes(data);
			return Hash(dataByte);
		}

		public static uint Hash(byte[] data)
		{
			//return Hash(data, 0xc58f1a7a);
			int length = data.Length;
			return Hash(Normalize(data), 1, length);
		}

		const uint m = 0x5bd1e995;
		const int r = 24;
		public static uint Hash(byte[] data, uint seed, int length)
		{

			if (length == 0)
				return 0;
			uint h = seed ^ (uint)length;
			int currentIndex = 0;
			while (length >= 4)
			{
				uint k = (uint)(data[currentIndex++] | data[currentIndex++] << 8 | data[currentIndex++] << 16 | data[currentIndex++] << 24);
				k *= m;
				k ^= k >> r;
				k *= m;

				h *= m;
				h ^= k;
				length -= 4;
			}
			switch (length)
			{
				case 3:
					h ^= (UInt16)(data[currentIndex++] | data[currentIndex++] << 8);
					h ^= (uint)(data[currentIndex] << 16);
					h *= m;
					break;
				case 2:
					h ^= (UInt16)(data[currentIndex++] | data[currentIndex] << 8);
					h *= m;
					break;
				case 1:
					h ^= data[currentIndex];
					h *= m;
					break;
				default:
					break;
			}

			h ^= h >> 13;
			h *= m;
			h ^= h >> 15;

			return h;
		}

		public static byte[] SubArray(byte[] data, int index, int length)
		{
			byte[] result = new byte[length];
			Array.Copy(data, index, result, 0, length);
			return result;
		}

		public static byte[] Normalize(byte[] array)
		{

			int bufferSize = array.Length;

			int counter = 0;
			byte c;

			for (int a = 0; a < bufferSize; a++)
			{

				c = array[a];

				if (!(c == 9 || c == 10 || c == 13 || c == 32)) //No es espacio
				{
					array[counter] = array[a];
					counter++;
				}
			}

			return SubArray(array, 0, counter);

		}

		public static uint HashNormalize(byte[] array)
		{

			int bufferSize = array.Length;

			int counter = 0;
			byte c;

			for (int a = 0; a < bufferSize; a++)
			{

				c = array[a];

				if (!(c == 9 || c == 10 || c == 13 || c == 32)) //No es espacio
				{
					array[counter] = array[a];
					counter++;
				}
			}

			return Hash(array, 1, counter);
		}
	}

}
