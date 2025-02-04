﻿using LatticeProject.Game;
using LatticeProject.Game.Belts;
using LatticeProject.Lattices;

namespace LatticeProject.Rendering
{
    internal static class BeltItemRenderer
    {
        private const float itemSize = 0.225f;

        public static void DrawBeltItems(Lattice lattice, BeltSegment segment)
        {
            if (segment.inventoryManager.inventory.IsEmpty()) return;

            BeltSegmentTraverser traverser = segment.GetTraverser();
            traverser.ResetEnd();
            bool firstItem = true;

            foreach (BeltInventoryElement element in segment.inventoryManager.inventory)
            {
                traverser.AdvanceReverse(element.distance);

                if (firstItem == true)
                {
                    //hacky method of aligning minItemDistance from tail to be exactly on tail
                    traverser.Advance(GameRules.minItemDistance);
                    firstItem = false;
                }

                GameItemRenderer.DrawGameItemTextured(
                    item: element.item, 
                    position: RenderConfig.scale * segment.GetPositionAlongPiece(
                        lattice, 
                        traverser.CurrentVertex, 
                        traverser.PositionAlongPiece),
                    size: RenderConfig.scale * itemSize * 2
                    );
            }
        }
    }
}