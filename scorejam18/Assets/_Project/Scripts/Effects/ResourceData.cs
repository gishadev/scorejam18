using UnityEngine;

namespace Gisha.Effects
{
    public class ResourceData
    {
        public Object o;
        public string name;

        public bool isSelected = false;

        public ResourceData(string _name, Object _o)
        {
            name = _name;
            o = _o;
        }
    }
}