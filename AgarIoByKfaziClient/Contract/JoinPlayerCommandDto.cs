using System;

namespace AgarIoByKfaziClient.Contract
{
    public class JoinPlayerCommandDto : PlayerCommandDto
    {
        public JoinPlayerCommandDto() : base(PlayerCommandType.Join)
        {
        }

        public Guid PlayerId { get; set; }
    }
}