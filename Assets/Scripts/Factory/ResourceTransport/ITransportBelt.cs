using Factory.ResourceCreation;

namespace Factory.ResourceTransport
{
    public interface ITransportBelt
    {
        Resource CurrentResource { get; }

        void PlaceResourceAt(Resource resource, int pathIndex);

        void RemoveCurrentResource();

        void DiscardCurrentResource();
    }
}