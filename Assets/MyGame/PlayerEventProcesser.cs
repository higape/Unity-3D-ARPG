using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame
{
    public class PlayerEventProcesser : MonoBehaviour
    {
        public Player player;

        // animation event
        private void EnableCollider()
        {
            player.weapon.SetColliderEnabled(true);
        }

        // animation event
        private void DisableCollider()
        {
            player.weapon.SetColliderEnabled(false);
        }
    }
}
