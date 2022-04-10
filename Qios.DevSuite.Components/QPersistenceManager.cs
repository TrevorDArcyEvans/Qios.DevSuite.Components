// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QPersistenceManager
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;

namespace Qios.DevSuite.Components
{
  [ToolboxBitmap(typeof (QPersistenceManager), "Resources.ControlImages.QPersistenceManager.bmp")]
  [ToolboxItem(true)]
  public class QPersistenceManager : Component, IQWeakEventPublisher
  {
    private bool m_bWeakEventHandlers = true;
    private QPersistableObjectCollection m_oPersistableObjects;
    private Guid m_oPersistGuid = Guid.NewGuid();
    private bool m_bThrowException = true;
    private QPersistableHostCollection m_oPersistableHosts;
    private Control m_oOwnerControl;
    private QWeakDelegate m_oPersistableObjectRequestedDelegate;
    private QWeakDelegate m_oPersistableHostRequestedDelegate;
    private QWeakDelegate m_oPersistableObjectCreatedDelegate;
    private QWeakDelegate m_oPersistableObjectLoadedDelegate;
    private QWeakDelegate m_oPersistableObjectUnloadedDelegate;
    private QWeakDelegate m_oPersistableObjectSavedDelegate;

    public QPersistenceManager(IContainer container)
    {
      if (container == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (container)));
      container.Add((IComponent) this);
      this.InternalConstruct();
    }

    public QPersistenceManager() => this.InternalConstruct();

    private void InternalConstruct()
    {
      this.m_oPersistableObjects = new QPersistableObjectCollection();
      this.m_oPersistableHosts = new QPersistableHostCollection();
    }

    [Description("This event is raised when the Persistence is loaded and a persistableObject cannot be found in the PersistableObjects collection")]
    [QWeakEvent]
    [Category("QEvents")]
    public event QPersistenceEventHandler PersistableObjectRequested
    {
      add => this.m_oPersistableObjectRequestedDelegate = QWeakDelegate.Combine(this.m_oPersistableObjectRequestedDelegate, (Delegate) value, this.m_bWeakEventHandlers);
      remove => this.m_oPersistableObjectRequestedDelegate = QWeakDelegate.Remove(this.m_oPersistableObjectRequestedDelegate, (Delegate) value);
    }

    [Category("QEvents")]
    [QWeakEvent]
    [Description("This event is raised when the Persistence is loaded, and a persistableHost cannot be found in the PersistableHosts collection")]
    public event QPersistenceEventHandler PersistableHostRequested
    {
      add => this.m_oPersistableHostRequestedDelegate = QWeakDelegate.Combine(this.m_oPersistableHostRequestedDelegate, (Delegate) value, this.m_bWeakEventHandlers);
      remove => this.m_oPersistableHostRequestedDelegate = QWeakDelegate.Remove(this.m_oPersistableHostRequestedDelegate, (Delegate) value);
    }

    [Category("QEvents")]
    [QWeakEvent]
    [Description("This event is raised when a new PersistableObject is created based on the PersistableObjectElement")]
    public event QPersistenceEventHandler PersistableObjectCreated
    {
      add => this.m_oPersistableObjectCreatedDelegate = QWeakDelegate.Combine(this.m_oPersistableObjectCreatedDelegate, (Delegate) value, this.m_bWeakEventHandlers);
      remove => this.m_oPersistableObjectCreatedDelegate = QWeakDelegate.Remove(this.m_oPersistableObjectCreatedDelegate, (Delegate) value);
    }

    [Category("QEvents")]
    [QWeakEvent]
    [Description("This event is raised when a persistableObject is loaded based on the PersistableObjectElement")]
    public event QPersistenceEventHandler PersistableObjectLoaded
    {
      add => this.m_oPersistableObjectLoadedDelegate = QWeakDelegate.Combine(this.m_oPersistableObjectLoadedDelegate, (Delegate) value, this.m_bWeakEventHandlers);
      remove => this.m_oPersistableObjectLoadedDelegate = QWeakDelegate.Remove(this.m_oPersistableObjectLoadedDelegate, (Delegate) value);
    }

    [Description("This event is raised when a persistableObject is unloaded via the QPersistenceManager.")]
    [Category("QEvents")]
    [QWeakEvent]
    public event QPersistenceEventHandler PersistableObjectUnloaded
    {
      add => this.m_oPersistableObjectUnloadedDelegate = QWeakDelegate.Combine(this.m_oPersistableObjectUnloadedDelegate, (Delegate) value, this.m_bWeakEventHandlers);
      remove => this.m_oPersistableObjectUnloadedDelegate = QWeakDelegate.Remove(this.m_oPersistableObjectUnloadedDelegate, (Delegate) value);
    }

    [Category("QEvents")]
    [QWeakEvent]
    [Description("This event is raised when a persistableObject is saved to Xml.")]
    public event QPersistenceEventHandler PersistableObjectSaved
    {
      add => this.m_oPersistableObjectSavedDelegate = QWeakDelegate.Combine(this.m_oPersistableObjectSavedDelegate, (Delegate) value, this.m_bWeakEventHandlers);
      remove => this.m_oPersistableObjectSavedDelegate = QWeakDelegate.Remove(this.m_oPersistableObjectSavedDelegate, (Delegate) value);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [DefaultValue(true)]
    public bool WeakEventHandlers
    {
      get => this.m_bWeakEventHandlers;
      set => this.m_bWeakEventHandlers = value;
    }

    [RefreshProperties(RefreshProperties.Repaint)]
    [Category("QBehavior")]
    [Description("Gets or sets the OwnerControl. This Control can be used by persistableObjects to add themselves to the owner and is used during the InitializeFromOwner. If you have a Form use OwnerForm, else use this property.")]
    [DefaultValue(null)]
    public Control OwnerControl
    {
      get => this.m_oOwnerControl;
      set => this.m_oOwnerControl = value;
    }

    [RefreshProperties(RefreshProperties.Repaint)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("QBehavior")]
    [DefaultValue(null)]
    [Description("Gets or sets the OwnerControl as a Form. This Form can be used by persistableObjects to add themselves to the owner and is used during the InitializeFromOwner.")]
    public Form OwnerForm
    {
      get => this.m_oOwnerControl as Form;
      set => this.m_oOwnerControl = (Control) value;
    }

    [Description("Gets or sets the guid that identifies this object in the persistFile")]
    [Category("QPersistence")]
    public Guid PersistGuid
    {
      get => this.m_oPersistGuid;
      set => this.m_oPersistGuid = value;
    }

    [Browsable(false)]
    public QPersistableObjectCollection PersistableObjects => this.m_oPersistableObjects;

    [Browsable(false)]
    public QPersistableHostCollection PersistableHosts => this.m_oPersistableHosts;

    [Description("Gets or sets if an exception must be thrown when an error occurs.")]
    [Category("QBehavior")]
    [DefaultValue(true)]
    public bool ThrowException
    {
      get => this.m_bThrowException;
      set => this.m_bThrowException = value;
    }

    public void InitializeFromOwner() => this.InitializeFromOwner(true, true);

    public void InitializeFromOwner(bool childControls, bool ownedForms)
    {
      if (this.m_oOwnerControl == null)
        throw new QPersistenceException(QResources.GetException("QPersistence_OwnerNull"));
      this.m_oPersistableHosts.Clear();
      this.m_oPersistableObjects.Clear();
      this.AddObjectsFromControl(this.m_oOwnerControl, childControls);
      if (!ownedForms || this.OwnerForm == null)
        return;
      foreach (Control ownedForm in this.OwnerForm.OwnedForms)
        this.AddObjectsFromControl(ownedForm, true);
      for (int index = 0; index < QCustomToolWindow.AllWindows.Count; ++index)
      {
        if (QCustomToolWindow.AllWindows[index] != null && QCustomToolWindow.AllWindows[index].Owner == this.OwnerForm)
          this.AddObjectsFromControl((Control) QCustomToolWindow.AllWindows[index], true);
      }
    }

    private static void BeginInitializeCollection(ICollection collection)
    {
      foreach (object obj in (IEnumerable) collection)
      {
        if (obj is ISupportInitialize supportInitialize)
          supportInitialize.BeginInit();
      }
    }

    private static void EndInitializeCollection(ICollection collection)
    {
      foreach (object obj in (IEnumerable) collection)
      {
        if (obj is ISupportInitialize supportInitialize)
          supportInitialize.EndInit();
      }
    }

    public void BeginInitializeHosts() => QPersistenceManager.BeginInitializeCollection((ICollection) this.PersistableHosts);

    public void EndInitializeHosts() => QPersistenceManager.EndInitializeCollection((ICollection) this.PersistableHosts);

    public void BeginInitializePersistableObjects() => QPersistenceManager.BeginInitializeCollection((ICollection) this.PersistableObjects);

    public void EndInitializePersistableObjects() => QPersistenceManager.EndInitializeCollection((ICollection) this.PersistableObjects);

    public void ResetIsPersisted()
    {
      for (int index = 0; index < this.PersistableObjects.Count; ++index)
        this.PersistableObjects[index].IsPersisted = false;
    }

    public void AddObjectsFromControl(Control control, bool childControls)
    {
      IQPersistableObject persistableObject = control != null ? control as IQPersistableObject : throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (control)));
      IQPersistableHost persistableHost = control as IQPersistableHost;
      if (persistableObject != null && !this.PersistableObjects.Contains(persistableObject.PersistGuid))
        this.PersistableObjects.Add(persistableObject);
      if (persistableHost != null && !this.PersistableHosts.Contains(persistableHost))
        this.PersistableHosts.Add(persistableHost);
      if (!childControls)
        return;
      for (int index = 0; index < control.Controls.Count; ++index)
        this.AddObjectsFromControl(control.Controls[index], true);
    }

    public bool Save(string fileName)
    {
      if (!File.Exists(fileName))
      {
        string directoryName = Path.GetDirectoryName(fileName);
        if (!Directory.Exists(directoryName))
        {
          try
          {
            Directory.CreateDirectory(directoryName);
          }
          catch (Exception ex)
          {
            if (this.ThrowException)
              throw new QPersistenceException(QResources.GetException("QPersistence_CannotCreateDirectory", (object) directoryName), ex);
            return false;
          }
        }
      }
      XmlDocument parentElement = new XmlDocument();
      bool flag = this.Save((IXPathNavigable) parentElement);
      try
      {
        parentElement.Save(fileName);
      }
      catch (Exception ex)
      {
        if (this.ThrowException)
          throw new QPersistenceException(QResources.GetException("QPersistence_CannotSaveFile", (object) fileName), ex);
        return false;
      }
      return flag;
    }

    public bool Save(IXPathNavigable parentElement)
    {
      bool flag = true;
      this.ResetIsPersisted();
      this.PersistableObjects.SortPersistableObjects();
      if (parentElement is XmlDocument parentNode && parentNode.DocumentElement == null)
        parentElement = QXmlHelper.AddElement((IXPathNavigable) parentNode, "persistance");
      IXPathNavigable parentElement1 = QXmlHelper.SelectChildNavigable(parentElement, ".//persistenceManager[@guid='" + this.m_oPersistGuid.ToString() + "']");
      if (parentElement1 == null)
      {
        parentElement1 = QXmlHelper.AddElement(parentElement, "persistenceManager");
        QXmlHelper.AddAttribute(parentElement1, "guid", (object) this.m_oPersistGuid);
      }
      for (int index = 0; index < this.m_oPersistableObjects.Count; ++index)
      {
        IQPersistableObject persistableObject = this.PersistableObjects[index];
        if (persistableObject.PersistObject)
        {
          if (!persistableObject.IsPersisted)
          {
            try
            {
              IXPathNavigable element = persistableObject.SavePersistableObject(this, parentElement1);
              QPersistenceEventArgs e = new QPersistenceEventArgs(persistableObject.Name, persistableObject.GetType(), persistableObject.PersistGuid, Guid.Empty);
              e.PersistableObject = persistableObject;
              e.PutPersistableObjectElement(element);
              this.OnPersistableObjectSaved(e);
              persistableObject.IsPersisted = true;
            }
            catch (Exception ex)
            {
              if (this.ThrowException)
                throw new QPersistenceException(QResources.GetException("QPersistence_CannotSavePersistableObject", (object) this.m_oPersistableObjects[index].Name), ex);
              flag = false;
            }
          }
        }
      }
      return flag;
    }

    public void UnloadAllPersistableObjects() => this.UnloadPersistableObjects(QPersistenceUnloadOptions.All);

    public void UnloadDefaultPersistableObjects() => this.UnloadPersistableObjects(QPersistenceUnloadOptions.Default);

    [Obsolete("Obsolete since 1.0.5.30, use the overload with QPersistenceUnloadOptions")]
    public void UnloadPersistableObjects(QPersistenceUnloadType options)
    {
      switch (options)
      {
        case QPersistenceUnloadType.WhereNotPersistObject:
          this.UnloadPersistableObjects(QPersistenceUnloadOptions.WhereNotPersistObject);
          break;
        case QPersistenceUnloadType.WherePersistObject:
          this.UnloadPersistableObjects(QPersistenceUnloadOptions.WherePersistObject);
          break;
        case QPersistenceUnloadType.All:
          this.UnloadPersistableObjects(QPersistenceUnloadOptions.All);
          break;
      }
    }

    public void UnloadPersistableObjects(QPersistenceUnloadOptions options)
    {
      bool flag1 = (options & QPersistenceUnloadOptions.All) == QPersistenceUnloadOptions.All;
      bool flag2 = (options & QPersistenceUnloadOptions.WherePersistObject) == QPersistenceUnloadOptions.WherePersistObject;
      bool flag3 = (options & QPersistenceUnloadOptions.WhereNotPersistObject) == QPersistenceUnloadOptions.WhereNotPersistObject;
      bool flag4 = (options & QPersistenceUnloadOptions.WhereRequiresUnload) == QPersistenceUnloadOptions.WhereRequiresUnload;
      for (int index = 0; index < this.PersistableObjects.Count; ++index)
      {
        if ((flag1 || !flag2 && !flag3 || flag2 && this.PersistableObjects[index].PersistObject || flag3 && !this.PersistableObjects[index].PersistObject) && (flag1 || flag4 && this.PersistableObjects[index].RequiresUnload || !flag4))
        {
          this.PersistableObjects[index].UnloadPersistableObject();
          this.TriggerPersistableObjectUnloaded(this.PersistableObjects[index]);
        }
      }
    }

    public bool Load(string fileName)
    {
      if (!File.Exists(fileName))
      {
        if (this.ThrowException)
          throw new QPersistenceException(QResources.GetException("QPersistence_CannotFindFile", (object) fileName));
        return false;
      }
      XmlDocument parentElement = new XmlDocument();
      try
      {
        parentElement.Load(fileName);
      }
      catch (Exception ex)
      {
        if (this.ThrowException)
          throw new QPersistenceException(QResources.GetException("QPersistence_CannotLoadFile", (object) fileName), ex);
        return false;
      }
      return this.Load((IXPathNavigable) parentElement);
    }

    public bool Load(IXPathNavigable parentElement)
    {
      bool flag = true;
      XPathNavigator xpathNavigator = QXmlHelper.SelectChildNavigator(parentElement, ".//persistenceManager[@guid='" + (object) this.PersistGuid + "']");
      if (xpathNavigator == null)
      {
        if (this.ThrowException)
          throw new QPersistenceException(QResources.GetException("QPersistence_ElementWithGuidNotFound", (object) ".//persistenceManager", (object) this.PersistGuid));
        return false;
      }
      if (!xpathNavigator.HasChildren)
        return true;
      XPathNodeIterator xpathNodeIterator = xpathNavigator.SelectChildren("persistableObject", "");
      this.BeginInitializeHosts();
      this.BeginInitializePersistableObjects();
      while (xpathNodeIterator.MoveNext())
      {
        IXPathNavigable navigableFromNavigator = QXmlHelper.GetNavigableFromNavigator(xpathNodeIterator.Current);
        if (navigableFromNavigator != null)
        {
          IQPersistableObject persistableObject = this.GetPersistableObject(navigableFromNavigator);
          if (persistableObject != null)
          {
            try
            {
              if (!persistableObject.LoadPersistableObject(this, navigableFromNavigator, (object) null))
              {
                this.EndInitializeHosts();
                this.EndInitializePersistableObjects();
                flag = false;
              }
              else
                this.TriggerPersistableObjectLoaded(persistableObject, navigableFromNavigator);
            }
            catch
            {
              if (this.ThrowException)
              {
                this.EndInitializeHosts();
                this.EndInitializePersistableObjects();
                throw;
              }
              else
                flag = false;
            }
          }
          else
          {
            int num = this.ThrowException ? 1 : 0;
          }
        }
      }
      this.EndInitializeHosts();
      this.EndInitializePersistableObjects();
      return flag;
    }

    public IXPathNavigable CreatePersistableObjectElement(
      IQPersistableObject persistableObject,
      IXPathNavigable parentElement)
    {
      IXPathNavigable persistableObjectElement = QXmlHelper.SelectChildNavigable(parentElement, "persistableObject[@guid= '" + (object) persistableObject.PersistGuid + "']") ?? (IXPathNavigable) (QXmlHelper.AddElement(parentElement, nameof (persistableObject)) as XmlElement);
      QPersistenceManager.InitializePersistableObjectElement(persistableObject, persistableObjectElement);
      return persistableObjectElement;
    }

    public static void InitializePersistableObjectElement(
      IQPersistableObject persistableObject,
      IXPathNavigable persistableObjectElement)
    {
      if (persistableObject == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (persistableObject)));
      QXmlHelper.RemoveAll(persistableObjectElement);
      if (persistableObject.CreateNew)
        QXmlHelper.AddAttribute(persistableObjectElement, "createNew", (object) persistableObject.CreateNew);
      QXmlHelper.AddAttribute(persistableObjectElement, "guid", (object) persistableObject.PersistGuid);
      QXmlHelper.AddAttribute(persistableObjectElement, "name", (object) persistableObject.Name);
      System.Type type = persistableObject.GetType();
      QXmlHelper.AddAttribute(persistableObjectElement, "type", (object) (type.FullName + ", " + type.Assembly.GetName().Name));
    }

    public IQPersistableHost GetPersistableHost(
      Guid persistableHostGuid,
      IQPersistableObject requestingPersistableObject)
    {
      IQPersistableHost qpersistableHost = this.m_oPersistableHosts[persistableHostGuid.ToString()];
      if (qpersistableHost == null)
      {
        QPersistenceEventArgs e = requestingPersistableObject == null ? new QPersistenceEventArgs((string) null, (System.Type) null, Guid.Empty, persistableHostGuid) : new QPersistenceEventArgs(requestingPersistableObject.Name, requestingPersistableObject.GetType(), requestingPersistableObject.PersistGuid, persistableHostGuid);
        this.OnPersistableHostRequested(e);
        qpersistableHost = e.PersistableHost;
      }
      return qpersistableHost != null || !this.ThrowException ? qpersistableHost : throw new QPersistenceException(QResources.GetException("QPersistence_PersistableHostNotFound", (object) persistableHostGuid));
    }

    public IQPersistableObject GetPersistableObject(
      IXPathNavigable persistableObjectElement)
    {
      bool attributeBool = QXmlHelper.GetAttributeBool(persistableObjectElement, "createNew");
      Guid attributeGuid = QXmlHelper.GetAttributeGuid(persistableObjectElement, "guid");
      System.Type attributeType = QXmlHelper.GetAttributeType(persistableObjectElement, "type");
      string attributeString = QXmlHelper.GetAttributeString(persistableObjectElement, "name");
      if (attributeBool)
      {
        if (attributeType == null)
        {
          if (this.ThrowException)
            throw new QPersistenceException(QResources.GetException("QPersistence_TypeNotFound", (object) QXmlHelper.GetAttributeString(persistableObjectElement, "type")));
          return (IQPersistableObject) null;
        }
        object instance;
        try
        {
          instance = Activator.CreateInstance(attributeType, new object[0]);
        }
        catch (Exception ex)
        {
          if (this.ThrowException)
            throw new QPersistenceException(QResources.GetException("QPersistence_CannotCreateInstance", (object) QXmlHelper.GetAttributeString(persistableObjectElement, "type")), ex);
          return (IQPersistableObject) null;
        }
        if (!(instance is IQPersistableObject qpersistableObject))
        {
          if (this.ThrowException)
            throw new QPersistenceException(QResources.GetException("QPersistence_ObjectIsNotPersistableObject", (object) instance.GetType()));
          return (IQPersistableObject) null;
        }
        qpersistableObject.CreateNew = true;
        qpersistableObject.PersistGuid = attributeGuid;
        QPersistenceEventArgs e = new QPersistenceEventArgs((string) null, qpersistableObject.GetType(), qpersistableObject.PersistGuid, Guid.Empty);
        e.PersistableObject = qpersistableObject;
        e.PutPersistableObjectElement(persistableObjectElement);
        this.OnPersistableObjectCreated(e);
        return e.PersistableObject;
      }
      if (attributeGuid == Guid.Empty)
      {
        if (this.ThrowException)
          throw new QPersistenceException(QResources.GetException("QPersistence_GuidIsEmpty"));
        return (IQPersistableObject) null;
      }
      IQPersistableObject persistableObject = this.m_oPersistableObjects[attributeGuid.ToString()];
      if (persistableObject == null)
      {
        QPersistenceEventArgs e = new QPersistenceEventArgs(attributeString, attributeType, attributeGuid, Guid.Empty);
        e.PutPersistableObjectElement(persistableObjectElement);
        this.OnPersistableObjectRequested(e);
        persistableObject = e.PersistableObject;
      }
      return persistableObject != null || !this.ThrowException ? persistableObject : throw new QPersistenceException(QResources.GetException("QPersistence_PersistableObjectWithGuidnotFound", (object) attributeGuid, (object) attributeType, (object) attributeString));
    }

    public void TriggerPersistableObjectLoaded(
      IQPersistableObject persistableObject,
      IXPathNavigable persistableObjectElement)
    {
      if (persistableObject == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (persistableObject)));
      QPersistenceEventArgs e = new QPersistenceEventArgs(persistableObject.Name, persistableObject.GetType(), persistableObject.PersistGuid, Guid.Empty);
      e.PersistableObject = persistableObject;
      e.PutPersistableObjectElement(persistableObjectElement);
      this.OnPersistableObjectLoaded(e);
    }

    public void TriggerPersistableObjectUnloaded(IQPersistableObject persistableObject)
    {
      if (persistableObject == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (persistableObject)));
      this.OnPersistableObjectUnloaded(new QPersistenceEventArgs(persistableObject.Name, persistableObject.GetType(), persistableObject.PersistGuid, Guid.Empty)
      {
        PersistableObject = persistableObject
      });
    }

    protected virtual void OnPersistableObjectRequested(QPersistenceEventArgs e) => this.m_oPersistableObjectRequestedDelegate = QWeakDelegate.InvokeDelegate(this.m_oPersistableObjectRequestedDelegate, (object) this, (object) e);

    protected virtual void OnPersistableHostRequested(QPersistenceEventArgs e) => this.m_oPersistableHostRequestedDelegate = QWeakDelegate.InvokeDelegate(this.m_oPersistableHostRequestedDelegate, (object) this, (object) e);

    protected virtual void OnPersistableObjectCreated(QPersistenceEventArgs e) => this.m_oPersistableObjectCreatedDelegate = QWeakDelegate.InvokeDelegate(this.m_oPersistableObjectCreatedDelegate, (object) this, (object) e);

    protected virtual void OnPersistableObjectLoaded(QPersistenceEventArgs e) => this.m_oPersistableObjectLoadedDelegate = QWeakDelegate.InvokeDelegate(this.m_oPersistableObjectLoadedDelegate, (object) this, (object) e);

    protected virtual void OnPersistableObjectUnloaded(QPersistenceEventArgs e) => this.m_oPersistableObjectUnloadedDelegate = QWeakDelegate.InvokeDelegate(this.m_oPersistableObjectUnloadedDelegate, (object) this, (object) e);

    protected virtual void OnPersistableObjectSaved(QPersistenceEventArgs e) => this.m_oPersistableObjectSavedDelegate = QWeakDelegate.InvokeDelegate(this.m_oPersistableObjectSavedDelegate, (object) this, (object) e);

    protected override void Dispose(bool disposing)
    {
      try
      {
        int num = disposing ? 1 : 0;
      }
      finally
      {
        base.Dispose(disposing);
      }
    }
  }
}
