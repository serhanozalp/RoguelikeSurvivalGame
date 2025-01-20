using UnityEngine;

public abstract class BaseEntityAnimation : MonoBehaviour
{
    [SerializeField] protected Animator _entityAnimator;

    private void Awake()
    {
        Debug.Assert(_entityAnimator != null, $"{gameObject.name} is missing Animator!");
    }

    public void SetLocomotionAnimationValues(float dotRight, float dotForward)
    {
        _entityAnimator.SetFloat("DotRight", Mathf.Lerp(_entityAnimator.GetFloat("DotRight"), dotRight, GameConstants.Player.Animation.LOCOMOTION_BLENDTREE_LERPSCALE));
        _entityAnimator.SetFloat("DotForward", Mathf.Lerp(_entityAnimator.GetFloat("DotForward"), dotForward, GameConstants.Player.Animation.LOCOMOTION_BLENDTREE_LERPSCALE));
    }

    protected bool IsAnimationFinished(int layerIndex, string animationName)
    {
        AnimatorStateInfo stateInfo = _entityAnimator.GetCurrentAnimatorStateInfo(GameConstants.Player.Animation.ARMS_LAYER_INDEX);
        return stateInfo.IsName(animationName) && stateInfo.normalizedTime % 1f >= 0.99f;
    }
}
