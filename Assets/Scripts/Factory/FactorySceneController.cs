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

        [SerializeField]
        private FabricatorConfig _fabricatorConfig;

        private TransportBelt _transportBelt;
        private FabricatorController _fabricatorController;

        private void Start()
        {
            _transportBelt = new TransportBelt(_transportBeltConfig);
            _fabricatorController = new FabricatorController(_fabricators.ToList(), _transportBelt,
                _fabricatorConfig);

            _fabricatorController.Start();
        }

        private void OnDestroy()
        {
            _transportBelt.Dispose();
            _fabricatorController.Dispose();
        }
    }
}