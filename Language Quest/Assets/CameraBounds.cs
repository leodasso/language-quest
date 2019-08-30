using UnityEngine;
using  Sirenix.OdinInspector;



public class CameraBounds : MonoBehaviour
{
    public Space mode = Space.World;
    [ShowInInspector, SerializeField]
    Vector2 _upperLeft;
    [ShowInInspector, SerializeField]
    Vector2 _bottomRight;
    
    public Vector2 UpperLeft => SelectedModePosition(_upperLeft);
    public Vector2 UpperRight => SelectedModePosition(new Vector2(_bottomRight.x, _upperLeft.y));
    public Vector2 LowerLeft => SelectedModePosition(new Vector2(_upperLeft.x, _bottomRight.y));
    public Vector2 LowerRight => SelectedModePosition(_bottomRight);

    public float Left => UpperLeft.x;
    public float Right => LowerRight.x;
    public float Top => UpperLeft.y;
    public float Bottom => LowerRight.y;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(UpperLeft, UpperRight);
        Gizmos.DrawLine(UpperRight, LowerRight);
        Gizmos.DrawLine(LowerRight, LowerLeft);
        Gizmos.DrawLine(LowerLeft, UpperLeft);
        Gizmos.DrawSphere(UpperLeft, .3f);
        Gizmos.DrawSphere(LowerRight, .3f);
    }


    Vector2 SelectedModePosition(Vector2 localPos) => 
        mode == Space.Self ? (Vector2) transform.position + localPos : localPos;

}
