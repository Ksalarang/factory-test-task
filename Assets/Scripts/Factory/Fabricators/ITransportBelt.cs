namespace Factory.Fabricators
{
    public interface ITransportBelt
    {
        Resource CurrentResource { get; }

        void RemoveCurrentResource();

        void DiscardCurrentResource();
    }
}