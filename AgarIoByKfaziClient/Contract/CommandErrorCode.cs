namespace AgarIoByKfaziClient.Contract
{
    public enum CommandErrorCode
    {
        Success = 0,

        WrongLogin,

        CommandsLimitExceeded = 100,

        GameNotStarted,

        NotJoined,

        TooManyBlobs,

        TooLowMass,

        AlreadyJoined
    }
}