﻿// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="TsBooleanLiteral.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace Desalt.TypeScript.CodeModels.Expressions
{
    using Desalt.Core.CodeModels;
    using Desalt.Core.Utility;

    /// <summary>
    /// Represents an expression containing a numeric literal value.
    /// </summary>
    internal class TsBooleanLiteral : CodeModel, ITsBooleanLiteral
    {
        //// ===========================================================================================================
        //// Member Variables
        //// ===========================================================================================================

        public static readonly TsBooleanLiteral True = new TsBooleanLiteral(true);

        public static readonly TsBooleanLiteral False = new TsBooleanLiteral(false);

        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        private TsBooleanLiteral(bool value)
        {
            Value = value;
        }

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public bool Value { get; }

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        public void Accept(TypeScriptVisitor visitor) => visitor.VisitBooleanLiteral(this);

        public T Accept<T>(TypeScriptVisitor<T> visitor) => visitor.VisitBooleanLiteral(this);

        public override string ToCodeDisplay() => Value ? "true" : "false";

        public override void WriteFullCodeDisplay(IndentedTextWriter writer) => writer.Write(ToCodeDisplay());
    }
}
