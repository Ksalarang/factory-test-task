using System.Linq;
using Factory.Fabricators;
using UnityEngine;

namespace Factory
{
    public class FactorySceneController : MonoBehaviour
    {
        [SerializeField]
        private Fabricator[] _fabricators;

        private void Start()
        {
            var fabricatorController = new FabricatorController(_fabricators.ToList());
            fabricatorController.Start();
        }
    }
}