using UnityEngine;

public class CameraTargetMovement : MonoBehaviour
{
    private StackVisualizer stackVisualizer;
    private StackController stackController;

    private void Awake()
    {
        stackVisualizer = GameObject.FindObjectOfType<StackVisualizer>();
        stackController = GameObject.FindObjectOfType<StackController>();
    }

    private void Update()
    {
        int stackHalf = stackController.StackWidth / 2;

        if (stackVisualizer.Layers.Count != 0)
        {
            LayerObject activeLayer = stackVisualizer.Layers[^1];

            BlockObject centerBlock = activeLayer.Blocks[stackHalf];
            this.transform.position = centerBlock.transform.position;
        }
    }
}
