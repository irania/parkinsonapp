using DefaultNamespace;
using RTLTMPro;
using UnityEngine;
using UnityEngine.UI;

public class LanguageToggle : MonoBehaviour
{

    private void Start()
    {
        if( DataManager.Instance.isFarsi)
            SetFarsi();
        else
        {
            SetEnglish();
        }
    }

    private void SetFarsi()
    {
        DataManager.Instance.isFarsi = true;
    }

    private void SetEnglish()
    {
        DataManager.Instance.isFarsi = false;
    }

    public void Toggle()
    {
        if( DataManager.Instance.isFarsi)
            SetEnglish();
        else
        {
            SetFarsi();
        }
        
        
    }
}
