using UnityEngine;

namespace MythicGameJam.Player
{
    [RequireComponent(typeof(Animator))]
    public sealed class PlayerAnimatorController : MonoBehaviour
    {
        [SerializeField]
        private PlayerStats _stats;
        [SerializeField]
        private Animator _animator;

        private void Awake()
        {
            if (_animator == null)
                _animator = GetComponent<Animator>();

            if (_stats != null && _stats.animatorController != null)
                _animator.runtimeAnimatorController = _stats.animatorController;
        }
    }
}
