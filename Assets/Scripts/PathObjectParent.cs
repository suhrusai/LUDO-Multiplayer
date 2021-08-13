using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathObjectParent : MonoBehaviour
{
    [Header("Player Path Points")]
    public PathPoint[] commonPathPoints;
    public PathPoint[] redPathPoints;
    public PathPoint[] greenPathPoints;
    public PathPoint[] bluePathPoints;
    public PathPoint[] yellowPathPoints;    
    [Header("Scales And Positions difference")]
    public float[] scales;
    public float[] PositionsDifference;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
