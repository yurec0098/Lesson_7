// Decompiled with JetBrains decompiler
// Type: Lesson_4.Program
// Assembly: Lesson_4, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03A6B648-EAB7-488B-8181-FE26453EDA38
// Assembly location: F:\Уроки\Введение в C#\Lesson_4\Lesson_4\bin\Release\net5.0\Lesson_4.dll

using System;
using System.Diagnostics;

namespace Lesson_4
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			while (true)
			{
				Console.Clear();
				Program.WriteLineCentr("Добро пожаловать!", 30);
				Program.WriteLineCentr("Выберите чем займёмся:", 30);
				Console.WriteLine();
				Console.WriteLine("  1. Вывод ФИО");
				Console.WriteLine("  2. Сумма чисел из массива");
				Console.WriteLine("  3. Сезон года");
				Console.WriteLine("  4. Фибоначчи");
				Console.WriteLine();
				Console.WriteLine("  0. Выход");

				string str = Console.ReadLine();
				Console.Clear();

				switch (str)
				{
					case "1":
						RunFullNames();
						break;

					case "2":
						RunStringSum();
						break;

					case "3":
						RunSeason();
						break;

					case "4":
						RunFibonacci();
						break;

					case "0":
						return;
				}
				Console.ReadLine();
			}
		}

		private static void WriteLineCentr(string text, int max = 40)
		{
			Console.SetCursorPosition((max - text.Length) / 2, Console.CursorTop);
			Console.WriteLine(text ?? "");
		}

		private static void RunFullNames()
		{
			Program.WriteLineCentr("Вывод ФИО", 30);
			Console.WriteLine(Program.GetFullName("Иванов", "Иван", "Иванович"));
			Console.WriteLine(Program.GetFullName("Сидоров", "Пётр", "Николаевич"));
			Console.WriteLine(Program.GetFullName("Крюкова", "Ольга", "Петровна"));
			Console.WriteLine(Program.GetFullName("Дудник", "Андрей", "Романович"));
		}

		private static string GetFullName(string firstName, string lastName, string patronymic) => firstName + " " + lastName + " " + patronymic;

		private static void RunStringSum()
		{
			Program.WriteLineCentr("Сумма чисел из массива", 30);
			Console.WriteLine("Введите массив чисел, разделённых пробелом:");
			Console.WriteLine(string.Format("Сумма введённых Вами чисел равна: {0}", StringNumberSum(Console.ReadLine())));
		}

		private static int StringNumberSum(string numbers)
		{
			int num = 0;
			foreach (string s in numbers.Split(' '))
				if (int.TryParse(s, out int result))
					num += result;
			
			return num;
		}

		private static void RunSeason()
		{
			Program.WriteLineCentr("Сезон года", 30);
			Console.WriteLine("Введите номер месяца:");

			string month = Console.ReadLine();
			while (Program.GetSeason(month) == Program.Season.None)
				month = Console.ReadLine();

			Console.WriteLine("Порядковый номер месяца (" + month + ") соответствует времени года " + Program.GetStringSeason(Program.GetSeason(month)));
		}

		private static Program.Season GetSeason(string month)
		{
			switch (month)
			{
				case "1":
				case "12":
				case "2":
					return Program.Season.Winter;

				case "10":
				case "11":
				case "9":
					return Program.Season.Autumn;

				case "3":
				case "4":
				case "5":
					return Program.Season.Spring;

				case "6":
				case "7":
				case "8":
					return Program.Season.Summer;

				default:
					Console.WriteLine("Ошибка: введите число от 1 до 12");
					return Program.Season.None;
			}
		}

		private static string GetStringSeason(Program.Season season)
		{
			switch (season)
			{
				case Program.Season.Winter:
					return "Зима";
				case Program.Season.Spring:
					return "Весна";
				case Program.Season.Summer:
					return "Лето";
				case Program.Season.Autumn:
					return "Осень";
				default:
					return "Ошибка: введите число от 1 до 12";
			}
		}

		private static void RunFibonacci()
		{
			Program.WriteLineCentr("Расчёт Числа Фибоначчи", 30);
			int min = -40;
			int max = 40;

			bool first = true;
			do
			{
				if (!first)
				{
				#if NET5_0
					(int Left, int Top) cursorPosition = Console.GetCursorPosition();				//	.NetCore
				#else
					(int Left, int Top) cursorPosition = (Console.CursorLeft, Console.CursorTop);   //	.NetFramework
				#endif
					ClearConsoleLines(cursorPosition.Left, cursorPosition.Top - 2, 3);
				}

				int num1 = ReadInt(string.Format("Введите число (от {0} до {1}):", min, max), min, max);

				Stopwatch stopwatch = Stopwatch.StartNew();
				long num2 = num1 < 0 ? Program.FibonacciNegative((long)num1) : Program.Fibonacci(num1);
				stopwatch.Stop();

				Console.WriteLine(string.Format("Число Фибоначчи для n = {0} составило {1}, расчитано за {2}", num1, num2, stopwatch.Elapsed));

				Console.WriteLine("Повторим? Введите +");
				first = false;
			}
			while ("+".Equals(Console.ReadLine()));
		}

		private static long Fibonacci(long n)
		{
			if (n == 0L)
				return 0;
			return n == 1L ? 1L : Program.Fibonacci(n - 1L) + Program.Fibonacci(n - 2L);
		}

		private static long FibonacciNegative(long n)
		{
			if (n == 0L)
				return 0;
			return n == 1L ? 1L : Program.FibonacciNegative(n + 2L) - Program.FibonacciNegative(n + 1L);
		}

		private static int ReadInt(string text, int min, int max)
		{
		#if NET5_0
			(int Left, int Top) cursorPosition = Console.GetCursorPosition();				//	.NetCore
		#else
			(int Left, int Top) cursorPosition = (Console.CursorLeft, Console.CursorTop);   //	.NetFramework
		#endif
			Console.WriteLine(text);
			int result;
			while (!int.TryParse(Console.ReadLine(), out result) || result < min || result > max)
			{
				Program.ClearConsoleLines(cursorPosition.Left, cursorPosition.Top, 2);
				Console.WriteLine("Повторим... " + text);
			}
			return result;
		}

		private static void ClearConsoleLines(int left, int top, int count)
		{
			Console.SetCursorPosition(left, top);
			for (int index = 0; index < count; ++index)
				Console.WriteLine(new string(' ', Console.WindowWidth));
			Console.SetCursorPosition(left, top);
		}

		private enum Season
		{
			None,
			Winter,
			Spring,
			Summer,
			Autumn,
		}
	}
}
