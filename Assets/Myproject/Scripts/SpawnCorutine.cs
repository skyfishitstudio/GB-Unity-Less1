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
        _enemy = new List<GameObject>(); //������������� ������ ������� ��������
    }

 
    void Start()
    {
        StartCoroutine(Spawner(3));
    }
    private IEnumerator Spawner (int count)
    {
        for (int i = 0; i < count; i++) //���� ������
        {
            _enemy.Add(Instantiate(_enemyprefab, transform.position, Quaternion.identity));
            yield return new WaitForSeconds(_timeCooldown);
            
            if (i == 5) //���������� ���������
                yield break;

        }
        yield return new WaitForSeconds(10f); //����� ����� ��������� ������ ������
        foreach (var enemy in _enemy) // �������� ��������
        {
            Destroy(enemy);
        }
        _enemy.Clear(); // �������� ������ �� ��������� �������
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            StopCoroutine(nameof(Spawner));
        }
    }
}
