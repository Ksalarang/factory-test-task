using UnityEngine;

namespace Factory.Fabricators
{
    public class Fabricator : MonoBehaviour
    {
        [SerializeField]
        private GameObject _resourcePrefab;

        [SerializeField]
        private Transform _resourcesParent;

        private int _resourceIndex;

        public Resource FabricateResource()
        {
            var resource = Instantiate(_resourcePrefab, _resourcesParent).GetComponent<Resource>();
            resource.name = $"{resource.name} {_resourceIndex++}";
            var rotation = resource.transform.eulerAngles;
            rotation.y += 90;
            resource.transform.rotation = Quaternion.Euler(rotation);
            return resource;
        }
    }
}