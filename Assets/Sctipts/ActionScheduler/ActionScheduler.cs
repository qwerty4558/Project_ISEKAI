using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    //Observer Pattern의 기반 동작이 되는 ActionScheduler 클래스
    //플레이어가 무슨 동작(이동, 공격)을 하는지 전달받고, 현재 상태가 무엇인지 저장한다.
    public class ActionScheduler : MonoBehaviour
    {
        IAction currentAction;

        public void StartAction(IAction action)
        {
            //현재 취하는 동작이 없으면 바로 입력받은 동작 수행,
            //만약 다른 동작으로의 전환이 있다면 취소 이후 다음 동작 수행(ex 공격 모션 캔슬 후 바로 뒤돌아서 이동)
            if (action == currentAction) return;
            if (currentAction != null)
            {
                currentAction.Cancel();
            }

            currentAction = action;
        }

        //현재 어떤 동작을 수행중이든 취소 -> 사망 처리 후에 사용
        public void CancelCurrentAction()
        {
            StartAction(null);
        }
    }
}