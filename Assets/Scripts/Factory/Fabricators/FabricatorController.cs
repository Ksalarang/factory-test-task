using System.Collections.Generic;

namespace Factory.Fabricators
{
    public class FabricatorController
    {
        private readonly List<Fabricator> _fabricators;

        public FabricatorController(List<Fabricator> fabricators)
        {
            _fabricators = fabricators;
        }

        public void Start()
        {
            _fabricators[0].FabricateResource();
        }
    }
}