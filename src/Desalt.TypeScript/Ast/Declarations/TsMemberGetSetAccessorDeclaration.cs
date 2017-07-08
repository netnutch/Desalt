﻿// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="TsMemberGetSetAccessorDeclaration.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace Desalt.TypeScript.Ast.Declarations
{
    using System;
    using Desalt.Core.Ast;
    using Desalt.Core.Emit;

    /// <summary>
    /// Represents either a 'get' or a 'set' member accessor declaration in a class.
    /// </summary>
    internal class TsMemberGetSetAccessorDeclaration : AstNode<TsVisitor>,
        ITsMemberGetAccessorDeclaration, ITsMemberSetAccessorDeclaration
    {
        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        public TsMemberGetSetAccessorDeclaration(
            ITsGetAccessor getAccessor,
            TsAccessibilityModifier? accessibilityModifier = null,
            bool isStatic = false)
        {
            GetAccessor = getAccessor ?? throw new ArgumentNullException(nameof(getAccessor));
            AccessibilityModifier = accessibilityModifier;
            IsStatic = isStatic;
        }

        public TsMemberGetSetAccessorDeclaration(
            ITsSetAccessor setAccessor,
            TsAccessibilityModifier? accessibilityModifier = null,
            bool isStatic = false)
        {
            SetAccessor = setAccessor ?? throw new ArgumentNullException(nameof(setAccessor));
            AccessibilityModifier = accessibilityModifier;
            IsStatic = isStatic;
        }

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public TsAccessibilityModifier? AccessibilityModifier { get; }
        public bool IsStatic { get; }
        public ITsGetAccessor GetAccessor { get; }
        public ITsSetAccessor SetAccessor { get; }

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        public override void Accept(TsVisitor visitor)
        {
            if (GetAccessor != null)
            {
                visitor.VisitMemberGetAccessorDeclaration(this);
            }
            else
            {
                visitor.VisitMemberSetAccessorDeclaration(this);
            }
        }

        public override string CodeDisplay =>
            $"{AccessibilityModifier.OptionalAccessibility()}{IsStatic.OptionalStaticDeclaration()}" +
            (GetAccessor != null ? GetAccessor.CodeDisplay : SetAccessor.CodeDisplay);

        public override void Emit(Emitter emitter)
        {
            AccessibilityModifier.EmitOptionalAccessibility(emitter);
            IsStatic.EmitOptionalStaticDeclaration(emitter);
            GetAccessor?.Emit(emitter);
            SetAccessor?.Emit(emitter);
            emitter.WriteLine();
        }
    }
}
