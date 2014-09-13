using UnityEngine;
using System.Collections;

public class FogTintController : Singleton<FogTintController> 
{
    public float minHeight, maxHeight;
    public Color minTint, maxTint;

	public static Color GetFogTint(float y)
    {
        float t = Mathf.Clamp(y,Instance.minHeight,Instance.maxHeight);
        t = (t - Instance.minHeight) / (Instance.maxHeight - Instance.minHeight);
        return Color.Lerp(Instance.minTint, Instance.maxTint, t);
    }

}
