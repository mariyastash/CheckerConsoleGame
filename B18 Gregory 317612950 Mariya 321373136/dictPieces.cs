using System;
using System.Collections.Generic;
using System.Text;

namespace B18_Gregory_317612950_Mariya_321373136
{
    public class dictPieces
    {
		public Dictionary<int, string> dictRow = new Dictionary<int, string>()
		{
			{0, "a" },
			{1, "b" },
			{2, "c" },
			{3, "d" },
			{4, "e" },
			{5, "f" },
			{6, "g" },
			{7, "h" },
			{8, "i" },
			{9, "j" }
		};
		public Dictionary<int, string> dictCol = new Dictionary<int, string>()
		{
			{0, "A" },
			{1, "B" },
			{2, "C" },
			{3, "D" },
			{4, "E" },
			{5, "F" },
			{6, "G" },
			{7, "H" },
			{8, "I" },
			{9, "J" }
		};

        public int GetKeyByValue(string i_Value, Dictionary<int, string> i_Dictionary)
        {
            int returnKey = -1; ;

            foreach(KeyValuePair<int, string> pair in i_Dictionary)
            {
                if( pair.Value == i_Value )
                {
                    returnKey = pair.Key;
                }
            }

            return returnKey;
        }
    }
}
