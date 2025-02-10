using DG.Tweening;
using Factory.Fabricators;

namespace Factory.ResourcePickUp
{
    public class ResourcePickUpArea : IResourcePickUpArea
    {
        public void PlaceResource(Resource resource)
        {
            var position = resource.transform.position;
            position.x += 2;
            resource.transform.DOMove(position, 0.5f);
        }
    }
}