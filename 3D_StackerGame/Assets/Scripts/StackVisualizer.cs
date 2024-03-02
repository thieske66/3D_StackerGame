using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class StackVisualizer : MonoBehaviour
{
    public StackController StackController;
    public Transform BlockPrototype;
    
    public Dictionary<int, List<Transform>> BlockObjects = new Dictionary<int, List<Transform>>();

    protected void Start()
    {
        BlockPrototype = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
        BlockPrototype.gameObject.SetActive(false);
        
        StackController.OnStackUpdate += updateLayer;
    }
    
    private List<Transform> SpawnLayer(Layer layer, int height)
    {
        List<Transform> newBlockObjects = new List<Transform>();

        for (int i = 0; i < layer.Blocks.Length; i++)
        {
            Transform blockObject = GameObject.Instantiate(BlockPrototype, new Vector3(i, height, 0), quaternion.identity);
            blockObject.name = $"{height}-{i}";
            blockObject.SetParent(this.transform);
            blockObject.gameObject.SetActive(true);
            newBlockObjects.Add(blockObject);
        }

        if (!BlockObjects.TryAdd(height, newBlockObjects))
        {
            Debug.Log("Could not add blockobjects");
        }

        return newBlockObjects;
    }

    private void updateLayer(Layer layer, int height)
    {
        if (!BlockObjects.TryGetValue(height, out List<Transform> blockObjects))
        {
            blockObjects = SpawnLayer(layer, height);
        }
        SetBlockObjects(layer, blockObjects);
    }

    private void SetBlockObjects(Layer layer, List<Transform> _blockObjects)
    {
        for (int i = 0; i < layer.Blocks.Length; i++)
        {
            _blockObjects[i].gameObject.SetActive(layer.Blocks[i].Enabled);
        }
    }
}