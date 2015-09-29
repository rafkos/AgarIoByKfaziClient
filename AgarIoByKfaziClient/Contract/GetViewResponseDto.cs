namespace AgarIoByKfaziClient.Contract
{
    public class GetViewResponseDto : CommandResponseDto
    {
        public BlobDto[] Blobs { get; set; }
    }
}