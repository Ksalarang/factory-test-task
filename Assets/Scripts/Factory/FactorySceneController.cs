using System.Linq;
using Factory.Configs;
using Factory.ResourceControl;
using Factory.ResourceCreation;
using Factory.ResourcePickUp;
using Factory.ResourceTransport;
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

        [SerializeField]
        private PickUpArea[] _pickUpAreas;

        [Header("Configs")]
        [SerializeField]
        private TransportBeltConfig _transportBeltConfig;

        [SerializeField]
        private FabricatorConfig _fabricatorConfig;

        private TransportBelt _transportBelt;
        private FabricatorController _fabricatorController;
        private ResourcePanelController _resourcePanelController;
        private ResourcePickUpHandler _pickUpHandler;

        private void Start()
        {
            _transportBelt = new TransportBelt(_transportBeltConfig);
            _fabricatorController = new FabricatorController(_fabricators.ToList(), _transportBelt, _fabricatorConfig);
            _pickUpHandler = new ResourcePickUpHandler(_pickUpAreas.ToList());
            _resourcePanelController = new ResourcePanelController(_resourceButtons.ToList(), _transportBelt,
                _pickUpHandler);

            _fabricatorController.Start();
        }

        private void OnDestroy()
        {
            _transportBelt.Dispose();
            _fabricatorController.Dispose();
            _resourcePanelController.Dispose();
            _pickUpHandler.Dispose();
        }
    }
}