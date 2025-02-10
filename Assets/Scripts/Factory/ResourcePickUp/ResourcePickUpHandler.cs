using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Factory.Fabricators;
using UnityEngine;

namespace Factory.ResourcePickUp
{
    public class ResourcePickUpHandler : IResourcePickUpHandler, IDisposable
    {
        private readonly List<PickUpArea> _pickUpAreas;
        private readonly CancellationTokenSource _tokenSource = new();

        public ResourcePickUpHandler(List<PickUpArea> pickUpAreas)
        {
            _pickUpAreas = pickUpAreas;
        }

        public void Dispose()
        {
            _tokenSource.Cancel();
            _tokenSource.Dispose();
        }

        public void PlaceResource(Resource resource)
        {
            PlaceResourceAsync(resource, _tokenSource.Token).Forget();
        }

        private async UniTask PlaceResourceAsync(Resource resource, CancellationToken token)
        {
            var area = _pickUpAreas.First(area => area.Type == resource.Type);
            var newPosition = CalculateResourcePosition(resource, area);
            var duration = 0.5f;

            area.Resources.Add(resource);

            var rotation = resource.transform.eulerAngles;
            rotation.y -= 90;
            var newRotation = rotation + area.transform.eulerAngles;
            resource.transform.DORotate(newRotation, duration).WithCancellation(token).Forget();

            await resource.transform.DOMove(newPosition, duration).WithCancellation(token);
        }

        private Vector3 CalculateResourcePosition(Resource resource, PickUpArea area)
        {
            var center = area.transform.position;
            var areaSize = area.Size;
            var lastResource = area.Resources.LastOrDefault();
            var edgeOffset = 0.2f;
            var resourceOffset = 0.1f;
            var resourceSize = GetResourceSize(resource);
            var newPosition = Vector3.zero;
            newPosition.y = resource.YOffset;

            if (lastResource == null)
            {
                newPosition.x = center.x - areaSize.x / 2 + resourceSize.x / 2 + edgeOffset;
                newPosition.z = center.z + areaSize.z / 2 - resourceSize.z / 2 - edgeOffset;
            }
            else
            {
                var lastResourcePosition = lastResource.transform.position;
                newPosition.x = lastResourcePosition.x + resourceSize.x + resourceOffset;
                newPosition.z = lastResourcePosition.z;

                if (newPosition.x + resourceSize.x / 2 > center.x + areaSize.x / 2)
                {
                    newPosition.x = center.x - areaSize.x / 2 + resourceSize.x / 2 + edgeOffset;
                    newPosition.z = lastResourcePosition.z - resourceSize.z - resourceOffset;
                }
            }

            return newPosition;
        }

        private Vector3 GetResourceSize(Resource resource)
        {
            if (resource.Type == ResourceType.MetalRod)
            {
                var scale = resource.transform.localScale;
                scale.x = scale.z;
                scale.z = scale.y * 2;
                return scale;
            }
            return resource.transform.localScale;
        }
    }
}