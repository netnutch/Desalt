﻿// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="Es5EmptyStatement.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace Desalt.JavaScript.Ast.Statements
{
    using Desalt.Core.Ast;
    using Desalt.Core.Emit;

    /// <summary>
    /// Represents an empty statement.
    /// </summary>
    public sealed class Es5EmptyStatement : AstNode<Es5Visitor>, IEs5Statement
    {
        //// ===========================================================================================================
        //// Member Variables
        //// ===========================================================================================================

        internal static readonly Es5EmptyStatement Instance = new Es5EmptyStatement();

        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        private Es5EmptyStatement()
        {
        }

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        public override void Accept(Es5Visitor visitor) => visitor.VisitEmptyStatement(this);

        public override string CodeDisplay => ";";

        public override void Emit(Emitter emitter) => emitter.WriteLine(";");
    }
}