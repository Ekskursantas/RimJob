using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuCollisionHandler : MonoBehaviour
{
    public Sprite unselected;
    public Sprite selected;

    public int id;

    // Start is called before the first frame update
    private bool colliding = false;
    private Image imageSwap;
    private Image previousButton;

    private void Start()
    {
        imageSwap = GetComponent<Image>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Pointer")) return;
        Selector pointer = other.GetComponent<Selector>();
        if (pointer.GetSelection() != null)
        {
            previousButton = pointer.GetSelection().GetComponent<Image>();
            previousButton.sprite = unselected;
        }

        imageSwap.sprite = selected;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Pointer")) return;
        Selector pointer = other.GetComponent<Selector>();
        pointer.SetSelection(transform.gameObject);
    }


    public void Deselect()
    {
        if (previousButton != null) previousButton.sprite = unselected;
        imageSwap.sprite = unselected;
    }
}