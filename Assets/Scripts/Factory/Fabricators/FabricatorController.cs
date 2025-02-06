using System.Collections.Generic;

namespace Factory.Fabricators
{
    public class FabricatorController
    {
        private readonly List<Fabricator> _fabricators;
        private readonly TransportBelt _transportBelt;

        public FabricatorController(List<Fabricator> fabricators, TransportBelt transportBelt)
        {
            _fabricators = fabricators;
            _transportBelt = transportBelt;
        }

        public void Start()
        {
            var resource = _fabricators[0].FabricateResource();
            _transportBelt.PlaceResourceAt(resource, 0);
        }
    }
}