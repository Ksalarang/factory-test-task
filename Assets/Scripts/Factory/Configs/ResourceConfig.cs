using UnityEngine;

namespace Factory.Configs
{
    [CreateAssetMenu(fileName = "ResourceConfig", menuName = "Configs/ResourceConfig", order = 0)]
    public class ResourceConfig : ScriptableObject
    {
        [field: SerializeField]
        public GameObject Prefab { get; private set; }

        [field: SerializeField]
        public Vector3 InitialPosition { get; private set; }
    }
}