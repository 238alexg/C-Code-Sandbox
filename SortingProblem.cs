using System;
using System.Collections.Generic;

namespace CodeSandbox
{
	class MainClass
	{
		public static bool DEBUG;

		public static void Main(string[] args)
		{
			string response = "\n";

			while (response != "n" && response != "y")
			{
				// Suspend the screen.
				Console.Write("Run in debug mode? Type y or n: ");

				// Get response
				response = Console.ReadLine();
			}

			DEBUG = (response == "y");

			int counter = 0;

			// Get the file path of the project folder
			string filePath = System.IO.Directory.GetParent(
				System.IO.Directory.GetCurrentDirectory()
			).Parent.FullName;

			// Append name of sample text file
			filePath += "/testNumbers.txt";

			// DEBUG: Prints file pathway
			if (DEBUG)
			{
				Console.WriteLine("File pathway: " + filePath);
			}

			string line;

			// Read the file and display it line by line.
			System.IO.StreamReader file =
					  new System.IO.StreamReader(filePath);

			while ((line = file.ReadLine()) != null)
			{

				if (DEBUG)
				{
					Console.WriteLine("Text file numbers: " + line);
				}

				List<int> unsorted = StringToIntList(line);

				Console.WriteLine("Unsorted list: " + ListToString(unsorted));

				List<int> sorted = SortNumbers(unsorted);

                Console.WriteLine("Sorted list: " + ListToString(sorted) + System.Environment.NewLine);

				counter++;
			}

			file.Close();
		}


		// Takes and parses a string of numbers, seperated by spaces or commas
		// Returns a list of integers in the same order as the string
		public static List<int> StringToIntList(string numbers)
		{
			List<int> unsorted = new List<int>();
			char[] delims = { ' ', ',' };

			string[] numStrings = numbers.Split(delims);

			// Parse all numbers in string, convert to ints and add to unsorted List
			foreach (string num in numStrings)
			{
				int newNumber;

				if (int.TryParse(num, out newNumber))
				{
					unsorted.Add(newNumber);
				}
				else
				{
					Console.WriteLine("Text parse failed. Only use spaces or commas as delimiters");
					return null;
				}
			}

			return unsorted;
		}

		// Concatenates all list items onto a string
		// Returns string with all list values
		public static string ListToString(List<int> list)
		{
			string s = "";

			foreach (int val in list)
			{
				s += val + " ";
			}

			return s;
		}

		// Takes an unsorted list of integers and sorts them in nlog(n) time
		// Returns sorted list of integers, from smallest to largest
		public static List<int> SortNumbers(List<int> unsorted)
		{
			List<int> sorted = new List<int>();
			int left, right, index;

			foreach (int val in unsorted)
			{

				// If list is empty, add the value
				if (sorted.Count == 0)
				{
					sorted.Add(val);
					continue;
				}

				// DEBUG: Shows each iteration in adding to the sorted list
				if (DEBUG)
				{
					Console.WriteLine("Sorted list: " + ListToString(sorted));
				}

				// Initialize left, right, and central index variables
				left = 0; right = sorted.Count; index = sorted.Count / 2;
				bool equalValue = false;

				// While the bounds we are searching are greater than 2 values
				// Finds the correct location in log(n) time
				while (right - left > 2)
				{
					// Value is less than location, reduce right size boundry by half
					if (sorted[index] > val)
					{
						right = index;
						index = (index + left) / 2;
					}
					// Value is greater than location, reduce left size boundry by half
					else if (sorted[index] < val)
					{
						left = index;
						index = (right + index) / 2; ;
					}
					// Value is equal to location
					else
					{
						equalValue = true;
						break;
					}
				}

				// If value equal to indexed location, insert immediately in front of location
				if (equalValue)
				{
					sorted.Insert(index, val);
					equalValue = false;
					continue;
				}
				// If 2 elements are left, insert before, after or between them
				else if (right - left == 2)
				{
					if (sorted[right - 1] < val)
					{
						sorted.Insert(right, val);
					}
					else if (sorted[left] > val)
					{
						sorted.Insert(left, val);
					}
					else
					{
						sorted.Insert(left + 1, val);
					}
				}
				// If 1 element is left, insert to left or right of it
				else
				{
					if (sorted[index] > val)
					{
						sorted.Insert(index, val);
					}
					else
					{
						sorted.Insert(index + 1, val);
					}
				}

				
			}

			return sorted;
		}
	}
}
