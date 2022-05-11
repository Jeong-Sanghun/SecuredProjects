using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioManager : MonoBehaviour
{
    public enum LevelType
    {
        Level1,
        Level2,
    }

    public LevelType levelType;

    public class TestInfo
    {
        public string text;
        public TalkState talkState;

        public TestInfo(string _value, TalkState _talkState)
        {
            this.text = _value;
            this.talkState = _talkState;
        }
    }


    public List<TestInfo> scenarioList1 = new List<TestInfo>();

    public List<TestInfo> scenarioList2 = new List<TestInfo>();


    public enum TalkState
    {
        PLAYER,
        NAR,
        SYSTEM,
        FISH,
        Mushroom,
        //NONE,
    }

    public TalkState talkState;

    private string[] scenarioText =
    {

    "...떨어진 게 사람인가?",
    "아?",
    "눈이 감긴 적도 없이 한 번 더 떠졌다.",
    "이게 뭐야?",
    "평소에는 절대로 안 입을 것 같은 복식이다.",
    "회사 어디 갔어?",
    "분명히 야근 중에, 회사 창문에서, 떨어지는 사람을 봤는데...",
    "근데 여긴 어디지?",
    "동화책에서 튀어 나온 듯한 종이 공작 별세계가 펼쳐져있다.",
    "쏟아질 듯 하늘을 가득 채운 별, TV에서나 보던 \n너른 들판과 세상을 집어삼킬 듯 새카만 호수.",
    "너무 생소한 광경에 무서울 지경이다.",
    "몸을 뒤적여봐도 고급스러운 옷감의 재질만 느껴질 뿐 정작 찾고 있는 휴대전화는 없다.",
    "내 옷 어디 가고 이런 걸 입고 있지?",
    "시선을 내려다보니 섬세한 손과 다리가 보인다. 낯설다.",
    "저 물웅덩이에서 한 번 제대로 살펴보자.",
    "화면의 좌, 우 터치로 양 옆으로 이동할 수 있습니다.", // 15
    "이거 누구야...",
    "완전 어린애잖아.",
    "헉! ",
    "이 눈... 아까 떨어지던 사람인가...?",
    "그러기엔 이 몸... 너무 어린데.",
    "나는 어떻게 된 걸까. 꿈이라도 꾸는 걸까?",
    "그래, 이런 게 둥둥 떠다니는 데 현실은 아니겠지.",
    "화면을 꾹 누른 채 위 아래로 슬라이드하면 조명의 밝기를 조절할 수 있습니다.",
    "...언제까지고 혼란스러워만 할 수는 없어. 뭐가 어떻게 된 건지 알아봐야겠어.", //24
    "악!", // 25
    "뭔 금붕어가 이렇게 커? 떠 다녀? 이게 뭔데!", //26
    "금붕어는 허공에 고요하게 떠 있다. 바람 한 점 불지 않는데 \n지느러미가 하늘하늘 나부낀다.", //27
    "살아 있는 건가...?", // 28
    "둥그렇게 뜬 눈을 빤히 바라보지만 미동도 없다.", // 29
    "모형치고는 보석처럼 잘 세공된 비늘과 나비 날개 같은 지느러미가 너무 정교하다.", //30
    "내 꿈에 온 걸 환영해.", //31
    "악! 살아있어! 말도 해! 뭔데, 이거 뭐냐고!",
    "내가 네 꿈 속에 있다고? 이건 무슨 소리야?",
    "여기서는 안전해. 걱정도 불안도 없으니 편히 쉬자.",
    "어... 혼자 쉬는 건 어때? 난 돌아가야 할 곳이 있어서...",
    "왜 돌아가고 싶은 거야?",
    "일단 여기는 내가 있던 곳이랑 너무 다르고, 언제까지나 꿈만 꾸고 있을 수는 없으니까...",
    "바깥은 위험하고 무섭고 차가워. 영원히 꿈을 꾸는 게 뭐가 나빠?",
    "이쪽으로 가자. 달빛이 잘 들어서 편안하거든.",
    "야, 어디 가!",
    "뭐 저렇게 제멋대로인 녀석이 있어!.",
    "너도 내가 제멋대로라고 생각하는구나?",
    "나밖에 모르고 나 하고 싶은대로만 한다고?",
    "...뭐 상관없어. 이제는 다 끝났으니까.",
    "다 끝났다고?",
    "너...",
    "방금 떨어진 그 애구나?",
    "...누구를 가리키는 건지 모르겠어.",
    "그럼 넌 누군데?",
    "아니, 말하기 싫으면 하지 않아도 돼. ",
    "그냥 나 좀 돌려보내 주면 안 돼?",
    "맵의 조형물을 통해 다음 맵으로 이동합니다.",
    };

    public string GetScenarioText(int _index)
    {
        string result = "";

        result += scenarioText[_index];
        Debug.Log(_index + " : " + result);
        return result;
    }

    public string GetScenarioIndex(int _index)
    {
        string result = "";

        result += scenarioList2[_index].text;
        Debug.Log(_index + " : " + result);
        return result;
    }

    public TalkState GetScenarioIndexType(int _index)
    {
        return scenarioList2[_index].talkState;
    }

    private void Start()
    {
        if (levelType == LevelType.Level1)
        {
           // scenarioList1.Add(new TestInfo(  // 0
           // "...떨어진 게 사람인가?",
           // TalkState.NAR));

           // scenarioList1.Add(new TestInfo(  // 1
           // "아?",
           // TalkState.NAR));

           // scenarioList1.Add(new TestInfo(  // 2
           // "눈이 감긴 적도 없이 한 번 더 떠졌다.",
           // TalkState.NAR));

           // scenarioList1.Add(new TestInfo(  // 3
           // "이게 뭐야?",
           // TalkState.PLAYER));

           // scenarioList1.Add(new TestInfo(  // 4
           //"평소에는 절대로 안 입을 것 같은 복식이다.",
           // TalkState.NAR));

           // scenarioList1.Add(new TestInfo(  // 5
           // "회사 어디 갔어?",
           // TalkState.PLAYER));

           // scenarioList1.Add(new TestInfo(  // 6
           // "분명히 야근 중에, 회사 창문에서, 떨어지는 사람을 봤는데...",
           // TalkState.NAR));

           // scenarioList1.Add(new TestInfo(  // 7
           // "근데 여긴 어디지?",
           // TalkState.NAR));

           // scenarioList1.Add(new TestInfo( // 8
           // "동화책에서 튀어 나온 듯한 종이 공작 별세계가 펼쳐져있다.",
           // TalkState.NAR));

           // scenarioList1.Add(new TestInfo( // 9
           // "쏟아질 듯 하늘을 가득 채운 별, TV에서나 보던 너른 들판과 \n세상을 집어삼킬 듯 새카만 호수.",
           // TalkState.NAR));

           // scenarioList1.Add(new TestInfo( // 10
           // "너무 생소한 광경에 무서울 지경이다.",
           // TalkState.NAR));
        }

        else if (levelType == LevelType.Level2)
        {



            scenarioList2.Add(new TestInfo(  // 0
            "아이고, 여긴 또 어디... 웬 숲...?",
            TalkState.PLAYER));

            scenarioList2.Add(new TestInfo( // 1
            "주변을 탐색하여 단서를 찾을 수 있습니다.",
            TalkState.NAR));

            scenarioList2.Add(new TestInfo( // 2
            "뭐야, 웬 버섯? 웬 가위? 좀... 징그러워.",
            TalkState.PLAYER));

            scenarioList2.Add(new TestInfo(// 3
            "크크크, 네가 봐도 그렇지? 징그러워!",
           TalkState.FISH));

            scenarioList2.Add(new TestInfo(// 4
            "ㅆ...없는...ㅈ...내.",
            TalkState.Mushroom));

            scenarioList2.Add(new TestInfo(// 5
           "헉, 뭐라고 말도 하는데.",
          TalkState.PLAYER));

            scenarioList2.Add(new TestInfo(// 6
       "쓸모없는 싹은 잘라내! 쓸모없는 싹은 잘라내!",
      TalkState.Mushroom));

            scenarioList2.Add(new TestInfo(// 7
       "징그러운 게 아니라 무서운 버섯이었어. 다른 곳으로 가자.",
      TalkState.PLAYER));

            scenarioList2.Add(new TestInfo(// 8
       "마귀할망구! 아줌마 히스테리! 깔깔깔!",
      TalkState.FISH));

            scenarioList2.Add(new TestInfo(// 9
     "갑자기 너도 좀 무서워지는 것 같아...",
    TalkState.PLAYER));

            scenarioList2.Add(new TestInfo( //10
    "악! 깜짝이야!",
    TalkState.PLAYER));

            scenarioList2.Add(new TestInfo( //11
    "...이건 그냥 비눗방울이잖아.",
    TalkState.PLAYER));

            scenarioList2.Add(new TestInfo( //12
    "잠깐, 안에서 뭐가 보이는 것 같은데...",
    TalkState.PLAYER));

            scenarioList2.Add(new TestInfo( //13
    "손바닥만한 작은 기사.\n 무대에 선 젊은 남자의 사진과 함께 어떤 공연을 홍보하고 있다.",
    TalkState.SYSTEM));

            scenarioList2.Add(new TestInfo( //14
    "저거 봐. 저렇게 좋을까?",
    TalkState.FISH));

            scenarioList2.Add(new TestInfo( //15
    "이젠 나도 없으니 끝내주게 행복하겠지?",
    TalkState.FISH));

            scenarioList2.Add(new TestInfo( //16
    "갑자기 금붕어가 슬퍼보여... 아니, 화가 난건가?",
    TalkState.PLAYER));

            scenarioList2.Add(new TestInfo( //17
    "그런데 이 장면, 이 감정...",
    TalkState.PLAYER));

            scenarioList2.Add(new TestInfo( //18
    "꼭 실제로 겪었던 일같아.",
    TalkState.PLAYER));

            scenarioList2.Add(new TestInfo( //19
    "근거도 없고 말도 안되지만 느껴진다. 이건 누군가의 기억이다.",
    TalkState.NAR));

            scenarioList2.Add(new TestInfo( //20
    "아마도, 창문 너머에서 떨어지던 그 사람의...",
    TalkState.NAR));

            scenarioList2.Add(new TestInfo( //21
    "그렇다면 내가 이 꼴이 된 것도 관련이 있을지 모른다.",
    TalkState.NAR));

            scenarioList2.Add(new TestInfo( //22
    "혹시 주변을 더 뒤져보면 다른 게 나올까?",
    TalkState.PLAYER));

            scenarioList2.Add(new TestInfo( //23
    "일단 단서를 더 모아보자. 목표는 여기서 빠져나가는 것.",
    TalkState.PLAYER));

            scenarioList2.Add(new TestInfo( //24
    "이건... 메달?",
    TalkState.NAR));



            //        scenarioList2.Add(new TestInfo( //25
            //"찾은 물건을 활용하여 새로운 단서를 발견할 수 있습니다.",
            //TalkState.SYSTEM));




            scenarioList2.Add(new TestInfo( //26
    "와, 메달이라니. 댄스스포츠...? 전국 우승?!",
    TalkState.PLAYER));

            scenarioList2.Add(new TestInfo( //27
    "내 인생의 1등... 한창 공부 열심히 했을 때 반 1등 정도였나?",
    TalkState.PLAYER));

            scenarioList2.Add(new TestInfo( //28
    "어쨌든 이 사람... 대단한 사람인가 봐.",
    TalkState.PLAYER));

            scenarioList2.Add(new TestInfo( //29
    "금붕어, 이거 네 거야? 넌 어떻게 살았어?",
    TalkState.PLAYER));

            scenarioList2.Add(new TestInfo( //30
    "저게 내 거냐고? 말도 안되는 소리.",
    TalkState.FISH));

            scenarioList2.Add(new TestInfo( //31
    "내가 어떻게 살았는지는... 별로 얘기하고 싶지 않아.",
    TalkState.FISH));

            scenarioList2.Add(new TestInfo( //32
    "네 얘기나 해봐. 넌 뭐하는 사람이야?",
    TalkState.FISH));

            scenarioList2.Add(new TestInfo( //33
    "갑자기? ...음, 난 그냥 회사원?",
    TalkState.PLAYER));

            scenarioList2.Add(new TestInfo( //34
    "회식을 피하려다 야근에 물려서 회사에 뿌리내리고 있었는데",
    TalkState.PLAYER));

            scenarioList2.Add(new TestInfo( //35
    "창밖에 뭐가 떨어지더니 여기로 들어왔네.",
    TalkState.PLAYER));

            scenarioList2.Add(new TestInfo( //36
    "좋았어? 행복했어? 바깥으로 돌아가고 싶어?",
    TalkState.FISH));

            scenarioList2.Add(new TestInfo( //37
    "현실로 돌아가고 싶냐고?",
    TalkState.PLAYER));

            scenarioList2.Add(new TestInfo( //38
    "당연하지. 행복했냐고 하면... 조금 생각해봐야겠지만.",
    TalkState.PLAYER));

            scenarioList2.Add(new TestInfo( //39
    "집에 가는 길에 맥주 한 캔 사서 마셔야 한단 말이야. 로또 번호도 확인하고. 빨래도 밀렸고.",
    TalkState.PLAYER));

            scenarioList2.Add(new TestInfo( //40
    "...난 절대 가고 싶지 않아.",
    TalkState.FISH));

            scenarioList2.Add(new TestInfo( //41
    "어... 그럴 수도 있지. 그게 좋다면야.",
    TalkState.PLAYER));

            scenarioList2.Add(new TestInfo( //42
    "그러니까 너도 여기 있어.",
    TalkState.FISH));

            scenarioList2.Add(new TestInfo( //43
    "어어?",
    TalkState.PLAYER));

            scenarioList2.Add(new TestInfo( //44
    "미안... 그건 좀.?",
    TalkState.PLAYER));

            scenarioList2.Add(new TestInfo( //45
    "왜? 너도 내가 싫어? 내가 나빠서?",
    TalkState.FISH));

            scenarioList2.Add(new TestInfo( //46
    "갑자기 금붕어의 기세가 사나워진다. 이러다간 꼼짝 없이 갇혀버리는 거 아니야?",
    TalkState.NAR));

            scenarioList2.Add(new TestInfo( //47
    "잠깐, 잠깐! 진정해.",
    TalkState.PLAYER));

            scenarioList2.Add(new TestInfo( //48
    "왜 밖에 나가기 싫은데?",
    TalkState.PLAYER));

            scenarioList2.Add(new TestInfo( //49
    "넌 이해 못해! 죽어도 이해 못할 걸?",
    TalkState.FISH));

            scenarioList2.Add(new TestInfo( //50
    "사람이 죽을 때 한이 많으면 귀신이 된다며. 난 이미 그렇게 된 걸지도 모르지.",
    TalkState.FISH));

            scenarioList2.Add(new TestInfo( //51
    "내가 왜 돌아가기 싫은지 궁금해?",
    TalkState.FISH));

            scenarioList2.Add(new TestInfo( //52
    "그렇게 궁금하면 봐. 네가 손톱만큼이라도 이해할 수 있을 것 같진 않지만.",
    TalkState.FISH));

            scenarioList2.Add(new TestInfo( //53
    "이건 너무 막무가내 아니야?",
    TalkState.NAR));

            scenarioList2.Add(new TestInfo( //54
    "하지만 이곳의 주인이 금붕어로 보이는 이상 다른 선택지는 없어.",
    TalkState.NAR));

            scenarioList2.Add(new TestInfo( //55
    "그, 그래. 그런데 나는 한을 풀고, 막 퇴마하고 이런 쪽이랑은 전혀 관계가 없거든?",
    TalkState.PLAYER));

            scenarioList2.Add(new TestInfo( //56
    "그래서, 싫다고?",
    TalkState.FISH));

            scenarioList2.Add(new TestInfo( //57
    "아니, 아니! 우리 같이 찾아보자고. 뭐가 그리 억울하고 마음이 아픈지!",
    TalkState.PLAYER));

            scenarioList2.Add(new TestInfo( //58
    "자, 이쪽으로 가면 되나?",
    TalkState.PLAYER));


        }

    }
}
