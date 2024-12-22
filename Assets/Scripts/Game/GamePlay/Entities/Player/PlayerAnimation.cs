using UnityEngine;

public class PlayerAnimation : BaseEntityAnimation
{
    public void SetLocomotionAnimationValues(float dotRight, float dotForward)
    {
        _entityAnimator.SetFloat("DotRight", Mathf.Lerp(_entityAnimator.GetFloat("DotRight"), dotRight, GameConstants.Animation.LOCOMOTION_BLENDTREE_LERPSCALE));
        _entityAnimator.SetFloat("DotForward", Mathf.Lerp(_entityAnimator.GetFloat("DotForward"), dotForward, GameConstants.Animation.LOCOMOTION_BLENDTREE_LERPSCALE));
    }
}
