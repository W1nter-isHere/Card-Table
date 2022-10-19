using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "Card Types", menuName = "Gameplay/New Card Type List")]
    public class CardTypeList : ScriptableObject
    {
        [CanBeNull] private static CardTypeList _instance;
        public static CardTypeList Instance => _instance ??= Resources.Load<CardTypeList>("CardTypes/List");
        
        [SerializeField] private List<CardType> allTypes;

        public int IndexOf(CardType cardType)
        {
            return allTypes.FindIndex(c => c == cardType);
        }

        public CardType Get(int index)
        {
            return allTypes[index];
        }
    }
}