using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Touchable : MonoBehaviour
{
    public UnityEvent onHoverEnter;

    public UnityEvent onHoverExit;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnHoverEnter()
    {
        onHoverEnter.Invoke();
    }

    public void OnHoverExit()
    {
        onHoverExit.Invoke();
    }
}
