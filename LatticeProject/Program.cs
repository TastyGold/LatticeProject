using Raylib_cs;
using System.Numerics;

namespace LatticeProject
{
    internal class Program
    {
        public static void Main()
        {
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