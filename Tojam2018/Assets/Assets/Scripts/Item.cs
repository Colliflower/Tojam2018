using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public abstract bool Tick();

    public abstract bool FireLeft();

    public abstract bool FireRight();

    public abstract bool FireUp();

    public abstract bool FireDown();
}
