using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Index<TElement, TObject> : ScriptableObject where TElement : Element where TObject : IntAttributeObject
{
    public ScriptableCharacter parent;
    public List<TElement> indexElements = new List<TElement>();

    public List<TElement> index = new List<TElement>();
    public List<TObject> indexValues = new List<TObject>();

    public void CreateAttributes()
    {
        foreach (TElement element in indexElements)
        {
            if (!index.Contains(element))
            {
                index.Add(element);

                TObject value = CreateInstance<TObject>();
                value.name = element.name;
                value.parent = parent;
                indexValues.Add(value);
            }
        }
        indexElements.Clear();
    }

    public bool Has(TElement element)
    {
        return index.Contains(element);
    }

    public TObject Get(TElement element)
    {
        int i = index.IndexOf(element);
        return indexValues[i];
    }
}
