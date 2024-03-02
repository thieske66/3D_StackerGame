using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class Stack
{
    public List<Layer> Layers;
    public int StackWidth;

    public Layer TopLayer
    {
        get
        {
            return Layers[^1];
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

    public void AddLayer(Layer layer)
    {
        if(layer.ActiveBlocksInLayer <= 0)
        {
            throw new ArgumentException("Cannot add layer without active blocks");
        }

        int firstBlock = new List<int>()
        { 
            layer.FirstBlock, 
            Layers[^1].FirstBlock, 
            0 
        }.Max();
        layer.FirstBlock = firstBlock;

        int lastBlock = new List<int>() 
        { 
            layer.LastBlock, 
            Layers[^1].LastBlock, 
            StackWidth - 1 
        }.Min();
        layer.LastBlock = lastBlock;

        Layers.Add(layer);
    }
}
