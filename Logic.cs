using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator
{
    class Logic
    {
		public static bool and2(bool a, bool b)
		{
			return (a && b);
		}
		public static bool and3(bool a, bool b, bool c)
		{
			return (a && b && c);
		}
		public static bool and4(bool a, bool b, bool c, bool d)
		{
			return a && b && c && d;
		}
		public static bool and5(bool a, bool b, bool c, bool d, bool e)
		{
			return (a && b && c && d && e);
		}
		public static bool or2(bool a, bool b)
		{
			return (a || b);
		}
		public static bool or4(bool a, bool b, bool c, bool d)
		{
			return (a || b || c || d);
		}
		public static bool xor2(bool a, bool b)
		{
			return ((a || b) && !(a && b));
		}
		public static bool not(bool a)
		{
			return !a;
		}
		public static bool nor2(bool a, bool b)
		{
			return !(a || b);
		}
		public static bool nor3(bool a, bool b, bool c)
		{
			return !(a || b || c);
		}
		public static bool nor4(bool a, bool b, bool c, bool d)
		{
			return !(a || b || c || d);
		}
		public static bool or3(bool a, bool b, bool c)
		{
			return a || b || c;
		}
		public static bool nand3(bool a, bool b, bool c)
		{
			return !(a && b && c);

		}
		public static bool nand4(bool a, bool b, bool c, bool d)
		{
			return !(a && b && c && d);

		}
		public static bool nand2(bool a, bool b)
		{
			return !(a && b);

		}
		public static bool nand5(bool a, bool b, bool c, bool d, bool e)
		{
			return !(a && b && c && d && e);

		}
	}
}
