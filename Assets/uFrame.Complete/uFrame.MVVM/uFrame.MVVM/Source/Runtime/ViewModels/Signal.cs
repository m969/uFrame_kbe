using System;
using uFrame.Kernel;
using uFrame.MVVM.Events;
using UniRx;

namespace uFrame.MVVM.ViewModels
{
    public class Signal<TClass> : ISubject<TClass>, ISignal<TClass> where TClass : IViewModelCommand, new()
    {

        private readonly SimpleSubject<TClass> _signalSubject = new SimpleSubject<TClass>();
        private readonly ViewModel _viewModel;
        private Action<TClass> _action;

        public Signal(ViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void OnCompleted()
        {
            _signalSubject.OnCompleted();
        }

        public void OnError(Exception error)
        {
            _signalSubject.OnError(error);
        }

        public void OnNext(TClass value)
        {
            value.Sender = _viewModel;

            //uFrame_kbe
            if (Action != null)
            {
                if (System.Threading.Thread.CurrentThread.ManagedThreadId == 1)
                {
                    Action(value);
                    Action.Invoke(value);
                }
                else
                {
                    uFrameKernel.EventAggregator.Publish(new ViewModelCommandExcuteEvent()
                    {
                        Action = Action,
                        Command = value
                    });
                }
            }
            //uFrame_kbe

            _signalSubject.OnNext(value);

        }

        public IDisposable Subscribe(IObserver<TClass> observer)
        {
            return _signalSubject.Subscribe(observer);
        }

        public Type SignalType
        {
            get { return typeof (TClass); }
        }

        public Action<TClass> Action
        {
            get { return _action; }
            set { _action = value; }
        }

        void ISignal.Publish(object data)
        {
            OnNext((TClass) data);
        }

        public void Publish(TClass data)
        {
            OnNext(data);
        }

        public void Publish()
        {
            OnNext(new TClass()
            {
                Sender = _viewModel
            });
        }
    }
}