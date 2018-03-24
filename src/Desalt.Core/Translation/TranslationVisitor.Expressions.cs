﻿// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="TranslationVisitor.Expressions.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace Desalt.Core.Translation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Desalt.Core.Extensions;
    using Desalt.Core.TypeScript.Ast;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Factory = Desalt.Core.TypeScript.Ast.TsAstFactory;

    internal sealed partial class TranslationVisitor
    {
        /// <summary>
        /// Called when the visitor visits a LiteralExpressionSyntax node.
        /// </summary>
        /// <returns>An <see cref="ITsExpression"/>.</returns>
        public override IEnumerable<IAstNode> VisitLiteralExpression(LiteralExpressionSyntax node)
        {
            switch (node.Kind())
            {
                case SyntaxKind.NumericLiteralExpression:
                    return node.Token.Text.StartsWith("0x", StringComparison.OrdinalIgnoreCase)
                        ? Factory.HexInteger(Convert.ToInt64(node.Token.Value)).ToSingleEnumerable()
                        : Factory.Number(Convert.ToDouble(node.Token.Value)).ToSingleEnumerable();
            }

            DefaultVisit(node);
            return Enumerable.Empty<IAstNode>();
        }
    }
}
