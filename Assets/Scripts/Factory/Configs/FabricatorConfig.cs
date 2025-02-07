using UnityEngine;

namespace Factory.Configs
{
    [CreateAssetMenu(fileName = "FabricatorConfig", menuName = "Configs/FabricatorConfig", order = 0)]
    public class FabricatorConfig : ScriptableObject
    {
        [field: SerializeField]
        public float FabricationInterval { get; private set; }
    }
}