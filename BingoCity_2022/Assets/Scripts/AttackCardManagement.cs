using UnityEngine;

public class AttackCardManagement : MonoBehaviour
{
    [SerializeField] private TextMesh AttackName;
    [SerializeField] private TextMesh CardCount;
    [SerializeField] private TextMesh CoinCount;

    public AttackCardScriptableObjects AttackCardType;

    void Start()
    {
        //AttackName.text = AttackCardType.name;
        //CardCount.text = AttackCardType.CardCount.ToString();
        //CoinCount.text = AttackCardType.CoinCount.ToString();
    }
}
