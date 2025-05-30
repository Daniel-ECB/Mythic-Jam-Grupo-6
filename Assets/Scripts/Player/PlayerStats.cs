using UnityEngine;

namespace MythicGameJam.Player
{
    [CreateAssetMenu(menuName = "MythicGameJam/PlayerStats")]
    public class PlayerStats : ScriptableObject
    {
        public float moveSpeed = 80f;
        public float acceleration = 35f;
        public float linearDamping = 5f;
        public GameObject projectilePrefab;
        public Sprite playerSprite;
        public RuntimeAnimatorController animatorController;
    }
}
