# BICTestRepository
for Test
-----------------------------------------------------------------
실제로 할때는 건물, 유닛 다 공용으로 사용할 최상위 부모 하나 만들기 
Unit 	- Humanoid
	- Building 
뭐 이런식으로든

-----------------------------------------------------------------
23:39 2023-01-17	프로젝트 업로드
작업내역
1. 60*15 큐브 깔기
2. 화면 스크롤 (방향키, 마우스 화면 테두리 가까이, 마우스클릭후 드래그ㄴ)
3. 유닛, 건물 프리팹(구분 가능하게 간단한 디자인)

22:46 2023-01-18	유닛 밑작업
할일	1. 에너미 소환해서 위치로 이동하는거 체크
	2. 엑셀파일 로드해서 맵 깔기 (이 이후에 적 소환할때 마다 타겟 지점 넥서스로 깔아주기)
		-> 네비매쉬 동적으로 설정하는거 필요
	3. 막사에서 라이플맨 소환. 
	4. 라이플맨 공격 확인. (넥서스 반대편 위치 찍고 일단 걷다가 적 발견하면 줘패기)

04:43 2023-01-20	건물 깔음.
할일	1. 에너미 소환, 네비 매쉬 에이전트 넣고 우리 넥서스 쪽으로 움직이기
	2. 동적으로 네비매쉬 깔기
	3. 막사에서 라이플맨 소환하기

21:07 2023-1-20	네비매쉬 동적으로 깔기 성공 
		-> 바닥이 될 친구한테 NavMeshSurface 깔아두고 거기에 obstacle 될 애들 레이어 정하기
		-> Navigation Baker은 네비 동적으로 까는 스크립트, 아무 오브젝트에 넣어놓고 surface에 navmeshSurface 들어간 바닥 친구 넣어주기
		-> 게임 시작 후 원할 때 Navigation Baker의 Bake 함수 실행!

22:51 2023-01-22	적 생성됨, 벙커에서 총알 나감
	할거 : 벙커에서 적 찾는거에서 거리 기준 추가하기
		유닛 작업시작

14:55 2023-01-23	벙커 적 서칭 재 정비 / 총알 잘 나감 / 남은적 text ui 
	할거 : 유닛 작업 시작

16:26 2023-01-23	보병 작동(적 찾기, 움직임, 공격 등) + 보병 소환
	할거 : 막사에서 유닛 소환하기

16:46 2023-01-23	막사에서 유닛 소환 완료

17:09 2023-01-23	트랩 완성

00:33 2023-01-25	메딕 완성

11:24 2023-01-25	UI연결 + 인구수

12:42 2023-01-25	머신건, 스나이퍼 작업완료
	좀있다가 로켓하고 스킬 ㄱㄱ

13:01 2023-01-25	로켓 작업완료
	할거 : 스킬 ㄱㄱ

https://velog.io/@acodeam/Unity-Projector-%EC%82%AC%EC%9A%A9%EC%8B%9C-%EC%9C%A0%EC%9D%98%EC%82%AC%ED%95%AD

13:26 2023-01-25	폭격 스킬 완성


