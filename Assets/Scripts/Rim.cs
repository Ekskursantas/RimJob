using UnityEngine;

public class Rim : MonoBehaviour
{
    // Start is called before the first frame update
    public int id;
    public Vector3 position;
    public Vector3 angle;
    public bool isKinematic;

    private void Update()
    {
        position = transform.localPosition;
        angle = transform.localEulerAngles;
    }


    public string[] getColors()
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        string[] colors = new string[renderers.Length];
        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = ColorUtility.ToHtmlStringRGBA(renderers[i].material.color);
        }

        return colors;
    }

    public bool kinematic()
    {
        return GetComponent<Rigidbody>().isKinematic;
    }
}