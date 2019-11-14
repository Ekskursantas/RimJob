using UnityEngine;

public class Selector : MonoBehaviour
{
    public float sensitivity = 5.0f;
    public float smoothness = 2.0f;

    private Vector2 mouseTravel;
    private Vector2 smooth;
    private Vector2 lastValidMouseMove;
    private Vector2 mouse;
    private GameObject sellection;

    public void SetSelection(GameObject obj)
    {
        sellection = obj;
    }

    public GameObject GetSelection()
    {
        return sellection;
    }
}