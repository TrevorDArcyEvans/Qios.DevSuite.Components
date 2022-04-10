// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QShapeCodeSerializer
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.CodeDom;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Reflection;

namespace Qios.DevSuite.Components.Design
{
  public class QShapeCodeSerializer : CodeDomSerializer
  {
    public override object Serialize(IDesignerSerializationManager manager, object value)
    {
      object obj = manager != null ? ((CodeDomSerializer) manager.GetSerializer(typeof (Component), typeof (CodeDomSerializer))).Serialize(manager, value) : throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (manager)));
      QShape qshape = value as QShape;
      CodeStatementCollection statementCollection = obj as CodeStatementCollection;
      if (qshape != null && qshape.ConvertedFromString && statementCollection != null && statementCollection.Count == 0)
      {
        CodePropertyReferenceExpression left = (CodePropertyReferenceExpression) null;
        if (manager.Context.Current != null)
        {
          PropertyInfo property = manager.Context.Current.GetType().GetProperty("Expression");
          if (property != null)
            left = property.GetValue(manager.Context.Current, new object[0]) as CodePropertyReferenceExpression;
        }
        if (left != null)
        {
          CodeObjectCreateExpression right = new CodeObjectCreateExpression(typeof (QShape), new CodeExpression[1]
          {
            (CodeExpression) new CodePropertyReferenceExpression((CodeExpression) new CodeTypeReferenceExpression(typeof (QBaseShapeType)), qshape.ClonedBaseShapeType.ToString())
          });
          statementCollection.Add((CodeStatement) new CodeAssignStatement((CodeExpression) left, (CodeExpression) right));
        }
      }
      return obj;
    }

    public override object Deserialize(IDesignerSerializationManager manager, object codeObject) => ((CodeDomSerializer) manager.GetSerializer(typeof (Component), typeof (CodeDomSerializer))).Deserialize(manager, codeObject);
  }
}
