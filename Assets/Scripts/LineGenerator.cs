using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LineGenerator : MonoBehaviour
{
    public GameObject linePrefab;
    [SerializeField] private Text debugText;
    private Line activeLine;
    [SerializeField] private InputActionAsset _inputMap;
    private InputAction _click;
    private InputAction _pos;
    private Vector2 beforePos;

    private void Start()
    {
        //enable is required only if you're not using PlayerInput anywhere else
        _inputMap.Enable();

        _click = _inputMap.FindAction("Click");
        _pos   = _inputMap.FindAction("Position");

        //listen from clicks
        _click.started += DrawingProcess;
        _click.canceled += EndDrawingProcess;
    }
    

    private void DrawingProcess(InputAction.CallbackContext callback)
    {
        //get the value from the position
        Log($"click action: {_pos.ReadValue<Vector2>()}");
        GameObject newLine = Instantiate(linePrefab);
        activeLine = newLine.GetComponent<Line>();
    }

    private void Update()
    {
        if (activeLine != null)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(_pos.ReadValue<Vector2>());
            activeLine.UpdateLine(mousePos);
        }
    }

    private void EndDrawingProcess(InputAction.CallbackContext callback)
    {
        //get the value from the position
        Log($"press action: {_pos.ReadValue<Vector2>()}");
        EndDrawing();
        
    }

    private void EndDrawing()
    {
        activeLine = null;
    }

    private void OnDestroy()
    {
        _click.started -= DrawingProcess;
    }

    private void Log(string message)
    {
        //debugText.text = message;
        Debug.Log(message);
    }
}
