﻿// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="TsEmitTests.LeftHandSideExpressions.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace Desalt.Core.Tests.TypeScript.Ast
{
    using Desalt.Core.TypeScript.Ast;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Factory = Desalt.Core.TypeScript.Ast.TsAstFactory;

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
                Factory.Call(s_x, Factory.ArgumentList(
                    Factory.Argument(s_y),
                    Factory.Argument(s_z, isSpreadArgument: true))),
                "x(y, ... z)");
        }

        [TestMethod]
        public void Emit_new_call_expression()
        {
            VerifyOutput(
                Factory.NewCall(s_x, Factory.ArgumentList(
                    Factory.Argument(s_y),
                    Factory.Argument(s_z, isSpreadArgument: true))),
                "new x(y, ... z)");
        }

        [TestMethod]
        public void Emit_super_call_expression()
        {
            VerifyOutput(
                Factory.SuperCall(Factory.ArgumentList(
                    Factory.Argument(s_y),
                    Factory.Argument(s_z, isSpreadArgument: true))),
                "super(y, ... z)");
        }

        [TestMethod]
        public void Emit_new_target_expression()
        {
            VerifyOutput(Factory.NewTarget, "new.target");
        }

        [TestMethod]
        public void Emit_arrow_functions()
        {
            VerifyOutput(Factory.ArrowFunction(s_x, s_y), "x => y");
            VerifyOutput(Factory.ArrowFunction(Factory.CallSignature(), s_y), "() => y");
            VerifyOutput(
                Factory.ArrowFunction(
                    Factory.CallSignature(
                        Factory.TypeParameters(Factory.TypeParameter(s_T)),
                        Factory.ParameterList(Factory.BoundRequiredParameter(s_x, s_TRef))),
                    s_y),
                "<T>(x: T) => y");

            VerifyOutput(
                Factory.ArrowFunction(
                    Factory.CallSignature(Factory.ParameterList(s_x)),
                    Factory.Return(s_y)),
                "(x) => {\n  return y;\n}");
        }
    }
}