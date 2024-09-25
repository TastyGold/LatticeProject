﻿using LatticeProject.Game.Belts;
using LatticeProject.Game.Buildings;
using LatticeProject.Lattices;
using LatticeProject.Rendering;
using LatticeProject.Utility;
using Raylib_cs;

namespace LatticeProject.Game
{
    internal static class GameManager
    {
        static readonly GameState game = new GameState(
            lattice: new HexagonLattice()
            );

        public static void Run()
        {
            Begin();

            while (!Raylib.WindowShouldClose())
            {
                Update();
                Draw();
            }

            End();
        }

        private static void Begin()
        {
            Raylib.SetConfigFlags(ConfigFlags.Msaa4xHint);
            Raylib.SetConfigFlags(ConfigFlags.FullscreenMode);

            Raylib.InitWindow(1600, 900, "Hello World");
            Raylib.SetTargetFPS(120);

            BuildingTypes.LoadBuildingData();
            GameItemRenderer.Initialise();
        }

        private static void Update()
        {
            GameUpdater.Update(game);
        }

        private static void Draw()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(new Color(13, 17, 23, 255));

            Raylib.BeginMode2D(game.mainCam.camera);

            GameWorldRenderer.Draw(game);

            Raylib.EndMode2D();

            GameUIRenderer.Draw(game);

            Raylib.EndDrawing();
        }

        private static void End()
        {
            Raylib.CloseWindow();
        }
    }
}