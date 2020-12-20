using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Telegram.Bot.Generators
{
    [Generator]
    public sealed class JsonAttributesGenerator : ISourceGenerator
    {
        // ==============================

        private const string Placeholder = @"// <auto-generated />
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace {0}
{{
    [MetadataType(typeof({1}Metadata))]
    partial class {1}
    {{
        internal sealed class {1}Metadata
        {{
{2}
        }}
    }}
}}
";

        // ==============================

        private const string PropertyPlaceholder = @"            [JsonPropertyName(""{0}"")]
            public {1} {2} {{ get; init; }}";

        // ==============================

        private const string NewLinePlaceholder = @"

            ";

        // ==============================

        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new JsonAttributesSyntaxReceiver());
        }

        public void Execute(GeneratorExecutionContext context)
        {
            try
            {
                if (context.SyntaxReceiver is not JsonAttributesSyntaxReceiver receiver)
                    return;

                Compilation compilation = context.Compilation;

                INamedTypeSymbol jsonPropertyNameAttr =
                    compilation.GetTypeByMetadataName("System.Text.Json.JsonPropertyNameAttribute");
                INamedTypeSymbol jsonIgnoreAttribute =
                    compilation.GetTypeByMetadataName("System.Text.Json.JsonIgnoreAttribute");

                foreach (TypeDeclarationSyntax typeSyntax in receiver.Candidates)
                {
                    SemanticModel model = compilation.GetSemanticModel(typeSyntax.SyntaxTree);
                    ITypeSymbol type = (ITypeSymbol) model.GetDeclaredSymbol(typeSyntax);

                    if (type is IErrorTypeSymbol errorTypeSymbol)
                    {
                        context.ReportDiagnostic(Diagnostic.Create(new DiagnosticDescriptor("MySG002", "Hello!", errorTypeSymbol.ToDisplayString(), "Goofiness", DiagnosticSeverity.Error, true), Location.None));
                    }

                    var properties = new List<string>();

                    foreach (IPropertySymbol prop in type?.GetMembers().OfType<IPropertySymbol>()
                        .Where(p => p.DeclaredAccessibility == Accessibility.Public &&
                                    p.GetMethod != null && p.SetMethod != null) ?? Enumerable.Empty<IPropertySymbol>())
                    {
                        // skip this property if it already has the JsonIgnoreAttribute or the JsonPropertyNameAttribute
                        ImmutableArray<AttributeData> attributes = prop.GetAttributes();
                        var skip = false;
                        foreach (AttributeData attribute in attributes)
                        {
                            if (SymbolEqualityComparer.Default.Equals(attribute.AttributeClass, jsonPropertyNameAttr) ||
                                SymbolEqualityComparer.Default.Equals(attribute.AttributeClass, jsonIgnoreAttribute))
                            {
                                skip = true;
                                break;
                            }
                        }

                        if (skip)
                            continue;

                        // declare the variables for GetPropertyPlaceholder method
                        string propertyName = prop.Name;
                        string propertyFullTypeName =
                            prop.Type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);

                        // get the property placeholder text and append it to the list
                        string propertyPlaceholder =
                            GetPropertyPlaceholder(propertyName, propertyFullTypeName);
                        properties.Add(propertyPlaceholder);
                    }

                    // declare the variables for GetPlaceholder method
                    string namespaceName = type.ContainingNamespace?.Name;
                    string className = type.Name;
                    string content = string.Join(NewLinePlaceholder, properties);

                    // get the auto-generated source code for this record type and apply it to the compilation context
                    string recordPlaceholder = GetPlaceholder(namespaceName, className, content);
                    //Console.WriteLine(recordPlaceholder);
                    context.AddSource($"{className}_generated.cs", recordPlaceholder);
                }
            }
            catch (Exception e)
            {
                context.ReportDiagnostic(Diagnostic.Create(new DiagnosticDescriptor("MySG001", "Hello!", e.ToString() + e.StackTrace, "Goofiness", DiagnosticSeverity.Error, true), Location.None));
            }
        }

        private static string GetPlaceholder(string namespaceName, string partialClassName, string content)
        {
            return string.Format(Placeholder, namespaceName, partialClassName, content);
        }

        private static string GetPropertyPlaceholder(string propertyName, string propertyFullTypeName)
        {
            string snakeCasePropertyName = propertyName;
            return string.Format(PropertyPlaceholder, snakeCasePropertyName, propertyFullTypeName, propertyName);
        }
    }
}
