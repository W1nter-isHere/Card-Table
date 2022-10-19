using System;
using System.Collections.Generic;
using Game;
using JetBrains.Annotations;
using UI;
using UnityEngine;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        [NonSerialized] [CanBeNull] public Card DraggingCard;
        
        private TableTopManager _tableTopManager;
        private CardLayout _layout;
        
        private void Start()
        {
            _tableTopManager = FindObjectOfType<TableTopManager>();
            _layout = FindObjectOfType<CardLayout>();
        }

        public void SpawnCard(CardType cardType)
        {
            Instantiate(cardType.prefab, _layout.transform).GetComponent<Card>().cardType = cardType;
            _layout.Align();
        }
    }
}