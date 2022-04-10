// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QContextMenuCodeSerializer
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.CodeDom;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;

namespace Qios.DevSuite.Components.Design
{
  public class QContextMenuCodeSerializer : CodeDomSerializer
  {
    public override object Serialize(IDesignerSerializationManager manager, object value)
    {
      if (manager == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (manager)));
      QContextMenu menu = (QContextMenu) value;
      object obj = ((CodeDomSerializer) manager.GetSerializer(typeof (QContextMenu).BaseType, typeof (CodeDomSerializer))).Serialize(manager, (object) menu);
      if (obj is CodeStatementCollection statementCollection)
      {
        CodeExpression expression1 = this.SerializeToExpression(manager, (object) menu);
        QContextMenuExtender extenderProviderOnSite = QContextMenuDesigner.GetExtenderProviderOnSite(menu.Site, false);
        if (extenderProviderOnSite != null)
        {
          Component[] components = extenderProviderOnSite.GetComponents(menu);
          CodeFieldReferenceExpression referenceExpression = expression1 as CodeFieldReferenceExpression;
          if (components.Length > 0 && statementCollection.Count == 1 && referenceExpression != null)
          {
            statementCollection.Add((CodeStatement) new CodeCommentStatement(string.Empty));
            statementCollection.Add((CodeStatement) new CodeCommentStatement(referenceExpression.FieldName));
            statementCollection.Add((CodeStatement) new CodeCommentStatement(string.Empty));
          }
          for (int index = 0; index < components.Length; ++index)
          {
            CodeExpression expression2 = this.SerializeToExpression(manager, (object) components[index]);
            if (expression2 != null)
              statementCollection.Add((CodeExpression) new CodeMethodInvokeExpression(expression1, "AddListener", new CodeExpression[1]
              {
                expression2
              }));
          }
        }
      }
      return obj;
    }

    public override object Deserialize(IDesignerSerializationManager manager, object codeObject)
    {
      if (manager == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (manager)));
      return ((CodeDomSerializer) manager.GetSerializer(typeof (QContextMenu).BaseType, typeof (CodeDomSerializer))).Deserialize(manager, codeObject);
    }
  }
}
