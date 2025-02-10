using Factory.Fabricators;
using UnityEngine;

namespace Factory.Carriers
{
    public class ResourceContainer : MonoBehaviour
    {
        [field: SerializeField]
        public ResourceType Type { get; private set; }
    }
}