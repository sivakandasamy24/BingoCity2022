using UnityEngine;

[CreateAssetMenu(fileName = "CharacterCard", menuName = "CharacterCardStatistics")]
public class CharacterCardScriptableObjects : ScriptableObject
{
    public int Ratings;
    public Sprite Character;
    public int CoinCount;
}
