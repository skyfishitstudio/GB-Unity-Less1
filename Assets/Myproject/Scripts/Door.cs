using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LearnProject
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private Transform _rotatePoint;
        [SerializeField] private bool _stoppedDoor;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && !_stoppedDoor )
                _rotatePoint.Rotate(Vector3.up, 90);
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player") && !_stoppedDoor)
                _rotatePoint.Rotate(Vector3.up, -90);
        }
        private void OnTriggerStay(Collider other)
        {
            if (Input.GetKeyDown(KeyCode.E))
                _stoppedDoor = !_stoppedDoor;
        }
    }
}