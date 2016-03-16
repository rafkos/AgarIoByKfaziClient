using System;
using System.IO;
using AgarIoByKfaziClient.Contract;
using AgarIoByKfaziClient.Helpers;

namespace AgarIoByKfaziClient
{
    internal class AgarIoServerGateway
    {
        private readonly StreamWriter _writer;
        private readonly StreamReader _reader;

        public AgarIoServerGateway(StreamWriter writer, StreamReader reader)
        {
            _writer = writer;
            _reader = reader;
        }

        public void JoinPlayer()
        {
            Command<CommandResponseDto>(new JoinPlayerCommandDto());
        }

        public void Login(string playerName, string password)
        {
            Command<CommandResponseDto>(new LoginDto { Login = playerName, Password = password });
        }
        public GetViewResponseDto GetView()
        {
            return Command<GetViewResponseDto>(new GetViewPlayerCommandDto());
        }

        public void Move(double destinationX, double destinationY)
        {
            Command(new MovePlayerCommandDto { Dx = destinationX, Dy = destinationY});
        }

        public void SplitMass()
        {
            Command(new SplitPlayerCommandDto());
        }

        public void EjectMass()
        {
            Command(new EjectMassPlayerCommandDto());
        }

        public T Command<T>(object dto)
        {
            var responseJson = Command(dto);
            return responseJson.FromJson<T>();
        }

        public string Command(object dto)
        {
            var commandJson = dto.ToJson();
            _writer.WriteLine(commandJson);
            var responseJson = _reader.ReadLine();
            Console.WriteLine(responseJson);
            return responseJson;
        }
    }
}