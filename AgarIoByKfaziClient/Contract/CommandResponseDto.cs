namespace AgarIoByKfaziClient.Contract
{
    public class CommandResponseDto
    {
        public CommandErrorCode ErrorCode { get; set; }

        public string Message { get; set; }
    }
}