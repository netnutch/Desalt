﻿// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="TsSimpleLexicalBinding.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace Desalt.Core.TypeScript.Ast.Declarations
{
    using System;
    using Desalt.Core.Ast;
    using Desalt.Core.Emit;

    /// <summary>
    /// Represents a simple lexical binding of the form 'x: type = y'.
    /// </summary>
    internal class TsSimpleLexicalBinding : AstNode, ITsSimpleLexicalBinding
    {
        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        public TsSimpleLexicalBinding(
            ITsIdentifier variableName,
            ITsType variableType = null,
            ITsExpression initializer = null)
        {
            VariableName = variableName ?? throw new ArgumentNullException(nameof(variableName));
            VariableType = variableType;
            Initializer = initializer;
        }

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public ITsIdentifier VariableName { get; }
        public ITsType VariableType { get; }
        public ITsExpression Initializer { get; }

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        public override void Accept(TsVisitor visitor) => visitor.VisitSimpleLexicalBinding(this);

        public override string CodeDisplay
        {
            get
            {
                string display = VariableName.CodeDisplay;
                display += VariableType?.OptionalTypeAnnotation();
                display += Initializer.OptionalAssignment();
                return display;
            }
        }

        protected override void EmitInternal(Emitter emitter)
        {
            VariableName.Emit(emitter);
            VariableType?.EmitOptionalTypeAnnotation(emitter);
            Initializer.EmitOptionalAssignment(emitter);
        }
    }
}
