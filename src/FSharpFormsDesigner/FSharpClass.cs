// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)

using System;
using ICSharpCode.SharpDevelop.Dom;
using Microsoft.FSharp.Compiler.SourceCodeServices;

namespace ICSharpCode.FSharpFormsDesigner
{
	public class FSharpClass : DefaultClass
	{
		TopLevelDeclaration topLevelDeclaration;
		
		public FSharpClass(ICompilationUnit compilationUnit, TopLevelDeclaration topLevelDeclaration)
			: base(compilationUnit, topLevelDeclaration.Declaration.Name)
		{
			this.topLevelDeclaration = topLevelDeclaration;
		}
	}
}
