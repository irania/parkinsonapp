using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class LanguageHelper : MonoBehaviour
{
    public List<GameObject> FaObjects;

    public List<GameObject> EnObjects;
    // Start is called before the first frame update
    void Update()
    {
        bool faActive = DataManager.Instance.isFarsi;
        foreach (var faObject in FaObjects)
        {
            faObject.SetActive(faActive);
        }

        foreach (var enObject in EnObjects)
        {
            enObject.SetActive(!faActive);
        }
    }
}
