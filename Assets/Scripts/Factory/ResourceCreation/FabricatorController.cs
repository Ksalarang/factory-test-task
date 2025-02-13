using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Factory.Configs;
using Factory.ResourceTransport;
using Random = UnityEngine.Random;

namespace Factory.ResourceCreation
{
    public class FabricatorController : IDisposable
    {
        private readonly ResourcePool _resourcePool;
        private readonly ITransportBelt _transportBelt;
        private readonly FabricatorConfig _config;
        private readonly CancellationTokenSource _tokenSource = new();

        public FabricatorController(ResourcePool resourcePool, ITransportBelt transportBelt,
            FabricatorConfig fabricatorConfig)
        {
            _resourcePool = resourcePool;
            _transportBelt = transportBelt;
            _config = fabricatorConfig;
        }

        public void Start()
        {
            StartAsync(_tokenSource.Token).Forget();
        }

        public void Dispose()
        {
            _tokenSource.Cancel();
            _tokenSource.Dispose();
        }

        private async UniTask StartAsync(CancellationToken token)
        {
            while (token.IsCancellationRequested == false)
            {
                var resourceTypes = Enum.GetValues(typeof(ResourceType));
                var index = Random.Range(0, resourceTypes.Length);
                var resource = _resourcePool.Get((ResourceType) resourceTypes.GetValue(index));

                _transportBelt.PlaceResourceAt(resource, index);

                await UniTask.Delay(TimeSpan.FromSeconds(_config.FabricationInterval), cancellationToken: token);
            }
        }
    }
}