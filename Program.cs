using System;
using System.Collections.Generic;

using OOP_Laba4;

using OOP_Laba5;

namespace OOP_Laba8 {
	class Program {
		static List<Set<double>> List { get; set; }
		delegate void Delegate();
		static readonly List<Delegate> Tasks = new List<Delegate> {
			new Delegate(Plus),
			new Delegate(Minus),
			new Delegate(Subset),
			new Delegate(Inequality),
			new Delegate(Intersection),
			new Delegate(Info),
			new Delegate(Sum),
			new Delegate(MinMaxDiff),
			new Delegate(Counting),
			//new Delegate(ShortestStr),
			new Delegate(Sort),
			new Delegate(WriteToFile),
			new Delegate(ReadFromFile)
		};

		static Set<Product> products = new Set<Product>(
			new PrintingDevice("Epson", "L132", "Чёрный", 392, 2.7, (482, 130, 222), 10, PrintingTechnology.Inkjet, (5760, 1440), 27),
			new PrintingDevice("Canon", "Pixma TS304", "Чёрный", 140, 2.9, (430, 143, 282), 10, PrintingTechnology.Inkjet, (4800, 1200), 7.7),
			new Scanner("Epson", "Perfection V19", "Чёрный", 243, 1.54, (248, 364, 390), 2.5, (4800, 4800), 48),
			new Scanner("Canon", "CanoScan LiDE 400", "Чёрный", 248.15, 1.7, (250, 367, 420), 4.5, (4800, 4800), 48),
			new Computer("Jet", "Multimedia GE20D8SD24VGALW50", "Чёрный", 649, 0, (0, 0, 0), 500, "Windows", "AMD Athlon 200GE", "AMD Radeon Vega 3", 8, 240),
			new Computer("Z-Tech", "J190-4-10-miniPC-D-0001n", "Чёрный", 529, 0, (0, 0, 0), 500, "Windows", "Intel Celeron J1900", "Intel HD Graphics", 4, 1000),
			new Tablet("Lenovo", "Tab E7 TB-7104I", "Чёрный", 189, 0.272, (110, 193, 10), 0, "Android 8.0", "MediaTek MT8321A/D", "ARM Mali-400 MP", 1, 16, 7, (1024, 600), 2750),
			new Tablet("Lenovo", "M10 Plus TB-X606F", "Чёрный", 599, 0.460, (244, 153, 8), 0, "Android 9.0", "MediaTek Helio P22T", "IMG GE8320", 4, 64, 10.3, (1920, 1200), 5000)
		);

		static void Main() {
			List = new List<Set<double>> {
					new Set<double>(new Set<double>.Owner("Set1"), 1, 2, 3, 4, 5, 6, 7, 8, 9, 10),
					new Set<double>(new Set<double>.Owner("Set2"), 1, 2, 3),
					new Set<double>(new Set<double>.Owner("Set3"), 2, 4, 6),
					new Set<double>(new Set<double>.Owner("Set4"), 1, 2, 3),
					new Set<double>(new Set<double>.Owner("Set5"), 1, 2, 5, -100.255, 2, 450, 2, 5)
				};


			while (true) {
				try {
					Console.Clear();
					Console.WriteLine("Текущие множества:");
					for (int i = 0; i < List.Count; i++) {
						Console.Write($"{i + 1}) {((IGetInfo)List[i]).GetOwnerName()}: ");
						foreach (var item in ((IGetInfo)List[i]).GetData())
							Console.Write(item + " ");
						Console.WriteLine();
					}

					Console.Write(
						"\n1) Операция сложения" +
						"\n2) Операция вычитания" +
						"\n3) Проверка на подмножество" +
						"\n4) Проверка на неравенство" +
						"\n5) Проверка на пересечение" +
						"\n6) Вывод информации о множестве" +
						"\n7) Сумма элементов" +
						"\n8) Разница между минимальным и максимальным элементом" +
						"\n9) Подсчёт кол-ва элементов" +
						//"\n10) Поиск самого короткого слова" +
						"\n10) Упорядочить множество" +
						"\n11) Запись в файл" +
						"\n12) Чтение из файла" +
						"\n0) Выход" +
						"\nВыберите действие: "
						);

					if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 0 && choice <= Tasks.Count) {
						if (choice == 0) {
							Console.WriteLine("Выход...");
							return;
						}
						Console.WriteLine();
						Tasks[choice - 1]();
					}
					else
						Console.WriteLine("Нет такого варианта");
				}
				catch (Exception e) {
					Console.WriteLine($"\n\n\t{e.Message}\n{e.StackTrace}\n{e.InnerException}");
				}
				finally {
					Console.WriteLine("\n---Press any key---");
					Console.ReadKey();
				}
			}
		}

		#region Services

		// Выбор множеств
		static Set<double> GetSet() {
			int set;
			do {
				Console.Write("\nВведите номер множества: ");
				if (int.TryParse(Console.ReadLine(), out set)) {
					if (set == 0)
						return null;
					if (set > 0 && set <= List.Count)
						break;
					else
						Console.WriteLine("Нет такого множества");
				}
			} while (true);
			return List[set - 1];
		}

		static (Set<double>, Set<double>) GetSets() {
			int set1, set2;
			do {
				Console.Write("Введите номер 1го множества: ");
				if (int.TryParse(Console.ReadLine(), out set1)) {
					if (set1 == 0)
						return (null, null);
					if (set1 > 0 && set1 <= List.Count)
						break;
					else
						Console.WriteLine("Нет такого множества");
				}
			} while (true);
			do {
				Console.Write("Введите номер 2го множества: ");
				if (int.TryParse(Console.ReadLine(), out set2)) {
					if (set2 == 0)
						return (null, null);
					if (set2 > 0 && set2 <= List.Count)
						break;
					else
						Console.WriteLine("Нет такого множества");
				}
			} while (true);
			return (List[set1 - 1], List[set2 - 1]);
		}

		//Ввод имени множества
		static void SetName(ref Set<double> set) {
			Console.Write("Нажмите \"1\" чтобы дать уникальное название новому множеству: ");
			if (Console.ReadKey().KeyChar == '1') {
				Console.Write("\nВведите название множества: ");
				set.OwnerData.Name = Console.ReadLine();
			}
			else
				set.OwnerData.Name = $"Set{List.Count + 1}";
		}

		#endregion

		static void Plus() {
			Set<double> set = GetSet();
			if (Equals(set, null))
				return;
			Console.Write("Введите с чем сложить это множество: ");
			if (!double.TryParse(Console.ReadLine(), out double item))
				throw new Exception("NaN entered");
			var result = set + item;
			SetName(ref result);
			List.Add(result);
		}

		static void Minus() {
			Set<double> set = GetSet();
			if (Equals(set, null))
				return;
			Console.Write("Введите что вычесть из этого множества: ");
			if (!double.TryParse(Console.ReadLine(), out double item))
				throw new Exception("NaN entered");
			var result = set - item;
			SetName(ref result);
			List.Add(result);
		}

		static void Subset() {
			Console.WriteLine("1 - подмножество, 2 - множество");
			var sets = GetSets();
			if (Equals(sets.Item1, null) || Equals(sets.Item2, null))
				return;
			if (sets.Item1 * sets.Item2)
				Console.WriteLine($"{sets.Item1.OwnerData.Name} является подмножеством {sets.Item2.OwnerData.Name}");
			else
				Console.WriteLine($"{sets.Item1.OwnerData.Name} не является подмножеством {sets.Item2.OwnerData.Name}");
		}

		static void Inequality() {
			var sets = GetSets();
			if (Equals(sets.Item1, null) || Equals(sets.Item2, null))
				return;
			if (sets.Item1 != sets.Item2)
				Console.WriteLine($"{sets.Item1.OwnerData.Name} не равно {sets.Item2.OwnerData.Name}");
			else
				Console.WriteLine($"{sets.Item1.OwnerData.Name} равно {sets.Item2.OwnerData.Name}");
		}

		static void Intersection() {
			var sets = GetSets();
			if (Equals(sets.Item1, null) || Equals(sets.Item2, null))
				return;
			if (sets.Item1 % sets.Item2)
				Console.WriteLine($"{sets.Item1.OwnerData.Name} пересекается с {sets.Item2.OwnerData.Name}");
			else
				Console.WriteLine($"{sets.Item1.OwnerData.Name} не пересекается с {sets.Item2.OwnerData.Name}");
		}

		static void Info() {
			var set = GetSet();
			Console.WriteLine("Элементы в множестве:");
			foreach (var item in set)
				Console.Write(item + " ");
			Console.WriteLine(
				$"\nID: {set.OwnerData.Id}" +
				$"\nИмя: {set.OwnerData.Name}" +
				$"\nОрганизация: {set.OwnerData.Organization}" +
				$"\nДата создания: {set.Date}"
				);
		}

		static void Sum() {
			var set = GetSet();
			var result = set.Sum();
			if (result.HasValue)
				Console.WriteLine($"Сумма всех чисел множества: {result.Value}");
			else
				Console.WriteLine("Множество не содержит числовых значений");
		}

		static void MinMaxDiff() {
			var set = GetSet();
			var result = set.MinMaxDiff();
			if (result.HasValue)
				Console.WriteLine($"Разница между самым минимальным и максимальным значением: {result.Value}");
			else
				Console.WriteLine("Множество не содержит числовых значений");
		}

		static void Counting() {
			var set = GetSet();
			Console.WriteLine($"Кол-во элементов во множестве: {set.Count()}");
		}

		//static void ShortestStr() {
		//	var set = GetSet();
		//	var result = set.Shortest();
		//	if (result.OnlyStr)
		//		if (result.Result.Count == 1)
		//			Console.WriteLine($"Самое короткое слово: {result.Result[0]}");
		//		else {
		//			Console.WriteLine("Самые короткие слова:");
		//			foreach (var item in result.Result)
		//				Console.Write(item + " ");
		//		}
		//	else
		//		if (result.Result.Count == 1)
		//		Console.WriteLine($"Слов во множестве нет, самое малозначное число: {result.Result[0]}");
		//	else {
		//		Console.WriteLine("Слов во множестве нет, самые малозначное числа:");
		//		foreach (var item in result.Result)
		//			Console.Write(item + " ");
		//	}
		//}

		static void Sort() {
			var set = GetSet();
			Console.WriteLine("До сортировки: ");
			foreach (var item in set)
				Console.Write(item + " ");
			set.Sort();
			Console.WriteLine("\nПосле сортировки: ");
			foreach (var item in set)
				Console.Write(item + " ");
		}

		static void WriteToFile() {
			var set = GetSet();
			Console.Write("Введите имя файла: ");
			var file = Console.ReadLine();
			set.Write(file);
			Console.WriteLine("Множество записано в файл");
		}

		static void ReadFromFile() {
			Console.Write("Введите имя файла: ");
			var file = Console.ReadLine();
			var result = Set<double>.Read(file);
			Console.WriteLine("Считанное множество содержит следующие элементы:");
			foreach (var item in result.Data) {
				Console.Write(item + " ");
			}
		}
	}
}
