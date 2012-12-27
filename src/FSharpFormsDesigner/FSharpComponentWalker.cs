// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)

using System;
using System.ComponentModel;
using System.Windows.Forms;
using ICSharpCode.Scripting;

namespace ICSharpCode.FSharpFormsDesigner
{
	public class FSharpComponentWalker : IComponentWalker
	{
		IComponentCreator componentCreator;
		
		public FSharpComponentWalker(IComponentCreator componentCreator)
		{
			this.componentCreator = componentCreator;
		}
		
		public IComponent CreateComponent(string code)
		{
			return componentCreator.CreateComponent(typeof(Form), "MainForm");
		}
	}
}
