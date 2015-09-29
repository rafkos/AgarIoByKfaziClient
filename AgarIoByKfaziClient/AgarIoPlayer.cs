using System;
using System.Linq;
using System.Threading;
using AgarIoByKfaziClient.Contract;

namespace AgarIoByKfaziClient
{
    internal class AgarIoPlayer
    {
        private readonly AgarIoServerGateway _serverGateway;
        private readonly string _playerName;

        public AgarIoPlayer(AgarIoServerGateway serverGateway, string playerName)
        {
            _serverGateway = serverGateway;
            _playerName = playerName;
        }

        public void GameLoop()
        {
            var random = new Random();
            while (true)
            {
                var getViewResponseDto = _serverGateway.GetView();
                if (getViewResponseDto.ErrorCode == CommandErrorCode.NotJoined)
                {
                    _serverGateway.JoinPlayer();
                    continue;
                }

                var myBlob = getViewResponseDto.Blobs.FirstOrDefault(x => x.Name == _playerName);

                double destinationX = random.Next(-255, 256);
                double destinationY = random.Next(-255, 256);
                
                var closestFood = getViewResponseDto.Blobs.Where(x => x.Type == BlobType.Food).OrderBy(
                        x => Math.Sqrt(Math.Pow(x.Position.X - myBlob.Position.X, 2) + Math.Pow(x.Position.Y - myBlob.Position.Y, 2)))
                        .FirstOrDefault();

                if (closestFood != null)
                {
                    destinationX = closestFood.Position.X - myBlob.Position.X;
                    destinationY = closestFood.Position.Y - myBlob.Position.Y;
                }
                
                _serverGateway.Move(destinationX * 1000, destinationY * 1000);
            }
        }
    }
}