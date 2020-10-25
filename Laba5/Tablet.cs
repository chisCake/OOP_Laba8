﻿namespace OOP_Laba5 {
	sealed class Tablet : Computer {
		public double DisplaySize { get; private set; }
		public (int X, int Y) DisplayResolution { get; private set; }
		public int BatteryCapacity { get; private set; }

		public Tablet(
			string brand,
			string name,
			string color,
			double price,
			double weight,
			(int Width, int Height, int Length) dimensions,
			double powerConsumption,
			string os,
			string cpu,
			string gpu,
			double ram,
			double storageCapacity,
			double displaySize,
			(int X, int Y) displayResolution,
			int batteryCapacity) :
			base(brand, name, color, price, weight, dimensions, powerConsumption, os, cpu, gpu, ram, storageCapacity) {
			DisplaySize = displaySize;
			DisplayResolution = displayResolution;
			BatteryCapacity = batteryCapacity;
		}

		public override string ToString() {
			return "Планшет\n" + base.ToString() + $"\nДиагональ экрана: {DisplaySize}\"; Разрешение экрана: {DisplayResolution.X}*{DisplayResolution.Y}";
		}
	}
}
