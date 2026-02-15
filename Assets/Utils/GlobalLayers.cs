using System;
using System.Collections.Generic;
using UnityEngine;

public class GlobalLayers : MonoBehaviour
{
    public static GlobalLayers Ref { get; private set; }

    private void Awake()
    {
        if (Ref != null && Ref != this) Destroy(this);
        else Ref = this;
    }

    [SerializeField] List<LayerData> layers;

    public enum Layers
    {
        Enemies,
        Hittable,
        Player,
        Grave
    }
    [Serializable]
    public struct LayerData
    {
        public LayerMask mask;
        public Layers layerName;
    }

    public static bool IsOnLayer(GameObject obj, Layers layer)
    {
        foreach (var l in Ref.layers) 
            if (l.layerName == layer)
                return ((1 << obj.layer) & l.mask.value) != 0;
        return false;
    }

    public static LayerMask GetLayerMask(Layers layer)
    {
        foreach (var l in Ref.layers)
            if (l.layerName == layer) return l.mask;
        return 0;
    }
}