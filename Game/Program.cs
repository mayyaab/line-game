using System;
using System.Text.RegularExpressions;
using Game.Logic;

namespace Game
{
    class Program
    {
        static void Main(string[] args)
        {
            bool doLoop = true;
            Field field = new Field();

            var commands = new CommandList();

            commands.Add("Usage", "Print the usage", () =>
            {
                commands.PrintCommandList();
            });

            commands.Add("Start", "Start a new game", () =>
            {
                Console.WriteLine("New game started");
                field = new Field();
                field.PlaceBalls();
                PrintField(field);
            });

            commands.Add("Next", "Add a temp", () =>
            {
                var balls = field.PlaceBalls();
                field.RemoveForBalls(balls);
                PrintField(field);
            });

            commands.Add("Switch", "Switch two balls", () =>
            {
                var destination = SwitchBalls(field);
                field.RemoveLines(destination);
                var balls = field.PlaceBalls();
                field.RemoveForBalls(balls);
                PrintField(field);
            });

            commands.Add("End", "Finish the game", () =>
            {
                doLoop = false;
            });

            commands.PrintCommandList();

            while (doLoop)
            {
                string line = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }

                if (commands.Contains(line))
                {
                    commands.Run(line);
                }
                else
                {
                    Console.WriteLine($"Invalid '{line}' command");
                }
            }
        }

        private static Position ParsePosition(string input)
        {
            string[] tokens = Regex.Split(input, @"\D+");
            try
            {
                int a = int.Parse(tokens[0]);
                int b = int.Parse(tokens[1]);

                var position = new Position(a, b);

                return position;
            }
            catch
            {
                return null;
            }
        }

        private static void PrintField(Field field)
        {
            const string format = "{0,8}";

            Console.Write(format, "");
            for (int i = 0; i < field.Width; i++)
            {
                Console.Write(format, i);
            }
            Console.WriteLine("\n");

            for (int row = 0; row < field.Height; row++)
            {
                Console.Write(format, row);
                for (int col = 0; col < field.Width; col++)
                {
                    Console.Write(format, field.GetBallColorAt(row, col));
                }
                Console.WriteLine();
            }
        }

        private static Position SwitchBalls(Field field)
        {
            Console.WriteLine("Source [row column]: ");
            var source = ParsePosition(Console.ReadLine());

            if (source == null)
            {
                Console.WriteLine("Wrong format!");
            }

            Console.WriteLine("Destination [row column]: ");
            var destination = ParsePosition(Console.ReadLine());
            if (destination == null)
            {
                Console.WriteLine("Wrong format!");
            }

            if (field.PathStrategy.GetPath(source, destination, new bool[field.Height, field.Width]) != null)
            {
                field.MoveBall(source, destination);
            }
            else
            {
                Console.WriteLine("No path");
              }

            return destination;
        }
    }
}
