using LatticeProject.Game;
using Raylib_cs;
using System.Numerics;

namespace LatticeProject.Core
{
    internal class Program
    {
        public static void Main()
        {
            BeltInventoryTests.RunTests();
            //Console.ReadLine();

            GameManager.Begin();

            while (!Raylib.WindowShouldClose())
            {
                GameManager.Update();
                GameManager.Draw();
            }

            GameManager.End();
        }
    }
}