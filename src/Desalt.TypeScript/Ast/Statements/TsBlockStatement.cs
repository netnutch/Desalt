﻿// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="TsBlockStatement.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace Desalt.TypeScript.Ast.Statements
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using Desalt.Core.Ast;
    using Desalt.Core.Emit;

    /// <summary>
    /// Represents a block statement of the form '{ statements }'.
    /// </summary>
    internal class TsBlockStatement : AstNode<TsVisitor>, ITsBlockStatement
    {
        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        public TsBlockStatement(IEnumerable<ITsStatementListItem> statements)
        {
            Statements = statements?.ToImmutableArray() ?? ImmutableArray<ITsStatementListItem>.Empty;
        }

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public ImmutableArray<ITsStatementListItem> Statements { get; }

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        public override void Accept(TsVisitor visitor) => visitor.VisitBlockStatement(this);

        public override string CodeDisplay => $"{{ {Statements.ToElidedList(Environment.NewLine)} }}";

        public override void Emit(Emitter emitter) => emitter.WriteBlock(Statements, skipNewlines: true);
    }
}