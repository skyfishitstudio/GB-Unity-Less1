using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace LearnProject
{
    public class PlayerManager : MonoBehaviour
    {
        public static int playerHealth;
        public TextMeshProUGUI playerHealthText;
        public static int playerAmmo;
        public TextMeshProUGUI playerAmmoText;
        public static bool gameOver;
        public static int killCounter = 0;
        public static int level = 1;

        void Start()
        {
            playerHealth = 100;
            playerAmmo = 25;
            gameOver = false;

        }
        void Update()
        {
            playerHealthText.text = "" + playerHealth;
            playerAmmoText.text = "" + playerAmmo;
            if (gameOver)
            {
                SceneManager.LoadScene("Level-1");
            }
        }
        void FixedUpdate()
        {
            
            if(playerHealth <= 0)
            {
                gameOver = true;
            }
        }

    }
}