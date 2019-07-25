using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Prop : MonoBehaviour
{
    [AssetsOnly]
    public PropDef propDef;
    SpriteRenderer _spriteRenderer;
    Collider2D _collider;
    readonly Color _lockedColor = new Color(0, 0, 0, .1f);
    
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
        
        if (propDef)
            SetUnlocked(propDef.unlocked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetUnlocked(bool isUnlocked)
    {
        if (_spriteRenderer)
        {
            _spriteRenderer.color = isUnlocked ? Color.white : _lockedColor;
        }

        if (_collider)
        {
            _collider.enabled = isUnlocked;
        }
    }
}
