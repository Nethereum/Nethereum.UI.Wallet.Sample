using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MvvmCross.ViewModels;
using ReactiveUI;
using INotifyPropertyChanging = ReactiveUI.INotifyPropertyChanging;
using PropertyChangingEventArgs = ReactiveUI.PropertyChangingEventArgs;
using PropertyChangingEventHandler = ReactiveUI.PropertyChangingEventHandler;

/*

Original source from https://github.com/aritchie/mvxgoodies
Pull down to allow using the latest version of MVVMCross and ReactiveUI 
JB: Added extra  public class MvxReactiveObject<T>
All credit to https://github.com/aritchie
 Microsoft Public License (MS-PL)


This license governs use of the accompanying software. If you use the software, you
accept this license. If you do not accept the license, do not use the software.


1. Definitions
The terms "reproduce," "reproduction," "derivative works," and "distribution" have the
same meaning here as under U.S. copyright law.
A "contribution" is the original software, or any additions or changes to the software.
A "contributor" is any person that distributes its contribution under this license.
"Licensed patents" are a contributor's patent claims that read directly on its contribution.


2. Grant of Rights
(A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution or any derivative works that you create.
(B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution in the software or derivative works of the contribution in the software.


3. Conditions and Limitations
(A) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.
(B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your patent license from such contributor to the software ends automatically.
(C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution notices that are present in the software.
(D) If you distribute any portion of the software in source code form, you may do so only under this license by including a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or object code form, you may only do so under a license that complies with this license.
(E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a particular purpose and non-infringement.

 */



namespace MvvmCross.ReactiveUI.Interop
{

    public class DisposableAction : IDisposable
    {
        readonly Action action;

        public DisposableAction(Action action)
        {
            this.action = action;
        }


        public void Dispose()
        {
            this.action();
        }
    }

    public class MvxReactiveObject : ReactiveObject
    {
    }


    public abstract class MvxReactiveViewModel<TParam> : MvxViewModel<TParam>, IReactiveNotifyPropertyChanged<IReactiveObject>,
        IReactiveObject, INotifyPropertyChanged, INotifyPropertyChanging
    {
        private readonly MvxReactiveObject reactiveObj = new MvxReactiveObject();
        private bool suppressNpc;

        protected override MvxInpcInterceptionResult InterceptRaisePropertyChanged(PropertyChangedEventArgs changedArgs)
        {
            if (this.suppressNpc)
                return MvxInpcInterceptionResult.DoNotRaisePropertyChanged;
            return base.InterceptRaisePropertyChanged(changedArgs);
        }

        public virtual IDisposable SuppressChangeNotifications()
        {
            this.suppressNpc = true;
            IDisposable suppressor = this.reactiveObj.SuppressChangeNotifications();
            return (IDisposable)new DisposableAction((Action)(() =>
            {
                this.suppressNpc = false;
                suppressor.Dispose();
            }));
        }

        public virtual void RaisePropertyChanging(PropertyChangingEventArgs args)
        {
            this.reactiveObj.RaisePropertyChanging<MvxReactiveObject>(args.PropertyName);
        }

        public IObservable<IReactivePropertyChangedEventArgs<IReactiveObject>> Changing => this.reactiveObj.Changing;

        public IObservable<IReactivePropertyChangedEventArgs<IReactiveObject>> Changed => this.reactiveObj.Changed;

        public event PropertyChangingEventHandler PropertyChanging
        {
            add { this.reactiveObj.PropertyChanging += value; }
            remove { this.reactiveObj.PropertyChanging -= value; }
        }

        public new bool SetProperty<TStore>(ref TStore storage, TStore value, [CallerMemberName] string propertyName = null)
        {
            TStore x = storage;
            IReactiveObjectExtensions.RaiseAndSetIfChanged(this, ref storage, value,
                propertyName);
            return !EqualityComparer<TStore>.Default.Equals(x, value);
        }
    }

    public abstract class MvxReactiveViewModel<T, TReturn> : MvxViewModel<T, TReturn>, IReactiveNotifyPropertyChanged<IReactiveObject>,
        IReactiveObject, INotifyPropertyChanged, INotifyPropertyChanging
    {
        private readonly MvxReactiveObject reactiveObj = new MvxReactiveObject();
        private bool suppressNpc;

        protected override MvxInpcInterceptionResult InterceptRaisePropertyChanged(PropertyChangedEventArgs changedArgs)
        {
            if (this.suppressNpc)
                return MvxInpcInterceptionResult.DoNotRaisePropertyChanged;
            return base.InterceptRaisePropertyChanged(changedArgs);
        }

        public virtual IDisposable SuppressChangeNotifications()
        {
            this.suppressNpc = true;
            IDisposable suppressor = this.reactiveObj.SuppressChangeNotifications();
            return (IDisposable)new DisposableAction((Action)(() =>
            {
                this.suppressNpc = false;
                suppressor.Dispose();
            }));
        }

        public virtual void RaisePropertyChanging(PropertyChangingEventArgs args)
        {
            this.reactiveObj.RaisePropertyChanging<MvxReactiveObject>(args.PropertyName);
        }

        public IObservable<IReactivePropertyChangedEventArgs<IReactiveObject>> Changing => this.reactiveObj.Changing;

        public IObservable<IReactivePropertyChangedEventArgs<IReactiveObject>> Changed => this.reactiveObj.Changed;

        public event PropertyChangingEventHandler PropertyChanging
        {
            add { this.reactiveObj.PropertyChanging += value; }
            remove { this.reactiveObj.PropertyChanging -= value; }
        }

        public new bool SetProperty<TStore>(ref TStore storage, TStore value, [CallerMemberName] string propertyName = null)
        {
            TStore x = storage;
            IReactiveObjectExtensions.RaiseAndSetIfChanged(this, ref storage, value,
                propertyName);
            return !EqualityComparer<TStore>.Default.Equals(x, value);
        }
    }

    public abstract class MvxReactiveViewModel : MvxViewModel, IReactiveNotifyPropertyChanged<IReactiveObject>, IReactiveObject
    {
        readonly MvxReactiveObject reactiveObj = new MvxReactiveObject();
        bool suppressNpc;


        protected override MvxInpcInterceptionResult InterceptRaisePropertyChanged(PropertyChangedEventArgs changedArgs)
        {
            if (this.suppressNpc)
                return MvxInpcInterceptionResult.DoNotRaisePropertyChanged;

            return base.InterceptRaisePropertyChanged(changedArgs);
        }


        public virtual IDisposable SuppressChangeNotifications()
        {
            this.suppressNpc = true;
            var suppressor = this.reactiveObj.SuppressChangeNotifications();

            return new DisposableAction(() =>
            {
                this.suppressNpc = false;
                suppressor.Dispose();
            });
        }


        public virtual void RaisePropertyChanging(PropertyChangingEventArgs args)
        {
            this.reactiveObj.RaisePropertyChanging(args.PropertyName);
        }


        public IObservable<IReactivePropertyChangedEventArgs<IReactiveObject>> Changing => this.reactiveObj.Changing;
        public IObservable<IReactivePropertyChangedEventArgs<IReactiveObject>> Changed => this.reactiveObj.Changed;


        public event PropertyChangingEventHandler PropertyChanging
        {
            add { this.reactiveObj.PropertyChanging += value; }
            remove { this.reactiveObj.PropertyChanging -= value; }
        }

        public new bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            var original = storage;
            IReactiveObjectExtensions.RaiseAndSetIfChanged(this, ref storage, value, propertyName);

            return !EqualityComparer<T>.Default.Equals(original, value);
        }
    }

}
