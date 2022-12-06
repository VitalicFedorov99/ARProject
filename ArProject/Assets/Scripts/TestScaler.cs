using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScaler : MonoBehaviour
{
    [SerializeField] private float _size;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale -= new Vector3(transform.localScale.x/_size, transform.localScale.y/_size, transform.localScale.z/_size);
    }

    
}
