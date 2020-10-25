using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Newtonsoft.Json;

using OOP_Laba8;

#pragma warning disable CS0660 // Тип определяет оператор == или оператор !=, но не переопределяет Object.Equals(object o)
#pragma warning disable CS0661 // Тип определяет оператор == или оператор !=, но не переопределяет Object.GetHashCode()
namespace OOP_Laba4 {
	class Set<T> : IGeneric<T>, IGetInfo where T : IComparable {
		// Данные
		public List<T> Data { get; private set; }
		public Owner OwnerData { get; private set; } = new Owner();
		public DateTime Date { get; private set; } = DateTime.Now;

		// Конструкторы
		public Set(Owner OwnerData = null) {
			Data = new List<T>();
			OwnerData = OwnerData != null ? new Owner(OwnerData) : new Owner();
		}

		public Set(T[] data, Owner OwnerData = null) {
			Data = new List<T>(data);
			OwnerData = OwnerData != null ? new Owner(OwnerData) : new Owner();
		}

		[JsonConstructor()]
		public Set(T[] data, DateTime dateTime, Owner OwnerData = null) {
			Data = new List<T>(data);
			OwnerData = OwnerData != null ? new Owner(OwnerData) : new Owner();
			Date = dateTime;
		}

		public Set(params T[] data) {
			var list = new List<T>();
			foreach (var item in data) {
				list.Add(item);
			}
			Data = list;
			OwnerData = new Owner();
		}

		public Set(Owner owner = null, params T[] data) {
			var list = new List<T>();
			foreach (var item in data) {
				list.Add(item);
			}
			Data = list;
			OwnerData = owner != null ? new Owner(owner) : new Owner();
		}

		public Set(Set<T> set, T item1, MathOperation type) {
			Data = new List<T>(set.Data);
			OwnerData = new Owner(set.OwnerData);
			switch (type) {
				case MathOperation.plus:
					Add(item1);
					break;
				case MathOperation.minus:
					if (Data.Contains(item1)) {
						Data.RemoveAll(item2 => item2.CompareTo(item1) == 0);
					}
					break;
				default:
					throw new Exception("Unknown math operation");
			}
		}

		// Индексатор
		public T this[int index] {
			get => Data[index];
			set => Data[index] = value;
		}

		public IEnumerator<T> GetEnumerator() {
			return Data.GetEnumerator();
		}

		// Методы
		public void AddItem(T item) {
			Data.Add(item);
		}

		public bool RemoveItem(T item1) {
			if (Data.Contains(item1)) {
				Data.RemoveAll(item2 => item2.CompareTo(item1) == 0);
				return true;
			}
			return false;
		}

		public bool RemoveItem(int index) {
			if (Data.Count - 1 > index)
				return false;
			Data.RemoveAt(index);
			return true;
		}

		public bool IsSetOf(Set<T> set) {
			if (Data.All(item => set.Data.Contains(item))) {
				return true;
			}
			return false;
		}

		public bool IsEqual(Set<T> set) {
			return (
				Data.All(item1 => set.Data.Any(item2 => item2.CompareTo(item1) == 0)) &&
				set.Data.All(item1 => Data.Any(item2 => item2.CompareTo(item1) == 0))
				);
		}

		// Реализация интерфейсов
		public T[] GetCollection() {
			T[] result = Data.ToArray();
			return result;
		}

		public void Add(T item) {
			AddItem(item);
		}

		public void Remove(int index) {
			RemoveItem(index);
		}

		public void Remove(T item) {
			RemoveItem(item);
		}

		public string GetOwnerName() {
			return OwnerData.Name;
		}

		public List<string> GetData() {
			List<string> result = new List<string>();
			foreach (var item in Data) {
				result.Add(item.ToString());
			}
			return result;
		}

		// Запись в файл
		public void Write(string file) {
			Directory.CreateDirectory("data");
			using var sw = new StreamWriter(@$"data/{file}.json");
			string json = JsonConvert.SerializeObject(this);
			sw.Write(json);
		}

		// Чтение из файла
		public static Set<T> Read(string file) {
			Set<T> result;
			using var sr = new StreamReader($"data/{file}.json");
			var json = sr.ReadToEnd();
			try {
			result = JsonConvert.DeserializeObject<Set<T>>(json);
			}
			catch (JsonException e) {
				throw new Exception("Невозможно преобразовать тип извлекаемых данных", e);
			}
			catch (Exception e) {
				throw new Exception("Неизвестная ошибка в чтение json файла", e);
			}
			return result;
		}

		// Перегружаемые операторы

		// Добавление
		public static Set<T> operator +(Set<T> set, T item) {
			return new Set<T>(set, item, MathOperation.plus);
		}

		// Удаление
		public static Set<T> operator -(Set<T> set, T item) {
			return new Set<T>(set, item, MathOperation.minus);
		}

		// Проверка на подмножество
		public static bool operator *(Set<T> set1, Set<T> set2) {
			return set1.IsSetOf(set2);
		}

		// Проверка на неравенство
		public static bool operator !=(Set<T> set1, Set<T> set2) {
			return !set1.IsEqual(set2);
		}

		// Проверка на равенство
		public static bool operator ==(Set<T> set1, Set<T> set2) {
			return set1.IsEqual(set2);
		}

		// Проверка на пересечение
		public static bool operator %(Set<T> set1, Set<T> set2) {
			return set1.Data.Any(item1 => set2.Data.Any(item2 => item2.CompareTo(item1) == 0));
		}

		public class Owner {
			public string Id { get; private set; }
			public string Name { get; set; }
			public string Organization { get; set; }

			public Owner() {
				Id = "Не указано";
				Name = "Не указано";
				Organization = "Не указана";
			}

			public Owner(string name) {
				Id = "0";
				Name = name;
				Organization = "Не указана";
			}

			public Owner(string id = "Не указано",
						string name = "Не указано",
						string organization = "Не указана") {

				Id = id;
				Name = name;
				Organization = organization;
			}

			public Owner(
				int id = 0,
				string name = "Не указано",
				string organization = "Не указана") {

				if (id == 0)
					Id = "Не указано";
				else
					Id = id.ToString();
				Name = name;
				Organization = organization;
			}

			public Owner(Owner owner) {
				Id = owner.Id;
				Name = owner.Name;
				Organization = owner.Organization;
			}
		}
	}
	enum MathOperation {
		plus,
		minus
	}
}
#pragma warning restore CS0661 // Тип определяет оператор == или оператор !=, но не переопределяет Object.GetHashCode()
#pragma warning restore CS0660 // Тип определяет оператор == или оператор !=, но не переопределяет Object.Equals(object o)
