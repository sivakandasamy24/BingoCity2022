using UnityEngine;

[CreateAssetMenu(fileName = "AttackCard",menuName ="AttackCardStats")]
public class AttackCardScriptableObjects : ScriptableObject
{
    public Sprite Icon;
    public int CardCount;
    public int CoinCount;
}
