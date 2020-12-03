using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Index<TElement, TObject> : ScriptableObject where TElement : Element where TObject : IntAttributeObject
{
    public ScriptableCharacter parent;
    public List<TElement> elementsToCreate = new List<TElement>();

    public List<TElement> indexElements = new List<TElement>();
    public List<TObject> indexValues = new List<TObject>();

    public void CreateAttributes()
    {
        elementsToCreate.Sort();
        foreach (TElement element in elementsToCreate)
        {
            if (!indexElements.Contains(element))
            {
                indexElements.Add(element);

                TObject value = CreateInstance<TObject>();
                SetupIndexElement(element, value);
                indexValues.Add(value);
            }
        }
        elementsToCreate.Clear();
    }

    protected virtual void SetupIndexElement(TElement element, TObject value)
    {
        value.name = element.name;
        value.parent = parent;
    }

    public bool Has(TElement element)
    {
        return indexElements.Contains(element);
    }

    public TObject Get(TElement element)
    {
        int i = indexElements.IndexOf(element);
        return indexValues[i];
    }
}
