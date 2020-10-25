using System.Collections.Generic;
using System.Linq;

namespace OOP_Laba4 {
	static class StatisticOperation {
		public static double? Sum(this Set<double> set) {
			double? sum = null;
			foreach (var item in set) {
				if (!sum.HasValue)
					sum = 0.0;
				sum += item;
			}
			return sum;
		}

		public static double? MinMaxDiff(this Set<double> set) {
			double? diff = null;
			if (set.Data.Count != 0) {
				diff = set.Data.Max() - set.Data.Min();
			}
			return diff;
		}

		public static int Count(this Set<double> set) {
			var list = new List<double>();
			int counter = 0;
			foreach (var item in set) {
				if (list.Contains(item))
					continue;
				else {
					list.Add(item);
					counter++;
				}
			}
			return counter;
		}

		public static (List<string> Result, bool OnlyStr) Shortest(this Set<string> set) {
			int length = int.MaxValue;
			var result = new List<string>();
			//var numbers = new List<double>();
			for (int i = 0; i < 2; i++) {
				foreach (var item in set) {
					if (i == 0) {
						if (double.TryParse(item, out double tRes)) {
							//numbers.Add(tRes);
							continue;
						}
					}
					if (item.Length == length)
						result.Add(item);
					else
					if (item.Length < length) {
						result.Clear();
						result.Add(item);
						length = item.Length;
					}
				}
				if (result.Count != 0 && i == 0)
					return (result, true);
			}
			return (result, false);
		}

		public static void Sort(this Set<double> set) {
			set.Data.Sort();
		}
	}
	enum SortType {
		NumsStr,
		StrsNums
	}
}
