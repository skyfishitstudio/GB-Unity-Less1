using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LearnProject
{
    public class ExitMirror : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
                Debug.Log("You WIN!");

        }
    }
}