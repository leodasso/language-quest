using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchCam : MonoBehaviour
{
    public bool beingDragged = false;
    public float clampIntensity = 15;
    [Space]
    public CameraBounds camBounds;
    TouchManager _touchManager;
    Camera _camera;

    float FrustrumHeight => GetCam().orthographicSize;
    float FrustrumWidth => GetCam().orthographicSize * GetCam().aspect;
    Vector3 FrustrumTop => transform.position + new Vector3(0, FrustrumHeight);
    Vector3 FrustrumBottom => transform.position - new Vector3(0,  FrustrumHeight);
    Vector3 FrustrumLeft => transform.position - new Vector3(FrustrumWidth, 0);
    Vector3 FrustrumRight => transform.position + new Vector3(FrustrumWidth, 0);

    Vector3 _clampedPoint;

    // zooming
    // scrolling
    //bounds

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_clampedPoint, .5f);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(FrustrumTop, .5f);
        Gizmos.DrawWireSphere(FrustrumBottom, .5f);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(FrustrumLeft, .5f);
        Gizmos.DrawWireSphere(FrustrumRight, .5f);
    }

    // Start is called before the first frame update
    void Start()
    {
        _touchManager = TouchManager.Get();
        _touchManager.onDragBegin += BeginDrag;
        _touchManager.onDragEnd += EndDrag;
        _touchManager.onDragUpdate += OnDrag;
    }
    
    Camera GetCam()
    {
        if (_camera) return _camera;
        _camera = GetComponent<Camera>();
        return _camera;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        _clampedPoint = transform.position;
        _clampedPoint.x = Mathf.Clamp(transform.position.x, camBounds.Left + FrustrumWidth,
            camBounds.Right - FrustrumWidth);

        _clampedPoint.y = Mathf.Clamp(transform.position.y, camBounds.Bottom + FrustrumHeight,
            camBounds.Top - FrustrumHeight);

        transform.position = Vector3.Lerp(transform.position, _clampedPoint, Time.unscaledDeltaTime * clampIntensity);
    }


    float AspectRatio => (float)Screen.currentResolution.height / Screen.currentResolution.width;

    void BeginDrag(Vector2 startWorldPos, int fingers)
    {
        beingDragged = true;
    }

    void EndDrag(Vector2 endWorldPos)
    {
        beingDragged = false;
    }

    void OnDrag(Vector2 dragDelta)
    {
        transform.position -= (Vector3)dragDelta;
    }
}
