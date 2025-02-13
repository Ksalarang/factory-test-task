using System.Linq;
using Factory.Carriers;
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
        [Header("Scene objects")]
        [SerializeField]
        private ResourceButton[] _resourceButtons;

        [SerializeField]
        private PickUpArea[] _pickUpAreas;

        [SerializeField]
        private Transform _resourceParent;

        [SerializeField]
        private Carrier _carrier;

        [Header("Prefabs")]
        [SerializeField]
        private Resource[] _resourcePrefabs;
        
        [Header("Configs")]
        [SerializeField]
        private TransportBeltConfig _transportBeltConfig;

        [SerializeField]
        private FabricatorConfig _fabricatorConfig;

        [SerializeField]
        private ResourceConfigBundle _resourceConfigBundle;

        private TransportBelt _transportBelt;
        private FabricatorController _fabricatorController;
        private ResourcePanelController _resourcePanelController;
        private ResourcePickUpHandler _pickUpHandler;

        private void Start()
        {
            var resourcePool = new ResourcePool(_resourcePrefabs.ToList(), _resourceConfigBundle, _resourceParent);
            _transportBelt = new TransportBelt(_transportBeltConfig, resourcePool);
            _fabricatorController = new FabricatorController(resourcePool, _transportBelt, _fabricatorConfig);
            _pickUpHandler = new ResourcePickUpHandler(_pickUpAreas.ToList());
            _resourcePanelController = new ResourcePanelController(_resourceButtons.ToList(), _transportBelt,
                _pickUpHandler);

            _carrier.SetResourcePool(resourcePool);

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