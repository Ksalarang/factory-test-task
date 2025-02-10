using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Factory.Utils;
using UnityEngine;

namespace Factory.ResourceControls
{
    public class ResourceButton : MonoBehaviour
    {
        public event Action Pressed;

        [SerializeField]
        private Transform _button;

        [SerializeField]
        private ClickListener _clickListener;

        [SerializeField]
        private float _pressOffset;

        [SerializeField]
        private float _pressDuration;

        private bool _pressing;

        private void Start()
        {
            _clickListener.OnClick += OnClick;
        }

        private void OnDestroy()
        {
            _clickListener.OnClick -= OnClick;
        }

        private void OnClick()
        {
            if (_pressing)
            {
                return;
            }

            _pressing = true;
            PressAsync(gameObject.GetCancellationTokenOnDestroy()).Forget();
        }

        private async UniTask PressAsync(CancellationToken token)
        {
            var y = _button.localPosition.y;
            var duration = _pressDuration / 2f;

            await _button.DOLocalMoveY(y + _pressOffset, duration).WithCancellation(token);
            await _button.DOLocalMoveY(y, duration).WithCancellation(token);

            Pressed?.Invoke();
            _pressing = false;
        }
    }
}