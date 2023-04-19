using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    //인터페이스, 특정 함수를 여러 클래스에서 사용할 수 있도록 공통적인 형태를 제시한다.
    //추상 클래스 -> is a / 인터페이스 -> has a

    //추상클래스는 자식이 단 한개만 소유할 수 있지만, 인터페이스는 여러 개 소유할 수 있다.
    //한 클래스에 대해 여러 기능을 참조하고 싶다면 인터페이스 사용
    public interface IAction
    {
        void Cancel();
    }
}