﻿using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventTrigger))]
public class DayToggle : Toggle {

    private DateTime? m_dateTime;
    public DateTime? dateTime
    {
        get
        {
            return m_dateTime;
        }
        set
        {
            m_dateTime = value;
            var todayMarker = GetTodayMarker();
            if (todayMarker != null)
            {
                GetTodayMarker().gameObject.SetActive(m_dateTime != null && DateTime.Today.IsSameDate((DateTime)m_dateTime));
            }
            
        }
    }

    public class OnDateTimeSelectedEvent : UnityEvent<DateTime?> { }
    public OnDateTimeSelectedEvent onDateSelected = new OnDateTimeSelectedEvent();
    bool m_clicked = false;

    private Image GetTodayMarker()
    {
        Component[] transforms = GetComponentsInChildren(typeof(Transform), true);

        foreach (Transform transform in transforms)
        {
            if (transform.gameObject.name == "Today Marker")
            {
                return transform.gameObject.GetComponent<Image>();
            }
        }
        return null;
    }
    protected override void Start()
    {
        base.Start();

        onValueChanged.AddListener(onToggleValueChanged);
        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((data) => { OnPointerDownDelegate((PointerEventData)data); });
        trigger.triggers.Add(entry);
    }

    private void OnPointerDownDelegate(PointerEventData data)
    {
        if (isActiveAndEnabled && IsInteractable())
        {
            m_clicked = true;
            onDateSelected.Invoke(dateTime);
        }
    }

    public void onToggleValueChanged(bool value)
    {
        AllowSwitchOffHack(value);
    }

    void AllowSwitchOffHack(bool value)
    {
        if (m_clicked && !value)
        {
            m_clicked = false;
            isOn = true;
        }
    }


    public void SetText(string text)
    {
        GetComponentInChildren<Text>().text = text;
    }

    public void ClearText()
    {
        SetText(string.Empty);
    }
}
