/// <summary>
/// Entry point for the Monkey console application.
/// </summary>
public class Program
{
	/// <summary>
	/// Main method to run the interactive monkey app.
	/// </summary>
	public static void Main(string[] args)
	{
		Console.OutputEncoding = System.Text.Encoding.UTF8;
		ShowWelcome();
		while (true)
		{
			Console.WriteLine("\nCommands: list | details [name] | random | exit");
			Console.Write("Enter command: ");
			var input = Console.ReadLine()?.Trim();
			if (string.IsNullOrEmpty(input)) continue;

			var parts = input.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
			var command = parts[0].ToLowerInvariant();
			switch (command)
			{
				case "list":
					ListMonkeys();
					break;
				case "details":
					if (parts.Length < 2)
					{
						Console.WriteLine("Usage: details [name]");
						break;
					}
					ShowMonkeyDetails(parts[1]);
					break;
				case "random":
					ShowRandomMonkey();
					break;
				case "exit":
					Console.WriteLine("Goodbye!");
					return;
				default:
					Console.WriteLine("Unknown command.");
					break;
			}
		}
	}

	/// <summary>
	/// Displays the welcome message.
	/// </summary>
	private static void ShowWelcome()
	{
		Console.WriteLine("============================");
		Console.WriteLine(" Welcome to Monkey Explorer ");
		Console.WriteLine("============================\n");
	}

	/// <summary>
	/// Lists all available monkeys.
	/// </summary>
	private static void ListMonkeys()
	{
		var monkeys = MonkeyHelper.GetMonkeys();
		Console.WriteLine("Available Monkeys:");
		foreach (var monkey in monkeys)
		{
			Console.WriteLine($"- {monkey.Name} ({monkey.Species})");
		}
	}

	/// <summary>
	/// Shows details for a specific monkey by name.
	/// </summary>
	private static void ShowMonkeyDetails(string name)
	{
		var monkey = MonkeyHelper.GetMonkeyByName(name);
		if (monkey == null)
		{
			Console.WriteLine($"Monkey '{name}' not found.");
			return;
		}
		Console.WriteLine($"\n{monkey.Name} ({monkey.Species})\n{monkey.Description}\n");
		Console.WriteLine(monkey.AsciiArt);
	}

	/// <summary>
	/// Picks and displays a random monkey.
	/// </summary>
	private static void ShowRandomMonkey()
	{
		var monkey = MonkeyHelper.GetRandomMonkey();
		Console.WriteLine($"\nRandom Monkey: {monkey.Name} ({monkey.Species})\n{monkey.Description}\n");
		Console.WriteLine(monkey.AsciiArt);
	}
}

namespace MyMonkeyApp;

/// <summary>
/// Represents a monkey species with attributes and ASCII art.
/// </summary>
public class Monkey
{
	/// <summary>
	/// Gets or sets the name of the monkey.
	/// </summary>
	public string Name { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the species of the monkey.
	/// </summary>
	public string Species { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the description of the monkey.
	/// </summary>
	public string Description { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the ASCII art representing the monkey.
	/// </summary>
	public string AsciiArt { get; set; } = string.Empty;
}

/// <summary>
/// Provides helper methods and sample data for monkeys.
/// </summary>
public static class MonkeyHelper
{
	private static readonly List<Monkey> monkeys = new()
	{
		new Monkey
		{
			Name = "Capuchin",
			Species = "Cebus capucinus",
			Description = "Small, intelligent New World monkey found in Central and South America.",
			AsciiArt = @"  .-""""-.
 /        \
|  O  O   |
|   --    |
 \      /
  '-.__.-'"
		},
		new Monkey
		{
			Name = "Mandrill",
			Species = "Mandrillus sphinx",
			Description = "Largest monkey species, known for its colorful face.",
			AsciiArt = @"   .-""""-.
  / .===. \
  \/ 6 6 \/
  ( \___/ )
___ooo__ooo___"
		},
		new Monkey
		{
			Name = "Howler",
			Species = "Alouatta",
			Description = "Known for loud vocalizations, native to South America.",
			AsciiArt = @"   .-""""-.
  /       \
 |  o   o |
 |   ^    |
 |  '-'   |
  \_____/
"
		}
	};

	/// <summary>
	/// Gets all monkeys.
	/// </summary>
	public static IReadOnlyList<Monkey> GetMonkeys() => monkeys;

	/// <summary>
	/// Gets a monkey by name (case-insensitive).
	/// </summary>
	public static Monkey? GetMonkeyByName(string name) =>
		monkeys.FirstOrDefault(m => m.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

	/// <summary>
	/// Gets a random monkey.
	/// </summary>
	public static Monkey GetRandomMonkey()
	{
		var rand = new Random();
		return monkeys[rand.Next(monkeys.Count)];
	}
}
