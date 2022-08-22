using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public void Interact();
}
public interface IUsable
{
    public void StartUse();
    public void StopUse();
}
public interface IEnemy
{
    public void Damage(float amount);
}
public interface IPlayer
{
    public void Damage(float amount);
}