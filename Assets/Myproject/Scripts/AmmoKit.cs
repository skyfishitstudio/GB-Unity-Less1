using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LearnProject
{
    public class AmmoKit : MonoBehaviour
    {
        public static int ammoKitCount;
        void Start()
        {
            ammoKitCount = 50;
        }

        private void OnTriggerStay(Collider other)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                PlayerManager.playerAmmo += ammoKitCount;
                Destroy(gameObject);
            }
        }
    }
}