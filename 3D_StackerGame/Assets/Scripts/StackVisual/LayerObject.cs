using System;
using System.Collections.Generic;
using UnityEngine;

public class LayerObject : MonoBehaviour
{
    public GameObject BlockPrototype;
    public List<BlockObject> Blocks;
    public float BlockSize = 1f;
    public float BlockMargin = 0f;

    private void Awake()
    {
        Blocks = new List<BlockObject>(this.gameObject.GetComponentsInChildren<BlockObject>());
    }

    public void UpdateLayer(Layer layer)
    {
        if (layer.Blocks.Length != Blocks.Count)
        {
            setupBlocks(layer);
        }

        for (int i = 0; i < layer.Blocks.Length; i++)
        {
            Blocks[i].SetVisible(layer.Blocks[i].Enabled);
        }
    }

    private void setupBlocks(Layer layer)
    {
        if (!BlockPrototype)
        {
            throw new Exception("Cannot spawn new block with no prototype set.");
        }

        // Destroy any existing blocks
        for (int i = 0; i < Blocks.Count; i++)
        {
            GameObject.Destroy(Blocks[i].gameObject);
        }
        Blocks.Clear();

        // Spawn entire layer of new blocks
        for (int i = 0; i < layer.Blocks.Length; i++)
        {
            GameObject newBlock = GameObject.Instantiate(BlockPrototype, this.transform);
            newBlock.name = "Block" + (i + 1);
            BlockObject newBlockObject = newBlock.GetComponent<BlockObject>();
            if (!newBlockObject)
            {
                throw new Exception("Cannot find BlockObject on newly spawned BlockPrototype.");
            }

            newBlockObject.transform.rotation = Quaternion.identity;
            newBlockObject.transform.localPosition = Vector3.right * (BlockSize + BlockMargin) * i;
            newBlockObject.SetVisible(false);

            Blocks.Add(newBlockObject);
        }
    }
}
