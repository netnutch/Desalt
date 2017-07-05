﻿// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="TsEmitTests.LiteralExpressions.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace Desalt.TypeScript.Tests.Ast
{
    using System;
    using Desalt.TypeScript.Ast;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Factory = Desalt.TypeScript.Ast.TsAstFactory;

    public partial class TsEmitTests
    {
        //// ===========================================================================================================
        //// Literal Expressions
        //// ===========================================================================================================

        [TestMethod]
        public void Emit_null_literal()
        {
            VerifyOutput(Factory.Null, "null");
        }

        [TestMethod]
        public void Emit_boolean_literals()
        {
            VerifyOutput(Factory.True, "true");
            VerifyOutput(Factory.False, "false");
        }

        [TestMethod]
        public void Emit_string_literals()
        {
            VerifyOutput(Factory.String("single"), "'single'");
            VerifyOutput(Factory.String("double", StringLiteralQuoteKind.DoubleQuote), "\"double\"");
        }

        [TestMethod]
        public void Number_literals_should_be_positive()
        {
            Action action = () => Factory.Number(-123);
            action.ShouldThrowExactly<ArgumentException>().And.ParamName.Should().Be("value");

            action = () => Factory.BinaryInteger(-123);
            action.ShouldThrowExactly<ArgumentException>().And.ParamName.Should().Be("value");

            action = () => Factory.OctalInteger(-123);
            action.ShouldThrowExactly<ArgumentException>().And.ParamName.Should().Be("value");

            action = () => Factory.HexInteger(-123);
            action.ShouldThrowExactly<ArgumentException>().And.ParamName.Should().Be("value");
        }

        [TestMethod]
        public void Emit_decimal_literals()
        {
            VerifyOutput(Factory.Number(123), "123");
            VerifyOutput(Factory.Number(1.23e4), "12300");
            VerifyOutput(Factory.Number(83e45), "8.3E+46");
            VerifyOutput(Factory.Number(53e-53), "5.3E-52");
        }

        [TestMethod]
        public void Emit_binary_integer_literals()
        {
            VerifyOutput(Factory.BinaryInteger(17), "0b10001");
        }

        [TestMethod]
        public void Emit_octal_integer_literals()
        {
            VerifyOutput(Factory.OctalInteger(20), "0o24");
        }

        [TestMethod]
        public void Emit_hex_integer_literal()
        {
            VerifyOutput(Factory.HexInteger(415), "0x19f");
            VerifyOutput(Factory.HexInteger(48879), "0xbeef");
        }

        [TestMethod]
        public void Emit_regular_expression_literals()
        {
            VerifyOutput(Factory.RegularExpression("a-z", "g"), "/a-z/g");
            VerifyOutput(Factory.RegularExpression("hello", null), "/hello/");
        }

        [TestMethod]
        public void Emit_array_literals()
        {
            VerifyOutput(
                Factory.Array(
                    Factory.ArrayElement(s_x),
                    Factory.ArrayElement(Factory.Number(10))),
                "[x, 10]");

            VerifyOutput(
                Factory.Array(
                    Factory.ArrayElement(s_y),
                    Factory.ArrayElement(s_z),
                    Factory.ArrayElement(Factory.String("str"))),
                "[y, z, 'str']");
        }

        [TestMethod]
        public void Emit_array_literals_with_spread_operator()
        {
            VerifyOutput(Factory.Array(Factory.ArrayElement(s_y, isSpreadElement: true)), "[... y]");
        }

        [TestMethod]
        public void Emit_template_literals()
        {
            VerifyOutput(Factory.TemplateString(new TsTemplatePart(template: "string")), "`string`");
            VerifyOutput(
                Factory.TemplateString(
                    new TsTemplatePart("xy=", s_x),
                    new TsTemplatePart(expression: s_y)),
                "`xy=${x}${y}`");
        }

        //// ===========================================================================================================
        //// Object Literal Expressions
        //// ===========================================================================================================

        [TestMethod]
        public void Emit_empty_object_literal()
        {
            VerifyOutput(Factory.EmptyObject, "{}");
        }
    }
}