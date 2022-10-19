using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "New Game Mode", menuName = "Gameplay/New Game Mode")]
    public class GameMode : ScriptableObject
    {
        public List<CardType> cardTypes;

        public virtual bool CanPlay(List<CardHistory> history, CardType card)
        {
            return cardTypes.Contains(card);
        }
    }
}