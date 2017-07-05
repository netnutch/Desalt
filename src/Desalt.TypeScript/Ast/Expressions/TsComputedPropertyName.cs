﻿// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="TsComputedPropertyName.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace Desalt.TypeScript.Ast.Expressions
{
    using System;
    using Desalt.Core.Ast;
    using Desalt.Core.Emit;

    /// <summary>
    /// Represents a property name inside of an object of the form '[ expression ]'.
    /// </summary>
    internal class TsComputedPropertyName : AstNode<TsVisitor>, ITsComputedPropertyName
    {
        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        public TsComputedPropertyName(ITsExpression expression)
        {
            Expression = expression ?? throw new ArgumentNullException(nameof(expression));
        }

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public ITsExpression Expression { get; }

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        public override void Accept(TsVisitor visitor) => visitor.VisitComputedPropertyName(this);

        public override string CodeDisplay => $"[{Expression.CodeDisplay}]";

        public override void Emit(Emitter emitter)
        {
            emitter.Write("[");
            Expression.Emit(emitter);
            emitter.Write("]");
        }
    }
}