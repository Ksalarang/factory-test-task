using System.Collections.Generic;
using Factory.Fabricators;
using UnityEngine;

namespace Factory.ResourcePickUp
{
    public class PickUpArea : MonoBehaviour
    {
        [field: SerializeField]
        public ResourceType Type { get; private set; }

        public List<Resource> Resources { get; private set; } = new();

        public Vector3 Size => transform.localScale * 10;
    }
}