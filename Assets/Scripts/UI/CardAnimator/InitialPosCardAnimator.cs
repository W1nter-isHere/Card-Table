using System.Collections;
using Game;
using UnityEngine;

namespace UI.CardAnimator
{
    [CreateAssetMenu(fileName = "Initial Pos Card Animator", menuName = "Card Animators/New Initial Pos Card Animator")]
    public class InitialPosCardAnimator : DefaultCardAnimator
    {
        [SerializeField] private float startXOffset = 0;
        [SerializeField] private float startYOffset = 3;

        public override IEnumerator MoveCards(Vector3[] newPositions, Card[] cards, Card[] existingCards, int count, Vector3 centerPos)
        {
            for (var i = 0; i < count; i++)
            {
                cards[i].RectTransform.position = centerPos + new Vector3(startXOffset, startYOffset);
            }

            yield return base.MoveCards(newPositions, cards, existingCards, count, centerPos);
        }
    }
}