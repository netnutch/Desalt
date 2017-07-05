﻿// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="TsEmitTests.LeftHandSideExpressions.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace Desalt.TypeScript.Tests.Ast
{
    using Desalt.TypeScript.Ast;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Factory = Desalt.TypeScript.Ast.TsAstFactory;

    public partial class TsEmitTests
    {
        /* 12.e Left-Hand-Side Expressions
         * -------------------------------
         * MemberExpression:
         *   PrimaryExpression
         *   MemberExpression [ Expression ]
         *   MemberExpression . IdentifierName
         *   MemberExpression TemplateLiteral
         *   SuperProperty
         *   MetaProperty
         *   new MemberExpression Arguments
         */

        [TestMethod]
        public void Emit_bracket_member_expression()
        {
            const string expected = @"x['throw']";
            ITsMemberBracketExpression expression =
                Factory.MemberBracket(s_x, Factory.String("throw"));

            VerifyOutput(expression, expected);
        }

        [TestMethod]
        public void Emit_dot_notation_member_expression()
        {
            VerifyOutput(Factory.MemberDot(s_x, "y"), "x.y");
        }

        [TestMethod]
        public void Emit_super_bracket_expression()
        {
            VerifyOutput(Factory.SuperBracket(s_z), "super[z]");
        }

        [TestMethod]
        public void Emit_super_dot_expression()
        {
            VerifyOutput(Factory.SuperDot("name"), "super.name");
        }

        [TestMethod]
        public void Emit_call_expression()
        {
            VerifyOutput(
                Factory.Call(s_x, Factory.Argument(s_y), Factory.Argument(s_z, isSpreadArgument: true)),
                "x(y, ... z)");
        }

        [TestMethod]
        public void Emit_new_call_expression()
        {
            VerifyOutput(
                Factory.NewCall(s_x, Factory.Argument(s_y), Factory.Argument(s_z, isSpreadArgument: true)),
                "new x(y, ... z)");
        }

        [TestMethod]
        public void Emit_super_call_expression()
        {
            VerifyOutput(
                Factory.SuperCall(Factory.Argument(s_y), Factory.Argument(s_z, isSpreadArgument: true)),
                "super(y, ... z)");
        }

        [TestMethod]
        public void Emit_new_target_expression()
        {
            VerifyOutput(Factory.NewTarget, "new.target");
        }
    }
}