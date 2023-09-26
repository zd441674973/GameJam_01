using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IEventInfo
{

}
//无参
public class EventInfo : IEventInfo
{
    public UnityAction actions;

    public EventInfo(UnityAction action)
    {
        actions += action;
    }
}
//单参数
public class EventInfo<T> : IEventInfo
{
    public UnityAction<T> actions;

    public EventInfo(UnityAction<T> action)
    {
        actions += action;
    }
}

//双参数
public class EventInfo<T, U> : IEventInfo
{
    public UnityAction<T, U> actions;

    public EventInfo(UnityAction<T, U> action)
    {
        actions += action;
    }
}
//三参数
public class EventInfo<T, U, I> : IEventInfo
{
    public UnityAction<T, U, I> actions;

    public EventInfo(UnityAction<T, U, I> action)
    {
        actions += action;
    }
}
//四参数
public class EventInfo<T, U, I, O> : IEventInfo
{
    public UnityAction<T, U, I, O> actions;

    public EventInfo(UnityAction<T, U, I, O> action)
    {
        actions += action;
    }
}


public class EventCenter : BaseManager<EventCenter>
{
    //Key对应事件名
    //Value对应监听该事件的对应委托函数
    //将函数记录为无返回值，有一个参数的委托
    private Dictionary<string, IEventInfo> eventDictionary = new Dictionary<string, IEventInfo>();

    /// <summary>
    /// 添加事件监听的单参数接口
    /// </summary>
    /// <param name="name"></param>
    /// <param name="action"></param>
    //name事件名。action准备用来处理事件的委托函数
    public void AddEventListener<T>(string name, UnityAction<T> action)
    {
        //是否存在对应的事件的监听
        //有
        if (eventDictionary.ContainsKey(name))
        {
            (eventDictionary[name] as EventInfo<T>).actions += action;
        }
        //没有
        else
        {
            eventDictionary.Add(name, new EventInfo<T>(action));
        }
    }
    /// <summary>
    /// 添加事件监听的双参数重载接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    /// <param name="name"></param>
    /// <param name="action"></param>
    public void AddEventListener<T, U>(string name, UnityAction<T, U> action)
    {
        if (eventDictionary.ContainsKey(name))
        {
            (eventDictionary[name] as EventInfo<T, U>).actions += action;
        }
        else
        {
            eventDictionary.Add(name, new EventInfo<T, U>(action));
        }
    }

    /// <summary>
    /// 添加事件监听的三参数重载接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    /// <param name="name"></param>
    /// <param name="action"></param>
    public void AddEventListener<T, U, I>(string name, UnityAction<T, U, I> action)
    {
        if (eventDictionary.ContainsKey(name))
        {
            (eventDictionary[name] as EventInfo<T, U, I>).actions += action;
        }
        else
        {
            eventDictionary.Add(name, new EventInfo<T, U, I>(action));
        }
    }

    /// <summary>
    /// 添加事件监听的四参数重载接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    /// <param name="name"></param>
    /// <param name="action"></param>
    public void AddEventListener<T, U, I, O>(string name, UnityAction<T, U, I, O> action)
    {
        if (eventDictionary.ContainsKey(name))
        {
            (eventDictionary[name] as EventInfo<T, U, I, O>).actions += action;
        }
        else
        {
            eventDictionary.Add(name, new EventInfo<T, U, I, O>(action));
        }
    }

    /// <summary>
    /// 添加事件监听的无参重载接口
    /// </summary>
    /// <param name="name"></param>
    /// <param name="action"></param>
    public void AddEventListener(string name, UnityAction action)
    {
        //是否存在对应的事件的监听
        //有
        if (eventDictionary.ContainsKey(name))
        {
            (eventDictionary[name] as EventInfo).actions += action;
        }
        //没有
        else
        {
            eventDictionary.Add(name, new EventInfo(action));
        }
    }


    /// <summary>
    /// 移除事件监听
    /// </summary>
    /// <param name="name"></param>
    /// <param name="action"></param>
    //name对应事件名
    //action对应之前添加的委托函数
    public void RemoveEventListener<T>(string name, UnityAction<T> action)
    {
        if (eventDictionary.ContainsKey(name))
            (eventDictionary[name] as EventInfo<T>).actions -= action;
    }

    public void RemoveEventListener<T, U>(string name, UnityAction<T, U> action)
    {
        if (eventDictionary.ContainsKey(name))
            (eventDictionary[name] as EventInfo<T, U>).actions -= action;
    }

    public void RemoveEventListener<T, U, I>(string name, UnityAction<T, U, I> action)
    {
        if (eventDictionary.ContainsKey(name))
            (eventDictionary[name] as EventInfo<T, U, I>).actions -= action;
    }


    /// <summary>
    /// 移除事件监听的无参重载接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <param name="action"></param>
    public void RemoveEventListener(string name, UnityAction action)
    {
        if (eventDictionary.ContainsKey(name))
            (eventDictionary[name] as EventInfo).actions -= action;
    }

    /// <summary>
    /// 单参数事件触发
    /// </summary>
    /// <param name="name"></param>
    /// <param name="info"></param>
    //name事件被触发了
    public void EventTrigger<T>(string name, T info)
    {
        //有没有对应的事件监听
        //有的情况
        if (eventDictionary.ContainsKey(name))
        {
            if ((eventDictionary[name] as EventInfo<T>).actions != null)
                //执行相关事件
                (eventDictionary[name] as EventInfo<T>).actions.Invoke(info);
        }
    }
    /// <summary>
    /// 双参数事件触发
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    /// <param name="name"></param>
    /// <param name="info"></param>
    /// <param name="info2"></param>
    public void EventTrigger<T, U>(string name, T info, U info2)
    {
        //有没有对应的事件监听
        //有的情况
        if (eventDictionary.ContainsKey(name))
        {
            if ((eventDictionary[name] as EventInfo<T, U>).actions != null)
                //执行相关事件
                (eventDictionary[name] as EventInfo<T, U>).actions.Invoke(info, info2);
        }
    }

    /// <summary>
    /// 三参数事件触发
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    /// <param name="name"></param>
    /// <param name="info"></param>
    /// <param name="info2"></param>
    public void EventTrigger<T, U, I>(string name, T info, U info2, I info3)
    {
        //有没有对应的事件监听
        //有的情况
        if (eventDictionary.ContainsKey(name))
        {
            if ((eventDictionary[name] as EventInfo<T, U, I>).actions != null)
                //执行相关事件
                (eventDictionary[name] as EventInfo<T, U, I>).actions.Invoke(info, info2, info3);
        }
    }

    public void EventTrigger<T, U, I, O>(string name, T info, U info2, I info3, O info4)
    {
        //有没有对应的事件监听
        //有的情况
        if (eventDictionary.ContainsKey(name))
        {
            if ((eventDictionary[name] as EventInfo<T, U, I, O>).actions != null)
                //执行相关事件
                (eventDictionary[name] as EventInfo<T, U, I, O>).actions.Invoke(info, info2, info3, info4);
        }
    }
    /// <summary>
    /// 事件触发的无参重载接口
    /// </summary>
    /// <param name="name"></param>
    public void EventTrigger(string name)
    {
        //有没有对应的事件监听
        //有的情况
        if (eventDictionary.ContainsKey(name))
        {
            if ((eventDictionary[name] as EventInfo).actions != null)
                //执行相关事件
                (eventDictionary[name] as EventInfo).actions.Invoke();
        }
    }

    /// <summary>
    /// 当场景切换时，清空事件中心储存的事件
    /// </summary>
    public void Clear()
    {
        eventDictionary.Clear();
    }
}
