using System;
using System.Collections.Generic;
using Factory.ResourceCreation;
using Factory.ResourcePickUp;
using Factory.ResourceTransport;

namespace Factory.ResourceControl
{
    public class ResourcePanelController : IDisposable
    {
        private readonly List<ResourceButton> _buttons;
        private readonly ITransportBelt _transportBelt;
        private readonly IResourcePickUpHandler _pickUpHandler;

        public ResourcePanelController(List<ResourceButton> buttons, ITransportBelt transportBelt,
            IResourcePickUpHandler pickUpHandler)
        {
            _buttons = buttons;
            _transportBelt = transportBelt;
            _pickUpHandler = pickUpHandler;

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
                _pickUpHandler.PlaceResource(currentResource);
            }
            else
            {
                _transportBelt.DiscardCurrentResource();
            }
        }
    }
}