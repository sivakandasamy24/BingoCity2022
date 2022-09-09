using UnityEngine;

[CreateAssetMenu(fileName = "AttackCard",menuName ="AttackCardStats")]
public class AttackCardScriptableObjects : ScriptableObject
{
    public string AttackName;
    public int CardCount;
    public int CoinCount;
}
