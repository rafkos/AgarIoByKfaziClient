using System;
using System.Linq;
using System.Net.Sockets;
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
                GetViewResponseDto getViewResponseDto;
                try
                {
                    getViewResponseDto = _serverGateway.GetView();
                }
                catch (SocketException e)
                {
                    Console.WriteLine("Could not connet to server! Message: {0}", e.Message);
                    continue;
                }

                switch (getViewResponseDto.ErrorCode)
                {
                    case CommandErrorCode.NotJoined:
                        _serverGateway.JoinPlayer();
                        continue;
                    case CommandErrorCode.GameNotStarted:
                        continue;
                }

                var foods = getViewResponseDto.Blobs.Where(x => x.Type == BlobType.Food);
                var players = getViewResponseDto.Blobs.Where(x => x.Type == BlobType.Food).OrderBy(b => b.Id);
                var viruses = getViewResponseDto.Blobs.Where(x => x.Type == BlobType.Virus);
                var myBlobs = getViewResponseDto.Blobs.Where(x => x.Type == BlobType.Player).Where(x => x.Name == _playerName);

                double destinationX = random.Next(-255, 256);
                double destinationY = random.Next(-255, 256);
                double speed = 1000;

                _serverGateway.Move(destinationX * speed, destinationY * speed);
                //_serverGateway.SplitMass();
                //_serverGateway.EjectMass();
            }
        }
    }
}