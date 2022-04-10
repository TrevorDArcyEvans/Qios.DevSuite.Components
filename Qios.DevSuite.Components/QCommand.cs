// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCommand
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  [DesignTimeVisible(false)]
  public abstract class QCommand : IComponent, IDisposable, ICloneable, IQWeakEventPublisher
  {
    private bool m_bWeakEventHandlers = true;
    private string m_sItemName;
    private string m_sDesignName;
    private Rectangle m_oBounds = Rectangle.Empty;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QCommand m_oParentCommand;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private IQCommandContainer m_oParentContainer;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private IQCommandContainer m_oChildContainer;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QCommandCollection m_oCommands;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private Hashtable m_aAdditionalProperties;
    private bool m_bIsDisposed;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private ISite m_oSite;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QWeakDelegate m_oDisposedDelegate;
    private static char m_cNameSeparator = '/';

    protected QCommand()
    {
    }

    protected QCommand(IContainer container)
    {
      if (container == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (container)));
      container.Add((IComponent) this);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [DefaultValue(true)]
    public bool WeakEventHandlers
    {
      get => this.m_bWeakEventHandlers;
      set => this.m_bWeakEventHandlers = value;
    }

    public static char NameSeparator
    {
      get => QCommand.m_cNameSeparator;
      set => QCommand.m_cNameSeparator = value;
    }

    [Category("QBehavior")]
    [Description("Contains the Name of the Command")]
    [DefaultValue(null)]
    [QXmlSave(QXmlSaveType.NeverSave)]
    public string ItemName
    {
      get => this.m_sItemName;
      set => this.m_sItemName = value;
    }

    [Browsable(false)]
    public string ParentFullName => this.ParentCommand != null ? this.ParentCommand.FullName : (string) null;

    [Browsable(false)]
    public string FullName
    {
      get
      {
        string parentFullName = this.ParentFullName;
        return parentFullName != null ? parentFullName + (object) QCommand.NameSeparator + this.ItemName : this.ItemName;
      }
    }

    [Category("QBehavior")]
    public bool HasChildItems => this.m_oCommands != null && this.m_oCommands.Count > 0;

    [Browsable(false)]
    public IQCommandContainer ParentContainer => this.m_oParentContainer == null || this.m_oParentContainer.IsDisposed ? (IQCommandContainer) null : this.m_oParentContainer;

    [Browsable(false)]
    public Control ParentControl => this.ParentContainer == null || !(this.ParentContainer is Control) ? (Control) null : (Control) this.ParentContainer;

    [Browsable(false)]
    public QCommand ParentCommand => this.m_oParentCommand;

    [Browsable(false)]
    public bool ChildContainerCreated => this.m_oChildContainer != null;

    [Browsable(false)]
    public IQCommandContainer ChildContainer
    {
      get
      {
        if (this.HasChildItems && this.m_oChildContainer == null)
          this.m_oChildContainer = this.CreateChildCommandContainer();
        return this.m_oChildContainer;
      }
    }

    internal string DesignName
    {
      get => this.m_sDesignName;
      set => this.m_sDesignName = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [QXmlSave(QXmlSaveType.NeverSave)]
    public virtual Rectangle Bounds
    {
      get => this.m_oBounds;
      set => this.m_oBounds = value;
    }

    [Browsable(false)]
    public int Left => this.Bounds.Left;

    [Browsable(false)]
    public int Top => this.Bounds.Top;

    [Browsable(false)]
    public int Right => this.Bounds.Right;

    [Browsable(false)]
    public int Bottom => this.Bounds.Bottom;

    [Browsable(false)]
    public int Width => this.Bounds.Width;

    [Browsable(false)]
    public int Height => this.Bounds.Height;

    public virtual bool IsCommandCollectionCreated => this.m_oCommands != null;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual QCommandCollection Commands
    {
      get
      {
        if (this.m_oCommands == null)
          this.m_oCommands = this.CreateCommandCollection();
        return this.m_oCommands;
      }
    }

    [Obsolete("Obsolete since 1.0.5.10, use NotifyParentContainerOfChange instead")]
    public void PerformParentLayout()
    {
      if (this.m_oParentContainer == null || this.m_oParentContainer.IsDisposed)
        return;
      this.NotifyParentContainerOfChange(QCommandUIRequest.PerformLayout);
    }

    [Obsolete("Obsolete since 1.0.5.10, use NotifyParentContainerOfChange instead")]
    public void PerformParentRefresh()
    {
      if (this.m_oParentContainer == null || this.m_oParentContainer.IsDisposed)
        return;
      this.NotifyParentContainerOfChange(QCommandUIRequest.Redraw);
    }

    public void NotifyParentContainerOfChange(QCommandUIRequest changeRequest)
    {
      if (this.m_oParentContainer == null || this.m_oParentContainer.IsDisposed)
        return;
      this.m_oParentContainer.HandleCommandChanged(changeRequest, this);
    }

    public virtual void HandleChildCommandCollectionChanged(int fromCount, int toCount)
    {
    }

    public virtual void HandleChildCommandChanged()
    {
    }

    protected abstract IQCommandContainer CreateChildCommandContainer();

    protected abstract QCommandCollection CreateCommandCollection();

    protected internal virtual void SetParent(IQCommandContainer container, QCommand command)
    {
      this.m_oParentContainer = container;
      this.m_oParentCommand = command;
    }

    public void SetAdditionalProperty(int key, object value)
    {
      if (this.m_aAdditionalProperties == null)
        this.m_aAdditionalProperties = new Hashtable();
      this.m_aAdditionalProperties[(object) key] = value;
    }

    public bool ContainsAdditionalProperty(int key) => this.m_aAdditionalProperties != null && this.m_aAdditionalProperties.Contains((object) key);

    public object GetAdditionalProperty(int key)
    {
      if (this.m_aAdditionalProperties == null)
        return (object) null;
      return this.m_aAdditionalProperties.Contains((object) key) ? this.m_aAdditionalProperties[(object) key] : (object) null;
    }

    public QCommand Clone(bool cloneChildItems)
    {
      QCommand qcommand = (QCommand) QObjectCloner.CloneObject((object) this);
      qcommand.SetAdditionalProperty(2, (object) this);
      if (cloneChildItems)
      {
        for (int index = 0; index < this.Commands.Count; ++index)
          qcommand.Commands.AddCommand(this.Commands.GetCommand(index).Clone(true));
      }
      return qcommand;
    }

    object ICloneable.Clone() => (object) this.Clone(true);

    event EventHandler IComponent.Disposed
    {
      add => this.Disposed += value;
      remove => this.Disposed -= value;
    }

    [QWeakEvent]
    public event EventHandler Disposed
    {
      add => this.m_oDisposedDelegate = QWeakDelegate.Combine(this.m_oDisposedDelegate, (Delegate) value, this.m_bWeakEventHandlers);
      remove => this.m_oDisposedDelegate = QWeakDelegate.Remove(this.m_oDisposedDelegate, (Delegate) value);
    }

    ISite IComponent.Site
    {
      get => this.Site;
      set => this.Site = value;
    }

    [Browsable(false)]
    [QXmlSave(QXmlSaveType.NeverSave)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ISite Site
    {
      get => this.m_oSite;
      set => this.m_oSite = value;
    }

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (this.m_bIsDisposed)
        return;
      int num = disposing ? 1 : 0;
      this.m_bIsDisposed = true;
      this.OnDisposed(EventArgs.Empty);
    }

    ~QCommand() => this.Dispose(false);

    private void OnDisposed(EventArgs e) => this.m_oDisposedDelegate = QWeakDelegate.InvokeDelegate(this.m_oDisposedDelegate, (object) this, (object) e);
  }
}
