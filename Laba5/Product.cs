using System;

namespace OOP_Laba5 {
	abstract class Product : IGetInfo, IComparable {
		public string Brand { get; private set; }
		public string Name { get; private set; }
		public string Color { get; private set; }
		public double Price { get; private set; }
		public double Weight { get; private set; }
		public (int Width, int Height, int Lenght) Dimensions { get; private set; }

		public Product(
			string brand,
			string name,
			string color,
			double price,
			double weight,
			(int Width, int Height, int Length) dimensions) {
			Brand = brand;
			Name = name;
			Color = color;
			Price = price;
			Weight = weight;
			Dimensions = dimensions;
		}

		public override string ToString() {
			return $"Производитель: {Brand}; Название: {Name}; Цвет: {Color}; Цена: {Price} руб; Вес: {Weight} кг; Габариты: {Dimensions.Width}мм, {Dimensions.Height}мм, {Dimensions.Lenght}мм";
		}

		public abstract void GetInfo();

		public int CompareTo(object obj) {
			return Price.CompareTo(obj);
		}
	}
}
