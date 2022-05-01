using UnityEngine;

namespace Gisha.scorejam18.Gameplay
{
    public class Collectable : MonoBehaviour
    {
        [SerializeField] private int price = 100;

        public int Price => price;
    }
}