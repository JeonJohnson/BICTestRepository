#계획
1. 마우스 조작 
	- 직교투영으로 변경 (줌은 SIZE로함)
	- 맵도 마름모꼴로 생성. (모든 오브젝트도)

2. 건물 생성
	- UI 눌러서 시작
	- 1. 그 바닥에 x,y 사이즈 보이게 
	- 2. 일단 건물 반투명은 없고 그냥 격자만
	- 3. 그리드에 딱딱 떨어지게 (기준은 최상단 그리드)
3. 미니맵	

4. 드래그 (다중선택)
	1순위) 유닛 (등급? / 
	2순위) 같은 종류 건물(많을수록 우선순위)

5. 전장의 안개
	* 아예 안 가 본 곳 - 검정색(지형, 적유닛, 오브젝트, 중립유닛 아예 안보임)
	* 유닛이 가 봤지만 현재 유닛,건물 시야에 보이지 않는 곳 - 반투명 회색(지형, 오브젝트는는 보이나 적 유닛은 안보임.)
	* 유닛, 건물 시야에 들어오는 곳 - 걍 밝어짐

설계
1. 클릭하는쪽으로 이동하는 유닛 만들기
2. 검은색 Plane 깔아두고 일단 특정 부분만 알파값 조절 할 수 있도록 해보기
3. 유닛 주위로 바꿔보기
**일단 단계 (아예 안가본곳 / 가봤지만 현재 시야는 없는곳 / 현재 시야 있는곳) 나누지 말고 특정 부분에만 알파값 조절이 되는지 확인해보자
4. 유닛 주위에 밝아지는거 확인이 되면
	단계별로 나눠보기
5. 마지막으로 높은 지형(돌산, 숲) 뒤에는 밝아지지 않도록 해보기
**https://rito15.github.io/posts/fog-of-war/

6. 건물 설치 가능/불가능 구역 (일단 나중에 맵 까는 툴 만들???? 응~몰라~) 
	=> 나중에  

----------------------------------------------------------------------------------------------------------------------------------------
근희 :    0206 월 : 맵 마름모로 까는거 테스트
   0207 화 ~ 0208 수 : 건물 설치하는거.
   0209 목 ~ 0210 토 : 미니맵.
   미니맵까지는 이번주 안(~0212일) 까지 할 듯
   안개는 일단 그 다음.   
----------------------------------------------------------------------------------------------------------------------------------------

	
	
	
#Work history
19:57 2023-01-31	마우스 조작 완료
			휠로 줌,인아웃
			화면 모서리에 마우스 가져다 대면 카메라 움직임.
	But 지금 줌 인 된 상태에서 화면을 정해진 구역 이상으로 나갔을 때 줌 아웃하면 카메라가 빠져나가는 상황 있음. 내일 해결 해볼 예정.

02:04 2023-02-04	대각선으로 맵 깔기 완성/ Orthographic (직교투영) 으로 카메라 기본 조작 완료.
			- 이동, 줌
	할거 1. 화면 스크롤 제한 걸기 
		2. 줌인,아웃에 유연하게 화면 스크롤 먹히게

06:19 2023-02-05	화면 스크롤 제한은 됨.
	근데 저번이랑 같은 방식으로 되서 줌인/아웃시 스크롤 막히게 할라면 좀 곤란

21:16 2023-02-05	화면 이동 완성. (줌인아웃, 스크롤제한)
		=> 나중에 본 프로젝트 시작시 맵 생성할때 맵 Boundary 지정해 두고 그거 쓰기
	할거 : 건물 생성 해보기

23:03 2023-02-07	맵 마름모로 깔았음
	할거 : 똥싸고 카메라 맵 어떻게 깔리던 유동적으로 작동되도록하기

00:26 2023-02-08	맵 마름모 깔고나서 그거에 맞춰서 카메라도 작동완료 
	버그 : 한번 줌 해야 Clamp 걸림 수정 예정
	할거 : 건물 설치 시작

22:55 2023-02-08	단색 반투명 쉐이더 완성

00:30 2023-02-09	설치한 위치 띄움

01:39 2023-02-09	임시건물(큐브 사이즈) 맞춰서 생성까지는 됌
	버그 : 지금 특정 좌표에서 개줫깥이 나오는 버그 유 

17:48 2023-02-10	일정 좌표값 이상에서 건물 grid 안나오는 버그 수정 완료.
	정사각형아닌 건물(문) tab눌러서 회전까지 하기

18:13 2023-02-10	 정사각형 아닌 건물도 grid뜸, tab눌러서 회전까지 완성
	미니맵들어가기

20:48 2023-02-10	미니맵 띄우긴했음.	근데 지금은 도식화 되어있는게 아님.
	할거 : 미니맵 조금 더 수학적인 계산으로 렌더링 범위 정하기 (맵사이즈 바껴도 잘 나오도록)
		실제 카메라 렌더링 범위 띄우기
		도식화된 걸로 출력하게 바꾸기

20:51 2023-02-12	미니맵 위치, Size 다 맵 크기 계산해서 하도록 했음.
	할거 : 실제 RTSCam 렌더링 범위 띄우기

22:44 2023-02-12	RTSCam 렌더링 범위 띄웠음
	할거 : 건물 도식화

23:49 2023-02-12	미니맵 끝(나중에 추가로 적은 빨강색)
	할거 : 안개 시작하고 끝나면 ㄹㅇ 프로젝트 생성하기

09:14 2023-02-13	안개 시작하기전 계획
	1. 클릭하는쪽으로 이동하는 유닛 만들기
	2. 검은색 Plane 깔아두고 일단 특정 부분만 알파값 조절 할 수 있도록 해보기
	3. 유닛 주위로 바꿔보기
일단 단계 (아예 안가본곳 / 가봤지만 현재 시야는 없는곳 / 현재 시야 있는곳) 나누지 말고 특정 부분에만 알파값 조절이 되는지 확인해보자

23:11 2023-02-13	이동하는 유닛 제작 완료