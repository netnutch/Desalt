﻿// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="TsArrayType.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace Desalt.Core.TypeScript.Ast.Types
{
    using System;
    using Desalt.Core.Emit;

    /// <summary>
    /// Represents a TypeScript array type.
    /// </summary>
    internal class TsArrayType : AstNode, ITsArrayType
    {
        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        public TsArrayType(ITsType type)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
        }

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public ITsType Type { get; }

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        public override void Accept(TsVisitor visitor) => visitor.VisitArrayType(this);

        public override string CodeDisplay => $"{Type.CodeDisplay}[]";

        protected override void EmitInternal(Emitter emitter)
        {
            Type.Emit(emitter);
            emitter.Write("[]");
        }
    }
}