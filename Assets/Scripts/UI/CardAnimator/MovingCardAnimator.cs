using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Game;
using UnityEngine;

namespace UI.CardAnimator
{
    [CreateAssetMenu(fileName = "Moving Card Animator", menuName = "Card Animators/New Moving Card Animator")]
    public class MovingCardAnimator : DefaultCardAnimator
    {
        [SerializeField] private float startXOffset;
        [SerializeField] private float endXOffset;

        [SerializeField] private float startYOffset;
        [SerializeField] private float endYOffset;

        [SerializeField] private float animationMoveDuration;
        [SerializeField] private Ease ease = Ease.InOutCubic;

        public override IEnumerator MoveCards(Vector3[] newPositions, Card[] cards, Card[] existingCards, int count, Vector3 centerPosition)
        {
            var newCards = cards.Except(existingCards).ToArray();
            var newCardsLength = newCards.Length;
            var moves = new Dictionary<int, TweenerCore<Vector3, Vector3, VectorOptions>>(newCardsLength);
            var cardsList = cards.ToList();
            
            for (var i = 0; i < newCardsLength; i++)
            {
                var card = newCards[i];
                card.RectTransform.position = centerPosition + new Vector3(startXOffset, startYOffset);
                moves[cardsList.IndexOf(card)] = SetTweener(card, card.RectTransform
                    .DOMove(centerPosition + new Vector3(endXOffset, endYOffset), animationMoveDuration)
                    .SetEase(ease));
            }

            for (var i = 0; i < count; i++)
            {
                if (moves.ContainsKey(i))
                {
                    moves[i].Kill();
                }

                var card = cards[i];
                SetTweener(card, card.RectTransform.DOMove(newPositions[i], moveDuration)
                    .SetEase(Ease.OutCubic));
                yield return new WaitForSeconds(intervalBetweenCards);
            }
        }
    }
}