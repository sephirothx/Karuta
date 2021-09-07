using System;

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
                Fun   = Math.Min(100, a.Fun   + b.Fun),
                Time  = Math.Min(100, a.Time  + b.Time)
            };
        }

        public override string ToString()
        {
            string ret = "";
            ret += $"Fuel  = {Fuel,4 }  {GetProgressBar(Fuel),-50 }{Environment.NewLine}";
            ret += $"Food  = {Food,4 }  {GetProgressBar(Food),-50 }{Environment.NewLine}";
            ret += $"Drink = {Drink,4}  {GetProgressBar(Drink),-50}{Environment.NewLine}";
            ret += $"Fun   = {Fun,4  }  {GetProgressBar(Fun),-50  }{Environment.NewLine}";
            ret += $"Time  = {Time,4 }  {GetProgressBar(Time),-50 }{Environment.NewLine}";

            return ret;
        }

        public int CalculateAP()
        {
            return (int)Math.Round((Food + Drink + Fun) / 6.0, MidpointRounding.AwayFromZero);
        }

        private static string GetProgressBar(int x)
        {
            if (x < 0) x = 0;
            var amount   = (int)Math.Round((decimal)x / 2);
            return new string('■', amount);
        }
    }
}
