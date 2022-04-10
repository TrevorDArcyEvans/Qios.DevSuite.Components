// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QWeakEventConsumer
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Reflection;

namespace Qios.DevSuite.Components
{
  internal class QWeakEventConsumer
  {
    private QWeakDelegate m_oTargetDelegate;
    private EventInfo m_oSourceEventInfo;
    private object m_oSourceObject;
    private Delegate m_oAttachedDelegate;

    public QWeakEventConsumer(Delegate targetDelegate, object sourceObject, string sourceEvent)
    {
      if ((object) targetDelegate == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (targetDelegate)));
      if (sourceObject == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (sourceObject)));
      EventInfo eventInfo = !QMisc.IsEmpty((object) sourceEvent) ? sourceObject.GetType().GetEvent(sourceEvent, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic) : throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (sourceEvent)));
      bool flag = eventInfo != null ? this.IsWeakEvent(sourceObject, eventInfo) : throw new InvalidOperationException(QResources.GetException("General_ReturnValueNull", (object) ("GetEvent(\"" + sourceEvent + "\"")));
      this.m_oTargetDelegate = new QWeakDelegate(targetDelegate, !flag);
      this.m_oAttachedDelegate = this.CreateAttachingDelegate();
      this.m_oSourceObject = sourceObject;
      this.m_oSourceEventInfo = eventInfo;
      this.m_oSourceEventInfo.AddEventHandler(this.m_oSourceObject, this.m_oAttachedDelegate);
    }

    protected virtual Delegate CreateAttachingDelegate() => (Delegate) new EventHandler(this.MethodInvoked);

    public QWeakDelegate TargetDelegate => this.m_oTargetDelegate;

    public object SourceObject => this.m_oSourceObject;

    public EventInfo SourceEventInfo => this.m_oSourceEventInfo;

    public Delegate AttachedDelegate => this.m_oAttachedDelegate;

    public void DetachEvent() => this.m_oSourceEventInfo.RemoveEventHandler(this.m_oSourceObject, this.m_oAttachedDelegate);

    private bool IsWeakEvent(object sourceObject, EventInfo eventInfo)
    {
      if (!(sourceObject is IQWeakEventPublisher qweakEventPublisher) || !qweakEventPublisher.WeakEventHandlers)
        return false;
      object[] customAttributes = eventInfo.GetCustomAttributes(typeof (QWeakEventAttribute), true);
      return customAttributes != null && customAttributes.Length > 0;
    }

    protected void InvokeTarget(params object[] args) => QWeakDelegate.InvokeDelegate(this.m_oTargetDelegate, args);

    private void MethodInvoked(object sender, EventArgs e)
    {
      if (!this.m_oTargetDelegate.TargetIsFinalized)
        this.InvokeTarget(sender, (object) e);
      else
        this.DetachEvent();
    }
  }
}
