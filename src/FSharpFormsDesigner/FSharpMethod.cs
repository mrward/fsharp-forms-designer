// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)

using System;
using ICSharpCode.SharpDevelop.Dom;
using Microsoft.FSharp.Compiler.SourceCodeServices;

namespace ICSharpCode.FSharpFormsDesigner
{
	public class FSharpMethod : DefaultMethod, IRegions
	{
		public FSharpMethod(IClass declaringType, DeclarationItem methodDeclaration)
			: base(declaringType, methodDeclaration.Name)
		{
		}
	}
}
