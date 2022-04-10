// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QWeakDelegate
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Qios.DevSuite.Components
{
  internal class QWeakDelegate
  {
    private QWeakDelegate m_oPrevious;
    private bool m_bIsWeak;
    private bool m_bIsStatic;
    private bool m_bIsRemoved;
    private object m_oStrongTargetObject;
    private GCHandle m_oTargetHandle;
    private MethodInfo m_oMethod;

    internal QWeakDelegate(Delegate delegateInstance, bool weak)
    {
      this.m_bIsWeak = weak;
      if (delegateInstance.Method.IsStatic)
      {
        this.m_bIsStatic = true;
        this.m_oMethod = delegateInstance.Method;
      }
      else
      {
        if (!this.m_bIsWeak)
          this.m_oStrongTargetObject = delegateInstance.Target;
        this.m_oMethod = delegateInstance.Method;
        this.m_oTargetHandle = GCHandle.Alloc(delegateInstance.Target, GCHandleType.Weak);
      }
    }

    public object Target => !this.m_oTargetHandle.IsAllocated ? (object) null : this.m_oTargetHandle.Target;

    public bool IsStatic => this.m_bIsStatic;

    public bool IsWeak => this.m_bIsWeak;

    public bool TargetIsFinalized
    {
      get
      {
        if (this.m_bIsStatic)
          return false;
        return !this.m_oTargetHandle.IsAllocated || this.m_oTargetHandle.Target == null;
      }
    }

    private void Invoke(params object[] args)
    {
      while (this.m_oPrevious != null && (this.m_oPrevious.TargetIsFinalized || this.m_oPrevious.m_bIsRemoved))
        this.m_oPrevious = this.m_oPrevious.m_oPrevious;
      if (this.m_oPrevious != null)
        this.m_oPrevious.Invoke(args);
      if (this.m_bIsStatic)
      {
        this.m_oMethod.Invoke((object) null, args);
      }
      else
      {
        if (this.Target == null)
          return;
        this.m_oMethod.Invoke(this.Target, args);
      }
    }

    public static QWeakDelegate InvokeDelegate(
      QWeakDelegate weakDelegate,
      params object[] args)
    {
      while (weakDelegate != null && (weakDelegate.TargetIsFinalized || weakDelegate.m_bIsRemoved))
        weakDelegate = weakDelegate.m_oPrevious;
      weakDelegate?.Invoke(args);
      while (weakDelegate != null && (weakDelegate.TargetIsFinalized || weakDelegate.m_bIsRemoved))
        weakDelegate = weakDelegate.m_oPrevious;
      return weakDelegate;
    }

    public override int GetHashCode()
    {
      int num = 0;
      if (!this.m_bIsStatic)
        num ^= this.Target != null ? this.Target.GetHashCode() : 0;
      return num ^ this.m_oMethod.GetHashCode() ^ this.m_bIsStatic.GetHashCode() ^ this.m_bIsWeak.GetHashCode();
    }

    public override bool Equals(object obj) => obj is QWeakDelegate qweakDelegate && qweakDelegate.Target == this.Target && qweakDelegate.m_bIsStatic == this.m_bIsStatic && qweakDelegate.m_bIsWeak == this.m_bIsWeak && this.m_oMethod.Equals((object) qweakDelegate.m_oMethod);

    public bool EqualsDelegate(Delegate value) => value.Target == this.Target && value.Method.Equals((object) this.m_oMethod);

    public static QWeakDelegate Combine(
      QWeakDelegate weakDelegate,
      Delegate delegateValue,
      bool weak)
    {
      while (weakDelegate != null && weakDelegate.TargetIsFinalized)
        weakDelegate = weakDelegate.m_oPrevious;
      Delegate[] invocationList = delegateValue.GetInvocationList();
      QWeakDelegate qweakDelegate1 = weakDelegate;
      QWeakDelegate qweakDelegate2 = (QWeakDelegate) null;
      for (int index = 0; index < invocationList.Length; ++index)
      {
        qweakDelegate2 = new QWeakDelegate(invocationList[index], weak);
        qweakDelegate2.m_oPrevious = qweakDelegate1;
        qweakDelegate1 = qweakDelegate2;
      }
      return qweakDelegate2;
    }

    public static QWeakDelegate RemoveFinalizedTargets(QWeakDelegate weakDelegate)
    {
      if (weakDelegate == null)
        return (QWeakDelegate) null;
      QWeakDelegate qweakDelegate1 = weakDelegate;
      QWeakDelegate qweakDelegate2 = (QWeakDelegate) null;
      QWeakDelegate qweakDelegate3 = (QWeakDelegate) null;
      for (; qweakDelegate1 != null; qweakDelegate1 = qweakDelegate1.m_oPrevious)
      {
        bool flag = false;
        if (!qweakDelegate1.m_bIsStatic && (!qweakDelegate1.m_oTargetHandle.IsAllocated || qweakDelegate1.m_oTargetHandle.Target == null))
        {
          if (qweakDelegate2 != null)
            qweakDelegate2.m_oPrevious = qweakDelegate1.m_oPrevious;
          flag = true;
          qweakDelegate1.m_bIsRemoved = true;
        }
        if (!flag)
        {
          qweakDelegate2 = qweakDelegate1;
          if (qweakDelegate3 == null)
            qweakDelegate3 = qweakDelegate1;
        }
      }
      return qweakDelegate3;
    }

    public static QWeakDelegate Remove(
      QWeakDelegate weakDelegate,
      Delegate delegateValue)
    {
      if (weakDelegate == null)
        return (QWeakDelegate) null;
      weakDelegate = QWeakDelegate.RemoveFinalizedTargets(weakDelegate);
      Delegate[] invocationList = delegateValue.GetInvocationList();
      QWeakDelegate qweakDelegate1 = weakDelegate;
      QWeakDelegate qweakDelegate2 = (QWeakDelegate) null;
      QWeakDelegate qweakDelegate3 = (QWeakDelegate) null;
      for (; qweakDelegate1 != null; qweakDelegate1 = qweakDelegate1.m_oPrevious)
      {
        bool flag = false;
        for (int index = 0; index < invocationList.Length && !flag; ++index)
        {
          if (qweakDelegate1.EqualsDelegate(invocationList[index]))
          {
            if (qweakDelegate2 != null)
              qweakDelegate2.m_oPrevious = qweakDelegate1.m_oPrevious;
            flag = true;
            qweakDelegate1.m_bIsRemoved = true;
          }
        }
        if (!flag)
        {
          qweakDelegate2 = qweakDelegate1;
          if (qweakDelegate3 == null)
            qweakDelegate3 = qweakDelegate1;
        }
      }
      return qweakDelegate3;
    }

    ~QWeakDelegate()
    {
      if (!this.m_oTargetHandle.IsAllocated)
        return;
      this.m_oTargetHandle.Free();
    }
  }
}
