﻿// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeScriptVisitor.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace Desalt.TypeScript.CodeModels
{
    using Desalt.Core.CodeModels;

    /// <summary>
    /// Represents an <see cref="ITypeScriptCodeModel"/> visitor that visits only the single model passed
    /// into its Visit method.
    /// </summary>
    public abstract partial class TypeScriptVisitor : CodeModelVisitor<ITypeScriptCodeModel>
    {
        public override void Visit(ITypeScriptCodeModel model) => model?.Accept(this);

        /// <summary>
        /// Visits a TypeScript implementation (.ts) source file.
        /// </summary>
        public virtual void VisitImplementationSourceFile(ImplementationSourceFile model) => DefaultVisit(model);
    }

    /// <summary>
    /// Represents an <see cref="ITypeScriptCodeModel"/> visitor that visits only the single model passed
    /// into its Visit method and produces a value of the type specified by the <typeparamref
    /// name="TResult"/> parameter.
    /// </summary>
    /// <typeparam name="TResult">The type of the return value this visitor's Visit method.</typeparam>
    public abstract partial class TypeScriptVisitor<TResult> : CodeModelVisitor<ITypeScriptCodeModel, TResult>
    {
        public override TResult Visit(ITypeScriptCodeModel model)
        {
            return model != null ? model.Accept(this) : default(TResult);
        }

        /// <summary>
        /// Visits a TypeScript implementation (.ts) source file.
        /// </summary>
        public virtual TResult VisitImplementationSourceFile(ImplementationSourceFile model) => DefaultVisit(model);
    }
}