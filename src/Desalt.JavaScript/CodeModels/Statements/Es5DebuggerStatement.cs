﻿// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="Es5DebuggerStatement.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace Desalt.JavaScript.CodeModels.Statements
{
    using Desalt.Core.Utility;

    /// <summary>
    /// Represents a JavaScript 'debugger' statement.
    /// </summary>
    public sealed class Es5DebuggerStatement : Es5CodeModel, IEs5Statement
    {
        //// ===========================================================================================================
        //// Member Variables
        //// ===========================================================================================================

        internal static readonly Es5DebuggerStatement Instance = new Es5DebuggerStatement();

        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        private Es5DebuggerStatement()
        {
        }

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        public override void Accept(Es5Visitor visitor)
        {
            visitor.VisitDebuggerStatement(this);
        }

        public override T Accept<T>(Es5Visitor<T> visitor)
        {
            return visitor.VisitDebuggerStatement(this);
        }

        public override string ToCodeDisplay() => "debugger;";

        public override void WriteFullCodeDisplay(IndentedTextWriter writer)
        {
            writer.Write("debugger");
        }
    }
}