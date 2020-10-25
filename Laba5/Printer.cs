using System;

namespace OOP_Laba5 {
	class Printer {
		public static void IAmPrinting(ref IGetInfo obj) {
			switch (obj.GetType().Name) {
				case "Product":
					Console.WriteLine("Product");
					((Product)obj).GetInfo();
					break;
				case "Technique":
					Console.WriteLine("Technique");
					((Technique)obj).GetInfo();
					break;
				case "PrintingDevice":
					Console.WriteLine("PrintingDevice");
					((PrintingDevice)obj).GetInfo();
					break;
				case "Scanner":
					Console.WriteLine("Scanner");
					((Scanner)obj).GetInfo();
					break;
				case "Computer":
					Console.WriteLine("Computer");
					((Computer)obj).GetInfo();
					break;
				case "Tablet":
					Console.WriteLine("Tablet");
					((Tablet)obj).GetInfo();
					break;
				default:
					Console.WriteLine("Тип объекта не распознан");
					break;
			}
		}

		public static void IAmPrinting(ref object obj) {
			switch (obj.GetType().Name) {
				case "Product":
					Console.WriteLine("Product");
					((Product)obj).GetInfo();
					break;
				case "Technique":
					Console.WriteLine("Technique");
					((Technique)obj).GetInfo();
					break;
				case "PrintingDevice":
					Console.WriteLine("PrintingDevice");
					((PrintingDevice)obj).GetInfo();
					break;
				case "Scanner":
					Console.WriteLine("Scanner");
					((Scanner)obj).GetInfo();
					break;
				case "Computer":
					Console.WriteLine("Computer");
					((Computer)obj).GetInfo();
					break;
				case "Tablet":
					Console.WriteLine("Tablet");
					((Tablet)obj).GetInfo();
					break;
				default:
					Console.WriteLine("Тип объекта не распознан");
					break;
			}
		}
	}
}
