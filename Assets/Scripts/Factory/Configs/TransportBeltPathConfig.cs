using UnityEngine;

namespace Factory.Configs
{
    [CreateAssetMenu(fileName = "TransportBeltPathConfig", menuName = "Configs/TransportBeltPathConfig", order = 0)]
    public class TransportBeltPathConfig : ScriptableObject
    {
        [field: SerializeField]
        public Vector3[] PathPoints { get; private set; }
    }
}