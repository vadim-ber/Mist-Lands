using UnityEngine;

public interface INewVectorHandler
{
    public bool HasNewVector { get; set; }
    public abstract void HandleNewVector(Vector3 newVector);
}
