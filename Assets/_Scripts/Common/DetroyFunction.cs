using UnityEngine;

public class DetroyFunction : MonoBehaviour
{
    public void DestroyParent()
    {
        Destroy(this.transform.parent);
    }
}
