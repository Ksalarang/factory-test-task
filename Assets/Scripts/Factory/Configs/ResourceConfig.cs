using Factory.ResourceCreation;
using UnityEngine;

namespace Factory.Configs
{
    [CreateAssetMenu(fileName = "ResourceConfig", menuName = "Configs/ResourceConfig", order = 0)]
    public class ResourceConfig : ScriptableObject
    {
        [field: SerializeField]
        public ResourceType Type { get; private set; }

        [field: SerializeField]
        public Vector3 InitialRotation { get; private set; }
    }
}