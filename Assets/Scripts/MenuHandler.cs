using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{

    public void LoadHandwritingScene()
    {
        Application.LoadLevel ("DrawingScene");
    }
    public void LoadSelfieScene()
    {
        Application.LoadLevel ("TakingSelfieScene");
    }
}
