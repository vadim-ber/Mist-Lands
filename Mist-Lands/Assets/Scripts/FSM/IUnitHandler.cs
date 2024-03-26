public interface IUnitHandler 
{
    public bool HasNewUnit { get; set; }
    public abstract void HandleNewUnit(Unit unit);
}
