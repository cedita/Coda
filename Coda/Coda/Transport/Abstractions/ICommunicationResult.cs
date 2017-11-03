namespace Coda.Transport.Abstractions
{
    public interface ICommunicationResult
    {
        CommunicationStatus Status { get; set; }
    }
}