﻿using CodeSmellAnnotations.Analyzers.Extensions;
using CodeSmellAnnotations.Attributes;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;

namespace CodeSmellAnnotations.Analyzers.Rules
{
    internal class DuplicateCodeAttributeRule : IRule
    {
        private static readonly DiagnosticDescriptor _descriptor
            = new DiagnosticDescriptor("SML002", 
                "Duplicate code", 
                "Duplcate code.{0}{1}", 
                "CodeSmell", 
                DiagnosticSeverity.Warning, 
                isEnabledByDefault: true,
                description: "Duplicate code",
                helpLinkUri: "https://github.com/rsvilenov/CodeSmellAnnotations/tree/master/docs/rules/SML002.md");

        public DiagnosticDescriptor Descriptor => _descriptor;
        public Type TriggeringAttributeType => typeof(DuplicateCodeAttribute);

        public string[] GetDiagnosticMessageArguments(AttributeSyntax attributeSyntax)
        {
            var reason = attributeSyntax.GetStringArgumentValue("Reason");
            var duplicates = attributeSyntax.GetStringArgumentValue("Duplicates");

            return new[] 
            { 
                string.IsNullOrEmpty(duplicates) ? null : $" Duplicates {duplicates}.", 
                string.IsNullOrEmpty(reason) ? null : $" Reason: {reason}", 
            };
        }
    }
}
