using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActionable
{
    public IEnumerator TriggerAction(float duration);
}
