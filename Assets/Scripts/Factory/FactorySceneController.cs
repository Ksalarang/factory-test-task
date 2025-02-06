using System.Linq;
using Factory.Configs;
using Factory.Fabricators;
using UnityEngine;

namespace Factory
{
    public class FactorySceneController : MonoBehaviour
    {
        [Header("Objects")]
        [SerializeField]
        private Fabricator[] _fabricators;

        [Header("Configs")]
        [SerializeField]
        private TransportBeltConfig _transportBeltConfig;

        private TransportBelt _transportBelt;

        private void Start()
        {
            _transportBelt = new TransportBelt(_transportBeltConfig);
            var fabricatorController = new FabricatorController(_fabricators.ToList(), _transportBelt);

            fabricatorController.Start();
        }

        private void OnDestroy()
        {
            _transportBelt.Dispose();
        }
    }
}