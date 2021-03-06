using System;
using System.Collections.Generic;
using KarutaDateSimulator;

Dictionary<string, State> dict = new()
{
    { "start", new State(100, 50, 50, 75, 100) },
    { "UP", new State(-10) },
    { "DOWN", new State(-10) },
    { "LEFT", new State(-10) },
    { "RIGHT", new State(-10) },
    { "action", new State(0, -4, -6, -8, -4) },
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
    { "airplane", new State(fun: -10) },
    { "noop", new State(0, 4, 6, 8, 4) }
};

Console.CursorVisible = false;

Start:
var state   = dict["start"];
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
T: AIRPLANE = -10 fun, rerolls the board
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
        ConsoleKey.T => "airplane",

        ConsoleKey.UpArrow    => "UP",
        ConsoleKey.DownArrow  => "DOWN",
        ConsoleKey.LeftArrow  => "LEFT",
        ConsoleKey.RightArrow => "RIGHT",

        _ => "noop"
    };

    state += dict[action] + dict["action"];
    if (action != "noop") actions.Add(action);

    Console.SetCursorPosition(0, 0);
    Console.WriteLine(state.ToString());

    Console.SetCursorPosition(0, 23);
    Console.WriteLine("History:");
    Console.WriteLine(string.Join(", ", actions));

    if (state.Food  <= 0 ||
        state.Drink <= 0 ||
        state.Time  <= 0 ||
        state.Fun   <= 0 ||
        state.Fuel  <= 0)
    {
        break;
    }
}

Console.SetCursorPosition(0, 27);
Console.WriteLine(state.Time == 0 ? $"YOU WON! Total AP: {state.CalculateAP()}" : "YOU LOST.");
Console.WriteLine("Press X to exit or any other key to restart");

if (Console.ReadKey(true).Key != ConsoleKey.X) goto Start;
