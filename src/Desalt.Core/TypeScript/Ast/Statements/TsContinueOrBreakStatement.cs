﻿// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="TsContinueOrBreakStatement.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace Desalt.Core.TypeScript.Ast.Statements
{
    using Desalt.Core.Ast;
    using Desalt.Core.Emit;

    /// <summary>
    /// Represents a continue or break statement of the form, 'continue|break [label]'.
    /// </summary>
    internal class TsContinueOrBreakStatement : AstNode, ITsContinueStatement, ITsBreakStatement
    {
        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        public TsContinueOrBreakStatement(bool isContinue, ITsIdentifier label = null)
        {
            IsContinue = isContinue;
            Label = label;
        }

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public ITsIdentifier Label { get; }
        public bool IsContinue { get; }

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        public override void Accept(TsVisitor visitor)
        {
            if (IsContinue)
            {
                visitor.VisitContinueStatement(this);
            }
            else
            {
                visitor.VisitBreakStatement(this);
            }
        }

        public override string CodeDisplay =>
            (IsContinue ? "continue" : "break") + (Label == null ? "" : $" {Label}") + ";";

        protected override void EmitInternal(Emitter emitter)
        {
            emitter.Write(IsContinue ? "continue" : "break");

            if (Label != null)
            {
                emitter.Write(" ");
                Label.Emit(emitter);
            }

            emitter.WriteLine(";");
        }
    }
}
