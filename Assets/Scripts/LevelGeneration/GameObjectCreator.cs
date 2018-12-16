﻿using System.Collections.Generic;
using UnityEngine;

namespace Project{
    public class GameObjectCreator : ScriptableObject
    {
        private RoomCreator _roomCreator;
        private ConnectionGenerator _connectionGenerator;

        public void OnEnable()
        {
            _roomCreator = CreateInstance("RoomCreator") as RoomCreator;
            _connectionGenerator = CreateInstance("ConnectionGenerator") as ConnectionGenerator;
        }

        public void Compose (ClassObject[] classes)
        {
            List<GameObject> items = null;
            foreach (var c in classes)
            {
               items = _roomCreator.CreateRoomAndExtensions(c);
            }
            if (items != null)
            {
                _connectionGenerator.CreateConnections(items);
            }
        }

        //Count number of relationships a class has
        public static int CountRels(ClassObject classObject)
        {
            int res = 0;

            foreach (string sc in classObject.subclasses) { res++; }
            foreach (string assoc in classObject.associations) { res++; }
            if (!string.IsNullOrEmpty(classObject.superclass)) { res++; }
            return res;
        }
    }
}

