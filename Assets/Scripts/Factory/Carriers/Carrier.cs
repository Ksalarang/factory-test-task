﻿using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Factory.ResourceCreation;
using Factory.ResourcePickUp;
using UnityEngine;

namespace Factory.Carriers
{
    public class Carrier : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody _rigidbody;

        [SerializeField]
        private PickUpArea[] _pickUpAreas;

        [SerializeField]
        private ResourceContainer[] _containers;

        [SerializeField]
        private float _moveSpeed;

        [SerializeField]
        private float _resourceOffset;

        [SerializeField]
        private float _containerOffset;

        [SerializeField]
        private Vector3 _doorPosition;

        [SerializeField]
        private Vector3 _waitPosition;

        [SerializeField]
        private Vector3 _resourceLocalPosition;

        [SerializeField]
        private float _turnDuration;

        private ResourcePool _resourcePool;

        private void Start()
        {
            StartAsync(gameObject.GetCancellationTokenOnDestroy()).Forget();
        }

        public void SetResourcePool(ResourcePool pool)
        {
            _resourcePool = pool;
        }

        private async UniTask StartAsync(CancellationToken token)
        {
            while (token.IsCancellationRequested == false)
            {
                await UniTask.WaitUntil(IsResourceAvailable, cancellationToken: token);

                var resource = GetNearestResource();
                await MoveToTargetAsync(resource.transform.position, _resourceOffset, Ease.OutSine, token);

                _pickUpAreas.First(area => area.Type == resource.Type).Resources.Remove(resource);
                resource.transform.parent = transform;
                resource.transform.localPosition = _resourceLocalPosition;

                await MoveToTargetAsync(_doorPosition, 0, Ease.InSine, token);

                var container = _containers.First(container => container.Type == resource.Type);
                await MoveToTargetAsync(container.transform.position, _containerOffset, Ease.OutSine, token);

                _resourcePool.Release(resource);

                await MoveToTargetAsync(_doorPosition, 0, Ease.InSine, token);

                if (IsResourceAvailable() == false)
                {
                    await MoveToTargetAsync(_waitPosition, 0, Ease.OutSine, token);
                }
            }
        }

        private bool IsResourceAvailable()
        {
            return _pickUpAreas.Any(area => area.Resources.Count > 0);
        }

        private Resource GetNearestResource()
        {
            var position = transform.position;
            var minDistance = float.MaxValue;
            Resource nearest = null;

            foreach (var area in _pickUpAreas)
            {
                foreach (var resource in area.Resources)
                {
                    var distance = Vector3.Distance(position, resource.transform.position);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        nearest = resource;
                    }
                }
            }

            return nearest;
        }

        private async UniTask MoveToTargetAsync(Vector3 targetPosition, float offset, Ease ease,
            CancellationToken token)
        {
            var position = transform.position;
            var destination = targetPosition - (position - targetPosition).normalized * offset;
            destination.y = position.y;
            var angleY = Mathf.Atan2(destination.x - position.x, destination.z - position.z) * Mathf.Rad2Deg;
            var duration = Vector3.Distance(position, destination) / _moveSpeed;

            _rigidbody.DORotate(new Vector3(0, angleY, 0), _turnDuration).WithCancellation(token).Forget();
            await _rigidbody.DOMove(destination, duration).SetEase(ease).WithCancellation(token);
        }
    }
}