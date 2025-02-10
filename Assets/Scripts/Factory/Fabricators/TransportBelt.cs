using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Factory.Configs;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Factory.Fabricators
{
    public class TransportBelt : ITransportBelt, IDisposable
    {
        public Resource CurrentResource { get; private set; }

        private readonly TransportBeltConfig _config;
        private readonly CancellationTokenSource _tokenSource = new();

        private CancellationTokenSource _currentResourceTokenSource;

        public TransportBelt(TransportBeltConfig config)
        {
            _config = config;
        }

        public void Dispose()
        {
            _tokenSource.Cancel();
            _tokenSource.Dispose();
            CancelCurrentResourceToken();
        }

        public void PlaceResourceAt(Resource resource, int pathIndex)
        {
            var pathConfig = _config.PathConfigs[pathIndex];
            var points = pathConfig.PathPoints;

            MoveResourceAsync(resource, points, _tokenSource.Token).Forget();
        }

        public void RemoveCurrentResource()
        {
            CancelCurrentResourceToken();

            CurrentResource = null;
        }

        public void DiscardCurrentResource()
        {
            CancelCurrentResourceToken();

            DiscardCurrentResourceAsync(CurrentResource, _tokenSource.Token).Forget();
        }

        private async UniTask MoveResourceAsync(Resource resource, Vector3[] points, CancellationToken token)
        {
            resource.OnResourceCollision += OnResourceCollision;

            for (var i = 0; i < points.Length && token.IsCancellationRequested == false; i++)
            {
                if (i + 1 == points.Length)
                {
                    break;
                }

                var point1 = points[i];
                point1.y += resource.YOffset;
                var point2 = points[i + 1];
                point2.y += resource.YOffset;
                resource.transform.position = point1;
                var duration = Vector3.Distance(point1, point2) / _config.MoveSpeed;

                await resource.transform.DOMove(point2, duration).SetEase(Ease.Linear).WithCancellation(token);
            }

            CurrentResource = resource;

            await UniTask.Delay(TimeSpan.FromSeconds(_config.DisappearanceDelay),
                cancellationToken: GetCurrentResourceToken());

            CurrentResource = null;

            await DiscardCurrentResourceAsync(resource, token);
        }

        private async UniTask DiscardCurrentResourceAsync(Resource resource, CancellationToken token)
        {
            // moving the resource one more time before destroying for OnTriggerExit to be called for other resources.
            var position = resource.transform.position;
            position.y = -1;
            await resource.transform.DOMove(position, 0.1f).SetEase(Ease.Linear).WithCancellation(token);

            resource.OnResourceCollision -= OnResourceCollision;
            Object.Destroy(resource.gameObject);
        }

        private void OnResourceCollision(Resource resource1, Resource resource2)
        {
            var endPoint = _config.PathConfigs[0].PathPoints[3];
            var resource = Vector3.Distance(resource1.transform.position, endPoint) >
                Vector3.Distance(resource2.transform.position, endPoint) ? resource1 : resource2;

            resource.PauseMovementAsync(_config.CollisionDelay, endPoint, _tokenSource.Token).Forget();
        }

        private CancellationToken GetCurrentResourceToken()
        {
            CancelCurrentResourceToken();

            _currentResourceTokenSource = CancellationTokenSource.CreateLinkedTokenSource(_tokenSource.Token);
            return _currentResourceTokenSource.Token;
        }

        private void CancelCurrentResourceToken()
        {
            if (_currentResourceTokenSource is { IsCancellationRequested: false })
            {
                _currentResourceTokenSource.Cancel();
                _currentResourceTokenSource.Dispose();
            }
        }
    }
}