using System;
using System.Linq;
using Game;
using UI.CardAnimator;
using UnityEngine;

namespace UI
{
    public class CardLayout : MonoBehaviour
    {
        [SerializeField] private GameObject empty;
        [SerializeField] private float spacing;
        [SerializeField] private DefaultCardAnimator cardAnimator;

        private Card[] _previousCards = Array.Empty<Card>();
        
        private void Start()
        {
            Align();
        }

        public void Align()
        {
            foreach (var obj in transform)
            {
                var o = ((Transform)obj).gameObject;
                if (!o.CompareTag("EmptyCardGroup")) continue;

                var groupChildrenCount = o.transform.childCount;
                if (groupChildrenCount > 0)
                {
                    var list = o.transform.Cast<Transform>().ToList();
                    foreach (var child in list)
                    {
                        child.SetParent(transform, false);
                    }
                }
                
                Destroy(o);
            }

            var children = transform.GetComponentsInChildren<Card>();
            var childrenCount = children.Length;
            if (childrenCount <= 0) return;

            var center = transform.position;
            var totalWidth = 0f;

            for (var i = 0; i < childrenCount; i++)
            {
                var child = children[i];
                totalWidth += child.RectTransform.rect.width;
                totalWidth += spacing;
            }

            totalWidth -= spacing;

            var group = Instantiate(empty, transform);
            var rectTransform = group.GetComponent<RectTransform>();
            var defaultChildRect = children[0].GetComponent<RectTransform>().rect;
            rectTransform.sizeDelta = new Vector2(totalWidth, defaultChildRect.height);
            rectTransform.position = center;

            var x = 0f;
            var left = rectTransform.position.x - totalWidth * 0.5f + defaultChildRect.width * 0.5f;

            var newPositions = new Vector3[childrenCount];

            for (var i = 0; i < childrenCount; i++)
            {
                var child = children[i];
                child.transform.SetParent(group.transform, false);
                var childRectTransform = child.RectTransform;
                var previousLocation = childRectTransform.localPosition;
                childRectTransform.localPosition = new Vector3(left + x, 0);
                newPositions[i] = childRectTransform.position;
                childRectTransform.localPosition = previousLocation;

                x += childRectTransform.rect.width;
                x += spacing;
            }

            StopAllCoroutines();
            StartCoroutine(cardAnimator.MoveCards(newPositions, children, _previousCards, childrenCount, center));

            _previousCards = children;
        }
    }
}