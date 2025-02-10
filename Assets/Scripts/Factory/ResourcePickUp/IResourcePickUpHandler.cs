using Factory.Fabricators;

namespace Factory.ResourcePickUp
{
    public interface IResourcePickUpHandler
    {
        void PlaceResource(Resource resource);
    }
}