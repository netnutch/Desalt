﻿// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="Es5IfStatement.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace Desalt.JavaScript.Ast.Statements
{
    using System;
    using System.Text;
    using Desalt.Core.Ast;
    using Desalt.Core.Emit;

    public sealed class Es5IfStatement : AstNode<Es5Visitor>, IEs5Statement
    {
        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        internal Es5IfStatement(
            IEs5Expression ifExpression,
            IEs5Statement ifStatement,
            IEs5Statement elseStatement)
        {
            IfExpression = ifExpression ?? throw new ArgumentNullException(nameof(ifExpression));
            IfStatement = ifStatement ?? throw new ArgumentNullException(nameof(ifStatement));
            ElseStatement = elseStatement;
        }

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public IEs5Expression IfExpression { get; }
        public IEs5Statement IfStatement { get; }
        public IEs5Statement ElseStatement { get; }

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        public Es5IfStatement WithElseStatement(IEs5Statement statement) =>
            new Es5IfStatement(IfExpression, IfStatement, statement);

        public override void Accept(Es5Visitor visitor) => visitor.VisitIfStatement(this);

        public override string CodeDisplay
        {
            get
            {
                var builder = new StringBuilder($"if ({IfExpression}) {IfStatement}");
                if (ElseStatement != null)
                {
                    builder.Append($"else {ElseStatement}");
                }

                return builder.ToString();
            }
        }

        public override void Emit(Emitter emitter)
        {
            bool ifStatementIsBlock = IfStatement is Es5BlockStatement;

            emitter.Write("if (");
            IfExpression.Emit(emitter);
            emitter.WriteStatementIndentedOrInBlock(IfStatement, ifStatementIsBlock, ")", ") ");

            if (ElseStatement == null)
            {
                return;
            }

            emitter.Write(ifStatementIsBlock ? " else" : "else");
            emitter.WriteStatementIndentedOrInBlock(ElseStatement, ElseStatement is Es5BlockStatement);
        }
    }
}