﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

namespace Zframework
{
    //最好用于发射频率不高/参数复杂的事件 因为每次发射都会new一个对象
    public abstract class SubjectArgs//Todo：对象池
    {
        public object sender = null;
        public abstract int SubjectId { get; }
    }


    public static class SubjectManager
    {
        private static Subject_Manager mInstance = new Subject_Manager();
        public static IObservable<T> GetSubject<T>() where T : SubjectArgs
        {
            return mInstance.GetSubject<T>();
        }
        public static void Fire<T>(T e) where T : SubjectArgs
        {
            mInstance.Fire<T>(e);
        }
    }

    public class Subject_Manager
    {

        private Dictionary<int, Subject<SubjectArgs>> mSubjectDic = new Dictionary<int, Subject<SubjectArgs>>();
        public IObservable<T> GetSubject<T>() where T : SubjectArgs
        {
            int subjectId = typeof(T).GetHashCode();
            Subject<SubjectArgs> subject = null;
            if (!mSubjectDic.TryGetValue(subjectId, out subject))
            {
                subject = new Subject<SubjectArgs>();
                mSubjectDic.Add(subjectId, subject);

            }
            return subject.Select(_ => _ as T);
        }

        public void Fire<T>(T e) where T : SubjectArgs
        {
            Subject<SubjectArgs> subject = null;
            if (!mSubjectDic.TryGetValue(e.SubjectId, out subject))
            {
                //Debug.Log("没有订阅者...发射失败:subjectId_"+ e.SubjectId);
                return;
            }
            subject.OnNext(e);
        }
    }
}