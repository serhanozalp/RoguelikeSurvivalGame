using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField]
    private Animator _playerAnimator;

    public void SetLocomotionAnimationValues(float dotRight, float dotForward)
    {
        _playerAnimator.SetFloat("DotRight", dotRight);
        _playerAnimator.SetFloat("DotForward", dotForward);
    }
}
