using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _score;
    [SerializeField] private float _speed;
    [SerializeField] private float _timeStay;

    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _endPoint;



    public void Activation(Transform start, Transform end)
    {
        _startPoint = start;
        _endPoint = end;
        transform.position = _startPoint.position;
        StartCoroutine(NextCoroutine());
    }

    IEnumerator CoroutineMove(Transform end)
    {
        while (Vector3.Distance(transform.position, end.position) > 0.2f)
        {
            transform.position =
                Vector3.MoveTowards(transform.position, end.position, _speed * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator NextCoroutine()
    {
        yield return StartCoroutine(CoroutineMove(_endPoint));
        StartCoroutine(CoroutineStay());
    }


    IEnumerator CoroutineStay()
    {
        yield return new WaitForSeconds(_timeStay);
        Debug.Log("Вызываюсь");
        StartCoroutine(CoroutineMove(_startPoint));
    }

    public void Test()
    {
        Activation(_startPoint, _endPoint);
    }
}
