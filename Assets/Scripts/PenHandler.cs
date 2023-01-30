using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PenHandler : MonoBehaviour
{
    [SerializeField] private InputActionAsset _inputMap;
    private InputAction _pos;

    private void Start()
    {
        _inputMap.Enable();
        _pos   = _inputMap.FindAction("Position");
    }

    private void Update()
    {
        var add = new Vector2(80, 80);
        var readedPos = _pos.ReadValue<Vector2>();

        if(readedPos.Equals(Vector2.zero))
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        else
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        var pos = Camera.main.ScreenToWorldPoint(_pos.ReadValue<Vector2>()+add);
        pos.z = 0;
        gameObject.GetComponent<Transform>().position = pos ;
    }
}
