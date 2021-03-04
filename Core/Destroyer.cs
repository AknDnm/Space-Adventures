using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public void Destroy()
    {
        this.gameObject.SetActive(false);
    }
}
