using System.Linq;
using Factory.ResourceCreation;
using UnityEngine;

namespace Factory.Configs
{
    [CreateAssetMenu(fileName = "ResourceConfigBundle", menuName = "Configs/ResourceConfigBundle", order = 0)]
    public class ResourceConfigBundle : ScriptableObject
    {
        [field: SerializeField]
        public ResourceConfig[] ResourceConfigs { get; private set; }

        public ResourceConfig GetConfig(ResourceType type)
        {
            return ResourceConfigs.First(config => config.Type == type);
        }
    }
}