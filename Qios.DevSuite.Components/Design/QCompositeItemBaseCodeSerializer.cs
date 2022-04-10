// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QCompositeItemBaseCodeSerializer
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.CodeDom;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;

namespace Qios.DevSuite.Components.Design
{
  public class QCompositeItemBaseCodeSerializer : CodeDomSerializer
  {
    public override object Serialize(IDesignerSerializationManager manager, object value)
    {
      if (manager == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (manager)));
      return value is IQPart qpart && qpart.IsSystemPart ? (object) null : ((CodeDomSerializer) manager.GetSerializer(typeof (Component), typeof (CodeDomSerializer))).Serialize(manager, value);
    }

    public override object Deserialize(IDesignerSerializationManager manager, object codeObject)
    {
      CodeDomSerializer serializer = (CodeDomSerializer) manager.GetSerializer(typeof (Component), typeof (CodeDomSerializer));
      if (codeObject is CodeStatementCollection statementCollection && statementCollection.Count > 1 && statementCollection[0] is CodeAssignStatement codeAssignStatement && codeAssignStatement.Right is CodeObjectCreateExpression && codeAssignStatement.Left is CodeFieldReferenceExpression left)
      {
        statementCollection.Insert(1, (CodeStatement) new CodeExpressionStatement((CodeExpression) new CodeMethodInvokeExpression((CodeExpression) left, "BeginCodeDeserialization", new CodeExpression[0])));
        statementCollection.Add((CodeExpression) new CodeMethodInvokeExpression((CodeExpression) left, "EndCodeDeserialization", new CodeExpression[0]));
      }
      return serializer.Deserialize(manager, codeObject);
    }
  }
}
