using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MyGame
{
    public class PlayerStatusPanel : MonoBehaviour
    {
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI lifeText;
        public Image lifeBar;
        public TextMeshProUGUI attackText;
        public TextMeshProUGUI defenceText;
        public GameObject invincibilityPiece;
        public TextMeshProUGUI invincibilityText;

        public void RefreshAll(Character character)
        {
            Player player = character as Player;
            nameText.text = player.name;
            lifeText.text = player.Life.ToString();
            lifeBar.transform.localScale = new Vector3((float)player.Life / player.MaxLife, 1, 1);
            attackText.text = player.attack.ToString();
            defenceText.text = player.defence.ToString();
            if (player.InvincibilityTimeCount > 0)
            {
                invincibilityText.text = player.InvincibilityTimeCount.ToString("0.00");
                invincibilityPiece.SetActive(true);
            }
            else
            {
                invincibilityPiece.SetActive(false);
            }
        }
    }
}
