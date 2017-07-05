﻿// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="TsEmitTests.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace Desalt.TypeScript.Tests.Ast
{
    using System.IO;
    using Desalt.Core.Ast;
    using Desalt.Core.Emit;
    using Desalt.Core.Extensions;
    using Desalt.TypeScript.Ast;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Factory = Desalt.TypeScript.Ast.TsAstFactory;

    [TestClass]
    public partial class TsEmitTests
    {
        private static readonly ITsIdentifier s_p = Factory.Identifier("p");
        private static readonly ITsIdentifier s_x = Factory.Identifier("x");
        private static readonly ITsIdentifier s_y = Factory.Identifier("y");
        private static readonly ITsIdentifier s_z = Factory.Identifier("z");

        private static void VerifyOutput(IAstNode node, string expected, EmitOptions options = null)
        {
            using (var stream = new MemoryStream())
            using (var emitter = new Emitter(stream, options: options ?? EmitOptions.UnixSpaces))
            {
                node.Emit(emitter);
                stream.ReadAllText(emitter.Encoding).Should().Be(expected);
            }
        }
    }
}