using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    protected PlayerController user;
    protected bool isActivated;

    public void PickedUp(PlayerController user)
    {
        this.user = user;
        OnPickedUp();
    }

    public bool Activated()
    {
        bool val = OnActivated();
        this.isActivated = !val;
        return val;
    }

    public bool Tick() { return OnTick(); }
    public void CleanUp() { OnCleanUp(); }
    public bool FireLeft() { return OnFireLeft(); }
    public bool FireRight() { return OnFireRight(); }
    public bool FireUp() { return OnFireUp(); }
    public bool FireDown() { return OnFireDown(); }
    public bool Fire(Vector2 direction) { return OnFire(direction); }

    public void AimLeft() { OnAimLeft(); }
    public void AimRight() { OnAimRight(); }
    public void AimUp() { OnAimUp(); }
    public void AimDown() { OnAimDown(); }
    public void Aim(Vector2 direction) { OnAim(direction); }

    // ===== Implement For Items ===== //

    // Called when the player picks up the item.
    public abstract void OnPickedUp();
    // Called when the player activates the item (so when the player starts aiming).
    public abstract bool OnActivated();
    // Called every tick that the item is held by the player (whether activated or not).
    public abstract bool OnTick();
    // Called when the item has been used up.
    public abstract void OnCleanUp();
    // Called once the player has moved and then released the analog stick.
    public abstract bool OnFireLeft();
    public abstract bool OnFireRight();
    public abstract bool OnFireUp();
    public abstract bool OnFireDown();
    // Alternative, currently not called.
    public abstract bool OnFire(Vector2 direction);

    // Called every frame that the item is activated.
    // Will not be called on the same frame as any Fire function ever.
    public abstract void OnAimLeft();
    public abstract void OnAimRight();
    public abstract void OnAimUp();
    public abstract void OnAimDown();
    // Alternative, currently not called.
    public abstract void OnAim(Vector2 direction);
}
