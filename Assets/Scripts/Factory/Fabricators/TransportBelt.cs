using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Factory.Configs;
using UnityEngine;

namespace Factory.Fabricators
{
    public class TransportBelt : IDisposable
    {
        private readonly TransportBeltConfig _config;
        private readonly CancellationTokenSource _tokenSource = new();

        public TransportBelt(TransportBeltConfig config)
        {
            _config = config;
        }

        public void PlaceResourceAt(Resource resource, int pathIndex)
        {
            var pathConfig = _config.PathConfigs[pathIndex];
            var points = pathConfig.PathPoints;

            MoveResourceAsync(resource, points, _tokenSource.Token).Forget();
        }

        public void Dispose()
        {
            _tokenSource.Cancel();
            _tokenSource.Dispose();
        }

        private async UniTask MoveResourceAsync(Resource resource, Vector3[] points, CancellationToken token)
        {
            for (var i = 0; i < points.Length && token.IsCancellationRequested == false; i++)
            {
                if (i + 1 == points.Length)
                {
                    break;
                }

                var point1 = points[i];
                var point2 = points[i + 1];
                resource.transform.position = point1;
                var duration = Vector3.Distance(point1, point2) / _config.MoveSpeed;

                await resource.transform.DOMove(point2, duration).SetEase(Ease.Linear).WithCancellation(token);
            }
        }
    }
}