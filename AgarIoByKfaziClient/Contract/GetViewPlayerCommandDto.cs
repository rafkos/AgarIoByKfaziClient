namespace AgarIoByKfaziClient.Contract
{
    public class GetViewPlayerCommandDto : PlayerCommandDto
    {
        public GetViewPlayerCommandDto() : base(PlayerCommandType.GetView)
        {
        }
    }
}