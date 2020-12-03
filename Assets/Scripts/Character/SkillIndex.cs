using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillIndex : Index<SkillElement, SkillObject>
{
    protected override void SetupIndexElement(SkillElement element, SkillObject value)
    {
        base.SetupIndexElement(element, value);
        value.formula = element.defaultFormula;
    }
}
