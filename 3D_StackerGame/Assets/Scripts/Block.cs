using UnityEngine;

public class Block : MonoBehaviour
{
    public bool Enabled = false;

    internal void SetEnabled(bool enabled)
    {
        if(Enabled == enabled)
        {
            return;
        }
        
        Enabled = enabled;
    }
}
