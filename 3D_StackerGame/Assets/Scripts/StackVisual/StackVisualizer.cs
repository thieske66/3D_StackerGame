using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StackController))]
public class StackVisualizer : MonoBehaviour
{
    private StackController stackController;
    private GameObject layerContainer;

    public GameObject BlockPrototype;
    public List<LayerObject> Layers = new List<LayerObject>();
    public float LayerHeight = 1f;
    public float LayerMargin = 0f;
    public float BlockSize = 1f;
    public float BlockMargin = 0f;

    private void Awake()
    {
        layerContainer = new GameObject("Layers");
        layerContainer.transform.parent = this.transform;
        layerContainer.transform.localPosition = Vector3.zero;
        layerContainer.transform.rotation = Quaternion.identity;
        stackController = GetComponent<StackController>();
    }

    protected void Start()
    {
        if (!BlockPrototype)
        {
            throw new ArgumentException("Cannot create stack without a prototype block");
        }

        stackController.OnStackUpdate += updateLayer;
    }

    private void updateLayer(Layer layer, int height)
    {
        getLayer(height).UpdateLayer(layer);
    }

    private LayerObject getLayer(int height)
    {
        if (height < 0 || height > Layers.Count)
        {
            throw new ArgumentException("Cannot get layer at height " + height);
        }

        if(height == Layers.Count)
        {
            Layers.Add(createNewLayer());
        }

        return Layers[height];
    }

    private LayerObject createNewLayer()
    {
        int currentLayerCount = Layers.Count;
        GameObject newLayer = new GameObject("Layer " + (currentLayerCount + 1));
        newLayer.transform.parent = this.layerContainer.transform;
        newLayer.transform.rotation = Quaternion.identity;
        newLayer.transform.localPosition = Vector3.zero;

        LayerObject newLayerObject = newLayer.AddComponent<LayerObject>();
        newLayer.transform.localPosition = Vector3.up * (LayerHeight + LayerMargin) * currentLayerCount;
        newLayerObject.BlockPrototype = BlockPrototype;
        newLayerObject.BlockSize = BlockSize;
        newLayerObject.BlockMargin = BlockMargin;

        return newLayerObject;
    }
}