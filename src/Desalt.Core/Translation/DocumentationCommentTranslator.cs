﻿// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="DocumentationCommentTranslator.cs" company="Justin Rockwood">
//   Copyright (c) Justin Rockwood. All Rights Reserved. Licensed under the Apache License, Version 2.0. See
//   LICENSE.txt in the project root for license information.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace Desalt.Core.Translation
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Desalt.Core.TypeScript.Ast;
    using Factory = Desalt.Core.TypeScript.Ast.TsAstFactory;

    /// <summary>
    /// Converts a CSharp XML documentation comment into a TypeScript JsDoc comment.
    /// </summary>
    internal class DocumentationCommentTranslator
    {
        //// ===========================================================================================================
        //// Member Variables
        //// ===========================================================================================================

        private const RegexOptions InlineElementOptions = RegexOptions.CultureInvariant |
            RegexOptions.IgnoreCase |
            RegexOptions.ExplicitCapture |
            RegexOptions.Singleline;

        private static readonly Regex s_seeCrefTypeRegex = new Regex(
            @"<see(also)?\s+cref\s*=\s*""T:(?<fullTypeName>(\w+\.?)+)""\s*/>",
            InlineElementOptions);

        private static readonly Regex s_seeCrefMemberRegex = new Regex(
            @"<see(also)?\s+cref\s*=\s*""(M|P|E):(?<fullTypeName>(\w+\.)+)(?<memberName>\w+).*""\s*/>",
            InlineElementOptions);

        private static readonly Regex s_seeLangwordRegex = new Regex(
            @"<see\s+langword\s*=\s*""(?<langword>[^""]+)""\s*/>",
            InlineElementOptions);

        private static readonly Regex s_ctagRegex = new Regex(@"<c>(?<content>[^>]*)</c>", InlineElementOptions);

        /// <summary>
        /// Used for <see cref="TranslateElementText"/> method, to prevent new allocation of string
        /// </summary>
        private static readonly string[] s_newLineAsStringArray = { Environment.NewLine };

        //// ===========================================================================================================
        //// Constructors
        //// ===========================================================================================================

        private DocumentationCommentTranslator(DocumentationComment comment)
        {
            Comment = comment ?? throw new ArgumentNullException(nameof(comment));
        }

        //// ===========================================================================================================
        //// Properties
        //// ===========================================================================================================

        public DocumentationComment Comment { get; }

        //// ===========================================================================================================
        //// Methods
        //// ===========================================================================================================

        public static ITsJsDocComment Translate(DocumentationComment documentationComment)
        {
            var translator = new DocumentationCommentTranslator(documentationComment);
            return translator.TranslateInternal();
        }

        private ITsJsDocComment TranslateInternal()
        {
            ITsJsDocCommentBuilder builder = Factory.JsDocCommentBuilder();

            // if there is <summary> and <remarks>, then append the remarks after the summary,
            // otherwise just use one or the other as the description
            string description = Comment.SummaryText;
            if (!string.IsNullOrEmpty(Comment.RemarksText))
            {
                if (description == null)
                {
                    description = Comment.RemarksText;
                }
                else
                {
                    description += "\n" + Comment.RemarksText;
                }
            }

            builder.SetDescription(TranslateElementText(description));

            // <example>
            builder.AddExampleTag(Comment.ExampleText);

            // translate each <param> tag
            foreach (string parameterName in Comment.ParameterNames)
            {
                string parameterText = Comment.GetParameterText(parameterName);
                builder.AddParamTag(parameterName, TranslateElementText(parameterText));
            }

            // translate each <typeparam> tags, even though there is no JSDoc equivalent
            foreach (string typeParameterName in Comment.TypeParameterNames)
            {
                string parameterText = Comment.GetTypeParameterText(typeParameterName);
                builder.AddTypeParamTag(typeParameterName, TranslateElementText(parameterText));
            }

            // <returns>
            builder.SetReturnsTag(TranslateElementText(Comment.ReturnsText));

            // translate each <exception> tag
            foreach (string typeName in Comment.ExceptionTypes)
            {
                foreach (string exceptionText in Comment.GetExceptionTexts(typeName))
                {
                    builder.AddThrowsTag(RemoveNamespace(typeName), TranslateElementText(exceptionText));
                }
            }

            return builder.Build();
        }

        private static ITsJsDocBlock TranslateElementText(string text)
        {
            ITsJsDocBlock translated = TranslateKnownXmlTags(text);
            return translated;
        }

        /// <summary>
        /// Converts known XML documentation tags (see, seealso, etc.) to the TypeScript equivalent.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static ITsJsDocBlock TranslateKnownXmlTags(string text)
        {
            if (text == null)
            {
                return null;
            }

            string translated = text;

            // translate <see langword="x"/> to `x`.
            translated = s_seeLangwordRegex.Replace(translated, match => $"`{match.Groups["langword"]}`");

            // translate <c>x</c> to `x`.
            translated = s_ctagRegex.Replace(translated, match => $"`{match.Groups["content"]}`");

            // translate <see/seealso cref="Type"/> to '{@link Type}'
            translated = s_seeCrefTypeRegex.Replace(
                translated,
                match => $"{{@link {RemoveNamespace(match.Groups["fullTypeName"].Value)}}}");

            // translate <see/seealso cref="M:Type.Member"/> to '@see Type.Member'
            translated = s_seeCrefMemberRegex.Replace(
                translated,
                match => $"{{@link {RemoveNamespace(match.Groups["fullTypeName"].Value)}.{match.Groups["memberName"]}}}");

            return Factory.JsDocBlock(translated);
        }

        private static string RemoveNamespace(string fullTypeName)
        {
            return fullTypeName.TrimEnd('.').Split('.').Last();
        }
    }
}
