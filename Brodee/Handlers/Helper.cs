﻿using UnityEngine;

namespace Brodee.Handlers
{
    public static class Helper
    {
        public static void LogGameObjectComponents(GameObject gameObject)
        {
            Logger.AppendLine($"LogGameObjectComponents name:{gameObject.name}");
            //Logger.AppendLine($"LogGameObjectComponents lossyScale:{gameObject.transform.lossyScale}");
            //Logger.AppendLine($"LogGameObjectComponents position:{gameObject.transform.position}");
            //Logger.AppendLine($"LogGameObjectComponents localRotation:{gameObject.transform.localRotation}");
            //Logger.AppendLine($"LogGameObjectComponents rotation:{gameObject.transform.rotation}");
            var comps = gameObject.GetComponents<Component>();
            foreach (var component in comps)
            {
                Logger.AppendLine($"component type:{component.GetType()}");
            }

            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                Logger.AppendLine($"child go name:{gameObject.transform.GetChild(i).name}");
            }
        }
    }
}