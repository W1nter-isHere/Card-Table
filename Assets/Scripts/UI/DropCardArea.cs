using System.Collections;
using DG.Tweening;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class DropCardArea : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
    {
        private TableTopManager _tableTopManager;
        private PlayerManager _playerManager;

        private TextMeshProUGUI _text;
        private CanvasGroup _canvasGroup;

        private void Start()
        {
            _tableTopManager = FindObjectOfType<TableTopManager>();
            _playerManager = FindObjectOfType<PlayerManager>();
            _text = GetComponentInChildren<TextMeshProUGUI>();
            _text.gameObject.SetActive(false);
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = 0;
        }

        public void OnDrop(PointerEventData eventData)
        {
            var draggingCard = _playerManager.DraggingCard;
            if (draggingCard is not null)
            {
                _tableTopManager.PlayCard(draggingCard);
            }
            Disable();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_playerManager.DraggingCard is not null)
            {
                Enable();
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Disable();
        }

        private void Enable()
        {
            _text.gameObject.SetActive(true);
            _canvasGroup.DOFade(1, 0.1f);
        }

        private void Disable()
        {
            StartCoroutine(DisableCoroutine());
        }

        private IEnumerator DisableCoroutine()
        {
            _canvasGroup.DOFade(0, 0.1f);
            yield return new WaitForSeconds(0.1f);
            _text.gameObject.SetActive(false);
        }
    }
}