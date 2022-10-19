using System;
using CommandTerminal;
using Game;
using Managers;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Debugging
{
    public static class Commands
    {
        [RegisterCommand(Help = "Spawns card", MaxArgCount = 1, MinArgCount = 1, Name = "SpawnCard")]
        private static void SpawnCard(CommandArg[] args)
        {
            var type = args[0].Int;
            if (Terminal.IssuedError) return;

            try
            {
                var cardType = CardTypeList.Instance.Get(type);
                var player = Object.FindObjectOfType<PlayerManager>();
                player.SpawnCard(cardType);
                Debug.Log("Spawned card of type: " + cardType.name);
            }
            catch (ArgumentOutOfRangeException)
            {
                Debug.LogWarning("Card type not found");
            }
        }
    }
}