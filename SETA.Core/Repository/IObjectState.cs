namespace SETA.Core.Repository
{
    public enum ObjectState
    {
        Unchanged,
        Added,
        Modified,
        Deleted
    }
    public interface IObjectState
    {
        ObjectState State { get; set; }
    }
}
