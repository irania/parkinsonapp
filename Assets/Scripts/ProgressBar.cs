using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] 
    private GameObject CirclePrefab;
    
    private IList<GameObject> Circles;

    [SerializeField] 
    private Color FillColor;

    [SerializeField] 
    private int StepsNumber;

    private int index;

    private void Start()
    {
        index = 0;
        GenerateCircles();
        ActiveCircle();
    }

    private void GenerateCircles()
    {
        Circles = new List<GameObject>();
        //generate circles
        int n = (StepsNumber-1)*20;
        for (int i = 0; i < StepsNumber; i++)
        {
            var newCircle = Instantiate(CirclePrefab,gameObject.transform);
            newCircle.transform.localPosition = new Vector3(i* 40-n, 0, 0);
            Circles.Add(newCircle);
        }

    }
    
    public void GoNext()
    {
        index++;
        if (index < Circles.Count)
            ActiveCircle();
    }
    
    public void GoPrev()
    {
        if (index > 0)
        {
            index--;
            ActiveCircle();
        }
    }

    public void ActiveCircle()
    {
        for (int i=0; i < Circles.Count;i++)
        {
            if(i<=index)
                Circles[i].GetComponent<SpriteRenderer>().color=FillColor;
            else
            {
                Circles[i].GetComponent<SpriteRenderer>().color=Color.grey;
            }
        }
    }
    
}
