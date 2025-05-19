<!DOCTYPE html>
<html lang="ko">
<head>
  <meta charset="UTF-8">
  <title>프로젝트 개요</title>
</head>
<body>
  <h1>프로젝트 개요</h1>
  <p>간단한 액션 플랫폼 게임 프로토타입으로, 1인칭/3인칭 전환, 다양한 상호작용 오브젝트, 적 AI, 아이템 시스템 등을 구현합니다.</p>
  <h2>🎮 주요 기능</h2>
  <ol>
    <li><strong>사용자 인터페이스 (UI)</strong>
      <ul>
        <li>HP 바</li>
        <li>스테미나 게이지 (점프 및 대쉬 소비)</li>
      </ul>
    </li>
    <li><strong>컨트롤</strong>
      <ul>
        <li>이동: WASD-</li>
        <li>점프: Spacebar</li>
        <li>대쉬: Left Shift</li>
        <li>시점 전환(Toggle): T</li>
        <li>3인칭 모드에서 벽이 시야를 가리면 카메라가 장애물 근처로 자동 줌인</li>
      </ul>
    </li>
    <li><strong>상호작용 오브젝트</strong>
      <ul>
        <li>점프대 (Jump Pad)</li>
        <li>무빙 플랫폼 (플랫폼 발사기)</li>
        <li>매달리기 가능한 벽 (Climbable Wall)</li>
        <li>레이저 트랩 (Laser Trap)</li>
        <li>상호작용 오브젝트 표시 (Player 근처 오브젝트 하이라이트)</li>
      </ul>
    </li>
    <li><strong>적 AI 성능 개선</strong>
      <ul>
        <li>네비게이션 경로 탐색 최적화</li>
        <li>행동 트리 기반 상태 관리 개선</li>
      </ul>
    </li>
    <li><strong>아이템 시스템</strong>
      <ul>
        <li><strong>지속 버프 아이템</strong>
          <ul>
            <li>신발 (Shoes, 빨간색): 이동 속도 증가</li>
            <li>날개 (Wings, 초록색): 점프력 증가</li>
          </ul>
        </li>
        <li><strong>1회용 사용 버프 아이템</strong>
          <ul>
            <li>이동 속도 버프 (Light Blue)</li>
            <li>점프력 버프 (Blue)</li>
            <li>순간 이동 스크롤 (Teleport)</li>
          </ul>
        </li>
      </ul>
    </li>
  </ol>
</body>
</html>
