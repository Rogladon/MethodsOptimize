using System;

namespace MetOptim {
	class Program {
		static float f1(Vector x) {
			return 100 * MathF.Pow(MathF.Pow(x[0], 2) - x[1], 2) + MathF.Pow(1 - x[0],2);
		}
		static float f2(Vector x) {
			return 100 * MathF.Pow(x[1] - MathF.Pow(x[0], 3), 2) + MathF.Pow(1 - x[0], 2);
		}
		static float f3(Vector x) {
			return 100 * MathF.Pow(x[1] - MathF.Pow(x[0], 3), 2) + MathF.Pow(1 - x[0], 2);
		}
		static float f4(Vector x) {
			return MathF.Pow(x[0] + 10 * x[1], 2) + 5 * MathF.Pow(x[2] - x[3], 2) +
				MathF.Pow(x[1] - 2 * x[2], 4) + 10 * MathF.Pow(x[0] - x[3], 4);
		}
		static void Main(string[] args) {
			Methods.Result[,] results = new Methods.Result[4,2];
			results[0,0] = Methods.PatternSearch(f1, new float[] { 1.2f, 1 },1,2,0.000001f);
			results[0,1] = Methods.SimplexMethod(f1, new float[] { 1.2f, 1 }, 0.00000000001f, 1, 0.5f, 2);
			results[1, 0] = Methods.PatternSearch(f2, new float[] { -1.2f, -1 }, 1, 2, 0.000001f);
			results[1, 1] = Methods.SimplexMethod(f2, new float[] { -1.2f, -1 }, 0.00000000001f, 1, 0.5f, 2);
			results[2, 0] = Methods.PatternSearch(f3, new float[] { 0, 0 }, 1, 2, 0.000001f);
			results[2, 1] = Methods.SimplexMethod(f3, new float[] { 0f, 0 }, 0.00000000001f, 1, 0.5f, 2);
			results[3, 0] = Methods.PatternSearch(f4, new float[] { 3, -1, 0, 1 }, 1, 2, 0.000001f);
			results[3, 1] = Methods.SimplexMethod(f4, new float[] { 3, -1,0,1 }, 0.00000000001f, 1, 0.5f, 2);
			for(int i = 0; i< results.GetLength(0); i++) {
				Console.WriteLine($"\tФункция {i}:");
				Console.WriteLine($"Метод Хука-Дживса");
				Console.WriteLine($"Итераций {results[i, 0].iteration}");
				Console.WriteLine($"Результат: {results[i, 0].result}");
				Console.WriteLine($"Метод Нелдера-МИда");
				Console.WriteLine($"Итераций {results[i, 1].iteration}");
				Console.WriteLine($"Результат: {results[i, 1].result}");
				Console.WriteLine("\n");
			}
		}
	}
}
