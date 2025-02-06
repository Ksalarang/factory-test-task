using Factory.Configs;
using UnityEngine;

namespace Factory.Fabricators
{
    public class Fabricator : MonoBehaviour
    {
        [SerializeField]
        private ResourceConfig _resourceConfig;

        [SerializeField]
        private Transform _resourcesParent;

        public Resource FabricateResource()
        {
            var resource = Instantiate(_resourceConfig.Prefab, _resourcesParent).GetComponent<Resource>();
            resource.transform.position = transform.position + _resourceConfig.InitialPosition;
            return resource;
        }
    }
}