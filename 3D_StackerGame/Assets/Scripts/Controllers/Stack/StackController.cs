using System;
using UnityEngine;
using static Layer;

public class StackController : MonoBehaviour
{
    private const float MINIMUM_SHIFT_INTERVAL = 0.01f;

    public int StackWidth = 11;
    public int InitialLayerSize = 5;
    public Stack Stack;
    public Layer ActiveLayer;
    [Range(0.1f, 1f)]
    public float StartShiftInterval = 1f;
    public float DifficultyScale = 0.1f;
    public bool Running = false;

    [SerializeField]
    private float currentShiftInterval = 1f;

    [SerializeField]
    private TimedTrigger timer;
    private Direction currentDirection = Direction.Right;

    [SerializeField]
    private bool DEBUG_AutoStack = false;

    public bool PlaceLayer = false;

    /// <summary>
    /// Called when anything in the stack changed. params ActiveLayer, LayerHeight
    /// </summary>
    public Action<Layer, int> OnStackUpdate;

    private void Start()
    {
        createStack();
        int firstBlock = Stack.StackWidth / 2 - InitialLayerSize / 2;
        int lastBlock = firstBlock + InitialLayerSize;
        createNewActiveLayer(Stack.StackWidth, firstBlock, lastBlock);
        StartRunning();
    }

    private void Update()
    {
        if (PlaceLayer)
        {
            PlaceActiveLayer();
            PlaceLayer = false;
        }
    }
    private void FixedUpdate()
    {
        if (Running)
        {
            timer?.Elapse(Time.fixedDeltaTime);
        }
    }

    public void StartRunning()
    {
        Running = true;
        restartTimer();

        Debug.Log("Started running!");
    }

    public void StopRunning()
    {
        Running = false;

        Debug.Log("Stopped running!");
    }

    public void PlaceActiveLayer()
    {
        if (DEBUG_AutoStack)
        {
            ActiveLayer = new Layer(Stack.StackWidth, Stack.TopLayer?.FirstBlock ?? 3, Stack.TopLayer?.LastBlock ?? 8);
        }

        if (!Stack.AddLayer(ActiveLayer))
        {
            StopRunning();
            return;
        }
        OnStackUpdate?.Invoke(ActiveLayer, Stack.LayerCount - 1);
        ActiveLayer = null;
        updateShiftInterval();
        createNewActiveLayer(Stack.StackWidth, Stack.TopLayer.FirstBlock, Stack.TopLayer.LastBlock);
    }

    private void createStack()
    {
        Stack = new Stack(StackWidth);
    }
    
    private void createNewActiveLayer(int blocksInLayer, int firstBlock, int lastBlock)
    {
        ActiveLayer = new Layer(blocksInLayer, firstBlock, lastBlock);
        OnStackUpdate?.Invoke(ActiveLayer, Stack.LayerCount);
    }

    private void updateShiftInterval()
    {
        currentShiftInterval = Mathf.Max(StartShiftInterval - (StartShiftInterval * DifficultyScale * Stack.LayerCount), MINIMUM_SHIFT_INTERVAL);
        timer.Interval = currentShiftInterval;
    }

    private void restartTimer()
    {
        timer = new TimedTrigger();
        timer.OnElapsed += onTimerTriggered;
        updateShiftInterval();
    }

    private void onTimerTriggered()
    {
        shiftActiveLayer();
    }

    private void shiftActiveLayer()
    {
        ActiveLayer.ShiftBlocks(1, currentDirection);
        updateDirection();
        OnStackUpdate?.Invoke(ActiveLayer, Stack.LayerCount);
    }

    private void updateDirection()
    {
        if (ActiveLayer.FirstBlock == Stack.StackWidth - 1 || ActiveLayer.LastBlock == 0)
        {
            currentDirection = (Direction)((int)currentDirection * -1);
        }
    }
}
