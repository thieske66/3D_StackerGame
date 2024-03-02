using System;
using UnityEngine;

public class Layer : MonoBehaviour
{
    public Block[] Blocks;
    public int FirstBlock;
    public int LastBlock;
    public int ActiveBlocksInLayer
    {
        get
        {
            return LastBlock - FirstBlock;
        }
    }

    public enum Direction
    {
        Left = -1,
        Right = 1
    }

    public Layer(int blocksInLayer, int firstBlock, int lastBlock)
    {
        Blocks = new Block[blocksInLayer];
        SetBlocksEnabled(firstBlock, lastBlock);
    }

    public void SetBlocksEnabled(int firstBlock, int lastBlock)
    {
        FirstBlock = firstBlock;
        LastBlock = lastBlock;
        updateBlocks();
    }

    public void ShiftBlocks(int amount, Direction direction)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Amount must be positive value");
        }

        int shiftValue = amount * (int)direction;

        FirstBlock = Math.Min(FirstBlock + shiftValue, Blocks.Length - 1);
        LastBlock = Math.Max(LastBlock + shiftValue, 0);
    }

    private void updateBlocks()
    {
        for (int i = 0; i < Blocks.Length; i++)
        {
            Blocks[i].SetEnabled(i >= FirstBlock && i <= LastBlock);
        }
    }
}
