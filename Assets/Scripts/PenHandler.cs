using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PenHandler : MonoBehaviour
{
    [SerializeField] private InputActionAsset _inputMap;
    private InputAction _pos;
    private InputAction _click;
    private bool isStarted;

    private void Start()
    {
        isStarted = false;
        _inputMap.Enable();
        _pos   = _inputMap.FindAction("Position");
        _click = _inputMap.FindAction("Click");
        
        _click.started += DrawingProcess;
        _click.canceled += EndDrawingProcess;
    }

    private void Update()
    {
        if (isStarted)
        {
            var add = new Vector2(0, 0);
            var pos = Camera.main.ScreenToWorldPoint(_pos.ReadValue<Vector2>() + add);
            pos.z = 0;
            if(pos.y>-3.7)
                gameObject.GetComponent<Transform>().position = pos;
            Debug.Log(pos);
        }
    }

    private void DrawingProcess(InputAction.CallbackContext callback)
    {
        isStarted = true;
    }

    private void EndDrawingProcess(InputAction.CallbackContext callback)
    {
        isStarted = false;
    }
}
