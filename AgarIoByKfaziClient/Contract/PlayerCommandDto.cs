using Newtonsoft.Json;

namespace AgarIoByKfaziClient.Contract
{
    public class PlayerCommandDto
    {
        [JsonConstructor]
        protected PlayerCommandDto(PlayerCommandType type)
        {
            Type = type;
        }

        public PlayerCommandType Type { get; private set; }
    }
}