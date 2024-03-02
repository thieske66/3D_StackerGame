using System;

[Serializable]
public class Layer
{
    public Block[] Blocks;
    public int FirstBlock;
    public int LastBlock;
    public int ActiveBlocksInLayer
    {
        get
        {
            return LastBlock - FirstBlock + 1;
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
        for (int i = 0; i < Blocks.Length; i++)
        {
            Blocks[i] = new Block();
        }
        SetBlocksEnabled(firstBlock, lastBlock);
    }

    public void SetBlocksEnabled(int firstBlock, int lastBlock)
    {
        if(firstBlock > lastBlock)
        {
            throw new ArgumentException("First block cannot be larger than Last block");
        }

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

        int firstBlock = Math.Min(FirstBlock + shiftValue, Blocks.Length - 1);
        int lastBlock = Math.Max(LastBlock + shiftValue, 0);
        SetBlocksEnabled(firstBlock, lastBlock);
    }

    private void updateBlocks()
    {
        for (int i = 0; i < Blocks.Length; i++)
        {
            Blocks[i].SetEnabled(i >= FirstBlock && i <= LastBlock);
        }
    }
}
