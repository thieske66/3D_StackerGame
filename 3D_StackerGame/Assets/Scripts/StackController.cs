using System.Timers;
using UnityEngine;
using static Layer;

public class StackController : MonoBehaviour
{
    public Stack Stack;
    public Layer ActiveLayer;
    public float StartShiftInterval = 1f;
    public float DifficultyScale = 0.1f;
    public bool Running = false;

    private float currentShiftInterval = 1f;

    private Timer timer;
    private Direction currentDirection = Direction.Right;


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
        createNewActiveLayer();
    }

    private void createNewActiveLayer()
    {
        ActiveLayer = new Layer(Stack.StackWidth, Stack.TopLayer.FirstBlock, Stack.TopLayer.LastBlock);
    }

    private void updateShiftInterval()
    {
        currentShiftInterval = StartShiftInterval * DifficultyScale * Stack.LayerCount;
        timer.Interval = currentShiftInterval;
    }

    private void startTimer()
    {
        timer = new Timer(currentShiftInterval);
        timer.AutoReset = true;
        timer.Elapsed += onTimerTriggered;
    }

    private void onTimerTriggered(object sender, ElapsedEventArgs e)
    {
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
