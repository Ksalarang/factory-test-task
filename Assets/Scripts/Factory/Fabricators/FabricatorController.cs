using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Factory.Configs;
using Random = UnityEngine.Random;

namespace Factory.Fabricators
{
    public class FabricatorController : IDisposable
    {
        private readonly List<Fabricator> _fabricators;
        private readonly TransportBelt _transportBelt;
        private readonly FabricatorConfig _config;
        private readonly CancellationTokenSource _tokenSource = new();

        public FabricatorController(List<Fabricator> fabricators, TransportBelt transportBelt,
            FabricatorConfig fabricatorConfig)
        {
            _fabricators = fabricators;
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
                var index = Random.Range(0, _fabricators.Count);
                var resource = _fabricators[index].FabricateResource();
                _transportBelt.PlaceResourceAt(resource, index);

                await UniTask.Delay(TimeSpan.FromSeconds(_config.FabricationInterval), cancellationToken: token);
            }
        }
    }
}