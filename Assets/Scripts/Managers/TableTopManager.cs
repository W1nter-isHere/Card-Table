using System.Collections.Generic;
using Game;
using KevinCastejon.MoreAttributes;
using Unity.Netcode;
using UnityEngine;

namespace Managers
{
    public class TableTopManager : NetworkBehaviour
    {
        [ReadOnly] public NetworkVariable<int> round = new();
        public List<CardHistory> history = new();
        public GameMode gameMode;

        public void PlayCard(Card card)
        {
            if (!IsClient) return;
            
            var cardType = card.cardType;
            if (gameMode.CanPlay(history, cardType))
            {
                card.cardState = CardState.Played;
                AddCardPlayed_ServerRPC(new CardHistory { card = cardType.Index() });
            }
            else
            {
                card.cardState = CardState.InvalidPlay;
            }
        }

        [ServerRpc]
        private void AddCardPlayed_ServerRPC(CardHistory cardHistory)
        {
            AddCardPlayed_ClientRPC(cardHistory);
        }

        [ClientRpc]
        private void AddCardPlayed_ClientRPC(CardHistory cardHistory)
        {
            history.Add(cardHistory);
            Debug.Log($"Added to history {cardHistory}");
        }
    }
}