using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class Stack
{
    public List<Layer> Layers = new List<Layer>();
    public int StackWidth;

    public Layer TopLayer
    {
        get
        {
            if (Layers.Count > 0)
            {
                return Layers[^1];
            }
            return null;
        }
    }
    public int LayerCount
    {
        get
        {
            return Layers.Count;
        }
    }

    public Stack(int stackWidth)
    {
        Layers = new List<Layer>();
        StackWidth = stackWidth;
    }

    public bool AddLayer(Layer layer)
    {
        if(layer.ActiveBlocksInLayer <= 0)
        {
            throw new ArgumentException("Cannot add layer without active blocks");
        }

        int firstBlock = new List<int>
        {
            layer.FirstBlock,
            0,
            TopLayer?.FirstBlock ?? 0
        }.Max();

        int lastBlock = new List<int>() 
        { 
            layer.LastBlock, 
            StackWidth - 1,
            TopLayer?.LastBlock ?? StackWidth -1
        }.Min();

        if (firstBlock > lastBlock)
        {
            return false;
        }

        layer.SetBlocksEnabled(firstBlock, lastBlock);
        Layers.Add(layer);

        return true;
    }
}
