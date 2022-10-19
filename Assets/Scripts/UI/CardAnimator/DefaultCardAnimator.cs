using System.Collections;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Game;
using UnityEngine;

namespace UI.CardAnimator
{
    [CreateAssetMenu(fileName = "Default Card Animator", menuName = "Card Animators/New Default Card Animator")]
    public class DefaultCardAnimator : ScriptableObject
    {
        [SerializeField] protected float moveDuration = 1.0f;
        [SerializeField] protected float intervalBetweenCards = 0.2f;

        public virtual IEnumerator MoveCards(Vector3[] newPositions, Card[] cards, Card[] existingCards, int count, Vector3 centerPosition)
        {
            for (var i = 0; i < count; i++)
            {
                var card = cards[i];
                SetTweener(card, card.RectTransform.DOMove(newPositions[i], moveDuration)
                    .SetEase(Ease.OutCubic));
                yield return new WaitForSeconds(intervalBetweenCards);
            }
        }

        protected TweenerCore<Vector3, Vector3, VectorOptions> SetTweener(Card card, TweenerCore<Vector3, Vector3, VectorOptions> tweenerCore)
        {
            card.Tweener = tweenerCore;
            return tweenerCore;
        }
    }
}