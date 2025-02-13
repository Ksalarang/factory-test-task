using Factory.ResourceCreation;

namespace Factory.ResourceTransport
{
    public interface ITransportBelt
    {
        Resource CurrentResource { get; }

        void RemoveCurrentResource();

        void DiscardCurrentResource();
    }
}