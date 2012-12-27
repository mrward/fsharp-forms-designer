// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)

using System;
using System.ComponentModel.Design.Serialization;
using ICSharpCode.FormsDesigner;
using ICSharpCode.Scripting;

namespace ICSharpCode.FSharpFormsDesigner
{
	public class FSharpFormsDesignerLoaderProvider : IDesignerLoaderProvider
	{
		public DesignerLoader CreateLoader(IDesignerGenerator generator)
		{
			return new FSharpFormsDesignerLoader(generator as IScriptingDesignerGenerator);
		}
	}
}
