using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Factory.ResourceCreation
{
    public class ResourceMovement : MonoBehaviour
    {
        public event Action<ResourceMovement, ResourceMovement> OnResourceCollision;

        private readonly List<ResourceMovement> _collidedResources = new();

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ResourceMovement otherResource))
            {
                _collidedResources.Add(otherResource);
                OnResourceCollision?.Invoke(this, otherResource);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out ResourceMovement otherResource))
            {
                _collidedResources.Remove(otherResource);
            }
        }

        public async UniTask PauseMovementAsync(float delay, Vector3 endPoint, CancellationToken token)
        {
            transform.DOPause();

            while (_collidedResources.Any(r => Vector3.Distance(r.transform.position, endPoint)
                < Vector3.Distance(transform.position, endPoint)))
            {
                await UniTask.Delay(TimeSpan.FromSeconds(delay), cancellationToken: token);
            }

            transform.DOPlay();
        }
    }
}