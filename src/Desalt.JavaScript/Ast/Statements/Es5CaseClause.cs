﻿// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="Es5CaseClause.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace Desalt.JavaScript.Ast.Statements
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using Desalt.Core.Ast;
    using Desalt.Core.Emit;

    /// <summary>
    /// Represents a 'case' clause within a 'switch' statement.
    /// </summary>
    public sealed class Es5CaseClause : AstNode<Es5Visitor>
    {
        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        internal Es5CaseClause(IEs5Expression expression, IEnumerable<IEs5Statement> statements = null)
        {
            Expression = expression ?? throw new ArgumentNullException(nameof(expression));
            Statements = statements?.ToImmutableArray() ?? ImmutableArray<IEs5Statement>.Empty;
        }

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public IEs5Expression Expression { get; }
        public ImmutableArray<IEs5Statement> Statements { get; }

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        public override void Accept(Es5Visitor visitor) => visitor.VisitCaseClause(this);

        public override string CodeDisplay => $"case {Expression}: {Statements.ToElidedList()}";

        public override void Emit(Emitter emitter)
        {
            emitter.Write("case ");
            Expression.Emit(emitter);
            emitter.WriteLine(":");
            emitter.IndentLevel++;

            foreach (IEs5Statement statement in Statements)
            {
                statement.Emit(emitter);
            }

            emitter.IndentLevel--;
        }
    }
}
