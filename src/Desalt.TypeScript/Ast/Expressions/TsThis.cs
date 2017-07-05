// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="TsThis.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace Desalt.TypeScript.Ast.Expressions
{
    using Desalt.Core.Ast;
    using Desalt.Core.Emit;

    /// <summary>
    /// Represents the 'this' expression.
    /// </summary>
    internal class TsThis : AstNode<TsVisitor>, ITsThis
    {
        //// ===========================================================================================================
        //// Member Variables
        //// ===========================================================================================================

        internal static readonly TsThis Instance = new TsThis();

        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        private TsThis()
        {
        }

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        public override void Accept(TsVisitor visitor) => visitor.VisitThis(this);

        public override string CodeDisplay => "this";

        public override void Emit(Emitter emitter) => emitter.Write("this");
    }
}