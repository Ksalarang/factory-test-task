using Factory.ResourceCreation;

namespace Factory.ResourcePickUp
{
    public interface IResourcePickUpHandler
    {
        void PlaceResource(Resource resource);
    }
}