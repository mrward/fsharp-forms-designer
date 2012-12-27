// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)

using System;
using ICSharpCode.FormsDesigner;
using ICSharpCode.Scripting;
using ICSharpCode.SharpDevelop;
using ICSharpCode.SharpDevelop.Editor;
using ICSharpCode.SharpDevelop.Gui;

namespace ICSharpCode.FSharpFormsDesigner
{
	public class FSharpFormsDesignerDisplayBinding : ISecondaryDisplayBinding
	{
		public bool ReattachWhenParserServiceIsReady {
			get { return true; }
		}
		
		public bool CanAttachTo(IViewContent content)
		{
			var textEditorProvider = content as ITextEditorProvider;
			return (textEditorProvider != null);
		}
		
		public IViewContent[] CreateSecondaryViewContent(IViewContent viewContent)
		{
			var textEditorView = new ScriptingTextEditorViewContent(viewContent);
			return this.CreateSecondaryViewContent(viewContent, textEditorView.TextEditorOptions);
		}
		
		public IViewContent[] CreateSecondaryViewContent(IViewContent viewContent, ITextEditorOptions textEditorOptions)
		{
			foreach (IViewContent existingView in viewContent.SecondaryViewContents) {
				if (existingView is FormsDesignerViewContent) {
					return new IViewContent[0];
				}
			}
			
			var loader = new FSharpFormsDesignerLoaderProvider();
			var generator = new FSharpFormsDesignerGenerator(textEditorOptions);
			
			return new IViewContent[] {
				new FSharpFormsDesignerView(viewContent, loader, generator)
			};
		}
	}
}