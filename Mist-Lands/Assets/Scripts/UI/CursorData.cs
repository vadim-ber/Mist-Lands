using UnityEngine;

[CreateAssetMenu(fileName = "CursorData", menuName = "ScriptableObjects/Create cursor data")]
public class CursorData : ScriptableObject
{
    [SerializeField] private Texture2D _baseCursor;
    [SerializeField] private Texture2D _meeleAttackCursor;
    [SerializeField] private Texture2D _rangedAttackCursor;

    public Texture2D BaseCursor
    {
        get => _baseCursor; 
    }
    public Texture2D MeeleAttackCursor
    {
        get => _meeleAttackCursor;
    }
    public Texture2D RangedAttackCursor
    {
        get => _rangedAttackCursor;
    }
}
