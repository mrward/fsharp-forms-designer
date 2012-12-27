// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)

using System;
using ICSharpCode.NRefactory;
using ICSharpCode.SharpDevelop.Dom;
using Microsoft.FSharp.Compiler.SourceCodeServices;

namespace ICSharpCode.FSharpFormsDesigner
{
	public class FSharpClassParser
	{
		DefaultCompilationUnit compilationUnit;
		FSharpNavigationItemsParser parser;
		string source;
		string[] lines;
		
		public FSharpClassParser(IProjectContent projectContent, string fileName, string source)
		{
			this.compilationUnit = new DefaultCompilationUnit(projectContent) {
				FileName = fileName
			};
			this.parser = new FSharpNavigationItemsParser();
			this.source = source;
		}
		
		public ICompilationUnit Parse()
		{
			NavigationItems items = parser.GetNavigationItems(compilationUnit.FileName, source);
			foreach (TopLevelDeclaration topLevelDeclaration in items.Declarations) {
				if (topLevelDeclaration.Declaration.Kind.IsTypeDecl) {
					AddClass(topLevelDeclaration);
				}
			}
			return compilationUnit;
		}
		
		void AddClass(TopLevelDeclaration topLevelDeclaration)
		{
			var fsharpClass = new FSharpClass(compilationUnit, topLevelDeclaration);
			AddRegions(fsharpClass, topLevelDeclaration.Declaration);
			AddMethods(fsharpClass, topLevelDeclaration);
			compilationUnit.Classes.Add(fsharpClass);
		}
		
		int GetEndOfDeclarationHeader(int startLine)
		{
			string[] lines = GetLines();
			string line = lines[startLine - 1];
			return line.IndexOf(')') + 1;
		}
		
		string[] GetLines()
		{
			if (lines == null) {
				lines = source.Split('\n');
			}
			return lines;
		}
		
		void AddMethods(FSharpClass fsharpClass, TopLevelDeclaration topLevelDeclaration)
		{
			foreach (DeclarationItem item in topLevelDeclaration.Nested) {
				if (item.Kind.IsMethodDecl) {
					AddMethod(fsharpClass, item);
				}
			}
		}
		
		void AddMethod(FSharpClass fsharpClass, DeclarationItem methodDeclaration)
		{
			var method = new FSharpMethod(fsharpClass, methodDeclaration);
			AddRegions(method, methodDeclaration);
			fsharpClass.Methods.Add(method);
		}
		
		void AddRegions(IRegions regions, DeclarationItem declarationItem)
		{
			Location headerStart = declarationItem.GetRangeStart();
			int headerEndColumn = GetEndOfDeclarationHeader(headerStart.Line) + 1;
			regions.Region = new DomRegion(headerStart.Line, 1, headerStart.Line, headerEndColumn);
			
			Location bodyEnd = declarationItem.GetBodyRangeEnd();
			regions.BodyRegion = new DomRegion(headerStart.Line, headerEndColumn, bodyEnd.Line, bodyEnd.Column);
		}
	}
}
