﻿// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="TsQualifiedName.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace Desalt.TypeScript.Ast
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Linq;
    using Desalt.Core.Ast;
    using Desalt.Core.Emit;

    /// <summary>
    /// Represents a qualified name, which has dots between identifiers. For example, 'ns.type.method'.
    /// </summary>
    internal class TsQualifiedName : AstNode<TsVisitor>, ITsQualifiedName
    {
        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        public TsQualifiedName(ITsIdentifier right, IEnumerable<ITsIdentifier> left = null)
        {
            Left = left?.ToImmutableArray() ?? ImmutableArray<ITsIdentifier>.Empty;
            Right = right ?? throw new ArgumentNullException(nameof(right));
        }

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public ImmutableArray<ITsIdentifier> Left { get; }
        public ITsIdentifier Right { get; }

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        public override void Accept(TsVisitor visitor) => visitor.VisitQualifiedName(this);

        public override string CodeDisplay => $"{string.Join(".", Left.Select(x => x.CodeDisplay))}{Right.CodeDisplay}";

        public override void Emit(Emitter emitter)
        {
            foreach (ITsIdentifier left in Left)
            {
                left.Emit(emitter);
                emitter.Write(".");
            }

            Right.Emit(emitter);
        }
    }
}