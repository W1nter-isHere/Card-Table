using System;
using Unity.Netcode;

namespace Managers
{
    [Serializable]
    public struct CardHistory : IEquatable<CardHistory>, INetworkSerializable
    {
        /// <summary>
        /// Player who played the card
        /// </summary>
        public int player;
        
        /// <summary>
        /// Card that the player played
        /// </summary>
        public int card;

        public CardHistory(int player, int card)
        {
            this.player = player;
            this.card = card;
        }

        public override string ToString()
        {
            return $"Player {player} played {card}";
        }

        public bool Equals(CardHistory other)
        {
            return player == other.player && card == other.card;
        }

        public override bool Equals(object obj)
        {
            return obj is CardHistory other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(player, card);
        }

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref player);
            serializer.SerializeValue(ref card);
        }
    }
}