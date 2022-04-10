// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QControlListenerManager
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Collections;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  internal sealed class QControlListenerManager
  {
    private static QControlListenerManager.QControlListenerCollection m_oListeners;

    public static void Attach(IQControlListenerClient client, Control control)
    {
      if (QControlListenerManager.m_oListeners == null)
        QControlListenerManager.m_oListeners = new QControlListenerManager.QControlListenerCollection();
      QControlListenerManager.QControlListener listener = QControlListenerManager.m_oListeners.FindListener(control);
      if (listener != null)
        listener.AddClient(client);
      else
        QControlListenerManager.m_oListeners.Add(new QControlListenerManager.QControlListener(client, control));
    }

    public static void Detach(IQControlListenerClient client)
    {
      if (QControlListenerManager.m_oListeners == null)
        return;
      for (int index = 0; index < QControlListenerManager.m_oListeners.Count; ++index)
        QControlListenerManager.m_oListeners[index].RemoveClient(client);
    }

    public static void Detach(IQControlListenerClient client, Control control)
    {
      if (QControlListenerManager.m_oListeners == null)
        return;
      for (int index = 0; index < QControlListenerManager.m_oListeners.Count; ++index)
      {
        if (QControlListenerManager.m_oListeners[index].Control == control)
        {
          QControlListenerManager.m_oListeners[index].RemoveClient(client);
          break;
        }
      }
    }

    private static void RemoveListener(QControlListenerManager.QControlListener listener)
    {
      if (QControlListenerManager.m_oListeners == null)
        return;
      QControlListenerManager.m_oListeners.Remove(listener);
    }

    private class QControlListenerCollection : CollectionBase
    {
      public void Add(QControlListenerManager.QControlListener listener) => this.InnerList.Add((object) listener);

      public void Remove(QControlListenerManager.QControlListener listener) => this.InnerList.Remove((object) listener);

      public QControlListenerManager.QControlListener this[int index] => this.InnerList[index] as QControlListenerManager.QControlListener;

      public QControlListenerManager.QControlListener FindListener(
        Control control)
      {
        for (int index = 0; index < this.Count; ++index)
        {
          if (this[index].Control == control)
            return this[index];
        }
        return (QControlListenerManager.QControlListener) null;
      }
    }

    private class QControlListener : NativeWindow
    {
      private bool m_bAssigned;
      private WeakReference m_oControlReference;
      private QWeakEventConsumerCollection m_oEventConsumers;
      private QControlListenerManager.QControlListener.QControlListenerClientCollection m_oClientCollection;

      public QControlListener(IQControlListenerClient client, Control control)
      {
        this.m_oControlReference = new WeakReference((object) control);
        this.m_oEventConsumers = new QWeakEventConsumerCollection();
        this.m_oEventConsumers.Add(new QWeakEventConsumer((Delegate) new EventHandler(this.Control_Disposed), (object) control, "Disposed"));
        this.m_oEventConsumers.Add(new QWeakEventConsumer((Delegate) new EventHandler(this.Control_HandleCreated), (object) control, "HandleCreated"));
        this.m_oEventConsumers.Add(new QWeakEventConsumer((Delegate) new EventHandler(this.Control_HandleDestroyed), (object) control, "HandleDestroyed"));
        this.m_oClientCollection = new QControlListenerManager.QControlListener.QControlListenerClientCollection();
        this.m_oClientCollection.Add(client);
        this.AssignHandle();
      }

      public Control Control => this.m_oControlReference.IsAlive ? this.m_oControlReference.Target as Control : (Control) null;

      protected override void WndProc(ref Message m)
      {
        for (int index = 0; index < this.m_oClientCollection.Count; ++index)
        {
          if (this.m_oClientCollection[index].HandleMessage(ref m))
            return;
        }
        base.WndProc(ref m);
      }

      public void AddClient(IQControlListenerClient client)
      {
        if (this.m_oClientCollection.Contains(client))
          return;
        this.m_oClientCollection.Add(client);
      }

      public void RemoveClient(IQControlListenerClient client)
      {
        this.m_oClientCollection.Remove(client);
        if (this.m_oClientCollection.Count != 0)
          return;
        this.m_oEventConsumers.DetachAndRemoveAll();
        QControlListenerManager.RemoveListener(this);
      }

      private void AssignHandle()
      {
        if (this.Control == null || !this.Control.IsHandleCreated || this.m_bAssigned)
          return;
        this.AssignHandle(this.Control.Handle);
        this.m_bAssigned = true;
      }

      public override void ReleaseHandle()
      {
        if (!this.m_bAssigned)
          return;
        base.ReleaseHandle();
        this.m_bAssigned = false;
      }

      private void Control_Disposed(object sender, EventArgs e)
      {
        this.m_oClientCollection.Clear();
        QControlListenerManager.RemoveListener(this);
      }

      private void Control_HandleDestroyed(object sender, EventArgs e) => this.ReleaseHandle();

      private void Control_HandleCreated(object sender, EventArgs e)
      {
        this.ReleaseHandle();
        this.AssignHandle();
      }

      private class QControlListenerClientCollection : CollectionBase
      {
        public IQControlListenerClient this[int index] => this.InnerList[index] as IQControlListenerClient;

        public bool Contains(IQControlListenerClient client) => this.InnerList.Contains((object) client);

        public void Add(IQControlListenerClient client)
        {
          if (this.InnerList.Contains((object) client))
            return;
          this.InnerList.Add((object) client);
        }

        public void Remove(IQControlListenerClient client)
        {
          if (!this.InnerList.Contains((object) client))
            return;
          this.InnerList.Remove((object) client);
        }
      }
    }
  }
}
