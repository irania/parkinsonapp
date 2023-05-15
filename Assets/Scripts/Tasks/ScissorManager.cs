using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScissorManager : MonoBehaviour
{
    private const int GameId = 5;
    public Sprite scissorOpenSprite;
    public Sprite scissorClosedSprite;
    public int tapCountGoal = 10;
    public float openCloseDuration = 0.2f;
    public float pinchThreshold = 100f;

    private int tapCount = 0;
    private bool isScissorOpen = true;
    private SpriteRenderer spriteRenderer;
    private List<float> touchTimeData;
    private List<Vector2> touchPositionsData;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        touchTimeData = new List<float>();
        touchPositionsData = new List<Vector2>();
    }

    void Update()
    {
        if (Keyboard.current.escapeKey.isPressed)
        {
            GoHome();
        }
        if (tapCount < tapCountGoal && Input.touchCount == 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            touchPositionsData.Add(touch1.position);
            touchPositionsData.Add(touch2.position);

            float pinchDistance = Vector2.Distance(touch1.position, touch2.position);
            bool isPinching = pinchDistance <= pinchThreshold;

            if (isPinching && isScissorOpen)
            {
                StartCoroutine(ToggleScissorState());
                tapCount++;
                touchTimeData.Add(Time.time);
            }
            else if (!isPinching && !isScissorOpen)
            {
                StartCoroutine(ToggleScissorState());
            }
        }
    }

    IEnumerator ToggleScissorState()
    {
        if (isScissorOpen)
        {
            spriteRenderer.sprite = scissorClosedSprite;
        }
        else
        {
            spriteRenderer.sprite = scissorOpenSprite;
        }

        isScissorOpen = !isScissorOpen;

        yield return new WaitForSeconds(openCloseDuration);
    }

    public List<float> GetTouchTimeData()
    {
        return touchTimeData;
    }

    public List<Vector2> GetTouchPositionsData()
    {
        return touchPositionsData;
    }
    
    
    public void GoHome()
    {
        Application.LoadLevel(0);
        DataManager.Instance.GetCurrentUser().LevelsDone[GameId] = true;
    }
}

