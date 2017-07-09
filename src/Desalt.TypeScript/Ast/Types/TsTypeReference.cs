﻿// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="TsTypeReference.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace Desalt.TypeScript.Ast.Types
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using Desalt.Core.Ast;
    using Desalt.Core.Emit;

    /// <summary>
    /// Represents a TypeScript type reference.
    /// </summary>
    internal class TsTypeReference : AstNode<TsVisitor>, ITsTypeReference
    {
        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        public TsTypeReference(ITsTypeName typeName, IEnumerable<ITsType> typeArguments = null)
        {
            TypeName = typeName ?? throw new ArgumentNullException(nameof(typeName));
            TypeArguments = typeArguments?.ToImmutableArray() ?? ImmutableArray<ITsType>.Empty;
        }

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public ITsTypeName TypeName { get; }
        public ImmutableArray<ITsType> TypeArguments { get; }

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        public override void Accept(TsVisitor visitor) => visitor.VisitTypeReference(this);

        public override string CodeDisplay =>
            TypeArguments.Length == 0 ? TypeName.CodeDisplay : $"{TypeName}<{TypeArguments.ToElidedList()}>";

        public override void Emit(Emitter emitter)
        {
            TypeName.Emit(emitter);

            if (TypeArguments.Length > 0)
            {
                emitter.WriteList(TypeArguments, indent: false, prefix: "<", suffix: ">", itemDelimiter: ", ");
            }
        }
    }
}
