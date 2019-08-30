using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toucher : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Color normalColor = Color.white;
    public Color pressedColor = Color.white;
    
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.color = normalColor;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        if (Input.GetMouseButtonDown(0)) OnMouseDown();
        if (Input.GetMouseButtonUp(0)) OnMouseUp();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check for a touchable
        Touchable touchable = other.GetComponent<Touchable>();
        if (!touchable) return;
        
        touchable.OnHoverEnter();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Check for a touchable
        Touchable touchable = other.GetComponent<Touchable>();
        if (!touchable) return;
        
        touchable.OnHoverExit();
    }

    void OnMouseDown()
    {
        spriteRenderer.color = pressedColor;
    }

    void OnMouseUp()
    {
        spriteRenderer.color = normalColor;
    }
}
