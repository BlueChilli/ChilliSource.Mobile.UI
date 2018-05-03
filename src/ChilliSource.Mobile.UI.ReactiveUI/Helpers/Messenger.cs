using System;
using System.Reactive.Linq;
using ReactiveUI;

namespace ChilliSource.Mobile.UI.ReactiveUI
{
    /// <summary>
    /// Convenience MessageBus wrapper to allow sending/receiving global messages defined as enums
    /// </summary>
    public static class Messenger
    {
        public static void Send(Enum messageType)
        {
            MessageBus.Current.SendMessage(new Message(messageType));
        }

        public static void Send<T>(Enum messageType, T parameter)
        {
            MessageBus.Current.SendMessage(new Message<T>(messageType, parameter));
        }

        public static IDisposable Listen(Enum messageType, Action action)
        {
            return MessageBus.Current.Listen<Message>()
                      .Where(arg => arg != null && arg.MessageType.Equals(messageType))
                      .Subscribe(msg => action?.Invoke());
        }

        public static IDisposable ListenOnce(Enum messageType, Action action)
        {
            return MessageBus.Current.Listen<Message>()
                      .Where(arg => arg != null && arg.MessageType.Equals(messageType))
                      .Take(1)
                      .Subscribe(msg => action?.Invoke());
        }

        public static IDisposable Listen<T>(Enum messageType, Action<T> action)
        {
            return MessageBus.Current.Listen<Message<T>>()
                      .Where(arg => arg != null && arg.MessageType.Equals(messageType))
                      .Subscribe(msg => action?.Invoke(msg.Parameter));
        }

        public static IDisposable ListenOnce<T>(Enum messageType, Action<T> action)
        {
            return MessageBus.Current.Listen<Message<T>>()
                      .Where(arg => arg != null && arg.MessageType.Equals(messageType))
                      .Take(1)
                      .Subscribe(msg => action?.Invoke(msg.Parameter));
        }

        class Message
        {
            public Message(Enum messageType)
            {
                MessageType = messageType;
            }
            public Enum MessageType { get; protected set; }
        };

        class Message<T> : Message
        {
            public Message(Enum messageType, T parameter) : base(messageType)
            {
                Parameter = parameter;
            }

            public T Parameter { get; protected set; }
        };
    }
}
