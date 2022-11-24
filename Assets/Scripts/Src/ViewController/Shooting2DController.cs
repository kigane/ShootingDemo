using UnityEngine;
using QFramework;

namespace ShootingDemo
{
    public abstract class Shooting2DController : MonoBehaviour, IController
    {
        IArchitecture IBelongToArchitecture.GetArchitecture()
        {
            return Shooting2D.Interface;
        }
    }
}
