using UnityEngine;

namespace Factory.Fabricators
{
    public class Fabricator : MonoBehaviour
    {
        [SerializeField]
        private GameObject _resourcePrefab;

        [SerializeField]
        private Transform _resourcesParent;

        public Resource FabricateResource()
        {
            return Instantiate(_resourcePrefab, _resourcesParent).GetComponent<Resource>();
        }
    }
}