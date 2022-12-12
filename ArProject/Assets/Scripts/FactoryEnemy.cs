using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryEnemy : MonoBehaviour
{
    [SerializeField] private Hole[] _holes;
    [SerializeField] private Enemy[] _enemies;
    [SerializeField] private int _countMonster;
    [SerializeField] private int _monsters;
    [SerializeField] private float _time;
    [SerializeField] private bool _flagWork = true;
    [SerializeField] private GameObject _mainObject;
    [SerializeField] private float _size;
    private void Update()
    {
        //var randomPosition = Random.Range(0, spawnPoints.Count);
        if (_mainObject.active)
        {
            if (_flagWork)
            {
                //var goblin = Instantiate(goblinPrefab, spawnPoints[randomPosition].position, Quaternion.identity);

                StartWork();
                _flagWork = false;
            }
        }
    }

    public void StartWork()
    {
        StartCoroutine(CoroutineCreate());
    }
    public void CreateMonster()
    {
        var rand = Random.Range(0, _holes.Length);
        while (!_holes[rand].GetFree())
        {
             rand = Random.Range(0, _holes.Length);
        }
        var randEnemy = Random.Range(0, _enemies.Length);

        var enemy = Instantiate(_enemies[randEnemy], _holes[rand].transform.position,Quaternion.identity);
        enemy.transform.localScale -= new Vector3(enemy.transform.localScale.x / _size, enemy.transform.localScale.y / _size, enemy.transform.localScale.z / _size);
        //enemy.transform.position = _holes[rand].GetStartPoint().position; 
        enemy.GetComponent<Enemy>().Activation(_holes[rand].GetStartPoint(), _holes[rand].GetEndPoint(),_holes[rand]);
        _holes[rand].IsFree(false);
    }

    public IEnumerator CoroutineCreate()
    {
        yield return new WaitForSeconds(_time);
        CreateMonster();
        StartCoroutine(CoroutineCreate());
    }
}
