using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    public bool dragging = false;
    
    public Action<Vector2, int> onDragBegin;
    public Action<Vector2> onDragEnd;
    public Action<Vector2> onDragUpdate;

    Vector2 _prevDragPosition;
    Vector2 _dragDelta;

    Vector2 touchPosition
    {
        get
        {
            if (Input.touchCount > 0)
                return Input.touches[0].position;
            return Input.mousePosition;
        }
    }
    
    static TouchManager _touchManager;

    public static TouchManager Get()
    {
        if (_touchManager) return _touchManager;
        _touchManager = FindObjectOfType<TouchManager>();
        return _touchManager;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    bool TouchDown()
    {
        if (Input.touchCount < 1) 
            return Input.GetButtonDown("TwoTouch");

        return Input.touches[0].phase == TouchPhase.Began;
    }

    bool TouchUp()
    {
        if (Input.touchCount < 1) 
            return Input.GetButtonUp("TwoTouch");

        return Input.touches[0].phase == TouchPhase.Canceled || Input.touches[0].phase == TouchPhase.Ended ;
    }

    // Update is called once per frame
    void Update()
    {        
        if (dragging)
        {
            if (TouchUp())
            {
                dragging = false;
                onDragEnd.Invoke(Camera.main.ScreenToWorldPoint(touchPosition));
            }
            else
            {
                // Drag update's parameter is the delta (in world space) of the drag
                _dragDelta = Camera.main.ScreenToWorldPoint(touchPosition) -
                                          Camera.main.ScreenToWorldPoint(_prevDragPosition);
                _prevDragPosition = touchPosition;
                onDragUpdate.Invoke(_dragDelta);
            }
        }
        
        // Desktop simulation of a two finger touch
        else if (TouchDown())
        {
            dragging = true;
            _prevDragPosition = touchPosition;
            onDragBegin.Invoke(Camera.main.ScreenToWorldPoint(touchPosition), 2);
        }
    }
}