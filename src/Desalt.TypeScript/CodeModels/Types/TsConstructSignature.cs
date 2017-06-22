﻿// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="TsConstructSignature.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace Desalt.TypeScript.CodeModels.Types
{
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using Desalt.Core.CodeModels;
    using Desalt.Core.Utility;

    /// <summary>
    /// Represents a constructor method signature of the form 'new &lt;T&gt;(parameter: type): type'.
    /// </summary>
    internal class TsConstructSignature : CodeModel, ITsConstructSignature
    {
        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        public TsConstructSignature(
            IEnumerable<ITsTypeParameter> typeParameters = null,
            ITsParameterList parameterList = null,
            ITsType typeAnnotation = null)
        {
            TypeParameters = typeParameters?.ToImmutableArray() ?? ImmutableArray<ITsTypeParameter>.Empty;
            ParameterList = parameterList;
            TypeAnnotation = typeAnnotation;
        }

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public ImmutableArray<ITsTypeParameter> TypeParameters { get; }
        public ITsParameterList ParameterList { get; }
        public ITsType TypeAnnotation { get; }

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        public void Accept(TypeScriptVisitor visitor) => visitor.VisitConstructSignature(this);

        public T Accept<T>(TypeScriptVisitor<T> visitor) => visitor.VisitConstructSignature(this);

        public override string ToCodeDisplay()
        {
            string display = "new ";

            if (TypeParameters.Length > 0)
            {
                display += $"<{TypeParameters.ToElidedList()}>";
            }

            display += $"(${ParameterList.ToCodeDisplay()})";

            if (TypeAnnotation != null)
            {
                display += $": {TypeAnnotation.ToCodeDisplay()}";
            }

            return display;
        }

        public override void WriteFullCodeDisplay(IndentedTextWriter writer)
        {
            writer.Write("new ");

            if (TypeParameters.Length > 0)
            {
                WriteItems(writer, TypeParameters, indent: false, prefix: "<", suffix: ">", itemDelimiter: ", ");
            }

            writer.Write("(");
            ParameterList.WriteFullCodeDisplay(writer);
            writer.Write(")");

            if (TypeAnnotation != null)
            {
                writer.Write(": ");
                TypeAnnotation.WriteFullCodeDisplay(writer);
            }
        }
    }
}
