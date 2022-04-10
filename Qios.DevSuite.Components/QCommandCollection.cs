// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCommandCollection
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;

namespace Qios.DevSuite.Components
{
  public abstract class QCommandCollection : 
    CollectionBase,
    IQWeakEventPublisher,
    IList,
    ICollection,
    IEnumerable
  {
    private bool m_bWeakEventHandlers = true;
    private int m_iClearingCount;
    private IQCommandContainer m_oContainer;
    private QCommand m_oParentCommand;
    private QWeakDelegate m_oCollectionChangedDelegate;

    protected QCommandCollection()
    {
    }

    protected QCommandCollection(IQCommandContainer container, QCommand parentCommand)
    {
      this.m_oContainer = container;
      this.m_oParentCommand = parentCommand;
    }

    [QWeakEvent]
    public event QCommandCollectionChangedEventHandler CollectionChanged
    {
      add => this.m_oCollectionChangedDelegate = QWeakDelegate.Combine(this.m_oCollectionChangedDelegate, (Delegate) value, this.m_bWeakEventHandlers);
      remove => this.m_oCollectionChangedDelegate = QWeakDelegate.Remove(this.m_oCollectionChangedDelegate, (Delegate) value);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [DefaultValue(true)]
    public bool WeakEventHandlers
    {
      get => this.m_bWeakEventHandlers;
      set => this.m_bWeakEventHandlers = value;
    }

    [Browsable(false)]
    internal IQCommandContainer CommandContainer
    {
      get => this.m_oContainer;
      set
      {
        if (this.m_oContainer == value)
          return;
        this.m_oContainer = value;
        this.SetParentOfCommands();
      }
    }

    [Browsable(false)]
    internal QCommand ParentCommand
    {
      get => this.m_oParentCommand;
      set
      {
        if (this.m_oParentCommand == value)
          return;
        this.m_oParentCommand = value;
        this.SetParentOfCommands();
      }
    }

    private void SetParentOfCommands()
    {
      for (int index = 0; index < this.Count; ++index)
        this.GetCommand(index).SetParent(this.m_oContainer, this.m_oParentCommand);
    }

    public void AddCommand(QCommand command)
    {
      if (command == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (command)));
      int count = this.Count;
      this.OnInsert(count, (object) command);
      this.InnerList.Add((object) command);
      command.SetParent(this.m_oContainer, this.m_oParentCommand);
      this.OnInsertComplete(this.InnerList.IndexOf((object) command), (object) command);
      this.HandleCollectionChanged(count, this.Count);
    }

    protected void RemoveCommand(QCommand command)
    {
      int index = command != null ? this.InnerList.IndexOf((object) command) : throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (command)));
      this.OnRemove(index, (object) command);
      int count = this.Count;
      this.InnerList.Remove((object) command);
      command.SetParent((IQCommandContainer) null, (QCommand) null);
      this.OnRemoveComplete(index, (object) command);
      this.HandleCollectionChanged(count, this.Count);
    }

    protected void RemoveCommandAt(int index)
    {
      object command = (object) this.GetCommand(index);
      this.OnRemove(index, command);
      int count = this.Count;
      ((QCommand) this.InnerList[index]).SetParent((IQCommandContainer) null, (QCommand) null);
      this.InnerList.RemoveAt(index);
      this.HandleCollectionChanged(count, this.Count);
      this.OnRemoveComplete(index, command);
    }

    protected void InsertCommand(int index, QCommand command)
    {
      if (command == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (command)));
      this.OnInsert(index, (object) command);
      int count = this.Count;
      this.InnerList.Insert(index, (object) command);
      command.SetParent(this.m_oContainer, this.m_oParentCommand);
      this.OnInsertComplete(index, (object) command);
      this.HandleCollectionChanged(count, this.Count);
    }

    protected override void OnClear()
    {
      this.m_iClearingCount = this.Count;
      for (int index = 0; index < this.Count; ++index)
        this.GetCommand(index).SetParent((IQCommandContainer) null, (QCommand) null);
      base.OnClear();
    }

    protected override void OnClearComplete()
    {
      base.OnClearComplete();
      this.HandleCollectionChanged(this.m_iClearingCount, this.Count);
    }

    public QCommand GetCommand(int index) => (QCommand) this.InnerList[index];

    public QCommand GetCommand(string name)
    {
      int index = this.IndexOfCommand(name);
      return index >= 0 ? this.GetCommand(index) : (QCommand) null;
    }

    public void CopyTo(QCommand[] commands, int index) => ((ICollection) this).CopyTo((Array) commands, index);

    protected int IndexOfCommand(QCommand command) => this.InnerList.Contains((object) command) ? this.InnerList.IndexOf((object) command) : -1;

    protected int IndexOfCommand(string name)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (this.GetCommand(index).ItemName != null && string.Compare(this.GetCommand(index).ItemName, name, true, CultureInfo.InvariantCulture) == 0)
          return index;
      }
      return -1;
    }

    protected bool ContainsCommand(QCommand command) => this.InnerList.Contains((object) command);

    protected void SetCommandAtIndex(int index, QCommand command)
    {
      if (index >= this.InnerList.Count)
        this.AddCommand(command);
      else
        this.InsertCommand(index, command);
    }

    protected QCommand FindCommandWithRelativeName(string relativeName)
    {
      if (relativeName == null)
        return (QCommand) null;
      int length = relativeName.IndexOf(QCommand.NameSeparator);
      if (length < 0)
        return relativeName.Length <= 0 ? (QCommand) null : this.GetCommand(relativeName);
      QCommand command = this.GetCommand(relativeName.Substring(0, length));
      if (command == null)
        return (QCommand) null;
      if (length >= relativeName.Length - 1)
        return command;
      if (command.Commands == null)
        return (QCommand) null;
      string relativeName1 = relativeName.Substring(length + 1);
      return command.Commands.FindCommandWithRelativeName(relativeName1);
    }

    protected QCommand GetCommandAtPosition(Point position)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (this.GetCommand(index).Bounds.Contains(position))
          return this.GetCommand(index);
      }
      return (QCommand) null;
    }

    protected virtual void HandleCollectionChanged(int fromCount, int toCount)
    {
      this.OnCollectionChanged(new QCommandCollectionChangedEventArgs(fromCount, toCount));
      if (this.m_oParentCommand != null)
        this.m_oParentCommand.HandleChildCommandCollectionChanged(fromCount, toCount);
      if (this.m_oContainer == null)
        return;
      this.m_oContainer.HandleCommandCollectionChanged(fromCount, toCount);
    }

    protected virtual void OnCollectionChanged(QCommandCollectionChangedEventArgs e) => this.m_oCollectionChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oCollectionChangedDelegate, (object) this, (object) e);

    int IList.Add(object value)
    {
      QCommand command = value as QCommand;
      this.AddCommand(command);
      return this.IndexOfCommand(command);
    }

    void IList.Clear() => this.Clear();

    bool IList.Contains(object value) => this.ContainsCommand((QCommand) value);

    int IList.IndexOf(object value) => this.IndexOfCommand((QCommand) value);

    void IList.Insert(int index, object value) => this.InsertCommand(index, (QCommand) value);

    void IList.Remove(object value) => this.RemoveCommand((QCommand) value);

    void IList.RemoveAt(int index) => this.RemoveCommandAt(index);

    bool IList.IsReadOnly => false;

    bool IList.IsFixedSize => false;

    object IList.this[int index]
    {
      get => (object) this.GetCommand(index);
      set => this.SetCommandAtIndex(index, (QCommand) value);
    }
  }
}
