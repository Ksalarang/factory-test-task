using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Factory.Fabricators
{
    public class Resource : MonoBehaviour
    {
        public event Action<Resource, Resource> OnResourceCollision;

        public bool MovementPaused { get; private set; }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Resource otherResource))
            {
                OnResourceCollision?.Invoke(this, otherResource);
            }
        }

        private void OnDestroy()
        {
            OnResourceCollision = null;
        }

        public async UniTask PauseMovementAsync(CancellationToken token)
        {
            transform.DOPause();
            MovementPaused = true;

            await UniTask.Delay(TimeSpan.FromSeconds(2f), cancellationToken: token);

            transform.DOPlay();
            MovementPaused = false;
        }
    }
}