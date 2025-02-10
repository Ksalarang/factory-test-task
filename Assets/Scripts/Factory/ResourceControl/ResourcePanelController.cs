using System;
using System.Collections.Generic;
using Factory.Fabricators;
using Factory.ResourcePickUp;

namespace Factory.ResourceControl
{
    public class ResourcePanelController : IDisposable
    {
        private readonly List<ResourceButton> _buttons;
        private readonly ITransportBelt _transportBelt;
        private readonly IResourcePickUpArea _pickUpArea;

        public ResourcePanelController(List<ResourceButton> buttons, ITransportBelt transportBelt,
            IResourcePickUpArea pickUpArea)
        {
            _buttons = buttons;
            _transportBelt = transportBelt;
            _pickUpArea = pickUpArea;

            foreach (var button in buttons)
            {
                button.Pressed += OnButtonPressed;
            }
        }

        public void Dispose()
        {
            foreach (var button in _buttons)
            {
                button.Pressed -= OnButtonPressed;
            }
        }

        private void OnButtonPressed(ResourceButton button)
        {
            var currentResource = _transportBelt.CurrentResource;

            if (currentResource == null)
            {
                return;
            }

            if (currentResource.Type == button.Type)
            {
                _transportBelt.RemoveCurrentResource();
                _pickUpArea.PlaceResource(currentResource);
            }
            else
            {
                _transportBelt.DiscardCurrentResource();
            }
        }
    }
}