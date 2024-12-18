using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField]
    private Animator _playerAnimator;

    public void SetLocomotionAnimationValues(float dotRight, float dotForward)
    {
        _playerAnimator.SetFloat("DotRight", Mathf.Lerp(_playerAnimator.GetFloat("DotRight"), dotRight, GameConstants.Animation.LOCOMOTION_BLENDTREE_LERPSCALE));
        _playerAnimator.SetFloat("DotForward", Mathf.Lerp(_playerAnimator.GetFloat("DotForward"), dotForward, GameConstants.Animation.LOCOMOTION_BLENDTREE_LERPSCALE));
    }
}
