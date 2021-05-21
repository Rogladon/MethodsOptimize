using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace MetOptim {
	public class Vector: IEnumerable<float> {
		float[] vector;
		public int Lenght => vector.Length;
		
		public float this[int index] {
			get {
				return vector[index];
			}
			set {
				vector[index] = value;
			}
		}
		public Vector(int lenght) {
			vector = new float[lenght];
		}
		public Vector(float[] arr) {
			vector = CopyArray(arr);
		}
		public Vector(Vector v) {
			vector = CopyArray(v.ToArray());
		}
		private float[] CopyArray(float[] arr) {
			float[] result = new float[arr.Length];
			for(int i = 0; i< arr.Length; i++){
				result[i] = arr[i];
			}
			return result;
		}
		public float[] ToArray() {
			return vector;
		}
		public static float Distance(Vector left, Vector right) {
			Vector v = right - left;
			float sum = 0;
			for(int i = 0; i< v.Lenght; i++) {
				sum += MathF.Pow(v[i],2);
			}
			return MathF.Sqrt(sum);
		}
		public static Vector Middle(Vector[] arr) {
			Vector result = Zero(arr[0].Lenght);
			foreach(var i in arr) {
				result += i;
			}
			return result / arr.Length;
		}
		public static float Area(Vector[] arr) {
			float sum = 0;
			Vector mid = Middle(arr);
			for(int i = 1; i< arr.Length; i++) {
				float a = Distance(arr[i - 1], arr[i]);
				float b = Distance(arr[i], mid);
				float c = Distance(mid, arr[i - 1]);
				float p =a + b+ c;
				sum += MathF.Sqrt(p * (p - a) * (p - b) * (p - c));
			}
			return sum;
		}
		public static Vector Zero(int lenght) {
			var v = new Vector(lenght);
			for(int i = 0; i< lenght; i++) {
				v[i] = 0;
			}
			return v;
		}

		public override bool Equals(object obj) {
			return base.Equals(obj);
		}

		public override int GetHashCode() {
			return base.GetHashCode();
		}

		public override string ToString() {
			string str = "(";
			foreach(var i in vector) {
				str += i + " ; ";
			}
			str = str.Remove(str.Length - 2, 1) + ")";
			return str;
		}

		public Vector Clone() {
			return new Vector(this);
		}

		public IEnumerator<float> GetEnumerator() {
			return ((IEnumerable<float>)vector).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return vector.GetEnumerator();
		}

		public static Vector operator+(Vector left, Vector right) {
			if(left.Lenght != right.Lenght) {
				throw new Exception("Expection! Early lenght vectors");
			}
			Vector result = new Vector(left.Lenght);
			for(int i = 0; i< result.Lenght; i++) {
				result[i] = left[i] + right[i];
			}
			return result;
		}
		public static Vector operator -(Vector left, Vector right) {
			if (left.Lenght != right.Lenght) {
				throw new Exception("Expection! Early lenght vectors");
			}
			Vector result = new Vector(left.Lenght);
			for (int i = 0; i < result.Lenght; i++) {
				result[i] = left[i] - right[i];
			}
			return result;
		}
		public static Vector operator *(float left, Vector right) {
			Vector result = new Vector(right.Lenght);
			for (int i = 0; i < result.Lenght; i++) {
				result[i] = left * right[i];
			}
			return result;
		}
		public static Vector operator *(Vector left, float right) {
			Vector result = new Vector(left.Lenght);
			for (int i = 0; i < result.Lenght; i++) {
				result[i] = left[i] * right;
			}
			return result;
		}
		public static Vector operator /(Vector left, float right) {
			Vector result = new Vector(left.Lenght);
			for (int i = 0; i < result.Lenght; i++) {
				result[i] = left[i] / right;
			}
			return result;
		}
		public static Vector operator *(Vector left, Vector right) {
			if (left.Lenght != right.Lenght) {
				throw new Exception("Expection! Early lenght vectors");
			}
			Vector result = new Vector(left.Lenght);
			for (int i = 0; i < result.Lenght; i++) {
				result[i] = left[i] * right[i];
			}
			return result;
		}
		public static Vector operator /(Vector left, Vector right) {
			if (left.Lenght != right.Lenght) {
				throw new Exception("Expection! Early lenght vectors");
			}
			Vector result = new Vector(left.Lenght);
			for (int i = 0; i < result.Lenght; i++) {
				result[i] = left[i] / right[i];
			}
			return result;
		}
		public static bool operator==(Vector left, Vector right) {
			if (left.Lenght != right.Lenght) {
				return false;
			}
			for (int i = 0; i < left.Lenght; i++) {
				if (left[i] != right[i])
					return false;
			}
			return true;
		}
		public static bool operator !=(Vector left, Vector right) {
			return !(left == right);
		}
	}
}
