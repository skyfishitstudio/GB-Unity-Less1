using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCorutine : MonoBehaviour
{
    [SerializeField] private GameObject _enemyprefab;
    [SerializeField] private float _timeCooldown;
    private List<GameObject> _enemy;
    void Awake()
    {
        _enemy = new List<GameObject>(); //»нициализаци€ списка игровых объектов
    }

 
    void Start()
    {
        StartCoroutine(Spawner(3));
    }
    private IEnumerator Spawner (int count)
    {
        for (int i = 0; i < count; i++) //блок рутины
        {
            _enemy.Add(Instantiate(_enemyprefab, transform.position, Quaternion.identity));
            yield return new WaitForSeconds(_timeCooldown);
            
            if (i == 5) //Ёкстренна€ остановка
                yield break;

        }
        yield return new WaitForSeconds(10f); //пауза перед следующим блоком рутины
        foreach (var enemy in _enemy) // удаление объектов
        {
            Destroy(enemy);
        }
        _enemy.Clear(); // удаление ссылок на удаленные объекты
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            StopCoroutine(nameof(Spawner));
        }
    }
}
