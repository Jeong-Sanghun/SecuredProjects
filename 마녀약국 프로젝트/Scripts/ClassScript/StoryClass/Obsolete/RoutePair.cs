using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Obsolete
{

    [System.Serializable]
    public class RoutePair
    {
        public string storyName;
        public List<int> pickedRouteList;

        public RoutePair()
        {
            storyName = "null";
            pickedRouteList = new List<int>();

        }
    }

}