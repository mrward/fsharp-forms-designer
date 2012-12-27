// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)

using System;
using ICSharpCode.SharpDevelop.Dom;
using Microsoft.FSharp.Compiler.SourceCodeServices;

namespace ICSharpCode.FSharpFormsDesigner
{
	public class FSharpClassParser
	{
		DefaultCompilationUnit compilationUnit;
		FSharpNavigationItemsParser parser;
		string source;
		
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
			AddRegions(fsharpClass, topLevelDeclaration);
			AddMethods(fsharpClass, topLevelDeclaration);
			compilationUnit.Classes.Add(fsharpClass);
		}
		
		void AddRegions(FSharpClass fsharpClass, TopLevelDeclaration topLevelDeclaration)
		{
			Console.WriteLine("Class.Range: " + topLevelDeclaration.Declaration.Range);
			Console.WriteLine("Class.BodyRange: " + topLevelDeclaration.Declaration.BodyRange);
			
			int startLine = topLevelDeclaration.Declaration.Range.Item1.Item2;
			int endColumn = GetEndOfClassOrMethodHeader(startLine) + 1;
			
			fsharpClass.Region = new DomRegion(startLine, 1, startLine, endColumn);
			
			Tuple<int, int> end = topLevelDeclaration.Declaration.BodyRange.Item2;
			fsharpClass.BodyRegion = new DomRegion(startLine, endColumn, end.Item2, end.Item1 + 1);
		}
		
		int GetEndOfClassOrMethodHeader(int startLine)
		{
			string[] lines = source.Split('\n');
			string line = lines[startLine - 1];
			return line.IndexOf(')') + 1;
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
			var method = new DefaultMethod(fsharpClass, methodDeclaration.Name);
			AddMethodRegions(method, methodDeclaration);
			fsharpClass.Methods.Add(method);
		}
		
		void AddMethodRegions(DefaultMethod method, DeclarationItem methodDeclaration)
		{
			Console.WriteLine("Method.Range: " + methodDeclaration.Range);
			Console.WriteLine("Method.BodyRange: " + methodDeclaration.BodyRange);
			
			Tuple<int, int> start = methodDeclaration.Range.Item1;
			int startLine = start.Item2;
			int endColumn = GetEndOfClassOrMethodHeader(startLine) + 1;
			method.Region = new DomRegion(startLine, 1, startLine, endColumn);
			
			Tuple<int, int> end= methodDeclaration.BodyRange.Item2;
			method.BodyRegion = new DomRegion(startLine, endColumn, end.Item2, end.Item1 + 1);
		}
	}
}
