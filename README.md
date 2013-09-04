# F# WinForms Designer for SharpDevelop

Currently at the early stages of development. Forms designer does not read or write F# code yet.

Supports

 * F# 2.0
 * SharpDevelop 4.2 and 4.3

Uses a custom build of F# 2.0 which makes various parser/lexer classes in the Microsoft.FSharp.Compiler.SourceCodeServices namesapce public instead of internal so they can be used by the forms designer.

License: MIT

