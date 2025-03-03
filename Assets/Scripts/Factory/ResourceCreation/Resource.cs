﻿using UnityEngine;

namespace Factory.ResourceCreation
{
    public class Resource : MonoBehaviour
    {
        [field: SerializeField]
        public ResourceType Type { get; private set; }

        [field: SerializeField]
        public ResourceMovement Movement { get; private set; }

        [field: SerializeField]
        public float YOffset { get; private set; }
    }
}