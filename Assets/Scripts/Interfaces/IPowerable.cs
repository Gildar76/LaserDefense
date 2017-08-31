using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPowerable
{
    float GetCurrentPower();
    float GetMaximumPower();
    void SetCurrentPower(float power);
    void SetMaximumpower(float power);


}
