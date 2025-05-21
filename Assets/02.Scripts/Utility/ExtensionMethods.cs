using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods 
{
    public static float GetObjectScreenHeight(Camera cam, Renderer renderer)
    {
        // 1) Bounds의 상단·하단 월드 좌표 구하기
        Bounds b = renderer.bounds;
        Vector3 topWorld = new Vector3(b.center.x, b.max.y, b.center.z);
        Vector3 bottomWorld = new Vector3(b.center.x, b.min.y, b.center.z);

        // 2) 월드 좌표를 스크린 좌표로 변환
        Vector3 topScreen = cam.WorldToScreenPoint(topWorld);
        Vector3 bottomScreen = cam.WorldToScreenPoint(bottomWorld);

        // 3) Y 차이(절대값) → 픽셀 높이
        return Mathf.Abs(topScreen.y - bottomScreen.y);
    }

    public static float GetObjectScreenHeight(Camera cam, Transform transform)
    {
        return Vector3.Distance(cam .transform.position , transform.position);
    }

}
