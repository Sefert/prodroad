namespace Base.Contracts.Domain;

public interface IModificationMeta
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

}