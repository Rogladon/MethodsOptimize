using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MetOptim {
	public static class Methods {
		public struct Result {
			public Vector result;
			public int iteration;
		}
		
		public static Result PatternSearch(Func<Vector, float> f, float[] point0, float h, float k, float eps) {

			Vector xk = new Vector(point0);
			Vector xk1 = new Vector(point0);
			int iteration = 0;
			do {
				iteration++;
				xk = xk1.Clone();
				for(int i = 0; i< xk.Lenght; i++) {
					Vector e = Vector.Zero(xk.Lenght);
					e[i] = 1;
					if (f(xk1 + e * h) < f(xk1)) {
						xk1[i] = xk1[i] + h;
						break;
					}
					if (f(xk1 - e * h) < f(xk1)) {
						xk1[i] = xk1[i] - h;
						break;
					}
				}
				if (xk1 == xk) {
					h = h / k;
					continue;
				}

				Vector xj = xk.Clone();
				Vector xj1 = xk1.Clone();
				while (true) {
					Vector p = xj+ (2* (xj1- xk));
					if (f(p) < f(xj1)) {
						xj = xj1.Clone();
						xj1 = p.Clone();
						continue;
					}
					xk1 = xj1.Clone();
					break;
				}
				if(Vector.Distance(xk, xk1) < eps)
					break;
				
			} while (true);
			return new Result { result = xk, iteration = iteration};
		}

		public static Result SimplexMethod(Func<Vector, float> f, float[] point0, float eps, float kMirr, float kComp, float kStret) {
			Vector[] simplex = new Vector[point0.Length+1];
			simplex[0] = new Vector(point0);
			for(int i = 1; i < simplex.Length; i++) {
				Vector e = Vector.Zero(point0.Length);
				e[i-1] = 1;
				simplex[i] = simplex[0] + 0.5f * e;
			}
			int iteration = 0;
			while (Vector.Area(simplex) > eps) {
				iteration++;
				simplex = Sort(simplex, (x, y) => f(x) > f(y));
				Vector[] goodPoint = Copy(simplex, 0, simplex.Length - 1);
				Vector badPoint = simplex[simplex.Length - 1];
				Vector mid = Vector.Middle(goodPoint);
				Vector xr = (1 + kMirr) * mid - kMirr * badPoint; 
				if(f(xr) < f(goodPoint[0])) {
					Vector xe = (1-kStret)*mid + kStret * xr;
					if(f(xe) < f(xr)) {
						simplex[simplex.Length - 1] = xe;
					} else {
						simplex[simplex.Length - 1] = xr;
					}
					continue;
				}
				if(f(xr) < f(badPoint)) {
					simplex[simplex.Length - 1] = xr;
				}
				Vector xc = mid + kStret * (badPoint - mid);
				if (f(xc) < f(goodPoint[0])) {
					simplex[simplex.Length - 1] = xc;
					continue;
				}
				for(int i = 1; i< simplex.Length; i++) {
					simplex[i] = simplex[0] + kComp*(simplex[i] - simplex[0]);
				}
			}
			return new Result { result = simplex[0],iteration = iteration};
		}
		private static T[] Copy<T>(T[] arr, int startIndex, int endIndex) {
			T[] result = new T[endIndex - startIndex];
			int index = 0;
			for(int i = 0; i < arr.Length; i++) {
				if(i >= startIndex && i < endIndex) {
					result[index] = arr[i];
					index++;
				}
			}
			return result;
		}
		private static T[] Sort<T>(T[] v, Func<T,T, bool> condition) {
			T[] arr = new T[v.Length];
			v.CopyTo(arr, 0);
			for (int i = 0; i < arr.Length - 1; i++) {
				for (int j = i + 1; j < arr.Length; j++) {
					if (condition(arr[i],arr[j])) {
						var temp = arr[i];
						arr[i] = arr[j];
						arr[j] = temp;
					}
				}
			}
			return arr;
		}
	}
}
