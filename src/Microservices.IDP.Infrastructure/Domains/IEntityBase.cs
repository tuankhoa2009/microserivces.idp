namespace Microservices.IDP.Infrastructure.Domains
{
    public interface IEntityBase<T>
    {
        T Id { get; set; }

    }
}
