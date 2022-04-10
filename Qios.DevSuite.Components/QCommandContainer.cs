// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCommandContainer
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  public abstract class QCommandContainer : QControl, IQCommandContainer
  {
    private IWin32Window m_oOwnerWindow;
    private bool m_bPerformingLayout;
    private QCommand m_oParentCommand;
    private IQCommandContainer m_oCustomParentContainer;
    private QCommandCollection m_oCommands;
    private int m_iFirstShownCommand;
    private int m_iLastShownCommand = -1;
    private int m_iSuspendChanges;

    protected QCommandContainer() => this.InternalConstruct();

    protected QCommandContainer(IQCommandContainer customParentContainer)
    {
      this.m_oCustomParentContainer = customParentContainer;
      this.InternalConstruct();
    }

    protected QCommandContainer(QCommand parentCommand, QCommandCollection commands)
    {
      this.m_oParentCommand = parentCommand;
      this.m_oCommands = commands;
      this.InternalConstruct();
    }

    private void InternalConstruct()
    {
      if (this.m_oCommands == null)
        return;
      this.m_oCommands.CommandContainer = (IQCommandContainer) this;
    }

    [Browsable(false)]
    public string FullName => this.ParentCommand != null ? this.ParentCommand.FullName : (string) null;

    [Browsable(false)]
    public virtual QCommandCollection Commands
    {
      get
      {
        if (this.m_oCommands == null)
          this.m_oCommands = this.CreateCommandCollection();
        return this.m_oCommands;
      }
    }

    [Browsable(false)]
    public QCommand ParentCommand => this.m_oParentCommand;

    [DefaultValue(null)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IQCommandContainer CustomParentCommandContainer
    {
      get => this.m_oCustomParentContainer;
      set => this.m_oCustomParentContainer = value;
    }

    [Browsable(false)]
    public IQCommandContainer ParentCommandContainer => this.m_oParentCommand != null && this.m_oParentCommand.ParentContainer != null ? this.m_oParentCommand.ParentContainer : this.m_oCustomParentContainer;

    [Browsable(false)]
    public bool PerformingLayout => this.m_bPerformingLayout;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public virtual IWin32Window OwnerWindow
    {
      get => this.ParentForm != null ? (IWin32Window) this.ParentForm : this.m_oOwnerWindow;
      set
      {
        if (this.m_oOwnerWindow == value)
          return;
        this.m_oOwnerWindow = value;
        this.SetOwnerWindowCore();
        for (int index = 0; index < this.Commands.Count; ++index)
        {
          QCommand command = this.Commands.GetCommand(index);
          if (command.ChildContainerCreated)
            command.ChildContainer.OwnerWindow = value;
        }
      }
    }

    [Browsable(false)]
    public bool ChangesSuspended => this.m_iSuspendChanges > 0;

    public virtual void SuspendChanges() => ++this.m_iSuspendChanges;

    public void ResumeChanges(QCommandUIRequest updateRequest)
    {
      this.m_iSuspendChanges = Math.Max(this.m_iSuspendChanges - 1, 0);
      if (this.m_iSuspendChanges != 0 || updateRequest == QCommandUIRequest.None)
        return;
      this.HandleCommandChanged(updateRequest, (QCommand) null);
    }

    protected virtual void SetOwnerWindowCore()
    {
    }

    Rectangle IQCommandContainer.Bounds
    {
      get => this.Bounds;
      set => this.Bounds = value;
    }

    bool IQCommandContainer.IsDisposed => this.IsDisposed;

    public new Rectangle Bounds
    {
      get => base.Bounds;
      set => base.Bounds = value;
    }

    public new bool IsDisposed => base.IsDisposed;

    [Browsable(false)]
    internal int FirstShownCommand
    {
      get
      {
        if (this.m_iFirstShownCommand < this.Commands.Count)
          return this.m_iFirstShownCommand;
        return this.Commands.Count > 0 ? this.Commands.Count - 1 : 0;
      }
    }

    [Browsable(false)]
    internal int LastShownCommand => this.m_iLastShownCommand >= 0 && this.m_iLastShownCommand < this.Commands.Count ? this.m_iLastShownCommand : this.Commands.Count - 1;

    public virtual void HandleCommandCollectionChanged(int fromCount, int toCount)
    {
      if (this.PerformingLayout)
        return;
      this.PerformLayout((Control) null, (string) null);
      this.Refresh();
    }

    [Obsolete("Obsolete since Qios.DevSuite 2.0. Use the overload with the sender")]
    public virtual void HandleCommandChanged(QCommandUIRequest changeRequest) => this.HandleCommandChanged(changeRequest, (QCommand) null);

    public virtual void HandleCommandChanged(QCommandUIRequest changeRequest, QCommand sender)
    {
      if (this.ChangesSuspended || this.PerformingLayout)
        return;
      if (changeRequest == QCommandUIRequest.PerformLayout)
      {
        this.PerformLayout((Control) null, (string) null);
        this.Refresh();
      }
      else
      {
        if (changeRequest != QCommandUIRequest.Redraw)
          return;
        this.Refresh();
      }
    }

    internal void PutFirstShownCommand(int index) => this.m_iFirstShownCommand = index;

    internal void PutLastShownCommand(int index) => this.m_iLastShownCommand = index;

    public Control RetrieveTopmostControl()
    {
      if (this.ParentCommand != null && this.ParentCommand.ParentContainer != null)
        return this.ParentCommand.ParentContainer.RetrieveTopmostControl();
      if (this.Parent == null)
        return (Control) this;
      Control parent = this.Parent;
      while (parent.Parent != null)
        parent = parent.Parent;
      return parent;
    }

    public virtual bool ContainsOrIsContainerWithHandle(IntPtr handle)
    {
      if (handle == IntPtr.Zero || !this.IsHandleCreated)
        return false;
      if (this.Handle == handle)
        return true;
      for (int index = 0; index < this.Commands.Count; ++index)
      {
        QCommand command = this.Commands.GetCommand(index);
        if (command.ChildContainerCreated && command.ChildContainer.ContainsOrIsContainerWithHandle(handle))
          return true;
      }
      return false;
    }

    protected abstract QCommandCollection CreateCommandCollection();

    protected void PutPerformingLayout(bool performingLayout) => this.m_bPerformingLayout = performingLayout;
  }
}
