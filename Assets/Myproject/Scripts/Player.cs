using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LearnProject
{
    public class Player : MonoBehaviour
    {
        // Start is called before the first frame update
        public GameObject shieldPrefab;
        public Transform spawnPositionShield;
        public GameObject ghostPrefab;
        public Transform spawnPositionGhost;
        private bool _IsSpawnShield;
        public int level = 1;
        private Vector3 _direction;
        public float speed = 2f;
        private bool _IsSprint;
        void Awake()
        {

        }


        void Start()
        {
            SpawnGhost();
            
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(1))
                _IsSpawnShield = true;
            _IsSprint = Input.GetButton("Sprint");
            _direction.x = Input.GetAxis("Horizontal");
            _direction.z = Input.GetAxis("Vertical");
        
        }   
        void FixedUpdate()
        {
            if (_IsSpawnShield)
            {
                SpawnShield();
                _IsSpawnShield = false;
            }
            Move(Time.fixedDeltaTime);
        }
        private void SpawnShield()
        {
            var shieldObj = Instantiate(shieldPrefab, spawnPositionShield.position, spawnPositionShield.rotation);
            var shield = shieldObj.GetComponent<Shield>();
            shield.Init(10 * level);
            shield.transform.SetParent(spawnPositionShield);
        }
        private void SpawnGhost()
        {
            var ghostObj = Instantiate(ghostPrefab, spawnPositionGhost.position, spawnPositionGhost.rotation);
            var ghost = ghostObj.GetComponent<Ghost>();
            ghost.Init(10 * level);
            ghost.transform.SetParent(spawnPositionGhost);
        }
        private void Move(float delta)
        {
            transform.position += _direction * (_IsSprint ? speed * 3 : speed) * delta;  
        }
    }

}
