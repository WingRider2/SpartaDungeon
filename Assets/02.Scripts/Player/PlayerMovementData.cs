using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerMovementData
{
    public float walkSpeed;
    public float jumpPower;
    public float dashPower;

    public IEnumerator addWalkSpeed(BuffData data)
    {
        walkSpeed += data.Velue;
        yield return new WaitForSecondsRealtime(data.Duration);
        walkSpeed -= data.Velue;
    }
    public IEnumerator addJumpPower(BuffData data)
    {
        jumpPower += data.Velue;
        yield return new WaitForSecondsRealtime(data.Duration);
        jumpPower -= data.Velue;
    }
    public IEnumerator addDashPower(BuffData data)
    {
        dashPower += data.Velue;
        yield return new WaitForSecondsRealtime(data.Duration);
        dashPower -= data.Velue;
    }
}
