using System.Timers;
using UnityEngine;
using static Layer;

public class StackController : MonoBehaviour
{
    public int StackWidth = 11;
    public int InitialLayerSize = 5;
    public Stack Stack;
    public Layer ActiveLayer;
    public float StartShiftInterval = 1f;
    public float DifficultyScale = 0.1f;
    public bool Running = false;

    private float currentShiftInterval = 1f;

    private Timer timer;
    private Direction currentDirection = Direction.Right;

    public bool PlaceLayer = false;



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
    }

    public void PlaceActiveLayer()
    {
        Stack.AddLayer(ActiveLayer);
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
    }

    private void updateShiftInterval()
    {
        currentShiftInterval = StartShiftInterval + (StartShiftInterval * DifficultyScale * Stack.LayerCount);
        timer.Interval = currentShiftInterval;
    }

    private void startTimer()
    {
        updateShiftInterval();
        timer = new Timer(currentShiftInterval);
        timer.AutoReset = true;
        timer.Elapsed += onTimerTriggered;
        timer.Start();
    }

    private void onTimerTriggered(object sender, ElapsedEventArgs e)
    {
        Debug.Log("Timer triggered");
        shiftActiveLayer();
    }

    private void shiftActiveLayer()
    {
        ActiveLayer.ShiftBlocks(1, currentDirection);
        updateDirection();
    }

    private void updateDirection()
    {
        if (ActiveLayer.FirstBlock == Stack.StackWidth - 1 || ActiveLayer.LastBlock == 0)
        {
            currentDirection = (Direction)((int)currentDirection * -1);
        }
    }
}
