using System.Linq;
using Factory.Configs;
using Factory.Fabricators;
using Factory.ResourceControl;
using Factory.ResourcePickUp;
using UnityEngine;

namespace Factory
{
    public class FactorySceneController : MonoBehaviour
    {
        [Header("Objects")]
        [SerializeField]
        private Fabricator[] _fabricators;

        [SerializeField]
        private ResourceButton[] _resourceButtons;

        [Header("Configs")]
        [SerializeField]
        private TransportBeltConfig _transportBeltConfig;

        [SerializeField]
        private FabricatorConfig _fabricatorConfig;

        private TransportBelt _transportBelt;
        private FabricatorController _fabricatorController;
        private ResourcePanelController _resourcePanelController;
        private ResourcePickUpArea _pickUpArea;

        private void Start()
        {
            _transportBelt = new TransportBelt(_transportBeltConfig);
            _fabricatorController = new FabricatorController(_fabricators.ToList(), _transportBelt, _fabricatorConfig);
            _pickUpArea = new ResourcePickUpArea();
            _resourcePanelController = new ResourcePanelController(_resourceButtons.ToList(), _transportBelt,
                _pickUpArea);

            _fabricatorController.Start();
        }

        private void OnDestroy()
        {
            _transportBelt.Dispose();
            _fabricatorController.Dispose();
            _resourcePanelController.Dispose();
        }
    }
}