﻿// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="TsArrayType.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace Desalt.TypeScript.Ast.Types
{
    using System;
    using Desalt.Core.Ast;
    using Desalt.Core.Emit;

    /// <summary>
    /// Represents a TypeScript array type.
    /// </summary>
    internal class TsArrayType : AstNode<TsVisitor>, ITsArrayType
    {
        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        public TsArrayType(ITsPrimaryType type)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
        }

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public ITsPrimaryType Type { get; }

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        public override void Accept(TsVisitor visitor) => visitor.VisitArrayType(this);

        public override string CodeDisplay => $"{Type.CodeDisplay}[]";

        public override void Emit(Emitter emitter)
        {
            Type.Emit(emitter);
            emitter.Write("[]");
        }
    }
}