using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LearnProject
{
    public class MedicalKit : MonoBehaviour
    {
        public static int medKitHealth;
        void Start()
        {
            medKitHealth = 50;
        }

        private void OnTriggerStay(Collider other)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                PlayerManager.playerHealth += medKitHealth;
                Destroy(gameObject);
            }
        }
    }
}