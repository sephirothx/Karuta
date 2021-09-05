using System;
using System.Collections.Generic;
using System.Linq;

namespace KarutaDateSimulator
{
    public struct State
    {
        public int Fuel  { get; set; }
        public int Food  { get; set; }
        public int Drink { get; set; }
        public int Fun   { get; set; }
        public int Time  { get; set; }

        public State(int fuel=0, int food=0, int drink=0, int fun=0, int time=0)
        {
            Fuel  = fuel;
            Food  = food;
            Drink = drink;
            Fun   = fun;
            Time  = time;
        }

        public static State operator +(State a, State b)
        {
            return new State
            {
                Fuel  = Math.Min(100, a.Fuel  + b.Fuel),
                Food  = Math.Min(100, a.Food  + b.Food),
                Drink = Math.Min(100, a.Drink + b.Drink),
                Fun   = Math.Min(100, a.Fun + b.Fun),
                Time  = Math.Min(100, a.Time + b.Time)
            };
        }

        public override string ToString()
        {
            string ret    = "";
            ret += $"Fuel  = {Fuel,4 }  {GetProgressBar(Fuel) ,-50}{Environment.NewLine}";
            ret += $"Food  = {Food,4 }  {GetProgressBar(Food) ,-50}{Environment.NewLine}";
            ret += $"Drink = {Drink,4}  {GetProgressBar(Drink),-50}{Environment.NewLine}";
            ret += $"Fun   = {Fun,4  }  {GetProgressBar(Fun)  ,-50}{Environment.NewLine}";
            ret += $"Time  = {Time,4 }  {GetProgressBar(Time) ,-50}{Environment.NewLine}";

            return ret;
        }

        private static string GetProgressBar(int x)
        {
            if (x < 0) x = 0;
            var amount = (int)Math.Round((decimal)x / 2);
            return new string('■', amount);
        }
    }

    class Program
    {
        private static readonly Dictionary<string, State> _dict = new()
        {
            { "start", new State(100, 50, 50, 75, 100) },
            { "UP", new State(-10) },
            { "DOWN", new State(-10) },
            { "LEFT", new State(-10) },
            { "RIGHT", new State(-10) },
            { "action", new State(0,  -4, -6, -8, -4) },
            { "taco", new State(food: 60) },
            { "pasta", new State(food: 60) },
            { "cocktail", new State(drink: 40, fun: 40) },
            { "flower", new State(fun: 100) },
            { "theater", new State(fun: 60) },
            { "gas", new State(fuel: 100) },
            { "lunapark", new State(fun: 40, food: 20, drink: 20) },
            { "coffee", new State(drink: 60) },
            { "juice", new State(drink: 60) },
            { "dance", new State(fun: 100, food: -10, drink: -15) },
            { "sandwich", new State(food: 40, drink: 20) },
            { "jewelry", new State() },
            { "noop", new State(0, 4, 6, 8, 4) }
        };

        private static void Main()
        {
            Console.CursorVisible = false;

Start:
            var state   = _dict["start"];
            var actions = new List<string>();

            Console.Clear();
            Console.WriteLine(state.ToString());
            Console.WriteLine(@"A: TACO = 60 food
S: PASTA = 60 food
D: COCKTAIL = 40 drink, 40 fun
F: FLOWER = 100 fun
Z: THEATER = 100 fun
X: GAS = 100 fuel
C: LUNAPARK = 40 fun, 20 food, 20 drink
Q: COFFEE = 60 drink
W: JUICE = 60 drink
E: DANCE = 100 fun, -10 food, -15 drink
R: SANDWICH = 40 food, 20 drink
V: RING / SHOPPING
Arrow keys: MOVEMENT = -10 fuel

Every action consumes 4 food, 6 drink, 8 fun and 4 time");

            while (true)
            {
                string action = Console.ReadKey(true).Key switch
                {
                    ConsoleKey.A => "taco",
                    ConsoleKey.S => "pasta",
                    ConsoleKey.D => "cocktail",
                    ConsoleKey.F => "flower",
                    ConsoleKey.Z => "theater",
                    ConsoleKey.X => "gas",
                    ConsoleKey.C => "lunapark",
                    ConsoleKey.Q => "coffee",
                    ConsoleKey.W => "juice",
                    ConsoleKey.E => "dance",
                    ConsoleKey.R => "sandwich",
                    ConsoleKey.V => "jewelry",

                    ConsoleKey.UpArrow    => "UP",
                    ConsoleKey.DownArrow  => "DOWN",
                    ConsoleKey.LeftArrow  => "LEFT",
                    ConsoleKey.RightArrow => "RIGHT",

                    _ => "noop"
                };

                state += _dict[action] + _dict["action"];
                actions.Add(action);

                Console.SetCursorPosition(0, 0);
                Console.WriteLine(state.ToString());

                Console.SetCursorPosition(0, 22);
                Console.WriteLine("History:");
                Console.WriteLine(string.Join(", ", actions.Where(a => a != "noop")));

                if (state.Food  <= 0 ||
                    state.Drink <= 0 ||
                    state.Time  <= 0 ||
                    state.Fun   <= 0 ||
                    state.Fuel  <= 0)
                {
                    break;
                }
            }

            Console.SetCursorPosition(0, 26);
            Console.WriteLine(state.Time == 0 ? "YOU WON!" : "YOU LOST.");
            Console.WriteLine("Press X to exit or any other key to restart");

            if (Console.ReadKey(true).Key != ConsoleKey.X) goto Start;
        }
    }
}
