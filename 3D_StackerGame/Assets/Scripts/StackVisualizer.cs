using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class StackVisualizer : MonoBehaviour
{
    public StackController StackController;
    public Transform BlockPrototype;
    public List<Transform> ActiveLayerObjects;

    protected void Start()
    {
        BlockPrototype = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
        BlockPrototype.gameObject.SetActive(false);

        StackController.OnActiveLayerShift += OnActiveLayerShift;
        StackController.OnLayerAddedToStack += OnLayerAddedToStack;
        StackController.OnActiveLayerCreated += OnActiveLayerCreated;
    }

    private void OnActiveLayerCreated(Layer newLayer)
    {
        SpawnLayer(newLayer);
    }

    private void OnLayerAddedToStack(Layer layerAddedToStack)
    {
        ActiveLayerObjects.Clear();
    }

    private void OnActiveLayerShift(Layer layer)
    {
        int activeLayerHeight = StackController.Stack.LayerCount;
        for (int i = 0; i < ActiveLayerObjects.Count; i++)
        {
            ActiveLayerObjects[i].position = new Vector3(layer.FirstBlock + i, activeLayerHeight, 0);
        }
    }

    private void SpawnLayer(Layer layer)
    {
        //Draw the new Layer
        int blockHeight = StackController.Stack.LayerCount;
        for (int i = 0; i < layer.Blocks.Length; i++)
        {
            if (layer.Blocks[i].Enabled)
            {
                Transform blockObject = GameObject.Instantiate(BlockPrototype, new Vector3(i, blockHeight, 0), quaternion.identity);
                blockObject.gameObject.SetActive(true);
                ActiveLayerObjects.Add(blockObject);
            }
        }
    }

    private void updateLayer(Layer layer, int height)
    {

    }

}