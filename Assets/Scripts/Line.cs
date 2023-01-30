using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Line : MonoBehaviour
{
    public LineRenderer LineRenderer;

    private List<Vector2> points;
    private List<long> times;

    public void Awake()
    {
        LineRenderer = gameObject.GetComponent<LineRenderer>();
    }

    public void UpdateLine(Vector2 position)
    {
        if (points == null)
        {
            points = new List<Vector2>();
            times = new List<long>();
            SetPoint(position);
            return;
        }

        if (Vector2.Distance(points.Last(), position) > .1f)
        {
            SetPoint(position);
        }
    }
    
    void SetPoint(Vector2 point)
    {
        points.Add(point);
        times.Add(DateTime.Now.Ticks);
        LineRenderer.positionCount = points.Count;
        LineRenderer.SetPosition(points.Count()-1,point);
    }

    public List<Vector2> getPoints()
    {
        return points;
    }

    public List<long> getTimes()
    {
        return times;
    }
}
