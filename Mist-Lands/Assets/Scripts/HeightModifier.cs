using UnityEngine;

[CreateAssetMenu(fileName = "HeightModifierData", menuName = "ScriptableObjects/Create HaightModifierData")]
public class HeightModifier: ScriptableObject
{
    [SerializeField] private float minModifier = 0.5f;
    [SerializeField] private float maxModifier = 1.5f;
    public float CalcualteModifier(float yAxis, float unitHeight)
    {
        yAxis = Mathf.Clamp(yAxis, -unitHeight, unitHeight);        
        float modifier = Mathf.Lerp(minModifier, maxModifier, (yAxis + unitHeight) / (2 * unitHeight));
        return modifier;
    }
}
