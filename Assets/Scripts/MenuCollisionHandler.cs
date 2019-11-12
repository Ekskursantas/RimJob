using System;
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
    private Image previousButton;

    private void Start()
    {
        imageSwap = GetComponent<Image>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other);
        if (!other.CompareTag("Pointer")) return;
        Selector pointer = other.GetComponent<Selector>();
        if (pointer.getSelection() != null)
        {
            previousButton = pointer.getSelection().GetComponent<Image>();
            previousButton.sprite = unselected;
        }

        imageSwap.sprite = selected;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Pointer")) return;
        Selector pointer = other.GetComponent<Selector>();
        pointer.setSelection(transform.gameObject);
    }


    public void deselect()
    {
        previousButton.sprite = unselected;
        imageSwap.sprite = unselected;
    }
}