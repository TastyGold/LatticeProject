﻿using LatticeProject.Game;
using LatticeProject.Game.Belts;
using LatticeProject.Lattices;
using LatticeProject.Utility;
using Raylib_cs;

namespace LatticeProject.Rendering
{
    internal static class GameWorldDebugRenderer
    {
        private static Color beltHighlightHeadColor = new(66, 11, 35, 255);
        private static Color beltHighlightMiddleColor = new(11, 35, 66, 255);
        private static Color beltHighlightTailColor = new(11, 66, 50, 255);

        private static Color beltConnectionColor = Color.Green;

        public static void DrawBeltHighlights(GameState game, IEnumerable<BeltSegment> belts)
        {
            foreach (BeltSegment belt in belts)
            {
                foreach (VecInt2 v in belt)
                {
                    LatticeRenderer.HighlightCell(game.mainLattice, v, beltHighlightMiddleColor);
                }
                if (belt.vertices.Count > 1)
                {
                    LatticeRenderer.HighlightCell(game.mainLattice, belt.vertices[0], beltHighlightHeadColor); //head
                    LatticeRenderer.HighlightCell(game.mainLattice, belt.vertices[^1], beltHighlightTailColor); //tail
                }
            }
        }

        public static void DrawBeltDepositConnections(Lattice lattice, GameState game, IEnumerable<BeltSegment> belts)
        {
            foreach (BeltSegment belt in belts)
            {
                if (belt.vertices.Count > 1 && belt.inventoryManager.depositInventory is not null && !ReferenceEquals(game.selection.connectingBelt, belt))
                {
                    Raylib.DrawLineV(
                        lattice.GetCartesianCoords(belt.vertices[^1]) * RenderConfig.scale,
                        lattice.GetCartesianCoords(belt.inventoryManager.depositInventory.RecieverTile) * RenderConfig.scale,
                        beltConnectionColor
                        );
                }
            }
            if (game.selection.connectingBelt is not null)
            {
                Raylib.DrawLineV(
                       lattice.GetCartesianCoords(game.selection.connectingBelt.vertices[^1]) * RenderConfig.scale,
                       game.mousePosition,
                       beltConnectionColor
                       );
            }
        }

        public static void DrawBeltBounds(Lattice lattice, BeltSegment belt)
        {
            HexBoundary bounds = new HexBoundary(int.MaxValue, int.MinValue, int.MaxValue, int.MinValue, int.MaxValue, int.MinValue);
            foreach (VecInt2 v in belt)
            {
                bounds.minQ = Math.Min(bounds.minQ, v.x);
                bounds.maxQ = Math.Max(bounds.maxQ, v.x);

                bounds.minR = Math.Min(bounds.minR, v.y);
                bounds.maxR = Math.Max(bounds.maxR, v.y);

                bounds.minS = Math.Min(bounds.minS, LatticeMath.GetS(v.x, v.y));
                bounds.maxS = Math.Max(bounds.maxS, LatticeMath.GetS(v.x, v.y));
            }

            BoundaryRenderer.DrawHexBoundaryLines(lattice, bounds);
        }

        public static void DrawBeltBounds(Lattice lattice, IEnumerable<BeltSegment> belts)
        {
            foreach (BeltSegment belt in belts)
            {
                DrawBeltBounds(lattice, belt);
            }
        }
    }
}