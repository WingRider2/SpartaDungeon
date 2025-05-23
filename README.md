<!DOCTYPE html>
<html lang="ko">
<head>
    <meta charset="UTF-8">
    <title>프로젝트 개요</title>
</head>
<body>
    <h1>프로젝트 개요</h1>
    <p>간단한 액션 플랫폼 게임 프로토타입으로, 다양한 상호작용 오브젝트, 적 AI, 아이템 시스템 등을 구현합니다.</p>

    <h2>🎮 주요 기능</h2>
    <ul>
        <li><strong>사용자 인터페이스 (UI)</strong>
            <ul>
                <li>HP 표시</li>
                <li>스태미나 게이지 (대쉬 소비)</li>
            </ul>
        </li>
        <li><strong>컨트롤</strong>
            <ul>
                <li>이동: WASD</li>
                <li>점프: Spacebar</li>
                <li>대쉬: Left Shift</li>
            </ul>
        </li>
        <li><strong>상호작용 오브젝트</strong>
            <ul>
                <li>점프대 (Jump Pad)</li>
                <li>무빙 플랫폼</li>
                <li>매달리기 가능한 벽 (GripWall)</li>
                <li>상호작용 오브젝트 표시 (Player 근처 오브젝트 하이라이트)</li>
                <li>상호작용 오브젝트 잡을 경우 크기 조절 가능 (충돌 계산 보강 필요)</li>
            </ul>
        </li>
        <li><strong>버프 시스템</strong>
            <ul>
                <li><em>지속 버프 아이템</em>
                    <ul>
                        <li>사과: 이동 속도 증가</li>
                        <li>고추: 점프력 증가</li>
                    </ul>
                </li>
                <li><em>영구 버프 아이템</em>
                    <ul>
                        <li>바나나: 이동 속도 증가</li>
                        <li>당근: 점프력 증가</li>
                    </ul>
                </li>
            </ul>
        </li>
    </ul>
</body>
</html>
