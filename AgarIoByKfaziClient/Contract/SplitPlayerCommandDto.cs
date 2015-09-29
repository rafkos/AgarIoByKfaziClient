namespace AgarIoByKfaziClient.Contract
{
    public class SplitPlayerCommandDto : PlayerCommandDto
    {
        public SplitPlayerCommandDto() : base(PlayerCommandType.Split)
        {
        }
    }
}