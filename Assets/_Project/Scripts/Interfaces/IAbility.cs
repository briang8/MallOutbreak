using UnityEngine;

public interface IAbility
{
    void Activate(GameObject user);
    float Cooldown { get; }
}