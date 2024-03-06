using UnityEngine;

public class BlockObject : MonoBehaviour
{
    public void SetVisible(bool value)
    {
        this.gameObject.SetActive(value);
    }
}
