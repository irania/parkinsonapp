using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class LanguageToggle : MonoBehaviour
{
    [SerializeField]
    private Sprite FaImage;
    [SerializeField]
    private Sprite EnImage;

    [SerializeField] private Image ToggleImage;

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
        ToggleImage.sprite = FaImage;
        DataManager.Instance.isFarsi = true;
    }

    private void SetEnglish()
    {
        ToggleImage.sprite = EnImage;
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
