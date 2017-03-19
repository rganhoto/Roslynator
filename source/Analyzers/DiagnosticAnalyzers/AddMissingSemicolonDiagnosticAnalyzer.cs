﻿// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Text;
using Roslynator.Diagnostics.Extensions;
using Roslynator.Extensions;

namespace Roslynator.CSharp.DiagnosticAnalyzers
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class AddMissingSemicolonDiagnosticAnalyzer : BaseDiagnosticAnalyzer
    {
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics
        {
            get { return ImmutableArray.Create(DiagnosticDescriptors.AddMissingSemicolon); }
        }

        public override void Initialize(AnalysisContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            context.RegisterSyntaxNodeAction(f => AnalyzeMethodDeclaration(f), SyntaxKind.MethodDeclaration);
            context.RegisterSyntaxNodeAction(f => AnalyzeOperatorDeclaration(f), SyntaxKind.OperatorDeclaration);
            context.RegisterSyntaxNodeAction(f => AnalyzeConversionOperatorDeclaration(f), SyntaxKind.ConversionOperatorDeclaration);
            context.RegisterSyntaxNodeAction(f => AnalyzePropertyDeclaration(f), SyntaxKind.PropertyDeclaration);
            context.RegisterSyntaxNodeAction(f => AnalyzeIndexerDeclaration(f), SyntaxKind.IndexerDeclaration);
            context.RegisterSyntaxNodeAction(f => AnalyzeFieldDeclaration(f), SyntaxKind.FieldDeclaration);
            context.RegisterSyntaxNodeAction(f => AnalyzeEventFieldDeclaration(f), SyntaxKind.EventFieldDeclaration);

            context.RegisterSyntaxNodeAction(f => AnalyzeBreakStatement(f), SyntaxKind.BreakStatement);
            context.RegisterSyntaxNodeAction(f => AnalyzeContinueStatement(f), SyntaxKind.ContinueStatement);
            context.RegisterSyntaxNodeAction(f => AnalyzeDoStatement(f), SyntaxKind.DoStatement);
            context.RegisterSyntaxNodeAction(f => AnalyzeExpressionStatement(f), SyntaxKind.ExpressionStatement);
            context.RegisterSyntaxNodeAction(f => AnalyzeGotoStatement(f), SyntaxKind.GotoStatement);
            context.RegisterSyntaxNodeAction(f => AnalyzeGotoStatement(f), SyntaxKind.GotoCaseStatement);
            context.RegisterSyntaxNodeAction(f => AnalyzeGotoStatement(f), SyntaxKind.GotoDefaultStatement);
            context.RegisterSyntaxNodeAction(f => AnalyzeLocalDeclarationStatement(f), SyntaxKind.LocalDeclarationStatement);
            context.RegisterSyntaxNodeAction(f => AnalyzeReturnStatement(f), SyntaxKind.ReturnStatement);
            context.RegisterSyntaxNodeAction(f => AnalyzeThrowStatement(f), SyntaxKind.ThrowStatement);
            context.RegisterSyntaxNodeAction(f => AnalyzeYieldStatement(f), SyntaxKind.YieldBreakStatement);
            context.RegisterSyntaxNodeAction(f => AnalyzeYieldStatement(f), SyntaxKind.YieldReturnStatement);

            context.RegisterSyntaxNodeAction(f => AnalyzeUsingDirective(f), SyntaxKind.UsingDirective);
            context.RegisterSyntaxNodeAction(f => AnalyzeExternAliasDirective(f), SyntaxKind.ExternAliasDirective);
        }

        private void AnalyzeMethodDeclaration(SyntaxNodeAnalysisContext context)
        {
            if (GeneratedCodeAnalyzer?.IsGeneratedCode(context) == true)
                return;

            var methodDeclaration = (MethodDeclarationSyntax)context.Node;

            if (methodDeclaration.ExpressionBody != null)
                AnalyzeSemicolon(context, methodDeclaration, methodDeclaration.SemicolonToken);
        }

        private void AnalyzeOperatorDeclaration(SyntaxNodeAnalysisContext context)
        {
            if (GeneratedCodeAnalyzer?.IsGeneratedCode(context) == true)
                return;

            var operatorDeclaration = (OperatorDeclarationSyntax)context.Node;

            if (operatorDeclaration.ExpressionBody != null)
                AnalyzeSemicolon(context, operatorDeclaration, operatorDeclaration.SemicolonToken);
        }

        private void AnalyzeConversionOperatorDeclaration(SyntaxNodeAnalysisContext context)
        {
            if (GeneratedCodeAnalyzer?.IsGeneratedCode(context) == true)
                return;

            var conversionOperatorDeclaration = (ConversionOperatorDeclarationSyntax)context.Node;

            if (conversionOperatorDeclaration.ExpressionBody != null)
                AnalyzeSemicolon(context, conversionOperatorDeclaration, conversionOperatorDeclaration.SemicolonToken);
        }

        private void AnalyzePropertyDeclaration(SyntaxNodeAnalysisContext context)
        {
            if (GeneratedCodeAnalyzer?.IsGeneratedCode(context) == true)
                return;

            var propertyDeclaration = (PropertyDeclarationSyntax)context.Node;

            if (propertyDeclaration.ExpressionBody != null
                || propertyDeclaration.Initializer != null)
            {
                AnalyzeSemicolon(context, propertyDeclaration, propertyDeclaration.SemicolonToken);
            }
        }

        private void AnalyzeIndexerDeclaration(SyntaxNodeAnalysisContext context)
        {
            if (GeneratedCodeAnalyzer?.IsGeneratedCode(context) == true)
                return;

            var indexerDeclaration = (IndexerDeclarationSyntax)context.Node;

            if (indexerDeclaration.ExpressionBody != null)
                AnalyzeSemicolon(context, indexerDeclaration, indexerDeclaration.SemicolonToken);
        }

        private void AnalyzeFieldDeclaration(SyntaxNodeAnalysisContext context)
        {
            if (GeneratedCodeAnalyzer?.IsGeneratedCode(context) == true)
                return;

            var fieldDeclaration = (FieldDeclarationSyntax)context.Node;

            AnalyzeSemicolon(context, fieldDeclaration, fieldDeclaration.SemicolonToken);
        }

        private void AnalyzeEventFieldDeclaration(SyntaxNodeAnalysisContext context)
        {
            if (GeneratedCodeAnalyzer?.IsGeneratedCode(context) == true)
                return;

            var eventFieldDeclaration = (EventFieldDeclarationSyntax)context.Node;

            AnalyzeSemicolon(context, eventFieldDeclaration, eventFieldDeclaration.SemicolonToken);
        }

        private void AnalyzeBreakStatement(SyntaxNodeAnalysisContext context)
        {
            if (GeneratedCodeAnalyzer?.IsGeneratedCode(context) == true)
                return;

            var breakStatement = (BreakStatementSyntax)context.Node;

            AnalyzeSemicolon(context, breakStatement, breakStatement.SemicolonToken);
        }

        private void AnalyzeContinueStatement(SyntaxNodeAnalysisContext context)
        {
            if (GeneratedCodeAnalyzer?.IsGeneratedCode(context) == true)
                return;

            var continueStatement = (ContinueStatementSyntax)context.Node;

            AnalyzeSemicolon(context, continueStatement, continueStatement.SemicolonToken);
        }

        private void AnalyzeDoStatement(SyntaxNodeAnalysisContext context)
        {
            if (GeneratedCodeAnalyzer?.IsGeneratedCode(context) == true)
                return;

            var doStatement = (DoStatementSyntax)context.Node;

            AnalyzeSemicolon(context, doStatement, doStatement.SemicolonToken);
        }

        private void AnalyzeExpressionStatement(SyntaxNodeAnalysisContext context)
        {
            if (GeneratedCodeAnalyzer?.IsGeneratedCode(context) == true)
                return;

            var expressionStatement = (ExpressionStatementSyntax)context.Node;

            AnalyzeSemicolon(context, expressionStatement, expressionStatement.SemicolonToken);
        }

        private void AnalyzeGotoStatement(SyntaxNodeAnalysisContext context)
        {
            if (GeneratedCodeAnalyzer?.IsGeneratedCode(context) == true)
                return;

            var gotoStatement = (GotoStatementSyntax)context.Node;

            AnalyzeSemicolon(context, gotoStatement, gotoStatement.SemicolonToken);
        }

        private void AnalyzeLocalDeclarationStatement(SyntaxNodeAnalysisContext context)
        {
            if (GeneratedCodeAnalyzer?.IsGeneratedCode(context) == true)
                return;

            var localDeclarationStatement = (LocalDeclarationStatementSyntax)context.Node;

            AnalyzeSemicolon(context, localDeclarationStatement, localDeclarationStatement.SemicolonToken);
        }

        private void AnalyzeReturnStatement(SyntaxNodeAnalysisContext context)
        {
            if (GeneratedCodeAnalyzer?.IsGeneratedCode(context) == true)
                return;

            var returnStatement = (ReturnStatementSyntax)context.Node;

            if (returnStatement.Expression != null)
                AnalyzeSemicolon(context, returnStatement, returnStatement.SemicolonToken);
        }

        private void AnalyzeThrowStatement(SyntaxNodeAnalysisContext context)
        {
            if (GeneratedCodeAnalyzer?.IsGeneratedCode(context) == true)
                return;

            var throwStatement = (ThrowStatementSyntax)context.Node;

            if (throwStatement.Expression != null)
                AnalyzeSemicolon(context, throwStatement, throwStatement.SemicolonToken);
        }

        private void AnalyzeYieldStatement(SyntaxNodeAnalysisContext context)
        {
            if (GeneratedCodeAnalyzer?.IsGeneratedCode(context) == true)
                return;

            var yieldStatement = (YieldStatementSyntax)context.Node;

            AnalyzeSemicolon(context, yieldStatement, yieldStatement.SemicolonToken);
        }

        private void AnalyzeUsingDirective(SyntaxNodeAnalysisContext context)
        {
            if (GeneratedCodeAnalyzer?.IsGeneratedCode(context) == true)
                return;

            var usingDirective = (UsingDirectiveSyntax)context.Node;

            AnalyzeSemicolon(context, usingDirective, usingDirective.SemicolonToken);
        }

        private void AnalyzeExternAliasDirective(SyntaxNodeAnalysisContext context)
        {
            if (GeneratedCodeAnalyzer?.IsGeneratedCode(context) == true)
                return;

            var externAliasDirective = (ExternAliasDirectiveSyntax)context.Node;

            AnalyzeSemicolon(context, externAliasDirective, externAliasDirective.SemicolonToken);
        }

        private static void AnalyzeSemicolon(SyntaxNodeAnalysisContext context, SyntaxNode node, SyntaxToken semicolon)
        {
            if (semicolon.IsMissing)
            {
                SyntaxToken token = node.GetLastToken();

                if (!token.IsKind(SyntaxKind.None))
                {
                    SyntaxTriviaList trailingTrivia = token.TrailingTrivia;

                    if (trailingTrivia.Any())
                    {
                        context.ReportDiagnostic(
                            DiagnosticDescriptors.AddMissingSemicolon,
                            Location.Create(semicolon.SyntaxTree, new TextSpan(trailingTrivia.Span.Start, 1)));
                    }
                }
            }
        }
    }
}
