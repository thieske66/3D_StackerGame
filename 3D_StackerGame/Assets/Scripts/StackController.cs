using System;
using System.Timers;
using UnityEngine;
using static Layer;
using Timer = System.Timers.Timer;

public class StackController : MonoBehaviour
{
    private const float MINIMUM_SHIFT_INTERVAL = 0.1f;

    public int StackWidth = 11;
    public int InitialLayerSize = 5;
    public Stack Stack;
    public Layer ActiveLayer;
    public float StartShiftIntervalMS = 1000f;
    public float DifficultyScale = 0.1f;
    public bool Running = false;

    [SerializeField]
    private float currentShiftInterval = 1f;

    private Timer timer;
    private Direction currentDirection = Direction.Right;

    public bool PlaceLayer = false;
    public Action<Layer> OnLayerAddedToStack;
    public Action<Layer> OnActiveLayerShift;
    public Action<Layer> OnActiveLayerCreated;



    private void Update()
    {
        if (!Running)
        {
            createStack();
            int firstBlock = Stack.StackWidth / 2 - InitialLayerSize / 2;
            int lastBlock = firstBlock + InitialLayerSize;
            createNewActiveLayer(Stack.StackWidth, firstBlock, lastBlock);
            StartRunning();
        }

        if (PlaceLayer)
        {
            PlaceActiveLayer();
            PlaceLayer = false;
        }
    }


    public void StartRunning()
    {
        Running = true;
        startTimer();

        Debug.Log("Started running!");
    }

    public void StopRunning()
    {
        Running = false;
        stopTimer();

        Debug.Log("Stopped running!");
    }

    public void PlaceActiveLayer()
    {
        if (!Stack.AddLayer(ActiveLayer))
        {
            StopRunning();
            return;
        }
        OnLayerAddedToStack?.Invoke(ActiveLayer);
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
        OnActiveLayerCreated?.Invoke(ActiveLayer);
    }

    private void updateShiftInterval()
    {
        currentShiftInterval = Mathf.Max(StartShiftIntervalMS - (StartShiftIntervalMS * DifficultyScale * Stack.LayerCount), MINIMUM_SHIFT_INTERVAL);
        timer.Interval = currentShiftInterval;
    }

    private void startTimer()
    {
        timer = new Timer();
        timer.SynchronizingObject = 
        timer.AutoReset = true;
        timer.Elapsed += onTimerTriggered;
        updateShiftInterval();
        timer.Start();
    }

    private void stopTimer()
    {
        timer.Dispose();
    }

    private void onTimerTriggered(object sender, ElapsedEventArgs e)
    {
        if (this == null)
        {
            stopTimer();
        }
        
        //Debug.Log("Timer triggered");
        shiftActiveLayer();
    }

    private void shiftActiveLayer()
    {
        ActiveLayer.ShiftBlocks(1, currentDirection);
        updateDirection();
        OnActiveLayerShift?.Invoke(ActiveLayer);
    }

    private void updateDirection()
    {
        if (ActiveLayer.FirstBlock == Stack.StackWidth - 1 || ActiveLayer.LastBlock == 0)
        {
            currentDirection = (Direction)((int)currentDirection * -1);
        }
    }

    private void OnDestroy()
    {
        stopTimer();
    }
}
