// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)

using System;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;

using ICSharpCode.Scripting;

namespace ICSharpCode.FSharpFormsDesigner
{
	/// <summary>
	/// Description of FSharpCodeDomSerializer.
	/// </summary>
	public class FSharpCodeDomSerializer : IScriptingCodeDomSerializer
	{
		string indentString = "\t";
		
		public FSharpCodeDomSerializer(string indentString)
		{
			this.indentString = indentString;
		}
		
		public string GenerateInitializeComponentMethodBody(
			IDesignerHost host,
			IDesignerSerializationManager serializationManager)
		{
			return GenerateInitializeComponentMethodBody(host, serializationManager, String.Empty, 0);
		}
		
		public string GenerateInitializeComponentMethodBody(
			IDesignerHost host,
			IDesignerSerializationManager serializationManager,
			string rootNamespace,
			int initialIndent)
		{
			//this.codeBuilder = new PythonCodeBuilder(initialIndent);
			//this.codeBuilder.IndentString = this.indentString;
			//CodeMemberMethod method = this.FindInitializeComponentMethod(host, serializationManager);
			//this.GetResourceRootName(rootNamespace, host.RootComponent);
			//this.AppendStatements(method.Statements);
			//return this.codeBuilder.ToString();
			return "TODO";
		}
	}
}
