using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    //Observer Pattern�� ��� ������ �Ǵ� ActionScheduler Ŭ����
    //�÷��̾ ���� ����(�̵�, ����)�� �ϴ��� ���޹ް�, ���� ���°� �������� �����Ѵ�.
    public class ActionScheduler : MonoBehaviour
    {
        IAction currentAction;

        public void StartAction(IAction action)
        {
            //���� ���ϴ� ������ ������ �ٷ� �Է¹��� ���� ����,
            //���� �ٸ� ���������� ��ȯ�� �ִٸ� ��� ���� ���� ���� ����(ex ���� ��� ĵ�� �� �ٷ� �ڵ��Ƽ� �̵�)
            if (action == currentAction) return;
            if (currentAction != null)
            {
                currentAction.Cancel();
            }

            currentAction = action;
        }

        //���� � ������ �������̵� ��� -> ��� ó�� �Ŀ� ���
        public void CancelCurrentAction()
        {
            StartAction(null);
        }
    }
}