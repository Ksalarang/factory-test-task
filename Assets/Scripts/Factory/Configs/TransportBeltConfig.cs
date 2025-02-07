using UnityEngine;

namespace Factory.Configs
{
    [CreateAssetMenu(fileName = "TransportBeltConfig", menuName = "Configs/TransportBeltConfig", order = 0)]
    public class TransportBeltConfig : ScriptableObject
    {
        [field: SerializeField]
        public TransportBeltPathConfig[] PathConfigs { get; private set; }

        [field: SerializeField]
        public float MoveSpeed { get; private set; }

        [field: SerializeField]
        public float DisappearanceDelay { get; private set; }

        [field: SerializeField]
        public float CollisionDelay { get; private set; }
    }
}