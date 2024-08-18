using LatticeProject.Game.Belts;
using LatticeProject.Lattices;
using LatticeProject.Rendering;
using Raylib_cs;

namespace LatticeProject.Game
{
    internal static class GameUpdater
    {
        public static void Update(GameState game)
        {
            //mouse position
            game.lastClosestVertex = game.closestVertex;

            game.mousePosition = Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), game.mainCam.camera);
            game.closestVertex = game.mainLattice.GetClosestVertex(game.mousePosition / RenderConfig.scale);

            //camera controls
            if (Raylib.IsKeyPressed(KeyboardKey.Left)) game.mainCam.camera.Rotation -= 30;
            if (Raylib.IsKeyPressed(KeyboardKey.Right)) game.mainCam.camera.Rotation += 30;

            //simulation speed
            if (Raylib.IsKeyDown(KeyboardKey.LeftShift) && Raylib.GetMouseWheelMove() != 0)
            {
                game.simulationSpeed *= Raylib.GetMouseWheelMove() > 0 ? 1.2f : 1 / 1.2f;
            }

            if (Raylib.IsKeyPressed((KeyboardKey)91)) game.frameAdvance = !game.frameAdvance;

            //belt placing
            if (Raylib.IsMouseButtonPressed(0))
            {
                BeltSegment newBelt = new BeltSegment();
                game.mainChunk.beltSegments.Add(newBelt);
                newBelt.vertices.Add(game.lastClosestVertex);
                newBelt.inventoryManager.RecieverTile = game.lastClosestVertex;
            }

            if (game.closestVertex != game.lastClosestVertex && Raylib.IsMouseButtonDown(0))
            {
                game.mainChunk.beltSegments[^1].vertices.Add(game.closestVertex);
            }

            if (Raylib.IsMouseButtonReleased(0))
            {
                game.mainChunk.beltSegments[^1].SimplifyVertices(game.mainLattice);
                game.mainChunk.beltSegments[^1].UpdateLengths(game.mainLattice);
            }

            //handle belt selection
            game.selection.belts.Clear();
            if (game.mainChunk.beltSegments.Count > 0)
            {
                foreach (BeltSegment segment in game.mainChunk.beltSegments)
                {
                    if (segment.IsOccupyingTile(game.closestVertex))
                    {
                        game.selection.belts.Add(segment);
                        break; //only allow one selected belt
                    }
                }
            }
            if (Raylib.IsKeyPressed(KeyboardKey.C))
            {
                if (game.selection.belts.Count > 0)
                {
                    if (game.selection.connectingBelt is null)
                    {
                        game.selection.connectingBelt = game.selection.belts[0];
                    }
                    else
                    {
                        game.selection.connectingBelt.inventoryManager.depositInventory = game.selection.belts[0].inventoryManager;
                        game.selection.connectingBelt = null;
                    }
                }
                else if (game.selection.connectingBelt is not null)
                {
                    game.selection.connectingBelt.inventoryManager.depositInventory = null;
                    game.selection.connectingBelt = null;
                }
            }

            //manually add/remove items
            if (game.selection.belts.Count > 0)
            {
                BeltInventory inventory = game.selection.belts[0].inventoryManager.inventory;
                if (inventory.CanRecieveItem() && Raylib.IsKeyDown(KeyboardKey.I))
                {
                    inventory.AddToHead(new GameItem(1), -GameRules.minItemDistance);
                }
                if (Raylib.IsKeyPressed(KeyboardKey.O))
                {
                    inventory.RemoveTailingItem();
                }
            }

            if (Raylib.IsKeyPressed(KeyboardKey.B)) game.debugMode = !game.debugMode;

            //game advancing
            if (Raylib.IsKeyPressed(KeyboardKey.P))
            {
                game.mainChunk.Update(GameRules.minItemDistance * 10);
            }
            if (Raylib.IsKeyPressed((KeyboardKey)93))
            {
                game.mainChunk.Update(1 / 60f * game.simulationSpeed);
            }
            else if (!game.frameAdvance)
            {
                game.mainChunk.Update(Math.Min(1 / 60f, Raylib.GetFrameTime()) * game.simulationSpeed);
            }

            //lattice swapping
            if (Raylib.IsKeyPressed(KeyboardKey.G))
            {
                game.mainLattice = (game.mainLattice is HexagonLattice) ? new SquareLattice() : new HexagonLattice();
            }

            //camera updating
            game.mainCam.UpdateCamera();
        }
    }
}