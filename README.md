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

6. 건물 설치 가능/불가능 구역 (일단 나중에 맵 까는 툴 만들???? 응~몰라~) 
	=> 나중에 
	
	
	
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