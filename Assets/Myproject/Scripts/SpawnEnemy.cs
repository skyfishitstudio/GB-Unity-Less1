using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LearnProject
{
    public class SpawnEnemy : MonoBehaviour
    {
        public GameObject ghostPrefab;
        public Transform spawnPositionGhost;
        public int _enemyhealth =30;
        public int _enemylevel = 1;
        void Start()
        {
            SpawnGhost();
        }
        private void SpawnGhost()
        {
            var ghostObj = Instantiate(ghostPrefab, spawnPositionGhost.position, spawnPositionGhost.rotation); //������� ������
            var ghost = ghostObj.GetComponent<Ghost>(); //����� ������ ��������� �� �������
            ghost.Init(_enemyhealth, _enemylevel); //�������������� ������� � ����� ��������
            ghost.transform.SetParent(spawnPositionGhost);//��������� ������ ����� ������ ���������
        }
    }
}