using UnityEngine;

[CreateAssetMenu(fileName = "TrailRendererConfiguration", menuName = "TrailRendererConfiguration")]
public class TrailRendererConfiguration : ScriptableObject
{
    public AnimationCurve WidthCurve;
    public float Time;
    public Gradient Color;
    public Material Material;
}
