using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using KevinCastejon.MoreAttributes;
using Managers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game
{
    public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [NonSerialized] public RectTransform RectTransform;
        [NonSerialized] public TweenerCore<Vector3, Vector3, VectorOptions> Tweener;
        
        [ReadOnly] public CardType cardType;
        [ReadOnly] public CardState cardState = CardState.Idle;
        
        private PlayerManager _playerManager;
        private Canvas _canvas;
        
        private bool _dragging;
        private Vector3 _initialDragPos;
        private Camera _camera;

        private Image _image;

        private void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
            _playerManager = FindObjectOfType<PlayerManager>();
            _canvas = GetComponent<Canvas>();
            _camera = Camera.main;
            _image = GetComponentInChildren<Image>();
        }

        private void Update()
        {
            if (_dragging)
            {
                var pos = _camera.ScreenToWorldPoint(Input.mousePosition);
                pos.Set(pos.x, pos.y, 0);
                transform.position = pos;
            }

            if (cardState == CardState.InvalidPlay)
            {
                // TODO
                cardState = CardState.Idle;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (IsTweening()) return;
            RectTransform.DOScale(1.2f, 0.1f);
            _canvas.overrideSorting = true;
            _canvas.sortingOrder = 10;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (IsTweening()) return;
            if (_dragging) return;
            RectTransform.DOScale(1f, 0.1f);
            _canvas.sortingOrder = 0;
            _canvas.overrideSorting = false;
        }

        public void OnBeginDrag(BaseEventData eventData)
        {
            if (IsTweening()) return;
            _playerManager.DraggingCard = this;
            _dragging = true;
            _initialDragPos = RectTransform.position;
            
            _canvas.overrideSorting = true;
            _canvas.sortingOrder = 10;
            RectTransform.DOScale(1.2f, 0.1f);

            _image.raycastTarget = false;
        }
        
        public void OnEndDrag(BaseEventData eventData)
        {
            if (cardState == CardState.Played)
            {
                _playerManager.DraggingCard = null;
                _dragging = false;
                Destroy(gameObject);
                return;
            }
            
            if (IsTweening()) return;
            _playerManager.DraggingCard = null;
            _dragging = false;
            Tweener = RectTransform.DOMove(_initialDragPos, 0.5f);
            
            _canvas.sortingOrder = 0;
            _canvas.overrideSorting = false;
            RectTransform.DOScale(1f, 0.1f);
            
            _image.raycastTarget = true;
        }

        private bool IsTweening()
        {
            if (Tweener is null) return false;
            if (Tweener.IsActive() && !Tweener.IsComplete()) return true;
            Tweener = null;
            return false;
        }
    }
}