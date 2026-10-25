using UnityEngine;

public class CrosshairUI : MonoBehaviour
{
    [SerializeField] private PickItem pickItem;

    void Update()
    {
        gameObject.SetActive(pickItem.HasItem());
    }
}