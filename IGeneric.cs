using System.Collections.Generic;

namespace OOP_Laba8 {
	interface IGeneric<T> {
		T[] GetCollection();
		void Add(T item);
		void Remove(int index);
		void Remove(T item);
	}
	interface IGetInfo {
		string GetOwnerName();
		List<string> GetData();
	}
}
