using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    //�������̽�, Ư�� �Լ��� ���� Ŭ�������� ����� �� �ֵ��� �������� ���¸� �����Ѵ�.
    //�߻� Ŭ���� -> is a / �������̽� -> has a

    //�߻�Ŭ������ �ڽ��� �� �Ѱ��� ������ �� ������, �������̽��� ���� �� ������ �� �ִ�.
    //�� Ŭ������ ���� ���� ����� �����ϰ� �ʹٸ� �������̽� ���
    public interface IAction
    {
        void Cancel();
    }
}