﻿using Macro.Infrastructure;
using Macro.Infrastructure.Serialize;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using Utils.Infrastructure;

namespace Macro.Models
{
    [Serializable]
    public abstract class BaseEventTriggerModel<T> : INotifyPropertyChanged
    {
        protected EventType _eventType = EventType.Image;
        protected MouseTriggerInfo _mouseTriggerInfo;
        protected string _keyboardCmd = "";
        protected ProcessInfo _processInfo;
        protected ObservableCollection<T> _subEventTriggers;
        protected int _afterDelay;
        protected RepeatInfoModel _repeatInfo;
        protected ulong _eventToNext = 0;
        protected ulong _triggerIndex = 0;

        [Order(1)]
        public Bitmap Image { get; set; }

        [Order(2)]
        public EventType EventType
        {
            get => _eventType;
            set
            {
                _eventType = value;
                OnPropertyChanged("EventType");
                OnPropertyChanged("Desc");
            }
        }

        [Order(3)]
        public MouseTriggerInfo MouseTriggerInfo
        {
            get => _mouseTriggerInfo ?? (_mouseTriggerInfo = new MouseTriggerInfo());
            set
            {
                _mouseTriggerInfo = value;
                OnPropertyChanged("MouseTriggerInfo");
                OnPropertyChanged("Desc");
            }
        }

        [Order(4)]
        public MonitorInfo MonitorInfo { get; set; }

        [Order(5)]
        public string KeyboardCmd
        {
            get => _keyboardCmd;
            set
            {
                _keyboardCmd = value;
                OnPropertyChanged("KeyboardCmd");
                OnPropertyChanged("Desc");
            }
        }

        [Order(6)]
        public ProcessInfo ProcessInfo
        {
            get => _processInfo;
            set
            {
                _processInfo = value;
                OnPropertyChanged("ProcessInfo");
            }
        }

        [Order(7)]
        public ObservableCollection<T> SubEventTriggers
        {
            get => _subEventTriggers ?? (_subEventTriggers = new ObservableCollection<T>());
            set
            {
                _subEventTriggers = value;
                OnPropertyChanged("SubEventTriggers");
            }
        }

        [Order(8)]
        public int AfterDelay
        {
            get => _afterDelay;
            set
            {
                _afterDelay = value;
                OnPropertyChanged("AfterDelay");
            }
        }
        [Order(9)]
        public RepeatInfoModel RepeatInfo
        {
            get => _repeatInfo ?? (_repeatInfo = new RepeatInfoModel());
            set
            {
                _repeatInfo = value;
                OnPropertyChanged("RepeatInfo");
            }
        }
        [Order(10)]
        public ulong TriggerIndex
        {
            set
            {
                _triggerIndex = value;
                OnPropertyChanged("TriggerIndex");
            }
            get => _triggerIndex;
        }

        [Order(11)]
        public ulong EventToNext
        {
            set
            {
                _eventToNext = value;
                OnPropertyChanged("EventToNext");
            }
            get => _eventToNext;
        }
        public string Desc
        {
            get
            {
                if (EventType == EventType.Mouse)
                {
                    if (MouseTriggerInfo.MouseInfoEventType != MouseEventType.Drag && MouseTriggerInfo.MouseInfoEventType != MouseEventType.None && MouseTriggerInfo.MouseInfoEventType != MouseEventType.Wheel)
                    {
                        return $"X : { MouseTriggerInfo.StartPoint.X.ToString()} Y : {MouseTriggerInfo.StartPoint.Y.ToString() }";
                    }
                    else if (MouseTriggerInfo.MouseInfoEventType == MouseEventType.None)
                    {
                        return "";
                    }
                    else if (MouseTriggerInfo.MouseInfoEventType == MouseEventType.Wheel)
                    {
                        if (MouseTriggerInfo.WheelData > 0)
                        {
                            return $"Wheel Up";
                        }
                        else
                        {
                            return $"Wheel Down";
                        }
                    }
                    else
                    {
                        return $"X : { MouseTriggerInfo.StartPoint.X.ToString("0")} Y : {MouseTriggerInfo.StartPoint.Y.ToString("0") }\r\n" +
                            $"X : { MouseTriggerInfo.EndPoint.X.ToString("0")} Y : {MouseTriggerInfo.EndPoint.Y.ToString("0") }";
                    }
                }
                else if (EventType == EventType.Keyboard)
                {
                    return KeyboardCmd;
                }
                else if (EventType == EventType.RelativeToImage)
                {
                    return $"X : { MouseTriggerInfo.StartPoint.X.ToString()} Y : {MouseTriggerInfo.StartPoint.Y.ToString() }";
                }
                else
                {
                    return "";
                }
            }
        }

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}
