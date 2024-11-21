using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MyGame
{
    public class EnemyStatusPanel : MonoBehaviour
    {
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI lifeText;
        public Image lifeBar;

        void Update()
        {
            transform.rotation = Camera.main.transform.rotation;
        }

        public void RefreshAll(Character character)
        {
            nameText.text = character.name;
            lifeText.text = character.Life.ToString();
            lifeBar.transform.localScale = new Vector3(
                (float)character.Life / character.MaxLife,
                1,
                1
            );
        }
    }
}
