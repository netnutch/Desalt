﻿// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="TsObjectLiteral.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace Desalt.TypeScript.Ast.Expressions
{
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using Desalt.Core.Ast;
    using Desalt.Core.Emit;

    /// <summary>
    /// Represents on object literal of the form '{ PropertyDefinition... }'.
    /// </summary>
    internal class TsObjectLiteral : AstNode<TsVisitor>, ITsObjectLiteral
    {
        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        public TsObjectLiteral(IEnumerable<ITsPropertyDefinition> propertyDefinitions)
        {
            PropertyDefinitions = propertyDefinitions?.ToImmutableArray() ??
                ImmutableArray<ITsPropertyDefinition>.Empty;
        }

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public ImmutableArray<ITsPropertyDefinition> PropertyDefinitions { get; }

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        public override void Accept(TsVisitor visitor) => visitor.VisitObjectLiteral(this);

        public override string CodeDisplay => $"Object Literal, PropertyCount = {PropertyDefinitions.Length}";

        public override void Emit(Emitter emitter)
        {
            WriteCommaNewlineSeparatedBlock(emitter, PropertyDefinitions);
        }
    }
}
