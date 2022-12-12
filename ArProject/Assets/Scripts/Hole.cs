using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _endPoint;
    [SerializeField] private bool _free=true;
    


    public bool GetFree()
    {
        return _free;
    }


    public Transform GetStartPoint()
    {
        return _startPoint;
    }

    public Transform GetEndPoint()
    {
        return _endPoint;
    }
    public void IsFree(bool flag)
    {
        _free = flag;
    }

}
