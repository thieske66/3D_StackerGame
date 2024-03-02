using System;

[Serializable]
public class Block
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
