using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "New Card Type", menuName = "Gameplay/New Card Type")]
    public sealed class CardType : ScriptableObject
    {
        public GameObject prefab;

        public int Index()
        {
            return CardTypeList.Instance.IndexOf(this);
        }
    }
}