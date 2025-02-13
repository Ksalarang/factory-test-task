using System.Collections.Generic;
using System.Linq;
using Factory.Configs;
using UnityEngine;
using UnityEngine.Pool;

namespace Factory.ResourceCreation
{
    public class ResourcePool
    {
        private readonly List<Resource> _resourcePrefabs;
        private readonly ResourceConfigBundle _resourceConfigBundle;
        private readonly Transform _resourceParent;

        private readonly Dictionary<ResourceType, ObjectPool<Resource>> _pools = new();

        private int _resourceIndex;

        public ResourcePool(List<Resource> resourcePrefabs, ResourceConfigBundle resourceConfigBundle,
            Transform resourceParent)
        {
            _resourcePrefabs = resourcePrefabs;
            _resourceConfigBundle = resourceConfigBundle;
            _resourceParent = resourceParent;

            _pools.Add(ResourceType.MetalRod,
                new ObjectPool<Resource>(() => CreateResource(ResourceType.MetalRod),
                    OnGet, OnRelease, OnDestroy, false));

            _pools.Add(ResourceType.StoneBrick,
                new ObjectPool<Resource>(() => CreateResource(ResourceType.StoneBrick),
                    OnGet, OnRelease, OnDestroy, false));

            _pools.Add(ResourceType.WoodenPlank,
                new ObjectPool<Resource>(() => CreateResource(ResourceType.WoodenPlank),
                    OnGet, OnRelease, OnDestroy, false));
        }

        public Resource Get(ResourceType type)
        {
            return _pools[type].Get();
        }

        public void Release(Resource resource)
        {
            _pools[resource.Type].Release(resource);
        }

        private Resource CreateResource(ResourceType type)
        {
            var prefab = _resourcePrefabs.First(r => r.Type == type);
            var resource = Object.Instantiate(prefab, _resourceParent);
            resource.name = $"{resource.Type} {_resourceIndex++}";
            return resource;
        }

        private void OnGet(Resource resource)
        {
            var config = _resourceConfigBundle.GetConfig(resource.Type);
            resource.transform.rotation = Quaternion.Euler(config.InitialRotation);
            resource.gameObject.SetActive(true);
        }

        private void OnRelease(Resource resource)
        {
            resource.transform.position = resource.transform.localPosition = Vector3.zero;
            resource.transform.parent = _resourceParent;
            resource.Movement.Reset();
            resource.gameObject.SetActive(false);
        }

        private void OnDestroy(Resource resource)
        {
            Object.Destroy(resource.gameObject);
        }
    }
}