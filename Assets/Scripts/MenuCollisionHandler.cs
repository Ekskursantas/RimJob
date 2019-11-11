using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuCollisionHandler : MonoBehaviour
{
    // Start is called before the first frame update
    private bool colliding = false;
    public Sprite unselected;
    public Sprite selected;
    public int id;
    private Image imageSwap;

    private void Start()
    {
        imageSwap = GetComponent<Image>();
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        imageSwap.sprite = selected;
        Selector pointer = other.GetComponent<Selector>();
        if (pointer.getSelection() == null) return;
        Image previousButton = pointer.getSelection().GetComponent<Image>();
        previousButton.sprite = unselected;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Selector pointer = other.GetComponent<Selector>();
        pointer.setSelection(transform.gameObject);
    }


    public void deselect()
    {
        imageSwap.sprite = unselected;
    }
}
